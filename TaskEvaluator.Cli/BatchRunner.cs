using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TaskEvaluator.Sinks;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Cli;

public sealed class BatchRunner(
    ILogger<BatchRunner> logger,
    ITaskLoader taskLoader,
    TaskRunner taskRunner,
    IEvaluationResultSink evaluationResultSink)
    : IHostedService {

    public Task StartAsync(CancellationToken cancellationToken) {
        logger.LogInformation("Started");

        logger.LogInformation("Loading Tasks");
        var taskSet = taskLoader.Load();

        return Parallel.ForEachAsync(taskSet, cancellationToken, async (task, token) => {
            logger.LogInformation("Generating {Task}", task.CodeGenerationTask.ToString());
            await foreach (var result in taskRunner.Process(task, token)) {
                logger.LogInformation("Evaluating {Task} | Result: {Result}", task.CodeGenerationTask.ToString(), result.ToString());
                evaluationResultSink.Send(result);
            }
        });
    }

    public Task StopAsync(CancellationToken cancellationToken) {
        logger.LogInformation("Stopped");
        return Task.CompletedTask;
    }
}
