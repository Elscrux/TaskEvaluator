using TaskEvaluator.Runtime;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Evaluator.UnitTest;

public sealed class UnitTestEvaluator(IRuntime runtime) : IRuntimeEvaluator {
    public async Task<IEvaluationResult> Evaluate(Code code, EvaluationModel evaluationModel, CancellationToken token = default) {
        var runtimeResult = await runtime.UnitTest(code, token);

        return new UnitTestEvaluationResult(code.Guid, runtimeResult);
    }
}
