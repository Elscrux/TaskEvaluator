using TaskEvaluator.Runtime;
namespace TaskEvaluator.Evaluator;

public interface IEvaluatorProvider {
    IEnumerable<IEvaluator> GetEvaluators(EvaluationModel model, IRuntime runtime);
}
