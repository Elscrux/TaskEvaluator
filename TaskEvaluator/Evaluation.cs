using System.Reflection;
using System.Runtime.Loader;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.OpenApi.Attributes;
namespace WebApplication1;

public sealed class CompilationFailedException(string message) : Exception(message);

public sealed class LanguageNotSupportedException(ProgrammingLanguage codeLanguage)
    : Exception($"The language {codeLanguage} is not supported");

public interface IEvaluator {
    IEnumerable<IEvaluationResult> Evaluate(TaskEvaluationModel model, IRuntime runtime);
}

public interface IStaticEvaluator : IEvaluator {}
public interface IRuntimeEvaluator : IEvaluator {}

public sealed class UnitTestEvaluator(RuntimeService runtimeService) : IRuntimeEvaluator {
    public IEnumerable<IEvaluationResult> Evaluate(TaskEvaluationModel model, IRuntime runtime) {
        foreach (var modelUnitTest in model.UnitTests) {
            var unitTestRuntime = runtimeService.CreateRuntime(modelUnitTest);
            var runtimeResult = unitTestRuntime.Run();
            yield return new UnitTestEvaluationResult("Unit Test", runtimeResult);
        }
    }
}

public sealed record UnitTestEvaluationResult(string Name, bool Success) : IEvaluationResult {
    public UnitTestEvaluationResult(string name, IRuntimeResult runtimeResult) : this(name, runtimeResult.Success) {}
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
    string Name { get; }
    bool Success { get; }
}

public interface IEvaluatorProvider {
    IEnumerable<IEvaluator> GetEvaluators(TaskEvaluationModel model);
}

public sealed class EvaluatorProvider(
    UnitTestEvaluator unitTestEvaluator) : IEvaluatorProvider {
    public IEnumerable<IEvaluator> GetEvaluators(TaskEvaluationModel model) {
        if (model.UnitTests.Count > 0) yield return unitTestEvaluator;
    }
}

public enum ProgrammingLanguage {
    C,
    CSharp,
    FSharp,
    Go,
    Haskell,
    Java,
    Javascript,
    Kotlin,
    Python,
    Rust,
    Scala,
}

public interface IRuntimeFactory {
    IRuntime Create(Code code);
}

public sealed record CSharpRuntimeResult(bool Success, object? ReturnValue) : IRuntimeResult;

public sealed class CSharpRuntimeFactory(ILogger<CSharpRuntimeFactory> logger) : IRuntimeFactory {
    public IRuntime Create(Code code) {
        logger.LogInformation("Parsing the code into the SyntaxTree");
        var syntaxTree = CSharpSyntaxTree.ParseText(code.Body);

        var mainDirectory = Path.GetDirectoryName(typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly.Location);

        var refPaths = Directory.EnumerateFiles(mainDirectory, "*.dll")
            .Where(x => {
                var fileName = Path.GetFileName(x).ToLower();
                return fileName.StartsWith("system", StringComparison.OrdinalIgnoreCase) && fileName.EndsWith(".dll", StringComparison.OrdinalIgnoreCase) && !fileName.Contains("native", StringComparison.OrdinalIgnoreCase);
            })
            .Concat(Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll"))
            .Distinct()
            .ToArray();

        var references = refPaths.Select(path => MetadataReference.CreateFromFile(path)).ToArray();

        logger.LogInformation("Adding the following references:\n{References}", string.Join("\n\t", refPaths));

        logger.LogInformation("Compiling ...");
        var compilation = CSharpCompilation.Create(
            Path.GetRandomFileName(),
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
        }

        logger.LogInformation("Compilation successful! Now instantiating and executing the code ...");
        ilStream.Seek(0, SeekOrigin.Begin);

        var assembly = AssemblyLoadContext.Default.LoadFromStream(ilStream);
        var pathElements = code.EntryPoint.FullPath.Split(".");

        string typeName;
        string methodName;

        switch (pathElements.Length) {
            case < 2:
                throw new CompilationFailedException($"The entry point {code.EntryPoint} is not valid, it must be in the format <Namespace>.<Class>.<Method>");
            case 2:
                typeName = pathElements[0];
                methodName = pathElements[1];
                break;
            default:
                typeName = string.Join(".", pathElements[..^1]);
                methodName = pathElements[^1];
                break;
        }

        var type = assembly.GetType(typeName);
        if (type?.FullName is null) throw new CompilationFailedException($"Could not find any types in the assembly {assembly.FullName}");

        var instance = assembly.CreateInstance(type.FullName);
        if (instance is null) throw new CompilationFailedException($"Could not create an instance of {type.FullName}");

        if (type.GetMember(methodName)[0] is not MethodInfo methodInfo) {
            throw new CompilationFailedException($"Could not find the entry point {code.EntryPoint}");
        }

        return new CSharpMethodRuntime(code, instance, methodInfo);
    }
}

public sealed class CSharpMethodRuntime(Code code, object instance, MethodInfo methodInfo) : IRuntime {
    public Code Context { get; } = code;
    public IRuntimeResult Run() {
        try {
            var invoke = methodInfo.Invoke(instance, new object?[] {});
            return new CSharpRuntimeResult(true, invoke);
        } catch (Exception e) {
            throw new CompilationFailedException($"Failed execution of {methodInfo.Name}: {e.Message} {e.InnerException}");
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

public sealed class RuntimeService(IServiceProvider serviceProvider) {
    public IRuntime CreateRuntime(Code code) {
        var compiler = serviceProvider.GetKeyedService<IRuntimeFactory>(code.Language);
        if (compiler is null) throw new LanguageNotSupportedException(code.Language);

        return compiler.Create(code);
    }
}

public sealed class TaskRunner(
    RuntimeService runtimeService,
    LocalTaskProvider localTaskProvider,
    IEvaluatorProvider evaluatorProvider) {

    public IEnumerable<IEvaluationResult> RunLocal() {
        return localTaskProvider
            .GetTasks()
            .SelectMany(Run);
    }

    public IEnumerable<IEvaluationResult> Run(TaskEvaluationModel model) {
        var runtime = runtimeService.CreateRuntime(model.Task.Code);

        foreach (var evaluator in evaluatorProvider.GetEvaluators(model)) {
            foreach (var evaluationResult in evaluator.Evaluate(model, runtime)) {
                yield return evaluationResult;
            }
        }
    }
}

public sealed record TaskEvaluationModel(
    Task Task,
    IList<Code> UnitTests);

public sealed record EntryPoint(string FullPath, object?[] Parameters) {
    /// <summary>
    /// Full path to the entry point including the full base type name and method name as applicable.
    /// </summary>
    /// <example>
    /// MyNamespace.MyClass.MyMethod
    /// </example>
    public string FullPath { get; init; } = FullPath;

    /// <summary>
    /// Parameters to pass to the entry point.
    /// </summary>
    public object?[] Parameters { get; init; } = Parameters;
}

public sealed record Code(string Body, EntryPoint EntryPoint, ProgrammingLanguage Language);
