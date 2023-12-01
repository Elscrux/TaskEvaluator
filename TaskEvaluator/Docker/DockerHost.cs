using Ductus.FluentDocker.Builders;
namespace TaskEvaluator.Docker;

public sealed class DockerHost(string hostname, int port) : IDisposable {
    private readonly Uri _uri = new UriBuilder(System.Uri.UriSchemeHttp, hostname, port).Uri;
    public Uri Uri(string path) => new(_uri, path);

    public Task StartContainer(string dockerfilePath, CancellationToken token = default) {
        new Builder()
            .UseContainer().ReuseIfExists()
            .UseCompose()
            .FromFile(dockerfilePath)
            .RemoveOrphans()
            .Build()
            .Start();

        return Task.CompletedTask;
    }

    public void Dispose() {
        
    }
}
