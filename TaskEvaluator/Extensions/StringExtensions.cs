namespace TaskEvaluator.Extensions;

public static class StringExtensions {
    public static string[] SplitMany(this string s, params string[] splitters) {
        string[] seed = [s];
        return splitters
            .Aggregate(seed, (current, splitter) 
                => current
                    .SelectMany(r => r.Split(splitter))
                    .ToArray());
    }

    public static string TryTrimBothSides(this string str, string trimStart, string trimEnd) {
        return str.StartsWith(trimStart) && str.EndsWith(trimEnd)
            ? str[trimStart.Length..^trimEnd.Length]
            : str;
    }
}