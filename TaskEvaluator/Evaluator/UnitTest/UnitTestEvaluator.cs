using TaskEvaluator.Runtime;
using TaskEvaluator.Task;
namespace TaskEvaluator.Evaluator.UnitTest;

public sealed class UnitTestEvaluator(RuntimeService runtimeService) : IRuntimeEvaluator {
    public IEnumerable<IEvaluationResult> Evaluate(TaskEvaluationModel model, IRuntime runtime) {
        foreach (var unitTest in model.UnitTests) {
            var unitTestRuntime = runtimeService.CreateRuntime(unitTest);
            var runtimeResult = unitTestRuntime.Run();
            yield return new UnitTestEvaluationResult(unitTest.Guid, runtimeResult);
        }
    }
}
