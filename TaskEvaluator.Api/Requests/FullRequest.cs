using TaskEvaluator.Evaluator;
using TaskEvaluator.Generation;
namespace TaskEvaluator.Api.Requests;

public sealed record FullRequest(
    CodeGenerationTask CodeGenerationTask,
    EvaluationModel EvaluationModel);
