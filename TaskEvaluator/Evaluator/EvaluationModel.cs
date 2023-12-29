using TaskEvaluator.Tasks;
namespace TaskEvaluator.Evaluator;

public sealed record EvaluationModel(
    IList<Code> UnitTests);
