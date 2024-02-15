using TaskEvaluator.Runtime;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Evaluator.UnitTest;

public sealed class UnitTestEvaluator(IRuntime runtime) : IRuntimeEvaluator {
    public async Task<IEvaluationResult> Evaluate(Code code, EvaluationModel evaluationModel, CancellationToken token = default) {
        if (evaluationModel.UnitTests is null) return new UnitTestEvaluationResult(code.Guid, false, "No unit tests provided.");

        var runtimeResult = await runtime.UnitTest(evaluationModel.UnitTests, token);

        return new UnitTestEvaluationResult(code.Guid, runtimeResult);
    }
}
