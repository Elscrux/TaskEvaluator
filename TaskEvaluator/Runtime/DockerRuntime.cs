using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using TaskEvaluator.Docker;
using TaskEvaluator.Evaluator.UnitTest;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Runtime;

public sealed class DockerRuntime : IRuntime {
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<DockerRuntime> _logger;
    private readonly IDockerHost _dockerHost;

    private volatile bool _initialized;

    public Code Context { get; }

    public DockerRuntime(
        ILogger<DockerRuntime> logger,
        IHttpClientFactory httpClientFactory,
        DockerHostFactory dockerHostFactory,
        DockerRuntimeOptions options) {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        Context = options.Context;
        _dockerHost = dockerHostFactory.Create();
        _dockerHost.StartContainer(options.DockerImageName, options.ProjectFolder, new[] {
                "\"CODE=" + Context.Body.Replace("\"", "\\\"") + "\""
            })
            .ContinueWith(_ => _initialized = true);
    }

    public async Task<UnitTestRuntimeResult> UnitTest(Code unitTest, CancellationToken token = default) {
        while (!_initialized) {
            await Task.Delay(100, token).ConfigureAwait(false);
        }

        using var httpClient = _httpClientFactory.CreateClient();

        try {
            var serviceUri = _dockerHost.Uri("unit-test");
            _logger.LogInformation("Calling {Uri}...", serviceUri);
            var result = await httpClient.PostAsJsonAsync(serviceUri, unitTest, token).ConfigureAwait(false);
            if (!result.IsSuccessStatusCode) {
                _logger.LogInformation("Service {Uri} failed with: {Result}", serviceUri, result);

                return new UnitTestRuntimeResult(false, result.ReasonPhrase);
            }

            var runtimeResult = await result.Content.ReadFromJsonAsync<UnitTestRuntimeResult>(token);
            if (runtimeResult is null) {
                throw new InvalidOperationException("Failed to deserialize runtime result");
            }

            _logger.LogInformation("Service {Uri} succeeded with: {Result}", serviceUri, runtimeResult);

            return runtimeResult;
        } catch (Exception e) {
            _logger.LogError(e, "Error while running code");

            return new UnitTestRuntimeResult(false, e.Message);
        }
    }

    public void Dispose() => _dockerHost.Dispose();
}

public sealed class DockerRuntimeOptions {
    public required Code Context { get; init; }
    public required string DockerImageName { get; init; }
    public required string ProjectFolder { get; init; }
}
