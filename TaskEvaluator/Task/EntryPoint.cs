namespace TaskEvaluator.Task;

public sealed record EntryPoint(string FullPath, object?[] Parameters) {
    /// <summary>
    /// Full path to the entry point including the full base type name and method name as applicable.
    /// </summary>
    /// <example>
    /// MyNamespace.MyClass.MyMethod
    /// </example>
    public string FullPath { get; init; } = FullPath;

    /// <summary>
    /// Parameters to pass to the entry point.
    /// </summary>
    public object?[] Parameters { get; init; } = Parameters;
}
