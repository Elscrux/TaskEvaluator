using TaskEvaluator.Generation;
using TaskEvaluator.Language;
namespace TaskEvaluator.Tasks;

public sealed record Code {
    public Code() : this(string.Empty, ProgrammingLanguage.CSharp) {}

    public Code(string body, ProgrammingLanguage language) {
        Language = language;
        Body = body.Replace("\r\n", "\n");
    }

    public Code(CodeGenerationTask task, string generatedCode) : this(
        task.Prefix + generatedCode + task.Suffix,
        task.Language) {}

    public Guid Guid { get; init; } = Guid.NewGuid();
    public string Body { get; init; }
    public ProgrammingLanguage Language { get; init; }
}
