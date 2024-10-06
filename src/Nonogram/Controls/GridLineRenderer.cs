using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace Nonogram.Controls;

public class GridLineRenderer : Control
{
    public static readonly StyledProperty<IBrush> LineColorProperty =
        AvaloniaProperty.Register<GridLineRenderer, IBrush>(nameof(LineColor), Brushes.Black);

    public IBrush LineColor
    {
        get => GetValue(LineColorProperty);
        set => SetValue(LineColorProperty, value);
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);

        if (Parent is Grid parentGrid)
        {
            var width = Bounds.Width;
            var height = Bounds.Height;

            var columnCount = parentGrid.ColumnDefinitions.Count;
            var rowCount = parentGrid.RowDefinitions.Count;

            for (var i = 0; i <= columnCount; i++)
            {
                var x = i * width / columnCount;
                context.DrawLine(new Pen(LineColor), new Point(x, 0), new Point(x, height));
            }

            for (var j = 0; j <= rowCount; j++)
            {
                var y = j * height / rowCount;
                context.DrawLine(new Pen(LineColor), new Point(0, y), new Point(width, y));
            }
        }
    }
}