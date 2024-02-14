using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using TaskEvaluator.Evaluator;
using TaskEvaluator.Evaluator.StaticCodeAnalysis;
namespace TaskEvaluator.SonarQube;

public sealed class SonarQubeApi(HttpClient httpClient) {
    public string Url => httpClient.BaseAddress!.ToString();

    public async Task<bool> TryCreateProject(string name, string key, CancellationToken cancellationToken = default) {
        var response = await httpClient
            .PostAsync(
                $"/api/projects/create?project={name}&name={key}",
                new FormUrlEncodedContent([
                    new KeyValuePair<string, string>("project", key),
                    new KeyValuePair<string, string>("name", name)
                ]), cancellationToken)
            .ConfigureAwait(false);

        return response.IsSuccessStatusCode;
    }

    public async IAsyncEnumerable<StaticCodeResult> SearchIssues(string projectKey, [EnumeratorCancellation] CancellationToken cancellationToken = default) {
        var response = await httpClient
            .GetAsync($"/api/issues/search?components={projectKey}", cancellationToken)
            .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        var issuesSearch = await response.Content.ReadFromJsonAsync<IssuesSearch>(cancellationToken);
        if (issuesSearch is null) {
            throw new InvalidOperationException("Failed to deserialize issues search");
        }

        foreach (var issue in issuesSearch.Issues) {
            if (!issue.Component.Contains("Program", StringComparison.OrdinalIgnoreCase)) continue;

            var severity = issue.Impacts[0].Severity switch {
                "LOW" => Severity.Low,
                "MEDIUM" => Severity.Medium,
                "HIGH" => Severity.High,
                "MINOR" => Severity.Low,
                "MAJOR" => Severity.High,
                "BLOCKER" => Severity.High,
                "CRITICAL" => Severity.High,
                "INFO" => Severity.Low,
                _ => throw new InvalidOperationException()
            };

            yield return new StaticCodeResult(
                issue.Message,
                severity,
                issue.Impacts[0].SoftwareQuality,
                issue.CleanCodeAttribute,
                issue.Line,
                new Dictionary<string, object> {
                    { "rule", issue.Rule },
                    { "message", issue.Message },
                    { "component", issue.Component },
                    { "project", issue.Project },
                    { "hash", issue.Hash },
                    { "effort", issue.Effort },
                    { "debt", issue.Debt },
                    { "tags", issue.Tags },
                    { "type", issue.Type },
                    { "scope", issue.Scope },
                    { "quickFixAvailable", issue.QuickFixAvailable },
                    { "cleanCodeAttributeCategory", issue.CleanCodeAttributeCategory },
                    { "cleanCodeAttribute", issue.CleanCodeAttribute },
                    { "textRange", issue.TextRange }
                });
        }
    }

    public async Task<string> GetUserToken(string? tokenName = null, CancellationToken cancellationToken = default) {
        tokenName ??= Guid.NewGuid().ToString();
        var content = new FormUrlEncodedContent([new KeyValuePair<string, string>("name", tokenName)]);
        var response = await httpClient
            .PostAsync("/api/user_tokens/generate", content, cancellationToken)
            .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        var tokenResponse = await response.Content
            .ReadFromJsonAsync<TokenResponse>(cancellationToken)
            .ConfigureAwait(false);

        if (tokenResponse is null) {
            throw new InvalidOperationException("Failed to deserialize token response");
        }

        return tokenResponse.Token;
    }
}
