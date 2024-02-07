using Noggog;
namespace TaskEvaluator.Docker;

public interface IDockerHost : IDisposableDropoff {
    Uri Uri(string path);
    Task StartContainer(string name, string workingFolder, string dockerfilePath, string[] environmentVariables, CancellationToken token = default);
}
