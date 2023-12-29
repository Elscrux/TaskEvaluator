namespace TaskEvaluator.Generation;

public interface ICodeGenerator {
    Task<CodeGenerationResult> Send(CodeGenerationTask task, CancellationToken token = default);
}
