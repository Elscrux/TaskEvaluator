using System.Reflection;
using System.Runtime.Loader;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using TaskEvaluator.Runtime;
using TaskEvaluator.Runtime.Exceptions;
using TaskEvaluator.Task;
namespace TaskEvaluator.Language.Implementations.CSharp;

public sealed class CSharpMethodRuntimeFactory(ILogger<CSharpMethodRuntimeFactory> logger) : IRuntimeFactory {
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
