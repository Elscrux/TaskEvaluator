using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TaskEvaluator.Generation;
using TaskEvaluator.Generator.Tabby.Model;
using TaskEvaluator.Runtime;
namespace TaskEvaluator.Generator.Tabby;

public sealed class TabbyCodeGenerator(
    ILogger<TabbyCodeGenerator> logger,
    IHttpClientFactory httpClientFactory,
    IOptions<TabbyConfiguration> config,
    LanguageFactory languageFactory)
    : ICodeGenerator {

    private readonly Random _random = new();

    public async Task<CodeGenerationResult> Send(CodeGenerationTask task, CancellationToken token = default) {
        var tabbyApiRequest = GetRequest(task);

        var httpClient = httpClientFactory.CreateClient();
        var startTime = Stopwatch.GetTimestamp();
        var response = await httpClient
            .PostAsJsonAsync(config.Value.CompletionsUrl, tabbyApiRequest, token)
            .ConfigureAwait(false);

        while (response.StatusCode == HttpStatusCode.RequestTimeout) {
            logger.LogWarning("Tabby API request timed out. Retrying...");
            response = await httpClient
                .PostAsJsonAsync(config.Value.CompletionsUrl, tabbyApiRequest, token)
                .ConfigureAwait(false);
        }
        var elapsedTime = Stopwatch.GetElapsedTime(startTime);

        response.EnsureSuccessStatusCode();

        var tabbyApiResponse = await response.Content
            .ReadFromJsonAsync<TabbyApiResponse>(token)
            .ConfigureAwait(false);

        if (tabbyApiResponse is null) return CodeGenerationResult.Failure(task, "Tabby", elapsedTime);

        return CodeGenerationResult.Successful(task, tabbyApiResponse.Choices[0].Text, "Tabby", elapsedTime);
    }

    public TabbyApiRequest GetRequest(CodeGenerationTask task) {
        var languageService = languageFactory.GetLanguageService(task.Language);

        return new TabbyApiRequest {
            Language = languageService.LanguageId,
            Seed = _random.Next(),
            Segments = new TabbyApiRequest.Segment {
                Prefix = task.Prefix,
                Suffix = task.Suffix
            }
        };
    }
}
