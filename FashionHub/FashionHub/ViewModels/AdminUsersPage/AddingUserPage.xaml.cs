using FashionHub.Commands;
using FashionHub.Models;
using FashionHub.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data.Entity;
using FashionHub.Views;
using FashionHub.ViewModels;
using NavigationService = FashionHub.Services.NavigationService;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;
using static FashionHub.Models.User;
using System.Collections.ObjectModel;

namespace FashionHub.Views
{
  /// <summary>
  /// Логика взаимодействия для AddingUserPage.xaml
  /// </summary>
  public partial class AddingUserPage : Page
  {
    public AddingUserPage(User user = null)
    {
      InitializeComponent();
      LoadData(user);
    }

    public AddingUserPage()
    {
      InitializeComponent();
      LoadData(null);
    }

    private void LoadData(User user)
    {
      var navigationService = ServiceLocator.NavigationService;

      if (user != null)
      {
        DataContext = new AddingUserPageViewModel(navigationService, user);
      }
      else
      {
        DataContext = new AddingUserPageViewModel(navigationService);
      }
    }

    private void AvatarBorder_DragOver(object sender, DragEventArgs e)
    {
      if (e.Data.GetDataPresent(DataFormats.FileDrop))
      {
        string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
        e.Effects = files.Any(f => IsImageFile(f)) ? DragDropEffects.Copy : DragDropEffects.None;
      }
      else
      {
        e.Effects = DragDropEffects.None;
      }

      e.Handled = true;
    }

    private void AvatarBorder_Drop(object sender, DragEventArgs e)
    {
      if (e.Data.GetDataPresent(DataFormats.FileDrop))
      {
        string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
        var imageFile = files.FirstOrDefault(IsImageFile);

        if (imageFile != null && DataContext is AddingUserPageViewModel vm)
        {
          vm.User.Profile.AvatarPath = imageFile;
        }
      }
    }


    private void OnPasswordChanged(object sender, RoutedEventArgs e)
    {
      if (DataContext is AddingUserPageViewModel viewModel)
      {
        viewModel.Password = ((PasswordBox)sender).Password;
      }
    }


    private bool IsImageFile(string path)
    {
      string extension = System.IO.Path.GetExtension(path).ToLower();
      return extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".bmp" || extension == ".gif";
    }
  }
}

namespace FashionHub.ViewModels
{
  public class AddingUserPageViewModel : NavigationBarViewModel
  {

    private PageStateEnum pageState;
    public PageStateEnum PageState
    {
      get => pageState;
      set
      {
        pageState = value;
        OnPropertyChanged(nameof(PageState));
      }
    }

    private User user;
    public User User
    {
      get => user;
      set
      {
        user = value;
        OnPropertyChanged(nameof(User));
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

    private string buttonText;
    public string ButtonText
    {
      get => buttonText;
      set
      {
        buttonText = value;
        OnPropertyChanged(nameof(ButtonText));
      }
    }

    private string titleText;
    public string TitleText
    {
      get => titleText;
      set
      {
        titleText = value;
        OnPropertyChanged(nameof(TitleText));
      }
    }

    

    public ObservableCollection<UserRole> Roles { get; set; } = new ObservableCollection<UserRole>();

    public Dictionary<string, string> UserErrors { get; } = new Dictionary<string, string>();

    public ICommand SaveCommand { get; }
    public ICommand AddAvatarCommand { get; }
    public ICommand RemoveAvatarCommand { get; }

    public AddingUserPageViewModel(INavigationService navigationService, User existingUser) : base(navigationService)
    {
      SaveCommand = new RelayCommand(SaveUser);
      AddAvatarCommand = new RelayCommand(AddAvatar);
      RemoveAvatarCommand = new RelayCommand(RemoveAvatar);

      ButtonText = "Обновить пользователя";
      TitleText = "Редактирование пользователя";
      pageState = PageStateEnum.EDITING;

      using (var dbContext = new DataBaseContext())
      {
        User = dbContext.Users.Include(u => u.Profile)
                              .FirstOrDefault(u => u.UserId == existingUser.UserId);

        if (User != null)
        {
          if (User.Profile == null)
          {
            var newProfile = new UserProfile
            {
              UserId = User.UserId,
            };

            User.Profile = newProfile;

            dbContext.UserProfiles.Add(newProfile);
          }

          dbContext.SaveChanges();
        }

        var rolesFromDb = dbContext.Roles.ToList();
        foreach (var role in rolesFromDb)
        {
          Roles.Add(role);
        }
      }

    }

    public AddingUserPageViewModel(INavigationService navigationService) : base(navigationService)
    {
      SaveCommand = new RelayCommand(SaveUser);
      AddAvatarCommand = new RelayCommand(AddAvatar);

      ButtonText = "Добавить пользователя";
      TitleText = "Добавление пользователя";
      User = new User { RoleId = (int)User.Role.USER };
      pageState = PageStateEnum.ADDITION;

      using (var dbContext = new DataBaseContext())
      {
        var rolesFromDb = dbContext.Roles.ToList();
        foreach (var role in rolesFromDb)
        {
          Roles.Add(role);
        }
      }
    }

    public AddingUserPageViewModel() : base() { }

    public enum PageStateEnum
    {
      EDITING,
      ADDITION
    }

    private void SaveUser(object parameter)
    {
      if (pageState == PageStateEnum.ADDITION)
      {
        AddUser();
      }
      else if (pageState == PageStateEnum.EDITING)
      {
        UpdateUser();
      }
    }

    private void AddUser()
    {
      try
      {
        ClearValidationErrors();

        var context = new ValidationContext(User);
        var validationResults = new List<ValidationResult>();

        User.PasswordHash = App.HashPassword(Password) ?? "";

        if (!Validator.TryValidateObject(User, context, validationResults, true))
        {
          SetValidationErrors(validationResults);
          return;
        }

        AddOrUpdateUser(user);

        CustomMessageBox.Show("Успех", "Пользователь успешно добавлен!");
        _navigationService.NavigateTo(typeof(AdminUsersPage), null);
      }
      catch (Exception ex)
      {
        CustomMessageBox.Show("Ошибка", $"Ошибка при добавлении пользователя: {ex.Message}");
      }
    }

    private void UpdateUser()
    {
      try
      {
        ClearValidationErrors();

        var context = new ValidationContext(User);
        var validationResults = new List<ValidationResult>();

        User.PasswordHash = App.HashPassword(Password) ?? "";

        if (!Validator.TryValidateObject(User, context, validationResults, true) && !(Password == null))
        {
          SetValidationErrors(validationResults);
          return;
        }

        validationResults = new List<ValidationResult>();

        if (User.Profile != null)
        {
          var profileContext = new ValidationContext(User.Profile);
          if (!Validator.TryValidateObject(User.Profile, profileContext, validationResults, true))
          {
            SetValidationErrors(validationResults);
            return;
          }
        }

        AddOrUpdateUser(user);

        CustomMessageBox.Show("Успех", "Данные пользователя обновлены!");
        _navigationService.NavigateTo(typeof(AdminUsersPage), null);
      }
      catch (Exception ex)
      {
        CustomMessageBox.Show("Ошибка", $"Ошибка при обновлении пользователя: {ex.Message}");
      }
    }

    private void AddOrUpdateUser(User user)
    {
      using (var dbContext = new DataBaseContext())
      {
        try
        {
          bool isLoginTaken = dbContext.Users.Any(u => u.Login == user.Login && u.UserId != user.UserId);
          if (isLoginTaken)
            throw new Exception("Пользователь с таким логином уже существует");

          if (user.UserId == 0)
          {
            dbContext.Users.Add(user);
          }
          else
          {
            var existingUser = dbContext.Users
                .Include(u => u.Profile)
                .FirstOrDefault(u => u.UserId == user.UserId);

            if (string.IsNullOrWhiteSpace(user.PasswordHash))
            {
              user.PasswordHash = existingUser.PasswordHash;
            }


            if (existingUser != null)
            {
              dbContext.Entry(existingUser).CurrentValues.SetValues(user);
              dbContext.Entry(existingUser.Profile).CurrentValues.SetValues(user.Profile);
            }
            else
            {
              throw new Exception("Пользователь не найден в базе данных");
            }
          }

          dbContext.SaveChanges();
        }
        catch (System.Data.Entity.Validation.DbEntityValidationException ex)
        {
          foreach (var validationErrors in ex.EntityValidationErrors)
          {
            var entity = validationErrors.Entry.Entity.GetType().Name;
            foreach (var validationError in validationErrors.ValidationErrors)
            {
              Console.WriteLine($"Entity: {entity}, Property: {validationError.PropertyName}, Error: {validationError.ErrorMessage}");
            }
          }
        }
        catch (DbUpdateException ex)
        {
          if (ex.InnerException?.InnerException is SqlException sqlEx)
          {
            if (sqlEx.Number == 2601 || sqlEx.Number == 2627)
            {
              throw new Exception("Нарушение уникальности — возможно, логин или email уже используются");
            }
          }
          throw;
        }
      }
    }



    private void AddAvatar(object parameter)
    {
      var openFileDialog = new OpenFileDialog
      {
        Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif",
        Title = "Выберите аватар пользователя"
      };

      if (openFileDialog.ShowDialog() == true)
      {
        User.Profile.AvatarPath = openFileDialog.FileName;
        OnPropertyChanged(nameof(User));
      }
    }

    private void RemoveAvatar(object parameter)
    {
      User.Profile.AvatarPath = null;
      OnPropertyChanged(nameof(User));
    }

    private void ClearValidationErrors()
    {
      UserErrors.Clear();
      OnPropertyChanged(nameof(UserErrors));
    }

    private void SetValidationErrors(List<ValidationResult> results)
    {
      foreach (var error in results)
      {
        foreach (var member in error.MemberNames)
        {
          UserErrors[member] = error.ErrorMessage;
        }
      }
      OnPropertyChanged(nameof(UserErrors));
    }
  }


}
