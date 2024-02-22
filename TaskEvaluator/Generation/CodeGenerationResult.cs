using TaskEvaluator.Tasks;
namespace TaskEvaluator.Generation;

public sealed record CodeGenerationResult(bool Success, Code Code, string Generator) {
    public static CodeGenerationResult Successful(Code code, string generator) => new(true, code, generator);
    public static CodeGenerationResult Failure(string generator) => new(false, null!, generator);
}
