using Noggog;
using TaskEvaluator.Runtime;
namespace TaskEvaluator.Docker;

public interface IDockerHost : IDisposableDropoff {
    Uri Uri(string path);
    Task StartContainer(DockerRuntimeOptions options, CancellationToken token = default);
}
