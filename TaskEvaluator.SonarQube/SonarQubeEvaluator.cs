using Microsoft.Extensions.Logging;
using TaskEvaluator.Evaluator;
using TaskEvaluator.Evaluator.StaticCodeAnalysis;
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

    public async Task<IEvaluationResult> Evaluate(Code code, EvaluationModel evaluationModel, CancellationToken token = default) {
        var projectKey = Guid.NewGuid().ToString();
        if (!await sonarQube.TryCreateProject(projectKey, projectKey, token)) {
            logger.LogError("Failed to create project {ProjectKey}", projectKey);
            return new StaticCodeEvaluationResult(code.Guid, false, $"Failed to create project {projectKey}", []);
        }

        var userToken = await sonarQube.GetUserToken(projectKey, token);
        var sonarScannerApi = sonarScannerApiFactory.Create(code.Language);

        var languageSpecification = languageFactory.GetLanguageService(code.Language);
        languageSpecification.CreateWorkingDirectory(WorkingDirectory, code);
        if (!await sonarScannerApi.Run(WorkingDirectory, code, sonarQube.Url, userToken, projectKey, token)) {
            logger.LogError("Failed to run sonar-scanner for project {ProjectKey}", projectKey);
            return new StaticCodeEvaluationResult(code.Guid, false, $"Failed to run sonar-scanner for project {projectKey}", []);
        }

        try {
            var issues = await sonarQube.SearchIssues(projectKey, token).ToListAsync(token);
            return new StaticCodeEvaluationResult(code.Guid, true, null, issues);
        } catch (Exception e) {
            return new StaticCodeEvaluationResult(code.Guid, false, e.Message, []);
        }
    }
}
