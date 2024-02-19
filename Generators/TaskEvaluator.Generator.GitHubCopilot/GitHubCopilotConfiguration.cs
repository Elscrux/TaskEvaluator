namespace TaskEvaluator.Generator.GitHubCopilot;

public record GitHubCopilotConfiguration {
    public required string TokenUrl { get; init; }
    public required string BearerToken { get; init; }
    public required string CompletionsUrl { get; init; }
    public required string UserAgent { get; init; }
    public required string UserAgentVersion { get; init; }
    public required string EditorVersion { get; init; }
    public required string EditorPluginVersion { get; init; }
    public required string OpenaiOrganization { get; init; }
    public required string OpenaiIntent { get; init; }
}
