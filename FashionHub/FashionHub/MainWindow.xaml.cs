using System.Windows;
using System.Windows.Navigation;
using FashionHub.Views;
using FashionHub.ViewModels;
using FashionHub.Services;
using System.Windows.Controls;
using System;

namespace FashionHub
{
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();

      var mainWindow = Application.Current.MainWindow as MainWindow;

      if (mainWindow != null)
      {
        var mainFrame = mainWindow.FindName("MainFrame") as Frame;
        ServiceLocator.Initialize(mainFrame);
      }
      else
      {
        throw new Exception("MainWindow или MainFrame не найдены.");
      }

      MainFrame.Navigate(new MainPage());
    }
  }
}
