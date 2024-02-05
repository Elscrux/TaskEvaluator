using TaskEvaluator.Language;
namespace TaskEvaluator.Tasks;

public sealed record Code {
    public Code(string Body, ProgrammingLanguage Language) {
        this.Language = Language;
        this.Body = Body.Replace("\r\n", "\n");
    }

    public Guid Guid { get; init; } = Guid.NewGuid();
    public string Body { get; init; }
    public ProgrammingLanguage Language { get; init; }
}
