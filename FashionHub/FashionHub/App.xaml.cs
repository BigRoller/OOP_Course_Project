using FashionHub.Commands;
using FashionHub.Models;
using FashionHub.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Navigation;
using FashionHub.Views;
using FashionHub.Services;
using System.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using NavigationService = FashionHub.Services.NavigationService;

namespace FashionHub
{
  /// <summary>
  /// Логика взаимодействия для App.xaml
  /// </summary>
  public partial class App : Application
  {

    public static string HashPassword(string password)
    {
      if (password is null)
      {
        return null;
      }
      using (var sha256 = System.Security.Cryptography.SHA256.Create())
      {
        var bytes = System.Text.Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
      }
    }


    public static DateTime GetRandomDate()
    {
      Random random = new Random();

      DateTime startDate = new DateTime(2025, 5, 30);
      DateTime endDate = new DateTime(2025, 6, 10);

      int range = (endDate - startDate).Days;

      DateTime randomDate = startDate.AddDays(random.Next(range + 1));
      return randomDate;
    }

    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);
      var services = new ServiceCollection();
      services.AddSingleton<DataBaseContext>();
      services.AddScoped<IClothingRepository, ClothingRepository>();
      services.AddScoped<IUnitOfWork, UnitOfWork>();
      ServiceLocator.ConfigureServices(services);
    }
  }


  public static class MyCommands
  {
    public static readonly RoutedUICommand SaveCommand =
        new RoutedUICommand("Save", "Save", typeof(MyCommands));
  }




  public abstract class BaseViewModel : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    internal readonly IClothingRepository _repository;

    public BaseViewModel(IClothingRepository repository)
    {
      _repository = repository;
    }

    public BaseViewModel()
    {
    }


    public void SaveProduct(ClothingItem product)
    {
      _repository.AddOrUpdateClothingItemAsync(product);
    }

    virtual protected void OnPropertyChanged(string propertyName)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }






  public class NavigationServiceViewModel : BaseViewModel
  {
    protected readonly INavigationService _navigationService;

    static private bool _isCatalogPage = false;
    public bool IsCatalogPage
    {
      get => _isCatalogPage;
      set
      {
        if (_isCatalogPage != value)
        {
          _isCatalogPage = value;
          OnPropertyChanged(nameof(IsCatalogPage));
        }
      }
    }

    ////static private string _userLoginText = "Войти";
    //public string UserLoginText
    //{
    //  get => Application.Current.Resources["Login"]?.ToString() ?? "Войти";
    //  //set
    //  //{
    //  //  _userLoginText = value;
    //  //  OnPropertyChanged(nameof(UserLoginText));
    //  //}
    //}


    public string UserLogin => CurrentUserService.UserLogin;
    public bool IsAdmin => CurrentUserService.IsAdmin;
    public bool IsManager => CurrentUserService.IsManager;
    public bool IsUserLoggedIn => CurrentUserService.UserId.HasValue;

    public ICommand NavigateCommand { get; }
    public ICommand GoBackCommand { get; }
    public ICommand GoForwardCommand { get; }

    public NavigationServiceViewModel(IClothingRepository repository, INavigationService navigationService) : base(repository)
    {

      GoBackCommand = new RelayCommand(GoBack);
      GoForwardCommand = new RelayCommand(GoForward);

      //CurrentUserService.UserStatusChanged += UpdateLoginText;
      _navigationService = navigationService;
      CurrentUserService.UserStatusChanged += OnUserStatusChanged;
      NavigateCommand = new RelayCommand(Navigate);
    }

    public NavigationServiceViewModel(INavigationService navigationService) : base()
    {

      GoBackCommand = new RelayCommand(GoBack);
      GoForwardCommand = new RelayCommand(GoForward);

      //CurrentUserService.UserStatusChanged += UpdateLoginText;
      _navigationService = navigationService;
      CurrentUserService.UserStatusChanged += OnUserStatusChanged;
      NavigateCommand = new RelayCommand(Navigate);
    }

    public NavigationServiceViewModel() : base() { } 

    protected virtual void Navigate(object parameter)
    {
      if (parameter is Type pageType)
      {
        IsCatalogPage = pageType == typeof(CatalogPage);
        _navigationService.NavigateTo(pageType, null);
      }
    }

    private void OnUserStatusChanged()
    {

      OnPropertyChanged(nameof(IsUserLoggedIn));
      OnPropertyChanged(nameof(UserLogin));
      OnPropertyChanged(nameof(IsAdmin));
      OnPropertyChanged(nameof(IsManager));
    }


    //private void UpdateLoginText()
    //{
    //  UserLoginText = string.IsNullOrEmpty(CurrentUserService.UserLogin)
    //      ? "Войти"
    //      : CurrentUserService.UserLogin;
    //}

    public virtual void GoBack(object parameter)
    {
      if (_navigationService.CanGoBack)
      {
        ServiceLocator.NavigationService.GoBack();
      }
    }

    public virtual void GoForward(object parameter)
    {
      if (_navigationService.CanGoForward)
      {
        ServiceLocator.NavigationService.GoForward();
      }
    }

    //~NavigationServiceViewModel()
    //{
    //  CurrentUserService.UserStatusChanged -= UpdateLoginText;
    //}


  }



  public abstract class RecomendationViewModel : NavigationServiceViewModel
  {
    private ObservableCollection<ClothingItem> _recommendationsItems;
    public ObservableCollection<ClothingItem> RecommendationsItems
    {
      get => _recommendationsItems;
      set
      {
        _recommendationsItems = value;
        OnPropertyChanged(nameof(RecommendationsItems));
      }
    }

    private ObservableCollection<ClothingItem> _favoritesItems;
    public ObservableCollection<ClothingItem> FavoritesItems
    {
      get => _favoritesItems;
      set
      {
        _favoritesItems = value;
        OnPropertyChanged(nameof(FavoritesItems));
      }
    }


    public RecomendationViewModel(IClothingRepository repository, INavigationService navigationService) : base(repository, navigationService)
    {
      FavoritesItems = new ObservableCollection<ClothingItem>();
      RecommendationsItems = new ObservableCollection<ClothingItem>();

      LoadData();
      CurrentUserService.UserStatusChanged += LoadData;
    }

    public RecomendationViewModel() : base() { }


    private void LoadData()
    {

      using (var db = new DataBaseContext())
      {

        var favoriteIds = db.UserFavorites
            .Where(uf => uf.UserId == CurrentUserService.UserId.Value)
            .Select(uf => uf.ProductId)
            .ToList();

        var favorites = db.Products
            .Include(p => p.Comments)
            .Where(item => favoriteIds.Contains(item.ProductId))
            .ToList();

        FavoritesItems = new ObservableCollection<ClothingItem>(favorites);

        if (favorites.Any())
        {

          var favoriteCategories = favorites
              .GroupBy(f => f.Category)
              .OrderByDescending(g => g.Count())
              .Select(g => g.Key)
              .Take(2)
              .ToList();

          var recommendedItems = db.Products
              .Include(p => p.Comments)
              .Where(p => favoriteCategories.Contains(p.Category) &&
                         !favoriteIds.Contains(p.ProductId))
              .Take(5)
              .ToList();

          RecommendationsItems = new ObservableCollection<ClothingItem>(recommendedItems);
        }
        else
        {
          RecommendationsItems = new ObservableCollection<ClothingItem>(
              db.Products
                  .Include(p => p.Comments)
                  .Take(4)
                  .ToList());
        }
      }
    }

  }



  public abstract class BaseWindowViewModel : BaseViewModel
  {
    public ICommand CloseWindowCommand { get; }
    public ICommand DragMoveCommand { get; }

    public BaseWindowViewModel() : base()
    {
      CloseWindowCommand = new RelayCommand(CloseWindow);
      DragMoveCommand = new RelayCommand(DragWindow);
    }


    private void DragWindow(object parameter)
    {
      Application.Current.Windows.OfType<Window>()
          .FirstOrDefault(w => w.IsActive)?.DragMove();
    }

    private void CloseWindow(object parameter)
    {
      Application.Current.Windows.OfType<Window>()
          .FirstOrDefault(w => w.IsActive)?.Close();
    }


  }

public static class LocalizationManager
  {
    private static ResourceDictionary _currentDictionary;

    public static void SetLanguage(string languageCode)
    {
      var dict = new ResourceDictionary
      {
        Source = new Uri($"../Resources/Resources.{languageCode}.xaml", UriKind.Relative)
      };

      if (_currentDictionary != null)
        Application.Current.Resources.MergedDictionaries.Remove(_currentDictionary);

      Application.Current.Resources.MergedDictionaries.Add(dict);
      _currentDictionary = dict;
    }
  }


  public static class ThemeManager
  {
    private static ResourceDictionary _currentTheme;

    public static void SetTheme(string theme)
    {
      var dict = new ResourceDictionary
      {
        Source = new Uri($"../Resources/Theme.{theme}.xaml", UriKind.Relative)
      };

      if (_currentTheme != null)
        Application.Current.Resources.MergedDictionaries.Remove(_currentTheme);

      Application.Current.Resources.MergedDictionaries.Add(dict);
      _currentTheme = dict;
    }
  }

  public interface IUnitOfWork : IDisposable
  {
    IClothingRepository ClothingRepository { get; }
    void Commit();
    void Rollback();
  }

  public class UnitOfWork : IUnitOfWork
  {
    private readonly DataBaseContext _context;
    public IClothingRepository ClothingRepository { get; }

    public UnitOfWork(DataBaseContext context, IClothingRepository clothingRepository)
    {
      _context = context;
      ClothingRepository = clothingRepository;
    }

    public void Commit()
    {
      _context.SaveChanges(); // Сохранение всех изменений
    }

    public void Rollback()
    {
      foreach (var entry in _context.ChangeTracker.Entries())
      {
        entry.State = EntityState.Unchanged; // Отменяем все изменения
      }
    }

    public void Dispose()
    {
      _context.Dispose(); // Освобождаем ресурсы
    }
  }




}
