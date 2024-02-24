using Avalonia.Data.Converters;
using Avalonia.Media;
namespace TaskEvaluator.Avalonia.Converters;

public static class BoolConverter {
    public static readonly FuncValueConverter<bool, IBrush> ToBrush
        = new(value => value ? Brushes.Green : Brushes.Red);
}
