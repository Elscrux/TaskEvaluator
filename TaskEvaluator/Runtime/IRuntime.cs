using TaskEvaluator.Evaluator.StaticCodeAnalysis;
using TaskEvaluator.Evaluator.SyntaxValidation;
using TaskEvaluator.Evaluator.UnitTest;
using TaskEvaluator.Tasks;
namespace TaskEvaluator.Runtime;

public interface IRuntime : IDisposable {
    Code Context { get; }

    Task<SyntaxValidationRuntimeResult> SyntaxValidation(CancellationToken token = default);
    Task<UnitTestRuntimeResult> UnitTest(Code unitTest, CancellationToken token = default);
    Task<StaticCodeRuntimeResult> StaticCodeQualityAnalysis(CancellationToken token = default);
}
