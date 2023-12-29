using TaskEvaluator.Language;
namespace TaskEvaluator.Tasks;

public sealed record Code(Guid Guid, string Body, ProgrammingLanguage Language) {
    public string Body { get; init; } = Body.Replace("\r\n", "\n");
}
