using TaskEvaluator.Tasks;
namespace TaskEvaluator.Evaluator;

public interface IEvaluator {
    IAsyncEnumerable<IEvaluationResult> Evaluate(Code code, EvaluationModel evaluationModel, CancellationToken token = default);
}
