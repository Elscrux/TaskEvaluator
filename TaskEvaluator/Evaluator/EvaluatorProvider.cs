using TaskEvaluator.Evaluator.UnitTest;
using TaskEvaluator.Task;
namespace TaskEvaluator.Evaluator;

public sealed class EvaluatorProvider(
    UnitTestEvaluator unitTestEvaluator) : IEvaluatorProvider {
    public IEnumerable<IEvaluator> GetEvaluators(TaskEvaluationModel model) {
        if (model.UnitTests.Count > 0) yield return unitTestEvaluator;
    }
}
