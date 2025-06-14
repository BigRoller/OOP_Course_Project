using FashionHub.Commands;
using FashionHub.Components;
using FashionHub.Models;
using FashionHub.ViewModels;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FashionHub.Services;
using FashionHub.Views;
using NavigationService = FashionHub.Services.NavigationService;

namespace FashionHub.Components
{
  /// <summary>
  /// Логика взаимодействия для UserControl1.xaml
  public partial class NavigationBar : UserControl
  {
    public NavigationBar()
    {
      InitializeComponent();
      LoadData();
    }

    private void LoadData()
    {
      var repository = ServiceLocator.GetService<IClothingRepository>(); // Получаем репозиторий
      var navigationService = ServiceLocator.NavigationService;
      DataContext = new NavigationBarViewModel(repository, navigationService);
    }

    public void OpenLoginWindow(object sender, RoutedEventArgs e)
    {
      var loginWindow = new LoginWindow();
      loginWindow.Show();
    }

    public void OpenSettingsWindow(object sender, RoutedEventArgs e)
    {
      var settingsWindow = new SettingsWindow();
      settingsWindow.Show();
    }
  }

}


namespace FashionHub.ViewModels
{
  public class NavigationBarViewModel : NavigationServiceViewModel
  {
    private readonly IClothingRepository _repository;

    private ObservableCollection<ClothingItem> _clothingItems;
    public ObservableCollection<ClothingItem> ClothingItems
    {
      get => _clothingItems;
      set
      {
        _clothingItems = value;
        OnPropertyChanged(nameof(ClothingItems));
      }
    }

    private Visibility _searchIconVisibility = Visibility.Visible;
    private Visibility _searchBoxVisibility = Visibility.Collapsed;

    public Visibility SearchIconVisibility
    {
      get => _searchIconVisibility;
      set
      {
        _searchIconVisibility = value;
        OnPropertyChanged(nameof(SearchIconVisibility));
      }
    }

    public Visibility SearchBoxVisibility
    {
      get => _searchBoxVisibility;
      set
      {
        _searchBoxVisibility = value;
        OnPropertyChanged(nameof(SearchBoxVisibility));
      }
    }

    private string _searchText;
    public string SearchText
    {
      get => _searchText;
      set
      {
        _searchText = value;
        OnPropertyChanged(nameof(SearchBoxVisibility));
      }
    }

    public ICommand OpenSearchBlockCommand { get; }
    public ICommand SearchCommand { get; }

    public NavigationBarViewModel(IClothingRepository repository, INavigationService navigationService)
        : base(repository, navigationService)
    {
      _repository = repository;
      SearchCommand = new RelayCommand(async param => await PerformSearchAsync(param));
      OpenSearchBlockCommand = new RelayCommand(ToggleSearchBox);
    }

    public NavigationBarViewModel(INavigationService navigationService)
    : base(navigationService)
    {
      SearchCommand = new RelayCommand(async param => await PerformSearchAsync(param));
      OpenSearchBlockCommand = new RelayCommand(ToggleSearchBox);
    }

    public NavigationBarViewModel() : base() { }

    protected virtual void ToggleSearchBox(object parameter)
    {
      SearchIconVisibility = Visibility.Collapsed;
      SearchBoxVisibility = Visibility.Visible;
    }

    public async Task PerformSearchAsync(object parameter)
    {
      string query = parameter as string;

      var allItems = await _repository.GetClothingItemsAsync();

      if (string.IsNullOrWhiteSpace(query))
      {
        ClothingItems = new ObservableCollection<ClothingItem>(allItems);
        return;
      }

      var results = allItems
        .Where(item => item.ShortName.Contains(query)
                    || item.FullName.Contains(query))
        .ToList();

      ClothingItems = new ObservableCollection<ClothingItem>(results);
    }


    public override void GoBack(object parameter)
    {
      if (_navigationService.CanGoBack)
      {
        _navigationService.GoBack();
        IsCatalogPage = false;
      }
    }

    public override void GoForward(object parameter)
    {
      if (_navigationService.CanGoForward)
      {
        _navigationService.GoForward();
        IsCatalogPage = false;
      }
    }
  }


}


