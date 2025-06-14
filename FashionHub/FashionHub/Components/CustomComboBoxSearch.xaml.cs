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
  public partial class CustomComboBoxSearch : UserControl
  {
    private bool _isInternalUpdate = false;

    public static readonly DependencyProperty SearchTextProperty =
    DependencyProperty.Register("SearchText", typeof(string), typeof(CustomComboBoxSearch), new PropertyMetadata(string.Empty));

    public string SearchText
    {
      get => (string)GetValue(SearchTextProperty);
      set => SetValue(SearchTextProperty, value);
    }

    public static readonly DependencyProperty SelectedValuePathProperty =
    DependencyProperty.Register("SelectedValuePath", typeof(string), typeof(CustomComboBoxSearch));

    public static readonly DependencyProperty SelectedValueProperty =
        DependencyProperty.Register("SelectedValue", typeof(object), typeof(CustomComboBoxSearch), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public static readonly DependencyProperty DisplayMemberPathProperty =
        DependencyProperty.Register("DisplayMemberPath", typeof(string), typeof(CustomComboBoxSearch));

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
    DependencyProperty.Register("ItemsSource", typeof(IEnumerable<object>), typeof(CustomComboBoxSearch));

    public static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register("SelectedItem", typeof(object), typeof(CustomComboBoxSearch), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.Register("Placeholder", typeof(string), typeof(CustomComboBoxSearch), new PropertyMetadata("Выбрать значение"));

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

    public CustomComboBoxSearch()
    {
      InitializeComponent();
    }


    private void MyComboBox_Loaded(object sender, RoutedEventArgs e)
    {
      if (MyComboBox.Template.FindName("PART_EditableTextBox", MyComboBox) is TextBox textBox1)
      { 

        MyComboBox.DropDownOpened += (s, args) =>
        {
          textBox1.SelectionLength = 0;
          textBox1.CaretIndex = textBox1.Text.Length;
        };
      }

      var comboBox = sender as ComboBox;
      if (comboBox?.Template != null)
      {
        var textBox = comboBox.Template.FindName("PART_EditableTextBox", comboBox) as TextBox;
        if (textBox != null)
        {
          textBox.TextChanged += EditableTextBox_TextChanged;
        }
      }
    }

    private void EditableTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
      if (_isInternalUpdate) return;

      var textBox = sender as TextBox;
      if (textBox != null)
      {
        SearchText = textBox.Text;

        if (string.IsNullOrWhiteSpace(textBox.Text))
        {
          MyComboBox.IsDropDownOpen = false;
        }
        else
        {
          MyComboBox.IsDropDownOpen = true;
        }
      }
    }

    private void MyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      string selectedText = null;

      if (MyComboBox.SelectedItem != null)
      {
        if (!string.IsNullOrEmpty(DisplayMemberPath))
        {
          selectedText = MyComboBox.SelectedItem.GetType()
              .GetProperty(DisplayMemberPath)
              ?.GetValue(MyComboBox.SelectedItem)
              ?.ToString();
        }
        else
        {
          selectedText = MyComboBox.SelectedItem.ToString();
        }

        Dispatcher.BeginInvoke(new Action(() =>
        {
          if (MyComboBox.Template.FindName("PART_EditableTextBox", MyComboBox) is TextBox tb)
          {
            _isInternalUpdate = true;

            tb.Text = selectedText ?? string.Empty;
            tb.SelectionLength = 0;
            tb.CaretIndex = tb.Text.Length;

            _isInternalUpdate = false;
          }

          SearchText = selectedText ?? string.Empty;

        }), System.Windows.Threading.DispatcherPriority.ApplicationIdle);
      }
    }





  }
}
