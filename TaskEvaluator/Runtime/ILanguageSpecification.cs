using TaskEvaluator.Language;
namespace TaskEvaluator.Runtime;

public interface ILanguageSpecification {
    ProgrammingLanguage Language { get; }
    string FileExtension { get; }
    string LineCommentSymbol { get; }
    string ProgramFileName => $"Program{FileExtension}";
}
