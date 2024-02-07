namespace TaskEvaluator.SonarQube;

public sealed record SonarQubeConfiguration {
    public required string Url { get; init; }
    public required string User { get; init; }
    public required string Password { get; init; }
}
