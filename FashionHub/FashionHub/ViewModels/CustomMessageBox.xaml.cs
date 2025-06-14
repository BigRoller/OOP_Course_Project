using FashionHub.Commands;
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
using System.Windows.Shapes;
using FashionHub.Views;
using FashionHub.ViewModels;

namespace FashionHub.Views
{
  /// <summary>
  /// Логика взаимодействия для CustomMessageBox.xaml
  /// </summary>
  public partial class CustomMessageBox : Window
  {
    public CustomMessageBox()
    {
      InitializeComponent();
    }

    public static bool? Show(string title, string message, bool showCancelButton = false)
    {
      var vm = new CustomMessageBoxViewModel(title, message, showCancelButton);
      var window = new CustomMessageBox
      {
        DataContext = vm
      };

      window.ShowDialog();
      return vm.DialogResult;
    }

  }
}

namespace FashionHub.ViewModels
{
  public class CustomMessageBoxViewModel
  {
    public string Title { get; }
    public string Message { get; }
    public bool? DialogResult { get; private set; }
    public bool ShowCancelButton { get; }

    public ICommand CloseCommand { get; }
    public ICommand DragMoveCommand { get; }
    public ICommand CancelCommand { get; }


    public CustomMessageBoxViewModel(string title, string message, bool showCancelButton)
    {
      Title = title;
      Message = message;
      ShowCancelButton = showCancelButton;

      CloseCommand = new RelayCommand(CloseWindow);
      DragMoveCommand = new RelayCommand(DragWindow);
      CancelCommand = new RelayCommand(CancelWindow);
    }

    private void CloseWindow(object parameter)
    {
      DialogResult = true;
      Application.Current.Windows.OfType<Window>()
       .FirstOrDefault(w => w.IsActive)?.Close();
    }

    private void CancelWindow(object parameter)
    {
      DialogResult = false;
      CloseCurrentWindow();
    }

    private void CloseCurrentWindow()
    {
      Application.Current.Windows.OfType<Window>()
        .FirstOrDefault(w => w.IsActive)?.Close();
    }


    private void DragWindow(object parameter)
    {
      Application.Current.Windows.OfType<CustomMessageBox>()
        .FirstOrDefault(w => w.IsActive)?
        .DragMove();
    }
  }
}
