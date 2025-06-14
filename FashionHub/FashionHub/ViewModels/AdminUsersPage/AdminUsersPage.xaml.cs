using FashionHub.Commands;
using FashionHub.Models;
using FashionHub.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using FashionHub.ViewModels;
using FashionHub.Views;
using NavigationService = FashionHub.Services.NavigationService;

namespace FashionHub.Views
{
  /// <summary>
  /// Логика взаимодействия для AdminUsersPage.xaml
  /// </summary>
  public partial class AdminUsersPage : Page
  {
    public AdminUsersPage()
    {
      InitializeComponent();
      LoadData();
    }

    private void LoadData()
    {
      var navigationService = ServiceLocator.NavigationService;
      DataContext = new AdminUsersPageViewModel(navigationService);
    }
  }
}


namespace FashionHub.ViewModels
{
  public class AdminUsersPageViewModel : NavigationServiceViewModel
  {
    public ObservableCollection<User> Users { get; set; }
    public User SelectedUser { get; set; }

    public ICommand AddUserCommand { get; }
    public ICommand EditUserCommand { get; }
    public ICommand DeleteUserCommand { get; }

    public AdminUsersPageViewModel(INavigationService navigationService) : base(navigationService)
    {
      LoadUsers();

      AddUserCommand = new RelayCommand(AddUser);
      EditUserCommand = new RelayCommand(EditUser);
      DeleteUserCommand = new RelayCommand(DeleteUser);
    }

    public AdminUsersPageViewModel() : base() { }

    private void LoadUsers()
    {
      using (var db = new DataBaseContext())
      {
        Users = new ObservableCollection<User>(db.Users.ToList());
      }
      OnPropertyChanged(nameof(Users));
    }

    private void AddUser(object parameter)
    {
      _navigationService.NavigateTo(typeof(AddingUserPage), null);
    }

    private void EditUser(object parameter)
    {
      if (parameter is User user)
      {
        _navigationService.NavigateTo(typeof(AddingUserPage), user);
      }
    }

    private void DeleteUser(object parameter)
    {
      if (parameter is User user)
      {
        var result = CustomMessageBox.Show("Подтверждение", "Вы уверены, что хотите удалить этотого пользователя?", true);
        if (result == true)
        {
          using (var db = new DataBaseContext())
          {
            var toDelete = db.Users.Find(user.UserId);
            if (toDelete != null)
            {
              db.Users.Remove(toDelete);
              db.SaveChanges();
            }
          }

          Users.Remove(user);
        }
      }
    }

  }

}
