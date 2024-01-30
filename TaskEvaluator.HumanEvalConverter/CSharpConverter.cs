using TaskEvaluator.Language;
namespace TaskEvaluator.HumanEvalConverter;

public interface IHumanEvalConverter {
    ProgrammingLanguage Language { get; }
    string Extension { get; }
    string GetProgram(Task task);
    string GetUnitTest(Task task);
}

public sealed class CSharpConverter : IHumanEvalConverter {
    public ProgrammingLanguage Language => ProgrammingLanguage.CSharp;
    public string Extension => ".cs";

    private string ConvertType(string typeStr) {
        if (typeStr.StartsWith("List[")) {
            var innerType = typeStr[5..^1];
            return $"List<{ConvertType(innerType)}>";
        }

        return typeStr switch {
            "str" => "string",
            "int" => "int",
            "float" => "double",
            "bool" => "bool",
            "None" => "null",
            _ => typeStr
        };
    }

    private string ToValidFunctionName(string str) {
        return string.Concat(str.Select(c => char.IsLetterOrDigit(c) ? c : '_'));
    }

    public string GetProgram(Task task) {
        var functionName = ToValidFunctionName(task.FunctionSignature.Name);
        var returnType = ConvertType(task.FunctionSignature.ReturnType);

        var parameterList = task.FunctionSignature.Parameters
            .Select(p => {
                var type = ConvertType(p.Type);
                return $"{type} {p.Name}";
            })
            .ToList();

        var parameterString = string.Join(", ", parameterList);

        return $$"""
                 namespace Task;

                 public class TaskClass {
                     /// <summary>
                     /// {{task.Documentation}}
                     /// </summary>
                     public static {{returnType}} {{functionName}}({{parameterString}}) {
                         INSERT_CODE_HERE
                     }
                 }
                 """;
    }

    public string GetUnitTest(Task task) {
        var functionName = ToValidFunctionName(task.FunctionSignature.Name);

        var unitTests = task.UnitTests
            .Select(u => {
                var input = u.Input.Replace("\'", "\"");
                var compareValue = u.CompareValue.Replace("\'", "\"");
                var assertion = u.Success ? "Equal" : "NotEqual";
                return $$"""
                             [Fact]
                             public void Test_{{ToValidFunctionName(input)}}() {
                                 var result = TaskClass.{{functionName}}({{input}});
                                 Assert.{{assertion}}({{compareValue}}, result);
                             }
                         """;
            })
            .ToList();

        return $$"""
                 using Xunit;
                 namespace Task;

                 public class Test {
                 {{string.Join(Environment.NewLine +  Environment.NewLine, unitTests)}}
                 }
                 """;
    }
}
