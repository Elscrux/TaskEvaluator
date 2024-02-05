using TaskEvaluator.Language;
namespace TaskEvaluator.Tasks;

public sealed record Code {
    public Code(string Body, ProgrammingLanguage Language) {
        this.Language = Language;
        this.Body = Body.Replace("\r\n", "\n");
    }

    public string Body { get; init; }
    public Guid Guid { get; init; } = Guid.NewGuid();
    public ProgrammingLanguage Language { get; init; }


    public string RootFileName => Language switch {
        ProgrammingLanguage.CSharp => "Program.cs",
        _ => throw new NotSupportedException($"Language {Language} is not supported.")
    };
}
