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
            if (line.TrimStart().StartsWith("def ")) {
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
        var (_, testCode) = test.Split("\n\n\n") switch {
            [var r] => (string.Empty, r),
            [var i, .. var r] => (i, string.Join("\n    assert", r)),
            _ => throw new ArgumentException("Test must contain at least two paragraphs separated by three newlines.")
        };

        return testCode.Split("\n    assert")
            .Select(l => l.Trim())
            .Where(l => !string.IsNullOrEmpty(l))
            .Skip(1) // skip test header
            .Select(l => {
                // Otherwise, try to parse it manually
                if (l.EndsWith("true", StringComparison.OrdinalIgnoreCase)) {
                    var input = TrimAll(l.Trim().TrimEnd("== True").TrimEnd("==True"));
                    return new UnitTest(input, "true", true);
                }
                if (l.EndsWith("false", StringComparison.OrdinalIgnoreCase)) {
                    var input = TrimAll(l.Trim().TrimEnd("== False").TrimEnd("==False"));
                    return new UnitTest(input, "false", true);
                }

                try {
                    var (functionParameter, result) = l
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

                    if (result.Contains("tuple(")) {
                        result = result
                            .TryTrimBothSides("tuple(", ")")
                            .Trim();
                    }

                    return new UnitTest(functionParameter, result, true);
                } catch (Exception e) {
                    return null;
                }
            })
            .NotNull()
            .ToList();

        string TrimAll(string x) => x.Trim()
            .TryTrimBothSides("tuple(", ")").Trim()
            .TryTrimBothSides("(candidate(", "))").Trim()
            .TryTrimBothSides("candidate(", ")").Trim();
    }
}
