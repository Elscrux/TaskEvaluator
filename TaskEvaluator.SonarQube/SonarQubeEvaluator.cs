using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using TaskEvaluator.Evaluator;
using TaskEvaluator.SonarQube.Scanner;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.SonarQube;

public sealed class SonarQubeEvaluator(
    ILogger<SonarQubeEvaluator> logger,
    ISonarScannerApiFactory sonarScannerApiFactory,
    SonarQubeApi sonarQube)
    : IStaticEvaluator {
    public async IAsyncEnumerable<IEvaluationResult> Evaluate(Code code, EvaluationModel evaluationModel, [EnumeratorCancellation] CancellationToken token = default) {
        var projectKey = Guid.NewGuid().ToString();
        if (!await sonarQube.TryCreateProject(projectKey, projectKey, token)) {
            logger.LogError("Failed to create project {ProjectKey}", projectKey);
            yield break;
        }

        var userToken = await sonarQube.GetUserToken(projectKey, token);
        var sonarScannerApi = sonarScannerApiFactory.Create(code.Language);
        if (!await sonarScannerApi.Run(code, sonarQube.Url, userToken, projectKey, token)) yield break;

        await foreach (var result in sonarQube.SearchIssues(projectKey, token)) {
            yield return result;
        }
    }
}
