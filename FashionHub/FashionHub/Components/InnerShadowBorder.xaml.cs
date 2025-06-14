using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace FashionHub.Components
{
  public partial class InnerShadowBorder : UserControl
  {

    public static readonly DependencyProperty ShadowFillProperty =
        DependencyProperty.Register(
            "ShadowFill",
            typeof(Brush),
            typeof(InnerShadowBorder),
            new PropertyMetadata(Brushes.Transparent));

    public static readonly DependencyProperty ContentBorderBrushProperty =
        DependencyProperty.Register(
            "ContentBorderBrush",
            typeof(Brush),
            typeof(InnerShadowBorder),
            new PropertyMetadata(Brushes.Transparent));

    public static readonly DependencyProperty ContentBackgroundProperty =
        DependencyProperty.Register(
            "ContentBackground",
            typeof(Brush),
            typeof(InnerShadowBorder),
            new PropertyMetadata(Brushes.Transparent));

    public Brush ShadowFill
    {
      get => (Brush)GetValue(ShadowFillProperty);
      set => SetValue(ShadowFillProperty, value);
    }

    public Brush ContentBorderBrush
    {
      get => (Brush)GetValue(ContentBorderBrushProperty);
      set => SetValue(ContentBorderBrushProperty, value);
    }

    public Brush ContentBackground
    {
      get => (Brush)GetValue(ContentBackgroundProperty);
      set => SetValue(ContentBackgroundProperty, value);
    }

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(
            "CornerRadius",
            typeof(CornerRadius),
            typeof(InnerShadowBorder),
            new PropertyMetadata(new CornerRadius(0)));

    public static readonly DependencyProperty InnerContentProperty =
        DependencyProperty.Register(
            "InnerContent",
            typeof(object),
            typeof(InnerShadowBorder),
            new PropertyMetadata(null));

    public CornerRadius CornerRadius
    {
      get => (CornerRadius)GetValue(CornerRadiusProperty);
      set => SetValue(CornerRadiusProperty, value);
    }

    public object InnerContent
    {
      get => GetValue(InnerContentProperty);
      set => SetValue(InnerContentProperty, value);
    }

    public InnerShadowBorder()
    {
      InitializeComponent();
    }
  }
}