namespace TaskEvaluator.Runtime.Exceptions;

public sealed class RuntimeFailedException(string message) : Exception(message);
