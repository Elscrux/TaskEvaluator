using System.Text.Json;
using Microsoft.Extensions.Options;
using TaskEvaluator.Runtime;
using TaskEvaluator.SonarQube;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Specification.CSharp;

public sealed class CSharpDockerRuntimeFactory(
    DockerRuntimeFactory dockerRuntimeFactory,
    IOptions<SonarQubeConfiguration> sonarQubeConfig) : IRuntimeFactory {

    public Task<IRuntime> Create(Code code, CancellationToken token = default) {
        var dockerRuntimeOptions = new DockerRuntimeOptions {
            Context = code,
            DockerImageName = "task-evaluator/csharp",
            DockerfilePath = Path.Combine(AppContext.BaseDirectory,
                "Runtimes",
                "TaskEvaluator.Runtime.CSharp",
                "Dockerfile"),
            WorkingFolder = Path.Combine(AppContext.BaseDirectory),
            UnitTestEndpoint = "unit-test",
            StaticCodeQualityAnalysisEndpoint = "sonar-qube",
            EnvironmentVariables = [
                $"\"SONARQUBE={JsonSerializer.Serialize(sonarQubeConfig.Value).Replace("\"", "\\\"")}\"",
            ],
            Networks = [ "taskevaluator_sonarqube_net" ]
        };

        return dockerRuntimeFactory.Create(dockerRuntimeOptions, token);
    }
}
