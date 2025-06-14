using FashionHub.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NavigationService = FashionHub.Services.NavigationService;

namespace FashionHub.Components
{
  /// <summary>
  /// Логика взаимодействия для CustomComboBox.xaml
  /// </summary>
  public partial class CustomComboBox : UserControl
  {

    public static readonly DependencyProperty SelectedValuePathProperty =
    DependencyProperty.Register("SelectedValuePath", typeof(string), typeof(CustomComboBox));

    public static readonly DependencyProperty SelectedValueProperty =
        DependencyProperty.Register("SelectedValue", typeof(object), typeof(CustomComboBox), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public static readonly DependencyProperty DisplayMemberPathProperty =
        DependencyProperty.Register("DisplayMemberPath", typeof(string), typeof(CustomComboBox));

    public string SelectedValuePath
    {
      get => (string)GetValue(SelectedValuePathProperty);
      set => SetValue(SelectedValuePathProperty, value);
    }

    public object SelectedValue
    {
      get => GetValue(SelectedValueProperty);
      set => SetValue(SelectedValueProperty, value);
    }

    public string DisplayMemberPath
    {
      get => (string)GetValue(DisplayMemberPathProperty);
      set => SetValue(DisplayMemberPathProperty, value);
    }

    public static readonly DependencyProperty ItemsSourceProperty =
    DependencyProperty.Register("ItemsSource", typeof(IEnumerable<object>), typeof(CustomComboBox));

    public static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register("SelectedItem", typeof(object), typeof(CustomComboBox), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.Register("Placeholder", typeof(string), typeof(CustomComboBox), new PropertyMetadata("Выбрать значение"));

    public static readonly DependencyProperty MaxItemsProperty =
    DependencyProperty.Register("MaxItems", typeof(int), typeof(CustomComboBox),
        new FrameworkPropertyMetadata(5, null, new CoerceValueCallback(CoerceMaxItems)),
        new ValidateValueCallback (ValidateMaxItems));

    private static bool ValidateMaxItems(object value)
    {
      int val = (int)value;
      return val > 0 && val <= 50;
    }

    private static object CoerceMaxItems(DependencyObject d, object baseValue)
    {
      int val = (int)baseValue;
      return val > 30 ? 30 : val;
    }

    public static readonly RoutedEvent DirectSelectionChangedEvent = EventManager.RegisterRoutedEvent(
    "DirectSelectionChanged", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(CustomComboBox));

    public event RoutedEventHandler DirectSelectionChanged
    {
      add => AddHandler(DirectSelectionChangedEvent, value);
      remove => RemoveHandler(DirectSelectionChangedEvent, value);
    }

    public static readonly RoutedEvent TunnelingSelectionChangedEvent = EventManager.RegisterRoutedEvent(
    "TunnelingSelectionChanged", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(CustomComboBox));

    public static readonly RoutedEvent BubblingSelectionChangedEvent = EventManager.RegisterRoutedEvent(
    "BubblingSelectionChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CustomComboBox));

    protected void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      RaiseEvent(new RoutedEventArgs(BubblingSelectionChangedEvent));
    }



    public IEnumerable<object> ItemsSource
    {
      get => (IEnumerable<object>)GetValue(ItemsSourceProperty);
      set => SetValue(ItemsSourceProperty, value);
    }

    public object SelectedItem
    {
      get => GetValue(SelectedItemProperty);
      set => SetValue(SelectedItemProperty, value);
    }

    public string Placeholder
    {
      get => (string)GetValue(PlaceholderProperty);
      set => SetValue(PlaceholderProperty, value);
    }

    public int MaxItems
    {
      get => (int)GetValue(MaxItemsProperty);
      set => SetValue(MaxItemsProperty, value);
    }

    public event RoutedEventHandler BubblingSelectionChanged
    {
      add => AddHandler(BubblingSelectionChangedEvent, value);
      remove => RemoveHandler(BubblingSelectionChangedEvent, value);
    }

    public CustomComboBox()
    {
      InitializeComponent();
    }

  }

}
