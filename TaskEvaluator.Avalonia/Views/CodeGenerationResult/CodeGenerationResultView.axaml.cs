using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
namespace TaskEvaluator.Avalonia.Views.CodeGenerationResult;

public partial class CodeGenerationResultView : UserControl {
    public CodeGenerationResultView() {
        InitializeComponent();
    }

    private void Expand(object? sender, RoutedEventArgs e) {
        if (e.Source is not Control control) return;

        var treeViewItem = control.FindAncestorOfType<TreeViewItem>();
        if (treeViewItem is null) return;

        treeViewItem.IsExpanded = true;
    }
}
