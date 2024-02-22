using System.Text.Json.Serialization;
namespace TaskEvaluator.Generator.Tabby.Model;

public record TabbyApiResponse {
    [JsonPropertyName("id")] public required string Id { get; init; }
    [JsonPropertyName("choices")] public required IList<TabbyApiChoice> Choices { get; init; }
    [JsonPropertyName("debug_data")] public DebugData? Debug { get; init; }

    public record TabbyApiChoice {
        [JsonPropertyName("index")] public required int Index { get; init; }
        [JsonPropertyName("text")] public required string Text { get; init; }
    }

    public class DebugData {
        [JsonPropertyName("snippets")] public required IList<DebugDataSnippet>? Snippets { get; init; }
        [JsonPropertyName("prompt")] public string? Prompt { get; init; }
    }

    public class DebugDataSnippet {
        [JsonPropertyName("filepath")] public required string FilePath { get; init; }
        [JsonPropertyName("body")] public required string Body { get; init; }
        [JsonPropertyName("score")] public required float Score { get; init; }
    }
}
