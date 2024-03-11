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

    public static readonly FuncValueConverter<TaskState, bool> IsRunning
        = new(outcome => {
            return outcome switch {
                TaskState.NotStarted => false,
                TaskState.Running => true,
                TaskState.Success => false,
                TaskState.Fail => false,
                _ => false
            };
        });
}
