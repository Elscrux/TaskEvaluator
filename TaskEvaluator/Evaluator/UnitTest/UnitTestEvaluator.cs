using System.Runtime.CompilerServices;
using TaskEvaluator.Runtime;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Evaluator.UnitTest;

public sealed class UnitTestEvaluator(IRuntime runtime) : IRuntimeEvaluator {
    public async IAsyncEnumerable<IEvaluationResult> Evaluate(TaskEvaluationModel model, [EnumeratorCancellation] CancellationToken token = default) {
        var unitTestTasks = model.UnitTests
            .Select(unitTest => RunUnitTest(unitTest, token))
            .ToList();

        while (unitTestTasks.Count > 0) {
            var completedTask = await Task.WhenAny(unitTestTasks);
            unitTestTasks.Remove(completedTask);

            yield return await completedTask;
        }
    }

    private async Task<IEvaluationResult> RunUnitTest(Code unitTest, CancellationToken token = default) {
        var runtimeResult = await runtime.UnitTest(unitTest, token);

        return new UnitTestEvaluationResult(unitTest.Guid, runtimeResult);
    }
}
