using TaskEvaluator.Generation;
using TaskEvaluator.Language;
namespace TaskEvaluator.Tasks;

public sealed record Code {
    public Code() : this(string.Empty, ProgrammingLanguage.CSharp) {}
    public Code(string body, ProgrammingLanguage language) : this(Guid.NewGuid(), body, language) {}

    public Code(CodeGenerationTask task, string generatedCode) : this(
        task.Prefix + generatedCode + task.Suffix,
        task.Language) {}

    public Code(Guid codeId, string body, ProgrammingLanguage language) {
        Guid = codeId;
        Language = language;
        Body = body.Replace("\r\n", "\n");
    }

    public Guid Guid { get; init; }
    public string Body { get; init; }
    public ProgrammingLanguage Language { get; init; }
}
