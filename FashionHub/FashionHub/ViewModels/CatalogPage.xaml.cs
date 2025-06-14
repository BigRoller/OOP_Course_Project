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
  public partial class CatalogPage : Page
  {
    public CatalogPage()
    {
      InitializeComponent();
      Loaded += CatalogPage_Loaded;
    }

    private async void CatalogPage_Loaded(object sender, RoutedEventArgs e)
    {
      var repository = ServiceLocator.GetService<IClothingRepository>();
      var navigationService = ServiceLocator.NavigationService;
      var vm = new CatalogPageViewModel(repository, navigationService);
      DataContext = vm;
      await vm.InitializeAsync(); // Асинхронная загрузка данных
    }
  }

}

namespace FashionHub.ViewModels
{
  public partial class CatalogPageViewModel : AdminProductsPageViewModel
  {

    public ICommand ApplyFiltersCommand { get; }
    public ICommand EditItemCommand { get; }
    public ICommand ChangeLanguageCommand { get; }

    public Dictionary<string, string> FilterErrors { get; } = new Dictionary<string, string>();

    public CatalogPageViewModel(IClothingRepository repository, INavigationService navigationService) : base(repository, navigationService)
    {
      ApplyFiltersCommand = new RelayCommand(ApplyFilters);
    }

    public CatalogPageViewModel() : base() { }

    public async Task InitializeAsync()
    {
      var items = await _repository.GetClothingItemsAsync();
      ClothingItems = new ObservableCollection<ClothingItem>(items);
    }


    private void ClearValidationErrors()
    {
      FilterErrors.Clear();
      OnPropertyChanged(nameof(FilterErrors));
    }

    private void SetValidationError(string field, string message)
    {
      FilterErrors[field] = message;
      OnPropertyChanged(nameof(FilterErrors));
    }

    public async void ApplyFilters(object parameter)
    {
      var page = parameter as Page;
      if (page == null) return;

      ClearValidationErrors();

      string selectedCategory = null;

      var allFilter = page.FindName("CategoryFilter_All") as RadioButton;
      var menFilter = page.FindName("CategoryFilter_Men") as RadioButton;
      var womenFilter = page.FindName("CategoryFilter_Women") as RadioButton;
      var kidsFilter = page.FindName("CategoryFilter_Kids") as RadioButton;

      if (allFilter?.IsChecked == true)
        selectedCategory = CategoryFilterService.AllCategory;
      else if (menFilter?.IsChecked == true)
        selectedCategory = CategoryFilterService.ManCategory;
      else if (womenFilter?.IsChecked == true)
        selectedCategory = CategoryFilterService.WomanCategory;
      else if (kidsFilter?.IsChecked == true)
        selectedCategory = CategoryFilterService.KidsCategory;

      var priceFromTextBox = page.FindName("PriceFrom") as TextBox;
      var priceToTextBox = page.FindName("PriceTo") as TextBox;

      decimal priceFrom, priceTo;

      if (!decimal.TryParse(priceFromTextBox.Text, out priceFrom))
      {
        SetValidationError("PriceFrom", "Неверный формат цены");
        priceFrom = 0;
      }

      if (!decimal.TryParse(priceToTextBox.Text, out priceTo))
      {
        SetValidationError("PriceTo", "Неверный формат цены");
        priceTo = 10000;
      }

      if (priceFrom > priceTo)
      {
        SetValidationError("PriceRange", "Минимальная цена не может быть больше максимальной");
      }

      if (FilterErrors.Any())
        return;

      var allItems = await _repository.GetClothingItemsAsync();

      var filteredItems = allItems
          .Where(item =>
            (selectedCategory == CategoryFilterService.AllCategory || item.Category == selectedCategory) &&
            item.Price >= priceFrom && item.Price <= priceTo)
          .ToList();

      ClothingItems = new ObservableCollection<ClothingItem>(filteredItems);
    }
  }
  }
