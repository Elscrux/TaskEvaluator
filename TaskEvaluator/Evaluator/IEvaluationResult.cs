using System.Text.Json.Serialization;
using TaskEvaluator.Evaluator.StaticCodeAnalysis;
using TaskEvaluator.Evaluator.UnitTest;
namespace TaskEvaluator.Evaluator;

[JsonDerivedType(typeof(StaticCodeEvaluationResult))]
[JsonDerivedType(typeof(UnitTestEvaluationResult))]
public interface IEvaluationResult {
    Guid TaskId { get; }
    bool Success { get; }
    string? Context { get; }
}
