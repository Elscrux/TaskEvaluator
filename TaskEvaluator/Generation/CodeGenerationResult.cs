using TaskEvaluator.Tasks;
namespace TaskEvaluator.Generation;

public sealed record CodeGenerationResult(Guid TaskId, bool Success, Code Code, string GeneratedPart, string Generator, TimeSpan GenerationTime) {
    public static CodeGenerationResult Successful(CodeGenerationTask task, string generatedPart, string generator, TimeSpan generationTime) => new(task.TaskId, true, new Code(task, generatedPart), generatedPart, generator, generationTime);
    public static CodeGenerationResult Failure(CodeGenerationTask task, string generator, TimeSpan generationTime) => new(task.TaskId, false, null!, string.Empty, generator, generationTime);
}
