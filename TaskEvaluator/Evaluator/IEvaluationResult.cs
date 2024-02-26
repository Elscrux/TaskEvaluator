using System.Text.Json.Serialization;
using TaskEvaluator.Evaluator.StaticCodeAnalysis;
using TaskEvaluator.Evaluator.SyntaxValidation;
using TaskEvaluator.Evaluator.UnitTest;
namespace TaskEvaluator.Evaluator;

[JsonDerivedType(typeof(StaticCodeEvaluationResult))]
[JsonDerivedType(typeof(UnitTestEvaluationResult))]
[JsonDerivedType(typeof(SyntaxValidationResult))]
public interface IEvaluationResult {
    Guid CodeId { get; }
    bool Success { get; }
    string Evaluator { get; }
    string? Context { get; }

    bool IsValid();
}
