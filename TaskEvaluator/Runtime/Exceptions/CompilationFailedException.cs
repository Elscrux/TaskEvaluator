namespace TaskEvaluator.Runtime.Exceptions;

public sealed class CompilationFailedException(string message) : Exception(message);
