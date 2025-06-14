using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace FashionHub.Components
{
  public partial class CustomListBox : UserControl
  {
    public ObservableCollection<string> ItemsSource
    {
      get => (ObservableCollection<string>)GetValue(ItemsSourceProperty);
      set => SetValue(ItemsSourceProperty, value);
    }

    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register("ItemsSource", typeof(ObservableCollection<string>), typeof(CustomListBox));

    public ObservableCollection<string> SelectedItems
    {
      get => (ObservableCollection<string>)GetValue(SelectedItemsProperty);
      set => SetValue(SelectedItemsProperty, value);
    }

    public static readonly DependencyProperty SelectedItemsProperty =
        DependencyProperty.Register(nameof(SelectedItems), typeof(ObservableCollection<string>), typeof(CustomListBox), new PropertyMetadata(new ObservableCollection<string>()));


    public string Placeholder
    {
      get => (string)GetValue(PlaceholderProperty);
      set => SetValue(PlaceholderProperty, value);
    }

    public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(CustomListBox), new PropertyMetadata(string.Empty));


    public string SelectedSummary => string.Join(", ", SelectedItems);

    public CustomListBox()
    {
      InitializeComponent();
    }

    private void TogglePopup(object sender, MouseButtonEventArgs e)
    {
      ListPopup.IsOpen = !ListPopup.IsOpen;
    }
  }

}