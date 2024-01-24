using TaskEvaluator.Evaluator;
using TaskEvaluator.Generation;
namespace TaskEvaluator.Tasks;

public sealed record TaskSet(
    CodeGenerationTask CodeGenerationTask,
    EvaluationModel EvaluationModel);
