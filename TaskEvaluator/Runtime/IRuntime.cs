using TaskEvaluator.Task;
namespace TaskEvaluator.Runtime;

public interface IRuntime {
    Code Context { get; }

    IRuntimeResult Run();
}
