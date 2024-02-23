using TaskEvaluator.Language;
namespace TaskEvaluator.Generation;

public sealed record CodeGenerationTask(
    Guid TaskId,
    string Prefix,
    string Suffix,
    ProgrammingLanguage Language);
