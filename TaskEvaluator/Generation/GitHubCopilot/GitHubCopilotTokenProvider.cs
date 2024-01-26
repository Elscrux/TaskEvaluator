using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
namespace TaskEvaluator.Generation.GitHubCopilot;

public sealed class GitHubCopilotTokenProvider(
    IHttpClientFactory httpClientFactory,
    GitHubCopilotConfiguration config) {

    private DateTime _expirationTime = DateTime.MinValue;
    private string _token = string.Empty;

    public async Task<string> GetAuthToken(CancellationToken token = default) {
        return DateTime.Now >= _expirationTime
            ? await GetNewToken(token)
            : _token;
    }

    private async Task<string> GetNewToken(CancellationToken token = default) {
        using var httpClient = httpClientFactory.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Get, config.TokenUrl);
        request.Headers.UserAgent.Add(new ProductInfoHeaderValue(config.UserAgent, config.UserAgentVersion));
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", config.BearerToken);
        request.Headers.Add("Editor-Version", config.EditorVersion);
        request.Headers.Add("Editor-Plugin-Version", config.EditorPluginVersion);
        var response = await httpClient.SendAsync(request, token).ConfigureAwait(true);
        if (!response.IsSuccessStatusCode) {
            throw new HttpRequestException("Could not send request to get GitHub Copilot token.");
        }

        var tokenResponse = await response.Content.ReadFromJsonAsync<GitHubCopilotTokenResponse>(token);
        if (tokenResponse is null) throw new HttpRequestException("Could not deserialize GitHub Copilot token response.");

        _token = tokenResponse.Token ?? throw new HttpRequestException("GitHub Copilot token is null.");
        _expirationTime = DateTimeOffset.FromUnixTimeSeconds(tokenResponse.ExpiresAt).DateTime;
        return _token;
    }

    private sealed class GitHubCopilotTokenResponse {
        [JsonPropertyName("annotations_enabled")]
        public bool AnnotationsEnabled { get; set; }

        [JsonPropertyName("chat_enabled")]
        public bool ChatEnabled { get; set; }

        [JsonPropertyName("chat_jetbrains_enabled")]
        public bool ChatJetbrainsEnabled { get; set; }

        [JsonPropertyName("code_quote_enabled")]
        public bool CodeQuoteEnabled { get; set; }

        [JsonPropertyName("copilot_ide_agent_chat_gpt4_small_prompt")]
        public bool CopilotIdeAgentChatGpt4SmallPrompt { get; set; }

        [JsonPropertyName("copilotignore_enabled")]
        public bool CopilotIgnoreEnabled { get; set; }

        [JsonPropertyName("expires_at")]
        public long ExpiresAt { get; set; }

        [JsonPropertyName("intellij_editor_fetcher")]
        public bool IntellijEditorFetcher { get; set; }

        [JsonPropertyName("prompt_8k")]
        public bool Prompt8K { get; set; }

        [JsonPropertyName("public_suggestions")]
        public string? PublicSuggestions { get; set; }

        [JsonPropertyName("refresh_in")]
        public int RefreshIn { get; set; }

        [JsonPropertyName("sku")]
        public string? Sku { get; set; }

        [JsonPropertyName("snippy_load_test_enabled")]
        public bool SnippyLoadTestEnabled { get; set; }

        [JsonPropertyName("telemetry")]
        public string? Telemetry { get; set; }

        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonPropertyName("tracking_id")]
        public string? TrackingId { get; set; }

        [JsonPropertyName("vsc_panel_v2")]
        public bool VscPanelV2 { get; set; }
    }
}
