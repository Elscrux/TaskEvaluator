using TaskEvaluator.Tasks;
namespace TaskEvaluator.Evaluator;

public interface IEvaluator {
    Task<IEvaluationResult> Evaluate(Code code, EvaluationModel evaluationModel, CancellationToken token = default);
}
