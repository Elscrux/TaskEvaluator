using TaskEvaluator.Tasks;
namespace TaskEvaluator.Runtime;

public interface IRuntime : IDisposable {
    Code Context { get; }

    Task<IRuntimeResult> UnitTest(Code unitTest, CancellationToken token = default);
}
