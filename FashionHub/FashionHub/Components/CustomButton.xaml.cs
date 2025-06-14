using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FashionHub.Components
{
  public partial class CustomButton : UserControl
  {
    public static readonly DependencyProperty ButtonTextProperty =
        DependencyProperty.Register("ButtonText", typeof(string), typeof(CustomButton),
            new FrameworkPropertyMetadata("Нажми меня", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public static readonly DependencyProperty ButtonColorProperty =
        DependencyProperty.Register("ButtonColor", typeof(Brush), typeof(CustomButton),
            new PropertyMetadata(Brushes.LightBlue, null, CoerceButtonColor));

    public string ButtonText
    {
      get => (string)GetValue(ButtonTextProperty);
      set => SetValue(ButtonTextProperty, value);
    }

    public Brush ButtonColor
    {
      get => (Brush)GetValue(ButtonColorProperty);
      set => SetValue(ButtonColorProperty, value);
    }

    public CustomButton()
    {
      InitializeComponent();
    }

    private static object CoerceButtonColor(DependencyObject d, object baseValue)
    {
      Brush color = baseValue as Brush;
      return color ?? Brushes.Gray; // Если `null`, то установится серый цвет
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      RaiseEvent(new RoutedEventArgs(ButtonClickedEvent));
    }

    public static readonly RoutedEvent ButtonClickedEvent = EventManager.RegisterRoutedEvent(
        "ButtonClicked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CustomButton));

    public event RoutedEventHandler ButtonClicked
    {
      add => AddHandler(ButtonClickedEvent, value);
      remove => RemoveHandler(ButtonClickedEvent, value);
    }
  }
}
