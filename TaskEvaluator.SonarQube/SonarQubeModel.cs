using System.Text.Json.Serialization;
namespace TaskEvaluator.SonarQube;

internal class IssuesSearch {
    public required int Total { get; set; }
    public required int P { get; set; }
    public required int Ps { get; set; }
    public required List<Issue> Issues { get; set; } = [];

    internal class Issue {
        [JsonPropertyName("key")]
        public required string Key { get; set; }
        [JsonPropertyName("rule")]
        public required string Rule { get; set; }
        [JsonPropertyName("severity")]
        public required string Severity { get; set; }
        [JsonPropertyName("component")]
        public required string Component { get; set; }
        [JsonPropertyName("project")]
        public required string Project { get; set; }
        [JsonPropertyName("line")]
        public int Line { get; set; }
        [JsonPropertyName("hash")]
        public required string Hash { get; set; }
        [JsonPropertyName("textRange")]
        public TextRange TextRange { get; set; } = new();
        [JsonPropertyName("status")]
        public required string Status { get; set; }
        [JsonPropertyName("message")]
        public required string Message { get; set; }
        [JsonPropertyName("effort")]
        public required string Effort { get; set; }
        [JsonPropertyName("debt")]
        public required string Debt { get; set; }
        [JsonPropertyName("author")]
        public required string Author { get; set; }
        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; } = [];
        [JsonPropertyName("creationDate")]
        public required string CreationDate { get; set; }
        [JsonPropertyName("updateDate")]
        public required string UpdateDate { get; set; }
        [JsonPropertyName("type")]
        public required string Type { get; set; }
        [JsonPropertyName("scope")]
        public required string Scope { get; set; }
        [JsonPropertyName("quickFixAvailable")]
        public bool QuickFixAvailable { get; set; }
        [JsonPropertyName("cleanCodeAttribute")]
        public required string CleanCodeAttribute { get; set; }
        [JsonPropertyName("cleanCodeAttributeCategory")]
        public required string CleanCodeAttributeCategory { get; set; }
        [JsonPropertyName("impacts")]
        public List<Impact> Impacts { get; set; } = [];
    }

    internal sealed class TextRange {
        [JsonPropertyName("startLine")]
        public int StartLine { get; set; }
        [JsonPropertyName("endLine")]
        public int EndLine { get; set; }
        [JsonPropertyName("startOffset")]
        public int StartOffset { get; set; }
        [JsonPropertyName("endOffset")]
        public int EndOffset { get; set; }
    }

    internal sealed class Impact {
        [JsonPropertyName("softwareQuality")]
        public required string SoftwareQuality { get; set; }
        [JsonPropertyName("severity")]
        public required string Severity { get; set; }
    }
}

internal sealed record TokenResponse {
    [JsonPropertyName("login")]
    public required string Login { get; set; }
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("token")]
    public required string Token { get; set; }
    [JsonPropertyName("type")]
    public required string Type { get; set; }
    [JsonPropertyName("createdAt")]
    public required string CreatedAt { get; set; }
}
