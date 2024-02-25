using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TaskEvaluator.Sinks;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Cli;

public sealed class BatchRunner(
    ILogger<BatchRunner> logger,
    ITaskLoader taskLoader,
    TaskRunner taskRunner,
    IEnumerable<IFinalResultSink> evaluationResultSinks)
    : IHostedService {

    private readonly IList<IFinalResultSink> _evaluationResultSinks = evaluationResultSinks.ToList();

    public Task StartAsync(CancellationToken cancellationToken) {
        logger.LogInformation("Started");

        logger.LogInformation("Loading Tasks");
        var taskSet = taskLoader.Load();

        return taskSet.AwaitAll(async task => {
            logger.LogInformation("Generating {Task}", task.CodeGenerationTask.ToString());
            await foreach (var result in taskRunner.Process(task, cancellationToken)) {
                foreach (var evaluationResultSink in _evaluationResultSinks) {
                    evaluationResultSink.Send(result);
                }
            }
        }, 5, cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) {
        logger.LogInformation("Stopped");
        return Task.CompletedTask;
    }
}
