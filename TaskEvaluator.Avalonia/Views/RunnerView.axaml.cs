using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using Avalonia.VisualTree;
using TaskEvaluator.Avalonia.ViewModels;
namespace TaskEvaluator.Avalonia.Views;

public partial class RunnerView : ReactiveUserControl<IRunnerVM> {
    public RunnerView() {
        InitializeComponent();
    }

    private void Expand(object? sender, RoutedEventArgs e) {
        if (e.Source is not Control control) return;

        var treeViewItem = control.FindAncestorOfType<TreeViewItem>();
        if (treeViewItem is null) return;

        treeViewItem.IsExpanded = true;
    }
}
