using System.Runtime.CompilerServices;
using TaskEvaluator.Extensions;
using TaskEvaluator.Runtime;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Evaluator.UnitTest;

public sealed class UnitTestEvaluator(IRuntime runtime) : IRuntimeEvaluator {
    public async IAsyncEnumerable<IEvaluationResult> Evaluate(Code code, EvaluationModel evaluationModel, [EnumeratorCancellation] CancellationToken token = default) {
        var unitTestTasks = evaluationModel.UnitTests
            .Select(unitTest => RunUnitTest(unitTest, token));

        await foreach (var result in unitTestTasks.AwaitAll(token)) {
            yield return result;
        }
    }

    private async Task<IEvaluationResult> RunUnitTest(Code unitTest, CancellationToken token = default) {
        var runtimeResult = await runtime.UnitTest(unitTest, token);

        return new UnitTestEvaluationResult(unitTest.Guid, runtimeResult);
    }
}
