using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using TaskEvaluator.Extensions;
namespace TaskEvaluator.HumanEvalConverter.DataModel;

public sealed partial record FunctionSignature(
    string ReturnType,
    string Name,
    IReadOnlyList<Parameter> Parameters,
    string Documentation,
    IReadOnlyList<string> Examples) : IParsable<FunctionSignature> {
    private const string NameGroup = "name";
    private const string ParametersGroup = "parameters";
    private const string ReturnTypeGroup = "returnType";

    [GeneratedRegex($@"def\s+(?<{NameGroup}>\w+)\((?<{ParametersGroup}>.*)\)(\s*->\s*(?<{ReturnTypeGroup}>.+))?:")]
    private static partial Regex ParseRegex();
    // Example input: def below_zero(operations: List[int]) -> bool:\n  
    // Use regex to parse this. Use regex groups
    public static FunctionSignature Parse(string s) => Parse(s, CultureInfo.InvariantCulture);
    public static FunctionSignature Parse(string s, IFormatProvider? provider) {
        var (signatureStr, rest, _) = s.Split(""""
                                              """
                                              """") switch {
            [var sig, var d, var r] => (sig, d, r),
            _ => throw new ArgumentException("Prompt must contain exactly three paragraphs separated by escaped single quotes newlines.")
        };
        
        // split documentation by >>>, the first one is the actual doc, the rest is a list of examples
        var (documentation, examples) = rest.SplitMany(">>>", "For example:", "Examples:", "Example:", "Examples", "Example") switch {
            [var d, ..var e] => (d.Replace("\n   ", string.Empty).Replace("\n", string.Empty), e),
            _ => throw new ArgumentException("Documentation must contain at least one example.")
        };
        
        var match = ParseRegex().Match(signatureStr);
        if (!match.Success) {
            throw new ArgumentException("Failed to parse function signature.");
        }

        var name = match.Groups[NameGroup].Value;
        var parameters = match.Groups[ParametersGroup].Value;
        var returnType = match.Groups[ReturnTypeGroup].Value;

        var parameterList = new List<string>();
        var parameterBuilder = new StringBuilder();
        var parenthesisCount = 0;
        foreach (var c in parameters) {
            switch (c) {
                case '[':
                    parenthesisCount++;
                    break;
                case ']':
                    parenthesisCount--;
                    break;
            }

            if (c == ',' && parenthesisCount == 0) {
                parameterList.Add(parameterBuilder.ToString());
                parameterBuilder.Clear();
            } else {
                parameterBuilder.Append(c);
            }
        }
        
        parameterList.Add(parameterBuilder.ToString());

        var @params = parameterList
            .Select(p => {
                var parts = p.Split(':');
                return new Parameter(parts[1].Trim(), parts[0].Trim());
            })
            .ToList();

        return new FunctionSignature(returnType, name, @params, documentation, examples);
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