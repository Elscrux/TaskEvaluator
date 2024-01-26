namespace TaskEvaluator.Generation.GitHubCopilot;

public record GitHubCopilotConfiguration(
    string TokenUrl,
    string BearerToken,
    string CompletionsUrl,
    string UserAgent,
    string UserAgentVersion,
    string EditorVersion,
    string EditorPluginVersion,
    string OpenaiOrganization,
    string OpenaiIntent);
