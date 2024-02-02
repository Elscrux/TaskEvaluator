using System.Globalization;
namespace TaskEvaluator.HumanEvalConverter.Converter.Case;

public sealed class PascalCaseConverter : ICaseConverter {
    public string FromSnakeCase(string snakeCaseString) {
        snakeCaseString = snakeCaseString.ToLower().Replace("_", " ");
        var info = CultureInfo.CurrentCulture.TextInfo;
        snakeCaseString = info.ToTitleCase(snakeCaseString).Replace(" ", string.Empty);
        return snakeCaseString;
    }
}
