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
using FashionHub.Views;
using FashionHub.Services;
using NavigationService = FashionHub.Services.NavigationService;

namespace FashionHub.Components
{
  /// <summary>
  /// Логика взаимодействия для MainWindow.xaml 
  /// </summary>
  public partial class ProductCard : UserControl
  {

    public ProductCard()
    {
      InitializeComponent();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
      var navigationService = ServiceLocator.NavigationService;
      var favoriteService = new FavoriteService(new DataBaseContext());

      var item = DataContext as ClothingItem;
      if (item == null) return;

      DataContext = new ProductCardViewModel(navigationService, favoriteService, item)
      {
        Product = item
      };
    }

  }
}

namespace FashionHub.ViewModels
{
  public class ProductCardViewModel : NavigationServiceViewModel
  {
    private readonly FavoriteService _favoriteService;
    private readonly string defaultImage = "C:\\Users\\glora\\Desktop\\ООП курсовой\\FashionHub\\FashionHub\\Resources\\icons\\no-image.png";
    private ClothingItem _product;
    private bool _isFavorite;

    public ClothingItem Product
    {
      get => _product;
      set
      {
        _product = value;
        OnPropertyChanged(nameof(Product));
        OnPropertyChanged(nameof(Price));
        OnPropertyChanged(nameof(ShortName));
        OnPropertyChanged(nameof(Rating));

        if (_product != null && IsUserLoggedIn && _favoriteService != null)
        {
          IsFavorite = _favoriteService.IsFavorite(CurrentUserService.UserId.Value, _product.ProductId);
        }
      }
    }

    public bool IsFavorite
    {
      get => _isFavorite;
      set
      {
        _isFavorite = value;
        OnPropertyChanged(nameof(IsFavorite));
      }
    }

    public string Rating
    {
      get => Product?.Rating.ToString() ?? "Рейтинга нет";
    }



    public decimal Price => Product?.Price ?? 0;
    public string ShortName => Product?.ShortName ?? string.Empty;
    public string ImagePath
    {
      get 
      {
        if (Product.ImagePaths.Count != 0) 
        {
          return Product.ImagePaths.First() ?? defaultImage;
        }
        return defaultImage;
      }
    }


    public ICommand OnItemClickedCommand { get; }
    public ICommand ToggleFavoriteCommand { get; }


    public ProductCardViewModel(INavigationService navigationService, FavoriteService favoriteService, ClothingItem item) : base(navigationService)
    {
      Product = item;
      _favoriteService = favoriteService;
      OnItemClickedCommand = new RelayCommand(OnItemClicked);
      ToggleFavoriteCommand = new RelayCommand(ToggleFavorite);

      CurrentUserService.UserStatusChanged += OnUserStatusChanged;
    }

    public ProductCardViewModel() : base() { }

    private void OnItemClicked(object parameter)
    {
      var selectedItem = parameter as ClothingItem;

      if (selectedItem != null)
      {
        IsCatalogPage = false;
        _navigationService.NavigateTo(typeof(ItemDetailsPage), selectedItem);
      }
    }

    private void ToggleFavorite(object parameter)
    {
      _isFavorite = !_isFavorite;

      if (_product != null && IsUserLoggedIn && _favoriteService != null)
      {
        _favoriteService.ToggleFavorite(CurrentUserService.UserId.Value, _product.ProductId);
      }
    }


    private void OnUserStatusChanged()
    {
      if (Product != null && IsUserLoggedIn)
      {
        IsFavorite = _favoriteService.IsFavorite(CurrentUserService.UserId.Value, Product.ProductId);
      }
      else
      {
        IsFavorite = false;
      }

      OnPropertyChanged(nameof(IsUserLoggedIn));
    }

    ~ProductCardViewModel()
    {
      CurrentUserService.UserStatusChanged -= OnUserStatusChanged;
    }

  }
}
