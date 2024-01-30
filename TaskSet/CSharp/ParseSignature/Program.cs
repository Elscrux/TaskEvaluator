namespace Task;

public sealed record FunctionSignature(
    string ReturnType,
    string Name,
    IReadOnlyList<Parameter> Parameters);

public sealed record Parameter(
    string Type,
    string Name);

public class TaskClass {
    /// <summary>
    /// Parse a python function signature into the object model of type <see cref="FunctionSignature"/> provided above.
    /// Uses RegEx and RegEx Groups to parse this.
    /// <example>
    /// Example input: def below_zero(operations: List[int]) -> bool:\n
    /// </example>
    /// </summary>
    /// <param name="str">String containing python function signature</param>
    /// <returns>Valid FunctionSignature object of provided string</returns>
    public static FunctionSignature Parse(string str) {
        INSERT_CODE_HERE
    }
}
