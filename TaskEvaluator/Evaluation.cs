using System.Reflection;
using System.Runtime.Loader;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
namespace WebApplication1;

public interface IEvaluator {
	ITaskResult Evaluate(Task task);
}
public class UnitTestEvaluator : IEvaluator {
	public ITaskResult Evaluate(Task task) {
		
	}
}
public sealed record Task(string Name, Code Code);
public interface ITaskResult {
	bool Success { get; }
}
public interface IEvaluatorProvider {
	IEnumerable<IEvaluator> GetEvaluators(TaskEvaluationRequest request);
}
public class EvaluatorProvider : IEvaluatorProvider {
	public IEnumerable<IEvaluator> GetEvaluators(TaskEvaluationRequest request) {
		if (request.UnitTests.Count > 0) yield return new UnitTestEvaluator();
	}
}
public enum ProgrammingLanguage {
	CSharp,
}
public interface ICompiler {
	Assembly Compile(string code);
}
public class CSharpCompiler(ILogger<CSharpCompiler> logger) : ICompiler {
	public Assembly Compile(string code) {
		logger.LogInformation("Parsing the code into the SyntaxTree");
		var syntaxTree = CSharpSyntaxTree.ParseText(code);

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

		var references = refPaths.Select(r => MetadataReference.CreateFromFile(r)).ToArray();

		logger.LogInformation("Adding the following references");
		foreach (var r in refPaths) Console.WriteLine(r);

		logger.LogInformation("Compiling ...");
		var compilation = CSharpCompilation.Create(
			assemblyName,
			syntaxTrees: new[] { syntaxTree },
			references: references,
			options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

		using var stream = new MemoryStream();
		var result = compilation.Emit(stream);

		if (!result.Success) {
			logger.LogError("Compilation failed!");
			var failures = result.Diagnostics.Where(diagnostic =>
				diagnostic.IsWarningAsError ||
				diagnostic.Severity == DiagnosticSeverity.Error);

			foreach (var diagnostic in failures) {
				Console.Error.WriteLine("\t{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
			}
		} else {
			logger.LogInformation("Compilation successful! Now instantiating and executing the code ...");
			stream.Seek(0, SeekOrigin.Begin);

			var assembly = AssemblyLoadContext.Default.LoadFromStream(stream);
			var type = assembly.ExportedTypes.First();
			var instance = assembly.CreateInstance(type.FullName);
			var methodInfo = type.GetMember("Run").First() as MethodInfo;
			try {
				methodInfo?.Invoke(instance, new object?[] {});
			} catch (Exception e) {
				logger.LogError("Failed execution script {Name}: {Message} {InnerException}", type.FullName, e.Message, e.InnerException);
			}
		}

	}
}
public interface ITaskProvider {
	IEnumerable<Task> GetTasks();
}
public class LocalTaskProvider : ITaskProvider {
	public IEnumerable<Task> GetTasks() {
		throw new NotImplementedException();
	}
}
public class CodeCompilationService(IKeyedServiceProvider keyedServiceProvider) {
	public ICompiler Compile(Code code) {
		var compiler = keyedServiceProvider.GetRequiredKeyedService<ICompiler>(code.Language);
		var assembly = compiler.Compile(code.Text);
		assembly.CreateInstance()
	}
}
public class TaskRunner(
	CodeCompilationService compilationService,
	ITaskProvider taskProvider,
	IEvaluatorProvider evaluatorProvider) {
	public void Run(TaskEvaluationRequest request) {
		var tasks = taskProvider.GetTasks();
		foreach (var task in tasks) {
			compilationService.Compile(task.Code);

			task.Code

			evaluatorProvider.GetEvaluators(task)
			foreach (var evaluator in evaluatorProvider) {}
			var result = evaluator.Evaluate(task);
			if (result.Success) {}
		}
	}
}
public record TaskEvaluationRequest(
	Task Task,
	IList<string> UnitTests);

public sealed record Code(string Text, ProgrammingLanguage Language);
