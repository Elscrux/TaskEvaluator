using TaskEvaluator.Tasks;
namespace TaskEvaluator.Evaluator;

public interface IEvaluator {
    IAsyncEnumerable<IEvaluationResult> Evaluate(TaskEvaluationModel model, CancellationToken token = default);
}
