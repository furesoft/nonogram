using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Input;

namespace NonoGramGen.Controls;

public class Pixel : TemplatedControl
{
    public static readonly StyledProperty<bool> IsActivatedProperty =
        AvaloniaProperty.Register<Pixel, bool>(nameof(IsActivated));

    public bool IsActivated
    {
        get => GetValue(IsActivatedProperty);
        set => SetValue(IsActivatedProperty, value);
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        IsActivated = !IsActivated;
    }
}