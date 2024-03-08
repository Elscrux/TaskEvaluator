namespace TaskEvaluator.Generation;

public interface ICodeGenerator {
    string Identifier { get; }

    Task<CodeGenerationResult> Send(CodeGenerationTask task, CancellationToken token = default);
}
