using TaskEvaluator.Tasks;
namespace TaskEvaluator.Runtime;

public interface IRuntimeFactory : IDisposable {
    Task<IRuntime> Create(Code code, CancellationToken token = default);
}
