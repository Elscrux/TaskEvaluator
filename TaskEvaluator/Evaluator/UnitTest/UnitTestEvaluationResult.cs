using System.Text.Json;
using TaskEvaluator.Runtime;
namespace TaskEvaluator.Evaluator.UnitTest;

public sealed record UnitTestEvaluationResult(Guid Guid, bool Success, string? Context) : IEvaluationResult {
    public UnitTestEvaluationResult(Guid guid, IRuntimeResult runtimeResult) : this(
        guid,
        runtimeResult.Success,
        runtimeResult.ReturnValue switch {
            null => null,
            string str => str,
            _ => JsonSerializer.Serialize(runtimeResult.ReturnValue)
        }) {}
}
