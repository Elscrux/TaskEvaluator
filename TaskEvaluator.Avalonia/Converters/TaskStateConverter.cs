using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media;
using TaskEvaluator.Avalonia.ViewModels;
namespace TaskEvaluator.Avalonia.Converters;

public static class TaskStateConverter {
    public static readonly FuncValueConverter<TaskState, IBrush> ToBrush
        = new(outcome => {
            return outcome switch {
                TaskState.Running => Brushes.DodgerBlue,
                TaskState.Success => Brushes.Green,
                TaskState.Fail => Brushes.Red,
                _ => (string) Application.Current!.ActualThemeVariant.Key == "Dark" ? Brushes.White : Brushes.Black
            };
        });
}
