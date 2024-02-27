using System.Text.Json.Serialization;
namespace TaskEvaluator.Generator.GitHubCopilot.Model;

public class GitHubCopilotApiRequest {
    public const int TotalTokenLimit = 16382;

    [JsonPropertyName("prompt")] public required string Prompt { get; init; }
    [JsonPropertyName("suffix")] public required string Suffix { get; init; }
    [JsonPropertyName("max_tokens")] public int MaxTokens { get; init; } = 5000;
    [JsonPropertyName("temperature")] public int Temperature { get; init; } = 0;
    [JsonPropertyName("top_p")] public int TopP { get; init; } = 1;
    [JsonPropertyName("n")] public int N { get; init; } = 1;
    [JsonPropertyName("stream")] public bool Stream { get; init; } = true;
    [JsonPropertyName("isFimEnabled")] public bool IsFimEnabled { get; init; } = true;
    [JsonPropertyName("extra")] public required Extra Extras { get; init; }

    public class Extra {
        [JsonPropertyName("language")] public required string Language { get; init; }
        [JsonPropertyName("next_indent")] public int NextIndent { get; init; } = 4;
        [JsonPropertyName("trim_by_indentation")]
        public bool TrimByIndentation { get; init; } = true;
        [JsonPropertyName("prompt_tokens")] public required int PromptTokens { get; init; }
        [JsonPropertyName("suffix_tokens")] public required int SuffixTokens { get; init; }
    }
}
