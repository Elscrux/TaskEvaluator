using TaskEvaluator.Evaluator.UnitTest;
using TaskEvaluator.Runtime;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Evaluator;

public sealed class EvaluatorProvider : IEvaluatorProvider {
    public IEnumerable<IEvaluator> GetEvaluators(TaskEvaluationModel model, IRuntime runtime) {
        if (model.UnitTests.Count > 0) yield return new UnitTestEvaluator(runtime);
    }
}
