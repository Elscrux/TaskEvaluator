using System.Text.Json.Serialization;
namespace TaskEvaluator.Generator.Tabby.Model;

public class TabbyApiRequest {
    /// <summary>
    /// Language identifier, full list is maintained at https://code.visualstudio.com/docs/languages/identifiers
    /// </summary>
    [JsonPropertyName("language")] public required string Language { get; init; }
    [JsonPropertyName("segments")] public required Segment Segments { get; init; }
    /// <summary>
    /// A unique identifier representing your end-user, which can help Tabby to monitor & generating reports.
    /// </summary>
    [JsonPropertyName("user")] public string? User { get; init; }
    [JsonPropertyName("debug_options")] public DebugOption? DebugOptions { get; init; }
    /// <summary>
    /// The temperature parameter for the model, used to tune variance and "creativity" of the model output
    /// </summary>
    [JsonPropertyName("temperature")] public float? Temperature { get; init; }
    /// <summary>
    /// The seed used for randomly selecting tokens
    /// </summary>
    [JsonPropertyName("seed")] public int? Seed { get; init; }

    public class Segment {
        /// <summary>
        /// Content that appears before the cursor in the editor window.
        /// </summary>
        [JsonPropertyName("prefix")] public required string Prefix { get; init; }
        /// <summary>
        /// Content that appears after the cursor in the editor window.
        /// </summary>
        [JsonPropertyName("suffix")] public string? Suffix { get; init; }
        /// <summary>
        /// Clipboard content when requesting code completion.
        /// </summary>
        [JsonPropertyName("clipboard")] public string? Clipboard { get; init; }
    }

    public class DebugOption {
        /// <summary>
        /// This is useful for certain requests that aim to test the tabby's e2e quality.
        /// </summary>
        [JsonPropertyName("raw_prompt")] public string? RawPrompt { get; init; }
        /// <summary>
        /// When true, returns snippets in debug_data.
        /// </summary>
        [JsonPropertyName("return_snippets")] public bool ReturnSnippets { get; init; }
        /// <summary>
        /// When true, returns prompt in debug_data.
        /// </summary>
        [JsonPropertyName("return_prompt")] public bool ReturnPrompt { get; init; }
        /// <summary>
        /// When true, disable retrieval augmented code completion.
        /// </summary>
        [JsonPropertyName("disable_retrieval_augmented_code_completion")]
        public bool DisableRetrievalAugmentedCodeCompletion { get; init; }
    }
}
