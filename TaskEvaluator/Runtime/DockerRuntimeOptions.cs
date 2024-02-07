using TaskEvaluator.Tasks;
namespace TaskEvaluator.Runtime;

public sealed class DockerRuntimeOptions {
    public required Code Context { get; init; }
    public required string DockerImageName { get; init; }
    public required string DockerfilePath { get; init; }
    public required string WorkingFolder { get; init; }
}
