using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;

namespace Nonogram.Controls;

public class Pixel : TemplatedControl
{
    public static readonly StyledProperty<bool> IsActivatedProperty = AvaloniaProperty.Register<Pixel, bool>(nameof(IsActivated));

    public bool IsActivated
    {
        get => GetValue(IsActivatedProperty);
        set => SetValue(IsActivatedProperty, value);
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        base.OnPointerReleased(e);

        IsActivated = !IsActivated;
    }
}