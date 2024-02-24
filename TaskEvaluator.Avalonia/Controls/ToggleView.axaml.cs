using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
namespace TaskEvaluator.Avalonia.Controls;

public partial class ToggleView : UserControl {
    public static readonly StyledProperty<bool> ToggleProperty
        = AvaloniaProperty.Register<ToggleView, bool>(nameof(Toggle));

    public bool Toggle {
        get => GetValue(ToggleProperty);
        set => SetValue(ToggleProperty, value);
    }

    public static readonly StyledProperty<Control> EnabledProperty
        = AvaloniaProperty.Register<ToggleView, Control>(nameof(Enabled));

    public Control Enabled {
        get => GetValue(EnabledProperty);
        set => SetValue(EnabledProperty, value);
    }

    public static readonly StyledProperty<Control> DisabledProperty
        = AvaloniaProperty.Register<ToggleView, Control>(nameof(Disabled));

    public Control Disabled {
        get => GetValue(DisabledProperty);
        set => SetValue(DisabledProperty, value);
    }


    public ToggleView() {
        InitializeComponent();
    }

    private IDisposable? _subscription;

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e) {
        base.OnAttachedToVisualTree(e);

        _subscription = this.GetObservable(ToggleProperty)
            .Subscribe(toggle => {
                Control.Content = toggle ? Enabled : Disabled;
            });
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e) {
        base.OnDetachedFromVisualTree(e);

        _subscription?.Dispose();
    }
}
