using TaskEvaluator.Tasks;
namespace TaskEvaluator.Runtime;

public interface IRuntime {
    Code Context { get; }

    Task<IRuntimeResult> Run(CancellationToken token = default);
}
