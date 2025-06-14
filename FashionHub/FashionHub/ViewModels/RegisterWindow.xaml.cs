using FashionHub.Commands;
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
using FashionHub.Views;
using FashionHub.ViewModels;

namespace FashionHub.Views
{
  /// <summary>
  /// Логика взаимодействия для Login.xaml
  /// </summary>
  public partial class RegisterWindow : Window
  {

    public RegisterWindow()
    {
      InitializeComponent();
    }

    private void RegistrationPasswordBoxInput_PasswordChanged(object sender, RoutedEventArgs e)
    {
      if (DataContext is RegisterWindowViewModel viewModel)
      {
        viewModel.Password = ((PasswordBox)sender).Password;
      }
    }

    private void RepeatRegistrationPasswordBoxInput_PasswordChanged(object sender, RoutedEventArgs e)
    {
      if (DataContext is RegisterWindowViewModel viewModel)
      {
        viewModel.RepeatPassword = ((PasswordBox)sender).Password;
      }
    }
  }
  }

namespace FashionHub.ViewModels
{
  public partial class RegisterWindowViewModel : BaseWindowViewModel
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

    private string email;

    public string Email
    {
      get => email;
      set
      {
        email = value;
        OnPropertyChanged(nameof(Email));
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

    private string repeatPassword;

    public string RepeatPassword
    {
      get => repeatPassword;
      set
      {
        repeatPassword = value;
        OnPropertyChanged(nameof(RepeatPassword));
      }
    }

    private string _loginError;
    private string _emailError;
    private string _passwordError;
    private string _repeatPasswordError;

    public string LoginError
    {
      get => _loginError;
      set
      {
        _loginError = value;
        OnPropertyChanged(nameof(LoginError));
      }
    }

    public string EmailError
    {
      get => _emailError;
      set { 
        _emailError = value;
        OnPropertyChanged(nameof(EmailError)); 
      }
    }

    public string PasswordError
    {
      get => _passwordError;
      set {
        _passwordError = value; 
        OnPropertyChanged(nameof(PasswordError)); 
      }
    }

    public string RepeatPasswordError
    {
      get => _repeatPasswordError;
      set {
        _repeatPasswordError = value; 
        OnPropertyChanged(nameof(RepeatPasswordError)); 
      }
    }


    public ICommand OpenLoginCommand { get; }
    public ICommand RegisterCommand { get; }

    public RegisterWindowViewModel()
    {
      OpenLoginCommand = new RelayCommand(OpenLogin);
      RegisterCommand = new RelayCommand(Register);
    }


    private void OpenLogin(object parameter)
    {
      var LoginWindow = new LoginWindow();
      LoginWindow.Show();

      Application.Current.Windows.OfType<RegisterWindow>().FirstOrDefault()?.Close();
    }

    private void Register(object parameter)
    {
      LoginError = string.Empty;
      EmailError = string.Empty;
      PasswordError = string.Empty;
      RepeatPasswordError = string.Empty;

      if (Password != RepeatPassword)
      {
        RepeatPasswordError = "Пароли не совпадают";
      }

      try
      {
        var validationUser = new User
        {
          Login = LoginName,
          Email = Email,
          PasswordHash = App.HashPassword(Password) ?? "",
          RoleId = (int)User.Role.USER,
          Profile = new UserProfile()
        };

        var validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
        var validationContext = new ValidationContext(validationUser);

        if (!Validator.TryValidateObject(validationUser, validationContext, validationResults, true))
        {
          foreach (var error in validationResults)
          {
            if (error.MemberNames.Contains("Login"))
              LoginError = error.ErrorMessage;

            if (error.MemberNames.Contains("Email"))
              EmailError = error.ErrorMessage;

            if (error.MemberNames.Contains("PasswordHash"))
              PasswordError = error.ErrorMessage;
          }
          return;
        }

        using (var context = new DataBaseContext())
        {
          var existingUser = context.Users.FirstOrDefault(u => u.Login == LoginName);
          if (existingUser != null)
          {
            LoginError = "Пользователь с таким логином уже существует";
            return;
          }

          context.Users.Add(validationUser);
          context.SaveChanges();

          CustomMessageBox.Show("Регистрация", $"Регистрация успешна для {LoginName}");

          var loginWindow = new LoginWindow();
          loginWindow.Show();
          Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive)?.Close();
        }
      }
      catch (Exception ex)
      {
        CustomMessageBox.Show("Ошибка регистрации", $"Произошла ошибка: {ex.Message}");
      }
    }


  }

}
