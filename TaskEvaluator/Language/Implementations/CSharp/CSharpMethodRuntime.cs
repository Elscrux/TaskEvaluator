using System.Reflection;
using TaskEvaluator.Runtime;
using TaskEvaluator.Runtime.Exceptions;
using TaskEvaluator.Task;
namespace TaskEvaluator.Language.Implementations.CSharp;

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
