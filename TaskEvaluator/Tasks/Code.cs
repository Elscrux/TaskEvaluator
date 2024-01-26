using TaskEvaluator.Language;
namespace TaskEvaluator.Tasks;

public sealed record Code(Guid Guid, string Body, ProgrammingLanguage Language) {
    public Code(string body, ProgrammingLanguage language) : this(Guid.NewGuid(), body, language) {}
    public string Body { get; init; } = Body.Replace("\r\n", "\n");
}
