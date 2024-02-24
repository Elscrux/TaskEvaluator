using System.Collections.Generic;
using System.Linq;
using Avalonia.Data.Converters;
using Avalonia.Media;
using TaskEvaluator.Evaluator.UnitTest;
namespace TaskEvaluator.Avalonia.Converters;

public static class UnitTestResultListConverter {
    public static readonly FuncValueConverter<IEnumerable<UnitTestResult>, IBrush> ToBrush
        = new(list => {
            if (list is null) return Brushes.Gray;

            var failed = 0;
            var skipped = 0;
            foreach (var result in list) {
                switch (result.Outcome) {
                    case UnitTestOutcome.Failed:
                        failed++;
                        break;
                    case UnitTestOutcome.Skipped:
                        skipped++;
                        break;
                }
            }

            if (failed > 0) return Brushes.Red;
            if (skipped > 0) return Brushes.Yellow;

            return Brushes.Green;
        });

    public static readonly FuncValueConverter<IEnumerable<UnitTestResult>, string> CountSuccess
        = new(list => {
            if (list is null) return "0";

            return list.Count(e => e.Outcome == UnitTestOutcome.Passed).ToString();
        });
}
