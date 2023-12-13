using TaskEvaluator.Tasks;
namespace TaskEvaluator.Runtime;

public interface IRuntimeFactory {
    Task<IRuntime> Create(Code code, CancellationToken token = default);
}
