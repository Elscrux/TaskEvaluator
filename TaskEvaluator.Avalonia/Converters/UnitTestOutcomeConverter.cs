using System;
using Avalonia.Data.Converters;
using Avalonia.Media;
using TaskEvaluator.Evaluator.UnitTest;
namespace TaskEvaluator.Avalonia.Converters;

public static class UnitTestOutcomeConverter {
    public static readonly FuncValueConverter<UnitTestOutcome, IBrush> ToBrush
        = new(outcome => {
            return outcome switch {
                UnitTestOutcome.Failed => Brushes.Red,
                UnitTestOutcome.Passed => Brushes.Green,
                UnitTestOutcome.Skipped => Brushes.Yellow,
                _ => throw new ArgumentOutOfRangeException(nameof(outcome), outcome, null)
            };
        });
}
