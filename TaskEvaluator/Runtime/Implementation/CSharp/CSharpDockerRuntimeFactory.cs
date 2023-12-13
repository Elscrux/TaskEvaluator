using TaskEvaluator.Tasks;
namespace TaskEvaluator.Runtime.Implementation.CSharp;

public sealed class CSharpDockerRuntimeFactory(DockerRuntimeFactory dockerRuntimeFactory) : IRuntimeFactory {
    public Task<IRuntime> Create(Code code, CancellationToken token = default) {
        var dockerRuntimeOptions = new DockerRuntimeOptions {
            Context = code,
            DockerImageName = "task-evaluator/csharp",
            ProjectFolder = Path.Combine(AppContext.BaseDirectory, "Languages", "TaskEvaluator.Language.CSharp")
        };

        return dockerRuntimeFactory.Create(dockerRuntimeOptions, token);
    }
}
