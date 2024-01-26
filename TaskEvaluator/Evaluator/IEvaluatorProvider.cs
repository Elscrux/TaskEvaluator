using TaskEvaluator.Runtime;
namespace TaskEvaluator.Evaluator;

public interface IEvaluatorProvider {
    IAsyncEnumerable<IEvaluator> GetEvaluators(EvaluationModel model, IRuntime runtime);
}
