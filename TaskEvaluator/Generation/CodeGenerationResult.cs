using TaskEvaluator.Tasks;
namespace TaskEvaluator.Generation;

public sealed record CodeGenerationResult(Guid TaskId, bool Success, Code Code, string GeneratedPart, string Generator) {
    public static CodeGenerationResult Successful(CodeGenerationTask task, string generatedPart, string generator) => new(task.TaskId, true, new Code(task, generatedPart), generatedPart, generator);
    public static CodeGenerationResult Failure(CodeGenerationTask task, string generator) => new(task.TaskId, false, null!, string.Empty, generator);
}
