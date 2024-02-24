using TaskEvaluator.Disposable;
namespace TaskEvaluator.Docker;

public sealed class DockerHostFactory(IPortPool portPool) {
    private const string Hostname = "localhost";

    private readonly FluentDockerImageCreator _imageCreator = new();

    public IDockerHost Create() {
        var port = portPool.AllocatePort();
        var dockerHost = new FluentDockerHost(_imageCreator, Hostname, port);
        dockerHost.Add(new FuncDisposable(() => portPool.ReleasePort(port)));

        return dockerHost;
    }
}
