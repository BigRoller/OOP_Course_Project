using FashionHub.Commands;
using FashionHub.Models;
using FashionHub.Services;
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
using System.Data.Entity;
using System.IO;
using NavigationService = FashionHub.Services.NavigationService;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using FashionHub.ViewModels;
using FashionHub.Views;
using System.Runtime.Remoting.Contexts;

namespace FashionHub.Views
{
  /// <summary>
  /// Логика взаимодействия для ProfilePage.xaml
  /// </summary>
  public partial class ProfileInformationPage : Page
  {
    public ProfileInformationPage()
    {
      InitializeComponent();
      LoadData();
    }

    private void LoadData()
    {
      var repository = ServiceLocator.GetService<IClothingRepository>(); // Получаем репозиторий
      var navigationService = ServiceLocator.NavigationService;
      DataContext = new ProfileInformationPageViewModel(repository, navigationService);
    }

    private void Image_DragEnter(object sender, DragEventArgs e)
    {
      if (e.Data.GetDataPresent(DataFormats.FileDrop))
      {
        e.Effects = DragDropEffects.Copy;
      }
      else
      {
        e.Effects = DragDropEffects.None;
      }
    }

    private void Image_Drop(object sender, DragEventArgs e)
    {
      if (e.Data.GetDataPresent(DataFormats.FileDrop))
      {
        var files = (string[])e.Data.GetData(DataFormats.FileDrop);
        if (files.Length > 0 && File.Exists(files[0]))
        {
          var vm = DataContext as ProfileInformationPageViewModel;
          vm?.ChangeAvatarFromPath(files[0]);
        }
      }
    }
  }

}

namespace FashionHub.ViewModels
{
  public class ProfileInformationPageViewModel : RecomendationViewModel
  {
    private string _firstName;
    private string _lastName;
    private string _middleName;
    private string _email;
    private string _phone;
    private DateTime? _birthDate;
    private bool? _gender;
    private string _avatarPath;

    public string FirstName
    {
      get => _firstName;
      set { _firstName = value; OnPropertyChanged(nameof(FirstName)); }
    }

    public string LastName
    {
      get => _lastName;
      set { _lastName = value; OnPropertyChanged(nameof(LastName)); }
    }

    public string MiddleName
    {
      get => _middleName;
      set { _middleName = value; OnPropertyChanged(nameof(MiddleName)); }
    }

    public string Email
    {
      get => _email;
      set { _email = value; OnPropertyChanged(nameof(Email)); }
    }

    public string Phone
    {
      get => _phone;
      set { _phone = value; OnPropertyChanged(nameof(Phone)); }
    }

    public DateTime? BirthDate
    {
      get => _birthDate;
      set { _birthDate = value; OnPropertyChanged(nameof(BirthDate)); }
    }

    public bool? Gender
    {
      get => _gender;
      set { _gender = value; OnPropertyChanged(nameof(Gender)); }
    }

    public string AvatarPath
    {
      get => _avatarPath ?? CurrentUserService.DefaultAvatarPath;
      set { _avatarPath = value; OnPropertyChanged(nameof(AvatarPath)); }
    }

    public ICommand LogoutCommand { get; }
    public ICommand ChangeAvatarCommand { get; }
    public ICommand SaveChangesCommand { get; }
    public ICommand DeleteAvatarCommand { get; }
    

    public Dictionary<string, string> ProfileErrors { get; } = new Dictionary<string, string>();

    public ProfileInformationPageViewModel(IClothingRepository repository, INavigationService navigationService)
        : base(repository, navigationService)
    {
      LoadCurrentUser();
      LogoutCommand = new RelayCommand(Logout);
      ChangeAvatarCommand = new RelayCommand(ChangeAvatarFromDialog);
      SaveChangesCommand = new RelayCommand(SaveChanges);
      DeleteAvatarCommand = new RelayCommand(DeleteAvatar);
    }

    public ProfileInformationPageViewModel() : base() { }

    private void LoadCurrentUser()
    {
      using (var context = new DataBaseContext())
      {

        var profile = context.UserProfiles.Include(p => p.User).FirstOrDefault(p => p.UserId == CurrentUserService.UserId);
        if (profile != null)
        {
          FirstName = profile.FirstName;
          LastName = profile.LastName;
          MiddleName = profile.MiddleName;
          Phone = profile.PhoneNumber;
          Gender = profile.Gender;
          BirthDate = profile.BirthDate;
          AvatarPath = profile.AvatarPath ?? CurrentUserService.DefaultAvatarPath;
          Email = profile.User.Email;
        }
        else
        {
          User user = context.Users.FirstOrDefault(u => u.UserId == CurrentUserService.UserId);
          user.Profile = new UserProfile();
          user.Profile.AvatarPath = CurrentUserService.DefaultAvatarPath;
          Email = user.Email;
        }
        context.SaveChanges();
      }
    }

    public void ChangeAvatarFromDialog(object obj)
    {
      var dialog = new Microsoft.Win32.OpenFileDialog
      {
        Filter = "Image files (*.png;*.jpg)|*.png;*.jpg",
        Title = "Выберите изображение"
      };

      if (dialog.ShowDialog() == true)
      {
        ChangeAvatarFromPath(dialog.FileName);
      }
    }

    public void ChangeAvatarFromPath(string path)
    {
      AvatarPath = path;

      using (var context = new DataBaseContext())
      {
        var profile = context.UserProfiles.FirstOrDefault(p => p.UserId == CurrentUserService.UserId);
        if (profile != null)
        {
          profile.AvatarPath = path;
          context.SaveChanges();
        }
      }
    }

    private void SaveChanges(object parameter)
    {
      try
      {
        ClearValidationErrors();

        var profileToValidate = new UserProfile
        {
          FirstName = FirstName,
          LastName = LastName,
          MiddleName = MiddleName,
          PhoneNumber = Phone,
          BirthDate = BirthDate,
          Gender = Gender,
          AvatarPath = AvatarPath,
          User = new User { Email = Email }
        };

        var validationContext = new ValidationContext(profileToValidate);
        var validationResults = new List<ValidationResult>();

        if (!Validator.TryValidateObject(profileToValidate, validationContext, validationResults, true))
        {
          SetValidationErrors(validationResults);
          return;
        }

        var emailResults = new List<ValidationResult>();
        var emailContext = new ValidationContext(profileToValidate.User)
        {
          MemberName = nameof(User.Email)
        };

        if (!Validator.TryValidateProperty(profileToValidate.User.Email, emailContext, emailResults))
        {
          SetValidationErrors(emailResults);
          return;
        }

        using (var context = new DataBaseContext())
        {
          var profile = context.UserProfiles
              .Include(p => p.User)
              .FirstOrDefault(p => p.UserId == CurrentUserService.UserId);

          if (profile != null)
          {
            profile.FirstName = FirstName;
            profile.LastName = LastName;
            profile.MiddleName = MiddleName;
            profile.PhoneNumber = Phone;
            profile.BirthDate = BirthDate;
            profile.Gender = Gender;
            profile.AvatarPath = AvatarPath;

            if (profile.User != null)
            {
              profile.User.Email = Email;
            }

            context.SaveChanges();
            CustomMessageBox.Show("Успех", "Изменения успешно сохранены.");
          }
          else
          {
            CustomMessageBox.Show("Ошибка", "Профиль не найден.");
          }
        }
      }
      catch (DbEntityValidationException ex)
      {
        foreach (var validationErrors in ex.EntityValidationErrors)
        {
          foreach (var validationError in validationErrors.ValidationErrors)
          {
            Console.WriteLine($"Свойство: {validationError.PropertyName}, Ошибка: {validationError.ErrorMessage}");
          }
        }

        throw;
      }
      catch (Exception ex)
      {
        CustomMessageBox.Show("Ошибка", $"Произошла ошибка при сохранении изменений: {ex.Message}");
      }
    }

    private void ClearValidationErrors()
    {
      ProfileErrors.Clear();
      OnPropertyChanged(nameof(ProfileErrors));
    }

    private void SetValidationErrors(List<ValidationResult> results)
    {
      foreach (var error in results)
      {
        foreach (var member in error.MemberNames)
        {
          ProfileErrors[member] = error.ErrorMessage;
        }
      }
      OnPropertyChanged(nameof(ProfileErrors));
    }

    private void DeleteAvatar(object parameter)
    {
      using (var context = new DataBaseContext())
      {
        var profile = context.UserProfiles.Include(p => p.User).FirstOrDefault(p => p.UserId == CurrentUserService.UserId);
        profile.AvatarPath = null;
        AvatarPath = null;
        OnPropertyChanged(nameof(AvatarPath));
        context.SaveChanges();
      }
    }




    private void Logout(object parameter)
    {
      bool? flag = CustomMessageBox.Show("Подтверждение", "Вы уверены, что хотите выйти с аккаунта?", true);
      if (flag == true)
      {
        CurrentUserService.Logout();
        _navigationService.NavigateTo(typeof(MainPage), null);
      }

    } 
  }
}
