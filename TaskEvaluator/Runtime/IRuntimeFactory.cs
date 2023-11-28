using TaskEvaluator.Task;
namespace TaskEvaluator.Runtime;

public interface IRuntimeFactory {
    IRuntime Create(Code code);
}
