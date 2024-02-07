using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using TaskEvaluator.Evaluator;
using TaskEvaluator.Runtime;
using TaskEvaluator.SonarQube.Scanner;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.SonarQube;

public sealed class SonarQubeEvaluator(
    ILogger<SonarQubeEvaluator> logger,
    ISonarScannerApiFactory sonarScannerApiFactory,
    SonarQubeApi sonarQube,
    LanguageFactory languageFactory)
    : IStaticEvaluator {

    private static readonly string WorkingDirectory = Path.GetFullPath(Path.Combine("sonar-qube"));

    public async IAsyncEnumerable<IEvaluationResult> Evaluate(Code code, EvaluationModel evaluationModel, [EnumeratorCancellation] CancellationToken token = default) {
        var projectKey = Guid.NewGuid().ToString();
        if (!await sonarQube.TryCreateProject(projectKey, projectKey, token)) {
            logger.LogError("Failed to create project {ProjectKey}", projectKey);
            yield break;
        }

        var userToken = await sonarQube.GetUserToken(projectKey, token);
        var sonarScannerApi = sonarScannerApiFactory.Create(code.Language);

        var languageSpecification = languageFactory.GetLanguageSpecification(code.Language);
        languageSpecification.CreateWorkingDirectory(WorkingDirectory, code);
        if (!await sonarScannerApi.Run(WorkingDirectory, code, sonarQube.Url, userToken, projectKey, token)) yield break;

        await foreach (var result in sonarQube.SearchIssues(projectKey, token)) {
            yield return result;
        }
    }
}
