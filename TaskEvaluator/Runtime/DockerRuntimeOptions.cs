using TaskEvaluator.Tasks;
namespace TaskEvaluator.Runtime;

public sealed record DockerRuntimeOptions {
    public required Code Context { get; init; }
    public required string DockerImageName { get; init; }
    public required string DockerfilePath { get; init; }
    public required string WorkingFolder { get; init; }
    public required string? SyntaxValidationEndpoint { get; init; }
    public required string? UnitTestEndpoint { get; init; }
    public required string? StaticCodeQualityAnalysisEndpoint { get; init; }
    public string[] EnvironmentVariables { get; init; } = [];
    public string[] Networks { get; init; } = [];
}
