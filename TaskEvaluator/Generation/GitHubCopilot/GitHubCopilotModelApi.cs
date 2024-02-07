using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using Noggog;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Generation.GitHubCopilot;

public sealed class GitHubCopilotModelApi(
    IHttpClientFactory httpClientFactory,
    IOptions<GitHubCopilotConfiguration> config,
    GitHubCopilotTokenProvider tokenProvider,
    GitHubCopilotPromptGenerator promptGenerator)
    : ICodeGenerator {

    public async Task<CodeGenerationResult> Send(CodeGenerationTask task, CancellationToken token = default) {
        using var httpClient = httpClientFactory.CreateClient();

        var authToken = await tokenProvider.GetAuthToken(token);

        var requestMessage = new HttpRequestMessage(HttpMethod.Post, config.Value.CompletionsUrl);
        requestMessage.Headers.UserAgent.Add(new ProductInfoHeaderValue(config.Value.UserAgent, config.Value.UserAgentVersion));
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
        requestMessage.Headers.Add("Editor-Version", config.Value.EditorVersion);
        requestMessage.Headers.Add("Editor-Plugin-Version", config.Value.EditorPluginVersion);
        requestMessage.Headers.Add("Openai-Organization", config.Value.OpenaiOrganization);
        requestMessage.Headers.Add("Openai-Intent", config.Value.OpenaiIntent);

        var gitHubCopilotApiRequest = promptGenerator.GeneratePrompt(task);
        var serialize = JsonSerializer.Serialize(gitHubCopilotApiRequest);
        requestMessage.Content = new StringContent(serialize);
        var responseMessage = await httpClient.SendAsync(requestMessage, token);
        if (!responseMessage.IsSuccessStatusCode) {
            throw new HttpRequestException("Could not send request to GitHub Copilot.");
        }

        var fullContent = await responseMessage.Content
            .ReadAsStringAsync(token);

        var list = fullContent.Split("data:")
            .Select(x => {
                if (string.IsNullOrWhiteSpace(x) || x.Contains("[DONE]")) return null;

                return JsonSerializer.Deserialize<GitHubCopilotApiStreamResult>(x);
            })
            .NotNull()
            .Select(x => x.Choices?.FirstOrDefault()?.Text)
            .ToList();

        var fullCode = task.Prefix + string.Join(string.Empty, list) + task.Suffix;
        return new CodeGenerationResult(true, new Code(fullCode, task.Language));
    }

    private sealed class GitHubCopilotApiStreamResult {
        [JsonPropertyName("id")] public string? Id { get; init; }
        [JsonPropertyName("created")] public long Created { get; init; }
        [JsonPropertyName("choices")] public required IList<GitHubCopilotApiStreamResultChoice>? Choices { get; init; }

        internal sealed class GitHubCopilotApiStreamResultChoice {
            [JsonPropertyName("text")] public string? Text { get; init; }
            [JsonPropertyName("index")] public int Index { get; init; }
            [JsonPropertyName("finish_reason")] public string? FinishReason { get; init; }
            [JsonPropertyName("logprobs")] public string? LogProbs { get; init; }
        }
    }
}
