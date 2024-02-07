using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using TaskEvaluator.Docker;
using TaskEvaluator.Evaluator;
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
                "\"CODE=" + JsonSerializer.Serialize(Context).Replace("\"", "\\\"") + "\""
            })
            .ContinueWith(_ => _initialized = true);
    }

    public Task<UnitTestRuntimeResult> UnitTest(Code unitTest, CancellationToken token = default) {
        return CallEndpoint("unit-test", unitTest, message => new UnitTestRuntimeResult(false, message), token);
    }

    public Task<StaticCodeEvaluationResult> StaticCodeQualityAnalysis(CancellationToken token = default) {
        return CallEndpoint<string, StaticCodeEvaluationResult>("sonar-qube", null, _ => StaticCodeEvaluationResult.Failure, token);
    }

    private async Task<TOutput> CallEndpoint<TInput, TOutput>(string endpoint, TInput? input, Func<string, TOutput> errorOutputFactory, CancellationToken token = default) {
        while (!_initialized) {
            await Task.Delay(100, token).ConfigureAwait(false);
        }

        using var httpClient = _httpClientFactory.CreateClient();

        try {
            var serviceUri = _dockerHost.Uri(endpoint);
            _logger.LogInformation("Calling {Uri}...", serviceUri);
            var call = input is null
                ? httpClient.PostAsJsonAsync(serviceUri, string.Empty, token)
                : httpClient.PostAsJsonAsync(serviceUri, input, token);
            var result = await call.ConfigureAwait(false);
            result.EnsureSuccessStatusCode();

            var runtimeResult = await result.Content.ReadFromJsonAsync<TOutput>(token);
            if (runtimeResult is null) {
                throw new InvalidOperationException("Failed to deserialize runtime result");
            }

            _logger.LogInformation("Service {Uri} succeeded with: {Result}", serviceUri, runtimeResult);

            return runtimeResult;
        } catch (Exception e) {
            _logger.LogError(e, "Error while running code");

            return errorOutputFactory(e.Message);
        }
    }

    public void Dispose() => _dockerHost.Dispose();
}