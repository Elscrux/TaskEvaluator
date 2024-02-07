namespace TaskEvaluator.Tasks;

public sealed record TaskLoadConfiguration {
    public required string DirectoryPath { get; init; }
}
