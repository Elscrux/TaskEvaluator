using TaskEvaluator.Tasks;
namespace TaskEvaluator.Runtime.Implementation.CSharp;

public sealed class CSharpDockerRuntimeFactory(DockerRuntimeFactory dockerRuntimeFactory) : IRuntimeFactory {
    public Task<IRuntime> Create(Code code, CancellationToken token = default) {
        var dockerRuntimeOptions = new DockerRuntimeOptions {
            Context = code,
            DockerImageName = "task-evaluator/csharp",
            DockerfilePath = Path.Combine(AppContext.BaseDirectory, "Languages", "TaskEvaluator.Language.CSharp", "Dockerfile"),
            WorkingFolder = Path.Combine(AppContext.BaseDirectory)
        };

        return dockerRuntimeFactory.Create(dockerRuntimeOptions, token);
    }
}
