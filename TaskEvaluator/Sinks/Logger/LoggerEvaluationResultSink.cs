using System.Text.Json;
using Microsoft.Extensions.Logging;
using TaskEvaluator.Evaluator;
namespace TaskEvaluator.Sinks.Logger;

public sealed class LoggerEvaluationResultSink(ILogger<LoggerEvaluationResultSink> logger) : IEvaluationResultSink {
    public void Send(IEvaluationResult evaluationResult) {
        var serialized = JsonSerializer.Serialize(evaluationResult);
        logger.LogInformation("Returned evaluation result: {Result}", serialized);
    }
}
