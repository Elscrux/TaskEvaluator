using TaskEvaluator.Task;
namespace TaskEvaluator.Evaluator;

public interface IEvaluatorProvider {
    IEnumerable<IEvaluator> GetEvaluators(TaskEvaluationModel model);
}
