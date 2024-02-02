namespace TaskEvaluator.HumanEvalConverter.DataModel;

public sealed record DataSetTask(
    string TaskId,
    string Imports,
    FunctionSignature FunctionSignature,
    IReadOnlyList<FunctionSignature> HelperFunctions,
    string EndPoint,
    string CanonicalSolution,
    IReadOnlyList<UnitTest> UnitTests);
