using System.Text.Json;
using Microsoft.Extensions.Logging;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Sinks.Logger;

public sealed class LoggerFinalResultSink(ILogger<LoggerFinalResultSink> logger) : IFinalResultSink {
    public void Send(FinalResult finalResult) {
        var serialized = JsonSerializer.Serialize(finalResult);
        logger.LogInformation("Returned final result: {Result}", serialized);
    }
}
