using FashionHub.Commands;
using FashionHub.Models;
using FashionHub.Services;
using FashionHub.ViewModels;
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
using System.Xml.Linq;
using NavigationService = FashionHub.Services.NavigationService;

namespace FashionHub.Components
{
  /// <summary>
  /// Логика взаимодействия для ProductCardHorizontal.xaml
  /// </summary>
  public partial class ProductCardHorizontal : UserControl
  {
    public ProductCardHorizontal()
    {
      InitializeComponent();
    }
  }

  public class CartItemViewModel : BaseViewModel
  {
    public int CartItemId { get; set; }

    public ClothingItem Product { get; set; }

    public string SelectedSize { get; set; }
    public string SelectedColor { get; set; }

    public string ImagePath => Product.ImagePaths.First();

    private bool _isSelected;
    public bool IsSelected
    {
      get => _isSelected;
      set
      {
        _isSelected = value;
        OnPropertyChanged(nameof(IsSelected));
      }
    }

    private bool _isFavorite;
    public bool IsFavorite
    {
      get => _isFavorite;
      set
      {
        _isFavorite = value;
        OnPropertyChanged(nameof(IsFavorite));
      }
    }

    public ICommand ToggleFavoriteCommand { get; }
    public ICommand DeleteCommand { get; }

    private readonly Action<CartItemViewModel> _deleteAction;

    public CartItemViewModel()
    {
      ToggleFavoriteCommand = new RelayCommand(ToggleFavorite);
    }

    public CartItemViewModel(Action<CartItemViewModel> deleteAction)
    {
      _deleteAction = deleteAction;
      ToggleFavoriteCommand = new RelayCommand(ToggleFavorite);
      DeleteCommand = new RelayCommand(Delete);
    }

    public CartItemViewModel(ProductInOrderViewModel product)
    {
      Product = new ClothingItem
      {
        ShortName = product.Name,
        Price = product.PricePerItem,
        ImagePaths = new ObservableCollection<string> { product.ImagePath }
      };

      SelectedColor = product.Color;
      SelectedSize = product.Size;
    }

    private void ToggleFavorite(object parameter)
    {
      if (Product != null && CurrentUserService.UserId != null && Product.ProductId != 0)
      {
        var service = new FavoriteService(new DataBaseContext());
        service.ToggleFavorite(CurrentUserService.UserId.Value, Product.ProductId);
      }
    }


    private void Delete(object parameter)
    {
      _deleteAction?.Invoke(this);
    }

  }


}
