using TaskEvaluator.Evaluator;
using TaskEvaluator.Evaluator.UnitTest;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Runtime;

public interface IRuntime : IDisposable {
    Code Context { get; }

    Task<UnitTestRuntimeResult> UnitTest(Code unitTest, CancellationToken token = default);
    Task<StaticCodeEvaluationResult> StaticCodeQualityAnalysis(CancellationToken token = default);
}
