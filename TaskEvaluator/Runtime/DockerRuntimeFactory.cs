using Microsoft.Extensions.Logging;
using TaskEvaluator.Docker;
namespace TaskEvaluator.Runtime;

public sealed class DockerRuntimeFactory(
    IHttpClientFactory httpClientFactory,
    ILogger<DockerRuntime> logger,
    DockerHostFactory dockerHostFactory) {

    public Task<IRuntime> Create(DockerRuntimeOptions options, CancellationToken token = default) {
        return Task.FromResult<IRuntime>(new DockerRuntime(logger, httpClientFactory, dockerHostFactory, options));
    }
}
