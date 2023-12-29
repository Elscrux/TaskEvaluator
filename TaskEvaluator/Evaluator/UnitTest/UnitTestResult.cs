namespace TaskEvaluator.Evaluator.UnitTest;

public sealed record UnitTestResult(string TestName, UnitTestOutcome Outcome, TimeSpan Duration);
