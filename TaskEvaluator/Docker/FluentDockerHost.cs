using System.Net;
using Ductus.FluentDocker.Builders;
using Noggog;
using TaskEvaluator.Runtime;
namespace TaskEvaluator.Docker;

public sealed class FluentDockerHost(string hostname, int port) : IDockerHost {
    private readonly DisposableBucket _disposableBucket = new();
    private readonly Uri _uri = new UriBuilder(System.Uri.UriSchemeHttp, hostname, port).Uri;
    public Uri Uri(string path) => new(_uri, path);

    public Task StartContainer(DockerRuntimeOptions options, CancellationToken token = default) {
        // Create image if necessary
        using var image = new Builder()
            .DefineImage(options.DockerImageName).ReuseIfAlreadyExists()
            .FromFile(options.DockerfilePath).WorkingFolder(options.WorkingFolder)
            .ExposePorts(8080)
            .Build();

        // Create container
        new Builder()
            .UseContainer()
            .UseImage(options.DockerImageName)
            .WithEnvironment(options.EnvironmentVariables)
            .UseNetwork(options.Networks)
            .ExposePort(port, 8080)
            .WaitForHttp(Uri("health").ToString(), continuation: (r, c) => {
                return c switch {
                    <= 0 => 500,
                    <= 10 => r.Code == HttpStatusCode.OK ? -1 : c * 500,
                    _ => -1
                };
            })
            .Build()
            .DisposeWith(this)
            .Start();

        return Task.CompletedTask;
    }

    public void Add(IDisposable disposable) => _disposableBucket.Add(disposable);
    public void Dispose() => _disposableBucket.Dispose();
}
