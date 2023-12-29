using TaskEvaluator.Language;
namespace TaskEvaluator.Generation;

public sealed record CodeGenerationTask(string Prefix, string Suffix, ProgrammingLanguage Language);
