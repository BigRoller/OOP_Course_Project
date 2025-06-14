using FashionHub.Models;
using FashionHub.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionHub.Services
{
  public static class CurrentUserService
  {
    public static event Action UserStatusChanged;

    private static int? _userId;
    public static int? UserId
    {
      get => _userId;
      private set
      {
        _userId = value;
        UserStatusChanged?.Invoke();
      }
    }

    public static readonly string DefaultAvatarPath = @"C:\Users\glora\Desktop\ООП курсовой\FashionHub\FashionHub\Resources\icons\avatar.png";
    public static bool IsAdmin { get; private set; } = false;
    public static bool IsManager { get; private set; } = false;
    public static string UserLogin { get; private set; } = "";

    public static void Login(User user)
    {
      if (user == null) throw new ArgumentNullException(nameof(user));

      UserLogin = user.Login;
      UserId = user.UserId;
      IsAdmin = user.UserRole.RoleId == (int)User.Role.ADMIN;
      if (IsAdmin == true)
      {
        IsManager = true;
      }
      else
      {
        IsManager = user.UserRole.RoleId == (int)User.Role.MANAGER;
      }
      UserStatusChanged?.Invoke();
      var navigation = new NavigationServiceViewModel(ServiceLocator.NavigationService);
      navigation.IsCatalogPage = false;
      ServiceLocator.NavigationService.NavigateTo(typeof(MainPage), null);
      ServiceLocator.NavigationService.ClearStacks();
    }
    public static void Logout()
    {
      UserId = null;
      IsAdmin = false;
      UserLogin = "";
      ServiceLocator.NavigationService.ClearStacks();
    }
  }
}
