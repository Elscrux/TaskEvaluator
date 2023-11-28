namespace TaskEvaluator.Language.Exceptions;

public sealed class LanguageNotSupportedException(ProgrammingLanguage codeLanguage)
    : Exception($"The language {codeLanguage} is not supported");
