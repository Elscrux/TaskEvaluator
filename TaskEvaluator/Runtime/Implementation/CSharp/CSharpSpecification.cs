using TaskEvaluator.Language;
namespace TaskEvaluator.Runtime.Implementation.CSharp;

public sealed class CSharpSpecification : ILanguageSpecification {
    public ProgrammingLanguage Language => ProgrammingLanguage.CSharp;
    public string FileExtension => ".cs";
    public string LineCommentSymbol => "//";
}
