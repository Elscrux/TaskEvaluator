﻿namespace TaskEvaluator.HumanEvalConverter.DataModel;

public sealed record UnitTest(
    string Input,
    string CompareValue,
    string? Precision,
    bool Success);
