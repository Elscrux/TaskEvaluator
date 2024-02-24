using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Projektanker.Icons.Avalonia;
using ObservableExtensions = System.ObservableExtensions;
namespace TaskEvaluator.Avalonia.Controls;

public partial class ValidView : UserControl {
    public static readonly StyledProperty<bool> IsValidProperty
        = AvaloniaProperty.Register<ToggleView, bool>(nameof(IsValid));

    public bool IsValid {
        get => GetValue(IsValidProperty);
        set => SetValue(IsValidProperty, value);
    }

    public ValidView() {
        InitializeComponent();
    }

    private IDisposable? _subscription;

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e) {
        base.OnAttachedToVisualTree(e);

        _subscription = ObservableExtensions.Subscribe(this.GetObservable(IsValidProperty), toggle => {
            Control.Content = new Icon {
                Value = "fa-soldid " + (toggle ? "fa-check" : "fa-xmark"),
                Foreground = toggle ? Brushes.Green : Brushes.Red
            };
        });
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e) {
        base.OnDetachedFromVisualTree(e);

        _subscription?.Dispose();
    }
}
