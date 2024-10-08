using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using NonoGramGen.Model;

namespace NonoGramGen.Controls;

public class NonogramCanvas : TemplatedControl
{
    private Grid? _grid;

    public static readonly StyledProperty<int> RowCountProperty =
        AvaloniaProperty.Register<NonogramCanvas, int>(nameof(RowCount));

    public static readonly StyledProperty<int> ColumnCountProperty =
        AvaloniaProperty.Register<NonogramCanvas, int>(nameof(RowCount));

    public static readonly StyledProperty<Nonogram> NonogramProperty =
        AvaloniaProperty.Register<NonogramCanvas, Nonogram>(nameof(Nonogram));

    public int RowCount
    {
        get => GetValue(RowCountProperty);
        set => SetValue(RowCountProperty, value);
    }

    public int ColumnCount
    {
        get => GetValue(ColumnCountProperty);
        set => SetValue(ColumnCountProperty, value);
    }

    public Nonogram Nonogram
    {
        get => GetValue(NonogramProperty);
        set => SetValue(NonogramProperty, value);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        _grid = (Grid)e.NameScope.Find("grid")!;
        
        InitGrid();
        
        base.OnApplyTemplate(e);
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        
        InitGrid();
    }

    private void InitGrid()
    {
        if (_grid is null)
        {
            return;
        }
        
        _grid.ColumnDefinitions.Clear();
        _grid.RowDefinitions.Clear();
        _grid.Children.Clear();
        
        InitGridConfig();

        InitPixels();

        InitLineRenderer();
    }

    private void InitPixels()
    {
        for (int row = 0; row < RowCount; row++)
        {
            for (int col = 0; col < ColumnCount; col++)
            {
                var pixel = new Pixel();
                _grid!.Children.Add(pixel);

                var index = row * ColumnCount + col;
                if (index < RowCount)
                {
                    pixel.IsActivated = Nonogram.Goal[index] == '1';
                }

                Grid.SetRow(pixel, row);
                Grid.SetColumn(pixel, col);
            }
        }
    }

    private void InitGridConfig()
    {
        for (int i = 0; i < ColumnCount; i++)
        {
            _grid!.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        }

        for (int i = 0; i < RowCount; i++)
        {
            _grid!.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
        }
    }

    private void InitLineRenderer()
    {
        var lineRenderer = new GridLineRenderer();
        lineRenderer.Bind(GridLineRenderer.LineColorProperty, Resources.GetResourceObservable("GridColor"));
        
        Grid.SetColumnSpan(lineRenderer, ColumnCount);
        Grid.SetRowSpan(lineRenderer, RowCount);
        Grid.SetRow(lineRenderer, 0);
        Grid.SetColumn(lineRenderer, 0);
        
        _grid.Children.Add(lineRenderer);
    }
}