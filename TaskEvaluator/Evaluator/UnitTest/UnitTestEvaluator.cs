using System.Runtime.CompilerServices;
using TaskEvaluator.Runtime;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Evaluator.UnitTest;

public sealed class UnitTestEvaluator(IRuntime runtime) : IRuntimeEvaluator {
    public async IAsyncEnumerable<IEvaluationResult> Evaluate(Code code, EvaluationModel evaluationModel, [EnumeratorCancellation] CancellationToken token = default) {
        if (evaluationModel.UnitTests is null) yield break;

        var runUnitTest = await RunUnitTest(evaluationModel.UnitTests, token);
        yield return runUnitTest;
    }

    private async Task<IEvaluationResult> RunUnitTest(Code unitTest, CancellationToken token = default) {
        var runtimeResult = await runtime.UnitTest(unitTest, token);

        return new UnitTestEvaluationResult(unitTest.Guid, runtimeResult);
    }
}
