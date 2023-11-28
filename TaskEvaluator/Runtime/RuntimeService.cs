using TaskEvaluator.Language.Exceptions;
using TaskEvaluator.Task;
namespace TaskEvaluator.Runtime;

public sealed class RuntimeService(IServiceProvider serviceProvider) {
    public IRuntime CreateRuntime(Code code) {
        var runtime = serviceProvider.GetKeyedService<IRuntimeFactory>(code.Language);
        if (runtime is null) throw new LanguageNotSupportedException(code.Language);

        return runtime.Create(code);
    }
}
