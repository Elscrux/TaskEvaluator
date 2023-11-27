using System.Reflection;
using System.Runtime.Loader;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
namespace WebApplication1;

public sealed class CompilationFailedException(string message) : Exception(message);

public interface IEvaluator {
    IEvaluationResult Evaluate(IRuntime runtime);
}

public interface IStaticEvaluator : IEvaluator {}
public interface IRuntimeEvaluator : IEvaluator {}

public sealed class UnitTestEvaluator : IRuntimeEvaluator {
    public IEvaluationResult Evaluate(IRuntime runtime) {}
}

public interface IRuntime {
    Code Context { get; }

    IRuntimeResult Run();
}

public interface IRuntimeResult {
    bool Success { get; }
    object? ReturnValue { get; }
}

public sealed record Task(string Name, Code Code);

public interface IEvaluationResult {
    bool Success { get; }
}

public interface IEvaluatorProvider {
    IEnumerable<IEvaluator> GetEvaluators(TaskEvaluationModel model);
}

public sealed class EvaluatorProvider : IEvaluatorProvider {
    public IEnumerable<IEvaluator> GetEvaluators(TaskEvaluationModel model) {
        if (model.UnitTests.Count > 0) yield return new UnitTestEvaluator();
    }
}

public enum ProgrammingLanguage {
    CSharp,
}

public interface IRuntimeFactory {
    IRuntime Create(Code code);
}

public sealed record CSharpRuntimeResult(bool Success, object? ReturnValue) : IRuntimeResult;

public sealed class CSharpRuntimeFactory(ILogger<CSharpRuntimeFactory> logger) : IRuntimeFactory {
    public IRuntime Create(Code code) {
        logger.LogInformation("Parsing the code into the SyntaxTree");
        var syntaxTree = CSharpSyntaxTree.ParseText(code.Body);

        var assemblyName = Path.GetRandomFileName();
        var mainDirectory = Path.GetDirectoryName(typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly.Location);

        var refPaths = Directory.EnumerateFiles(mainDirectory)
            .Where(x => {
                var fileName = Path.GetFileName(x).ToLower();
                return fileName.StartsWith("system") && fileName.EndsWith(".dll") && !fileName.Contains("native");
            })
            .Concat(Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll"))
            .Distinct()
            .ToArray();

        var references = refPaths.Select(path => MetadataReference.CreateFromFile(path)).ToArray();

        logger.LogInformation("Adding the following references");
        foreach (var r in refPaths) Console.WriteLine(r);

        logger.LogInformation("Compiling ...");
        var compilation = CSharpCompilation.Create(
            assemblyName,
            syntaxTrees: new[] { syntaxTree },
            references: references,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        using var ilStream = new MemoryStream();
        var result = compilation.Emit(ilStream);

        if (!result.Success) {
            var failures = result.Diagnostics
                .Where(diagnostic =>
                    diagnostic.IsWarningAsError ||
                    diagnostic.Severity == DiagnosticSeverity.Error)
                .Select(diagnostic => $"\t{diagnostic.Id}: {diagnostic.GetMessage()}");

            throw new CompilationFailedException(string.Join("\n", failures));
        } else {
            logger.LogInformation("Compilation successful! Now instantiating and executing the code ...");
            ilStream.Seek(0, SeekOrigin.Begin);

            var assembly = AssemblyLoadContext.Default.LoadFromStream(ilStream);
            var type = assembly.ExportedTypes.First();
            var instance = assembly.CreateInstance(type.FullName);
            var methodInfo = type.GetMember(code.EntryPoint)[0] as MethodInfo;
            if (methodInfo is null) throw new CompilationFailedException($"Could not find the entry point {entryPoint}");

            return new CSharpMethodRuntime(code, instance, methodInfo);
        }

    }
}

public sealed class CSharpMethodRuntime(Code code, object instance, MethodInfo methodInfo, ILogger<CSharpMethodRuntime> logger) : IRuntime {
    public Code Context { get; } = code;
    public IRuntimeResult Run() {
        try {
            var invoke = methodInfo.Invoke(instance, new object?[] {});
            return new CSharpRuntimeResult(true, invoke);
        } catch (Exception e) {
            logger.LogError("Failed execution script {Name}: {Message} {InnerException}", methodInfo.Name, e.Message, e.InnerException);
        }
    }
}

public interface ITaskProvider {
    IEnumerable<TaskEvaluationModel> GetTasks();
}

public sealed class LocalTaskProvider : ITaskProvider {
    public IEnumerable<TaskEvaluationModel> GetTasks() {
        throw new NotImplementedException();
    }
}

public sealed class RuntimeService(IKeyedServiceProvider keyedServiceProvider) {
    public IRuntime CreateRuntime(Code code) {
        var compiler = keyedServiceProvider.GetRequiredKeyedService<IRuntimeFactory>(code.Language);
        return compiler.Create(code);
    }
}

public sealed class TaskRunner(
    RuntimeService runtimeService,
    LocalTaskProvider localTaskProvider,
    IEvaluatorProvider evaluatorProvider) {

    public void RunLocal() {
        foreach (var request in localTaskProvider.GetTasks()) {
            Run(request);
        }
    }

    public void Run(TaskEvaluationModel model) {
        var runtime = runtimeService.CreateRuntime(model.Task.Code);

        foreach (var evaluator in evaluatorProvider.GetEvaluators(model)) {
            var result = evaluator.Evaluate(runtime);
            if (result.Success) {}
        }
    }
}

public sealed record TaskEvaluationModel(
    Task Task,
    IList<string> UnitTests);

public sealed record Code(string Body, string EntryPoint, ProgrammingLanguage Language);
