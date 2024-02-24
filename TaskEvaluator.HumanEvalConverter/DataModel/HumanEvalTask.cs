using System.Text;
using System.Text.Json.Serialization;
using Noggog;
namespace TaskEvaluator.HumanEvalConverter.DataModel;

public sealed partial record HumanEvalTask(
    [property: JsonPropertyName("task_id")]
    string TaskId,
    [property: JsonPropertyName("prompt")] string Prompt,
    [property: JsonPropertyName("entry_point")]
    string EntryPoint,
    [property: JsonPropertyName("canonical_solution")]
    string CanonicalSolution,
    [property: JsonPropertyName("test")] string Test) {
    public DataSetTask ToConversionTask() {
        var importsBuilder = new StringBuilder();
        var functionBuilders = new List<StringBuilder>();
        var importsDone = false;
        foreach (var line in Prompt.Split("\n")) {
            if (line.TrimStart().StartsWith("def ", StringComparison.OrdinalIgnoreCase)) {
                importsDone = true;
                functionBuilders.Add(new StringBuilder(line));
            } else if (importsDone) {
                functionBuilders[^1].AppendLine(line);
            } else {
                importsBuilder.AppendLine(line);
            }
        }

        var allFunctionSignatures = functionBuilders
            .Select(
                x => FunctionSignature.Parse(x
                    .ToString()
                    .Replace(Environment.NewLine, "\n")))
            .ToList();

        return new DataSetTask(
            TaskId,
            importsBuilder.ToString().Replace(Environment.NewLine, "\n"),
            allFunctionSignatures[^1],
            allFunctionSignatures[..^1],
            EntryPoint,
            CanonicalSolution,
            ParseTests(Test));
    }

    private static List<UnitTest> ParseTests(string test) {
        return test.Split("\n    assert")
            .Select(l => l.Trim())
            .Where(l => !string.IsNullOrEmpty(l))
            .Skip(1) // skip test header
            .Select(l => {
                // Otherwise, try to parse it manually
                if (l.EndsWith("true", StringComparison.OrdinalIgnoreCase)) {
                    var input = TrimAll(l.Trim().TrimEnd("== True").TrimEnd("==True"));
                    return new UnitTest(input, "true", null, true);
                }
                if (l.EndsWith("false", StringComparison.OrdinalIgnoreCase)) {
                    var input = TrimAll(l.Trim().TrimEnd("== False").TrimEnd("==False"));
                    return new UnitTest(input, "false", null, true);
                }
                if (!l.Contains("==") && !l.Contains(" < ")) {
                    var input = TrimAll(l.Trim());
                    string compareValue;
                    if (input.StartsWith("not", StringComparison.OrdinalIgnoreCase)) {
                        input = TrimAll(input[3..]).Trim();
                        compareValue = "false";
                    } else {
                        compareValue = "true";
                    }
                    return new UnitTest(input, compareValue, null, true);
                }

                try {
                    string functionParameter;
                    string result;
                    string? precision = null;
                    if (l.Contains("==")) {
                        (functionParameter, result) = l
                                .Split("==")
                                .Select(TrimAll)
                                .ToList()
                            switch {
                                [var p, var s] => (
                                    p.Trim()
                                        .TryTrimBothSides("tuple(", ")")
                                        .Trim(),
                                    s.Trim()),
                                _ => throw new ArgumentException("Test must contain exactly one '==' character.")
                            };
                    } else if (l.Contains('<')) {
                        ((functionParameter, result), precision) = l
                                .Split("<")
                                .Select(TrimAll)
                                .ToList()
                            switch {
                                [var v, var p] => (
                                    v.Split("-")
                                        .Select(TrimAll)
                                        .ToList() switch {
                                        [var f, var r] => (x: f, y: r),
                                        _ => throw new ArgumentException("Test must contain exactly one '-' character.")
                                    },
                                    p.Trim()),
                                _ => throw new ArgumentException("Test must contain exactly one '<' character.")
                            };
                    } else {
                        throw new ArgumentException("Test must contain exactly one '==' or '<' character.");
                    }

                    if (result.Contains("tuple(")) {
                        result = result
                            .TryTrimBothSides("tuple(", ")")
                            .Trim();
                    }

                    return new UnitTest(functionParameter, result, precision, true);
                } catch (Exception e) {
                    Console.WriteLine($"Failed to parse test: {l} because of error: {e.Message}");
                    return null;
                }
            })
            .NotNull()
            .ToList();

        string TrimAll(string x) => x.Trim()
            .TryTrimBothSides("abs(", ")").Trim()
            .TryTrimBothSides("tuple(", ")").Trim()
            .TryTrimBothSides("(candidate(", "))").Trim()
            .TryTrimBothSides("candidate(", ")").Trim();
    }
}
