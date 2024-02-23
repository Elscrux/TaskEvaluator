using TaskEvaluator.Tasks;
namespace TaskEvaluator.Generation;

public sealed record CodeGenerationResult(Guid TaskId, bool Success, Code Code, string Generator) {
    public static CodeGenerationResult Successful(CodeGenerationTask task, Code code, string generator) => new(task.TaskId, true, code, generator);
    public static CodeGenerationResult Failure(CodeGenerationTask task, string generator) => new(task.TaskId, false, null!, generator);
}
