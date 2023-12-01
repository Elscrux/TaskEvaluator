using TaskEvaluator.Docker;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Runtime.Implementation.CSharp;

public sealed class CSharpDockerRuntimeFactory(
    IHttpClientFactory httpClientFactory,
    ILogger<CSharpDockerRuntimeFactory> logger,
    DockerHostFactory dockerHostFactory) : IRuntimeFactory {

    public DockerHost DockerHost { get; } = dockerHostFactory.Create(50001);

    public async Task<IRuntime> Create(Code code, CancellationToken token = default) {
        await DockerHost.StartContainer(
            Path.Combine(AppContext.BaseDirectory, "Languages", "TaskEvaluator.Language.CSharp", "compose.yaml"),
            token);

        return new FuncRuntime(code, CallDockerContainer);
    }

    private async Task<IRuntimeResult> CallDockerContainer(CancellationToken token = default) {
        using var httpClient = httpClientFactory.CreateClient();

        try {
            var result = await httpClient.GetStringAsync(DockerHost.Uri("weatherforecast"), token).ConfigureAwait(false);

            logger.LogInformation("Returned: {Result}", result);

            return new CSharpRuntimeResult(true, result);
        } catch (Exception e) {
            logger.LogError(e, "Error while running code");

            return new CSharpRuntimeResult(false, e.Message);
        }
    }

    public void Dispose() => DockerHost.Dispose();
}
