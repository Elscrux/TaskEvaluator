using TaskEvaluator.Evaluator;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Api.Requests;

public sealed record CodeEvaluationRequest(
    Code Code,
    EvaluationModel EvaluationModel);