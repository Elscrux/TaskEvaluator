// See https://aka.ms/new-console-template for more information
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Noggog;
using TaskEvaluator.HumanEvalConverter;

var data = File.ReadAllText("human-eval-v2-20210705.json");
var tasks = JsonSerializer.Deserialize<HumanEvalTask[]>(data);
if (tasks == null) {
    throw new ArgumentException("Failed to deserialize tasks.");
}

List<CSharpConverter> converter = [new CSharpConverter()];

foreach (var humanEvalTask in tasks) {
    var task = humanEvalTask.ToConversionTask();

    foreach (var c in converter) {
        var program = c.GetProgram(task);
        var taskFolder = Path.Combine(c.Language.ToString(), task.TaskId);
        Directory.CreateDirectory(taskFolder);
        var programFile = Path.Combine(taskFolder, "Program" + c.Extension);
        File.WriteAllText(programFile, program);

        var unitTest = c.GetUnitTest(task);
        var unitTestFile = Path.Combine(taskFolder, "UnitTests" + c.Extension);
        File.WriteAllText(unitTestFile, unitTest);

        var metadataFile = Path.Combine(taskFolder, "metadata.json");
        File.WriteAllText(metadataFile, GetMetadata(humanEvalTask));
    }
}

string GetMetadata(HumanEvalTask task) {
    return $$"""
             {
                 "id": "{{Guid.NewGuid()}}",
                 "isHumanEval": true
             }
             """;
}

public sealed partial record HumanEvalTask(
    [property: JsonPropertyName("task_id")] string TaskId,
    [property: JsonPropertyName("prompt")] string Prompt,
    [property: JsonPropertyName("entry_point")]
    string EntryPoint,
    [property: JsonPropertyName("canonical_solution")]
    string CanonicalSolution,
    [property: JsonPropertyName("test")] string Test) {
    public Task ToConversionTask() {
        var (imports, rest) = Prompt.Split("\n\n\n") switch {
            [var r] => ("", r),
            [var i, var r] => (i, r),
            _ => throw new ArgumentException("Prompt must contain exactly two paragraphs separated by three newlines.")
        };

        var (signatureStr, rest2, _) = rest.Split(""""
                                                       """
                                                       """") switch {
            [var s, var d, var r] => (s, d, r),
            _ => throw new ArgumentException("Prompt must contain exactly three paragraphs separated by escaped single quotes newlines.")
        };

        // split documentation by >>>, the first one is the actual doc, the rest is a list of examples
        var (documentation, examples) = rest2.Split(">>>") switch {
            [var d, ..var e] => (d, e),
            _ => throw new ArgumentException("Documentation must contain at least one example.")
        };

        return new Task(
            TaskId,
            imports,
            FunctionSignature.Parse(signatureStr, CultureInfo.InvariantCulture),
            documentation.Replace("\n   ", string.Empty),
            examples,
            EntryPoint,
            CanonicalSolution,
            ParseTests(Test));
    }

    private static List<UnitTest> ParseTests(string test) {
        var (_, testCode) = test.Split("\n\n\n") switch {
            [var i, var r] => (i, r),
            _ => throw new ArgumentException("Test must contain exactly two paragraphs separated by three newlines.")
        };

        return testCode.Split("\n    assert candidate")
            .Select(l => l.Trim())
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Skip(1) // skip test header
            .Select(l => {
                // Otherwise, try to parse it manually
                if (l.EndsWith("true", StringComparison.OrdinalIgnoreCase)) {
                    var input = l.Trim().TrimEnd(" == True").Trim().Trim('(', ')').Trim();
                    return new UnitTest(input, "true", true);
                }
                if (l.EndsWith("false", StringComparison.OrdinalIgnoreCase)) {
                    var input = l.Trim().TrimEnd(" == False").Trim().Trim('(', ')').Trim();
                    return new UnitTest(input, "false", true);
                }

                var (functionParameters, result) = l.Split("==") switch {
                    [var p, var s] => (p.Trim().Trim('(', ')').Trim(), s),
                    _ => throw new ArgumentException("Test must contain exactly one '==' character.")
                };
                
                return new UnitTest(functionParameters, result, true);
            })
            .ToList();
    }
}

public sealed record Task(
    string TaskId,
    string Imports,
    FunctionSignature FunctionSignature,
    string Documentation,
    IReadOnlyList<string> Examples,
    string EndPoint,
    string CanonicalSolution,
    IReadOnlyList<UnitTest> UnitTests);

public sealed record UnitTest(
    string Input,
    string CompareValue,
    bool Success);

public sealed partial record FunctionSignature(
    string ReturnType,
    string Name,
    IReadOnlyList<Parameter> Parameters) : IParsable<FunctionSignature> {
    private const string NameGroup = "name";
    private const string ParametersGroup = "parameters";
    private const string ReturnTypeGroup = "returnType";

    [GeneratedRegex($@"def\s+(?<{NameGroup}>\w+)\((?<{ParametersGroup}>.*)\)\s*->\s*(?<{ReturnTypeGroup}>.+):")]
    private static partial Regex ParseRegex();
    // Example input: def below_zero(operations: List[int]) -> bool:\n  
    // Use regex to parse this. Use regex groups
    public static FunctionSignature Parse(string s, IFormatProvider? provider) {
        var match = ParseRegex().Match(s);
        if (!match.Success) {
            throw new ArgumentException("Failed to parse function signature.");
        }

        var name = match.Groups[NameGroup].Value;
        var parameters = match.Groups[ParametersGroup].Value;
        var returnType = match.Groups[ReturnTypeGroup].Value;

        var parameterList = parameters.Split(',')
            .Select(p => p.Split(':'))
            .Select(p => new Parameter(p[1].Trim(), p[0].Trim()))
            .ToList();

        return new FunctionSignature(returnType, name, parameterList);
    }
    public static bool TryParse(string? s, IFormatProvider? provider, out FunctionSignature result) {
        try {
            result = Parse(s, provider);
            return true;
        } catch {
            result = default!;
            return false;
        }
    }
}

public sealed record Parameter(
    string Type,
    string Name);
