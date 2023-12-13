﻿using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using TaskEvaluator.Docker;
using TaskEvaluator.Runtime.Implementation.CSharp;
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
        _dockerHost.StartContainer(options.DockerImageName, options.ProjectFolder)
            .ContinueWith(x => _initialized = true);
    }

    public async Task<IRuntimeResult> UnitTest(Code unitTest, CancellationToken token) {
        while (!_initialized) {
            await Task.Delay(100, token).ConfigureAwait(false);
        }

        using var httpClient = _httpClientFactory.CreateClient();

        try {
            var serviceUri = _dockerHost.Uri("unit-test");
            _logger.LogInformation("Calling {Uri}...", serviceUri);
            var result = await httpClient.PostAsJsonAsync(serviceUri, unitTest, token).ConfigureAwait(false);

            _logger.LogInformation("Service {Uri} returned: {Result}", serviceUri, result);

            return new CSharpRuntimeResult(true, result);
        } catch (Exception e) {
            _logger.LogError(e, "Error while running code");

            return new CSharpRuntimeResult(false, e.Message);
        }
    }

    public void Dispose() => _dockerHost.Dispose();
}

public sealed class DockerRuntimeOptions {
    public required Code Context { get; init; }
    public required string DockerImageName { get; init; }
    public required string ProjectFolder { get; init; }
}
