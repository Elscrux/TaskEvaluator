using System.Collections.Concurrent;
using Ductus.FluentDocker.Builders;
using TaskEvaluator.Runtime;
namespace TaskEvaluator.Docker;

public sealed class FluentDockerImageCreator {
    private readonly ConcurrentDictionary<string, Task> _createdImages = new();

    public Task CreateImage(DockerRuntimeOptions options) {
        return _createdImages.GetOrAdd(options.DockerImageName, _ => CreateImageInternal(options));
    }

    private Task CreateImageInternal(DockerRuntimeOptions options) {
        return Task.Run(() => new Builder()
            .DefineImage(options.DockerImageName).ReuseIfAlreadyExists().NoCache()
            .FromFile(options.DockerfilePath).WorkingFolder(options.WorkingFolder)
            .ExposePorts(8080)
            .Build());
    }
}
