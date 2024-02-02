using System.Text.RegularExpressions;
using TaskEvaluator.HumanEvalConverter.Converter.Case;
using TaskEvaluator.HumanEvalConverter.DataModel;
using TaskEvaluator.Language;
namespace TaskEvaluator.HumanEvalConverter.Converter.Language;

public sealed partial class CSharpConverter : IHumanEvalConverter {
    public ProgrammingLanguage Language => ProgrammingLanguage.CSharp;
    public ICaseConverter CaseConverter => new PascalCaseConverter();
    public string Extension => ".cs";

    private string ConvertType(string typeStr) {
        // List[int] => List<int>
        if (typeStr.StartsWith("List[")) {
            var innerType = typeStr[5..^1];
            return $"List<{ConvertType(innerType)}>";
        }
        
        // Dict[int, int] => Dictionary<int, int>
        if (typeStr.StartsWith("Dict[")) {
            var innerTypes = typeStr[5..^1].Split(", ");
            return $"Dictionary<{string.Join(", ", innerTypes.Select(ConvertType))}>";
        }

        // Tuple[int, int] => (int, int)
        if (typeStr.StartsWith("Tuple[")) {
            var innerTypes = typeStr[6..^1].Split(", ");
            typeStr = $"({string.Join(", ", innerTypes.Select(ConvertType))})";
        }

        // Optional[int] => int?
        if (typeStr.StartsWith("Optional[")) {
            var innerType = typeStr[9..^1];
            typeStr = $"{ConvertType(innerType)}?";
        }

        return typeStr switch {
            "Any" => "object",
            "str" => "string",
            "int" => "int",
            "float" => "double",
            "bool" => "bool",
            "None" => "null",
            _ => typeStr
        };
    }

    private string ConvertVariableName(string name) => "@" + name;

    private string GetDefaultValue(string type) {
        if (type.StartsWith("List<")) {
            return $"new List<{type[5..^1]}>()";
        }

        if (type.StartsWith('(')) {
            return $"({string.Join(", ", type[1..^1].Split(", ").Select(GetDefaultValue))})";
        }

        if (type.EndsWith('?')) {
            return "null";
        }

        return type switch {
            "string" => "string.Empty",
            "int" => "0",
            "double" => "0.0",
            "bool" => "false",
            "null" => "null",
            "object" => "new object()",
            _ => $"default({type})"
        };
    }

    private string ToValidFunctionName(string str) {
        var snakeCase = string.Concat(str.Select(c => char.IsLetterOrDigit(c) ? c : '_'));
        return CaseConverter.FromSnakeCase(snakeCase);
    }

    private string ToFunction(FunctionSignature functionSignature) {
        var functionName = ToValidFunctionName(functionSignature.Name);
        var returnType = ConvertType(functionSignature.ReturnType);

        var parameterList = functionSignature.Parameters
            .Select(p => {
                var type = ConvertType(p.Type);
                var name = ConvertVariableName(p.Name);
                return $"{type} {name}";
            })
            .ToList();

        var parameterString = string.Join(", ", parameterList);

        return $"{returnType} {functionName}({parameterString})";
    }

    public string GetProgram(DataSetTask dataSetTask) {
        var helperFunctions = dataSetTask.HelperFunctions.Count > 0
            ? "These helper functions are available: " + string.Join(", ", dataSetTask.HelperFunctions.Select(ToFunction))
            : string.Empty;

        var documentation = dataSetTask.FunctionSignature.Documentation.Replace("None", "null");

        return $$"""
                 namespace Task;

                 public class TaskClass {
                     /// <summary>
                     /// {{documentation}}
                     /// {{helperFunctions}}
                     /// </summary>
                     public static {{ToFunction(dataSetTask.FunctionSignature)}} {
                         //INSERT_CODE_HERE
                     }
                 }
                 """;
    }

    public string GetUnitTest(DataSetTask dataSetTask) {
        var functionName = ToValidFunctionName(dataSetTask.FunctionSignature.Name);

        var unitTests = dataSetTask.UnitTests
            .Select((u, i) => {
                var input = ConvertValueSimple(u.Input.Replace("\'", "\""));
                var compareValue = ConvertValue(u.CompareValue, ConvertType(dataSetTask.FunctionSignature.ReturnType));
                var assertion = u.Success ? "Equal" : "NotEqual";
                return $$"""
                             [Fact]
                             public void Test_{{i}}() {
                                 var result = TaskClass.{{functionName}}({{input}});
                                 Assert.{{assertion}}({{compareValue}}, result);
                             }
                         """;
            })
            .ToList();

        return $$"""
                 using Xunit;
                 namespace Task;

                 public class Test_{{functionName}} {
                 {{string.Join(Environment.NewLine + Environment.NewLine, unitTests)}}
                 }
                 """;
    }
    private string ConvertValue(string value, string type) {
        // Handle dictionaries {"a": 1, "b": 1, "c": 1, "d": 1, "g": 1} => new Dictionary<string, int> { {"a", 1}, ... }
        if (type.StartsWith("Dictionary<")) {
            var innerTypes = type[11..^1].Split(", ");
            var keyType = innerTypes[0];
            var valueType = innerTypes[1];

            var keyValuePairs = value[1..^1]
                .Split(",")
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(kv => {
                    var pair = kv.Split(":");
                    var key = ConvertValue(pair[0].Trim(), keyType);
                    var val = ConvertValue(pair[1].Trim(), valueType);
                    return $"{{{key}, {val}}}";
                });

            return $"new Dictionary<{keyType}, {valueType}> {{ {string.Join(", ", keyValuePairs)} }}";
        }

        return ConvertValueSimple(value);
    }

    [GeneratedRegex(@"(\d+)\*\*(\d+)")]
    private static partial Regex PowRegex();

    private string ConvertValueSimple(string value) {
        // Handle pow 2**3 => Math.Pow(2, 3)
        var powRegex = PowRegex();
        value = powRegex.Replace(value, "Math.Pow($1, $2)");

        return value.Replace("\'", "\"")
            .Replace("True", "true")
            .Replace("False", "false")
            .Replace("None", "null")
            .Replace("{}", "new Dictionary<object, object>()");
    }
}
