using TaskEvaluator.Evaluator;
using TaskEvaluator.Generation;
namespace TaskEvaluator.Tasks;

public record FinalResult(CodeGenerationResult CodeGenerationResult, IReadOnlyList<IEvaluationResult> EvaluationResults);
