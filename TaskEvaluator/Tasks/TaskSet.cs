using System.Text.Json.Serialization;
using TaskEvaluator.Evaluator;
using TaskEvaluator.Generation;
namespace TaskEvaluator.Tasks;

public sealed record Metadata(
    [property: JsonPropertyName("id")] Guid Id,
    [property: JsonPropertyName("isHumanEval")]
    bool IsHumanEval) {
    public static Metadata Default { get; } = new(Guid.NewGuid(), false);
}

public sealed record TaskSet(
    string Name,
    CodeGenerationTask CodeGenerationTask,
    EvaluationModel EvaluationModel,
    Metadata Metadata);
