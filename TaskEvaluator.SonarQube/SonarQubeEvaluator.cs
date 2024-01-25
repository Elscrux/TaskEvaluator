using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using TaskEvaluator.Evaluator;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.SonarQube;

public sealed class SonarQubeEvaluator(
    ILogger<SonarQubeEvaluator> logger,
    SonarScannerApi sonarScanner,
    Task<SonarQubeApi> sonarQubeTask)
    : IStaticEvaluator {
    public async IAsyncEnumerable<IEvaluationResult> Evaluate(Code code, EvaluationModel evaluationModel, [EnumeratorCancellation] CancellationToken token = default) {
        var sonarQube = await sonarQubeTask;

        var projectKey = Guid.NewGuid().ToString();
        if (!await sonarQube.TryCreateProject(projectKey, projectKey, token)) {
            logger.LogError("Failed to create project {ProjectKey}", projectKey);
            yield break;
        }

        var userToken = await sonarQube.GetUserToken(projectKey, token);
        if (!await sonarScanner.Run(code, sonarQube.Url, userToken, projectKey, token)) yield break;

        await foreach (var result in sonarQube.SearchIssues(token)) {
            yield return result;
        }
    }
}
