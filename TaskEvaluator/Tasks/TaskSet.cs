using System.Text.Json.Serialization;
using TaskEvaluator.Evaluator;
using TaskEvaluator.Generation;
namespace TaskEvaluator.Tasks;

public sealed record TaskMetadata(
    [property: JsonPropertyName("id")] Guid TaskId,
    [property: JsonPropertyName("isHumanEval")]
    bool IsHumanEval = false,
    [property: JsonPropertyName("definesCustomTypes")]
    bool DefinesCustomTypes = false) {
    public static TaskMetadata Default { get; } = new(Guid.NewGuid());
}

public sealed record TaskSet(
    string Name,
    CodeGenerationTask CodeGenerationTask,
    EvaluationModel EvaluationModel,
    TaskMetadata TaskMetadata);
