using FashionHub.Commands;
using FashionHub.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
  /// Логика взаимодействия для Login.xaml
  /// </summary>
  public partial class SettingsWindow : Window
  {
    public SettingsWindow()
    {
      InitializeComponent();
    }
  }
}

namespace FashionHub.ViewModels
{ 
  public partial class SettingsWindowViewModel : BaseWindowViewModel
  {
    public ICommand OpenLoginCommand { get; }

    public ICommand SetRussianCommand { get; }
    public ICommand SetEnglishCommand { get; }

    public ICommand SetLightThemeCommand { get; }
    public ICommand SetDarkThemeCommand { get; }

    public SettingsWindowViewModel() : base()
    {
      OpenLoginCommand = new RelayCommand(OpenLogin);

      SetRussianCommand = new RelayCommand(_ => LocalizationManager.SetLanguage("ru"));
      SetEnglishCommand = new RelayCommand(_ => LocalizationManager.SetLanguage("en"));

      SetLightThemeCommand = new RelayCommand(_ => ThemeManager.SetTheme("Light"));
      SetDarkThemeCommand = new RelayCommand(_ => ThemeManager.SetTheme("Dark"));

      ThemeManager.SetTheme("Dark");
    }

    private void OpenLogin(object parameter)
    {
      var LoginWindow = new LoginWindow();
      LoginWindow.Show();

      Application.Current.Windows.OfType<RegisterWindow>().FirstOrDefault()?.Close();
    }
  }

}
