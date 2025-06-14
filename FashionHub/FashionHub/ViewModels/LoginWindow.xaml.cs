using FashionHub.Commands;
using FashionHub.Components;
using FashionHub.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
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
using FashionHub.Services;
using FashionHub.Views;
using System.Security;
using FashionHub.ViewModels;

namespace FashionHub.Views
{
  /// <summary>
  /// Логика взаимодействия для Login.xaml
  /// </summary>
  public partial class LoginWindow : Window
  {
    public LoginWindow()
    {
      InitializeComponent();
    }

    private void LoginPasswordBoxInput_PasswordChanged(object sender, RoutedEventArgs e)
    {
      if (DataContext is LoginWindowViewModel viewModel)
      {
        viewModel.Password = ((PasswordBox)sender).Password;
      }
    }
  }
}

namespace FashionHub.ViewModels
{ 
  public partial class LoginWindowViewModel : BaseWindowViewModel
  {
    private string loginName;

    public string LoginName
    {
      get => loginName;
      set
      {
        loginName = value;
        OnPropertyChanged(nameof(LoginName));
      }
    }

    private string password;

    public string Password
    {
      get => password;
      set
      {
        password = value;
        OnPropertyChanged(nameof(Password));
      }
    }

    private string _loginError;

    public string LoginError
    {
      get => _loginError;
      set { _loginError = value; 
        OnPropertyChanged(nameof(LoginError));
      }
    }

    private string _passwordError;

    public string PasswordError
    {
      get => _passwordError;
      set { _passwordError = value; 
        OnPropertyChanged(nameof(PasswordError));
      }
    }

    public ICommand OpenRegistrationCommand { get; }
    public ICommand LoginCommand { get; }

    public LoginWindowViewModel() : base()
    {
      OpenRegistrationCommand = new RelayCommand(OpenRegistration);
      LoginCommand = new RelayCommand(Login);
    }


    private void OpenRegistration(object parameter)
    {
      var registrationWindow = new RegisterWindow();
      registrationWindow.Show();

      Application.Current.Windows.OfType<LoginWindow>().FirstOrDefault()?.Close();
    }

    private void Login(object parameter)
    {
      try
      {
        LoginError = string.Empty;
        PasswordError = string.Empty;

        var validationUser = new User
        {
          Login = LoginName,
          PasswordHash = App.HashPassword(Password) ?? "",
        };

        var validationResults = validationUser.Validate(new ValidationContext(validationUser)).ToList();

        if (validationResults.Any())
        {
          foreach (var error in validationResults)
          {
            if (error.MemberNames.Contains("Login"))
              LoginError = error.ErrorMessage;

            if (error.MemberNames.Contains("PasswordHash"))
              PasswordError = error.ErrorMessage;
          }
          return;
        }

        using (var context = new DataBaseContext())
        {
          var user = context.Users
                          .Include(u => u.UserRole)
                          .FirstOrDefault(u => u.Login == LoginName);

          if (user != null && user.PasswordHash == validationUser.PasswordHash)
          {

            Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w is LoginWindow)?.Close();
            CustomMessageBox.Show("Вход в аккаунт", "Успешный вход!");

            CurrentUserService.Login(user);
          }
          else
          {
            CustomMessageBox.Show("Вход в аккаунт", "Неверный логин или пароль!");
          }
        }
      }
      catch (Exception)
      {
        CustomMessageBox.Show("Ошибка входа", "Не удалось войти в аккаунт");
      }
    }

    
  }

}
