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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;
using FashionHub.Commands;
using FashionHub.ViewModels;
using FashionHub.Models;
using FashionHub.Services;
using NavigationService = FashionHub.Services.NavigationService;


namespace FashionHub.Views
{
  /// <summary>
  /// Логика взаимодействия для MainWindow.xaml
  /// </summary>
  public partial class MainPage : Page
  {
    public MainPage()
    {
      InitializeComponent();
      LoadData();
      Loaded += Page_Loaded;
    }

    private void LoadData()
    {
      var repository = ServiceLocator.GetService<IClothingRepository>();
      var navigationService = ServiceLocator.NavigationService;
      DataContext = new MainPageViewModel(repository, navigationService);
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
      var vm = (MainPageViewModel)DataContext;
      await vm.InitializeAsync();
    }

  }
}

namespace FashionHub.ViewModels
{
  public partial class MainPageViewModel : RecomendationViewModel
  {
    private ObservableCollection<ClothingItem> _popularItems;
    public ObservableCollection<ClothingItem> PopularItems
    {
      get => _popularItems;
      set
      {
        _popularItems = value;
        OnPropertyChanged(nameof(PopularItems));
      }
    }

    public MainPageViewModel(IClothingRepository repository, INavigationService navigationService)
        : base(repository, navigationService)
    {
    }

    public MainPageViewModel() : base() { }

    public async Task InitializeAsync()
    {
      await LoadPopularItemsAsync(_repository);
    }


    private async Task LoadPopularItemsAsync(IClothingRepository repository)
    {
      var allItems = await repository.GetClothingItemsAsync();
      PopularItems = new ObservableCollection<ClothingItem>(allItems.Take(10).ToList());
    }

  }
}
