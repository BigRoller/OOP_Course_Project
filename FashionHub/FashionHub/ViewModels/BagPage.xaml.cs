using FashionHub.Commands;
using FashionHub.Models;
using FashionHub.Services;
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
using FashionHub.Components;
using FashionHub.ViewModels;
using FashionHub.Views;
using NavigationService = FashionHub.Services.NavigationService;

namespace FashionHub.Views
{
  /// <summary>
  /// Логика взаимодействия для BagPage.xaml
  /// </summary>
  /// 

  public partial class BagPage : Page
  {
    public BagPage()
    {
      InitializeComponent();
      LoadData();
    }


    private void LoadData()
    {
      var _repository = ServiceLocator.GetService<IClothingRepository>();
      var navigationService = ServiceLocator.NavigationService;
      DataContext = new BagPageViewModel(_repository, navigationService);
    }
  }
}

namespace FashionHub.ViewModels
{
  public class BagPageViewModel : RecomendationViewModel
  {
    private ObservableCollection<CartItemViewModel> _cartItems;
    public ObservableCollection<CartItemViewModel> CartItems
    {
      get => _cartItems;
      set
      {
        _cartItems = value;
        OnPropertyChanged(nameof(CartItems));
        OnPropertyChanged(nameof(TotalPrice));
        OnPropertyChanged(nameof(TotalCount));
      }
    }

    private FavoriteService favoriteService { get; set; }

    public decimal TotalPrice => CartItems?.Where(ci => ci.IsSelected).Sum(ci => ci.Product?.Price ?? 0) ?? 0;
    public int TotalCount => CartItems?.Count(ci => ci.IsSelected) ?? 0;
  

    public ICommand DeleteSelectedCommand { get; }
    public ICommand SelectAllCommand { get; }
    public ICommand ProceedToCheckoutCommand { get; }

    public BagPageViewModel(IClothingRepository repository, INavigationService navigationService) : base(repository, navigationService)
    {
      favoriteService = new FavoriteService(new DataBaseContext());
      LoadCartItems();
      DeleteSelectedCommand = new RelayCommand(DeleteSelected);
      SelectAllCommand = new RelayCommand(SelectAll);
      ProceedToCheckoutCommand = new RelayCommand(ProceedToCheckout);
    }

    public BagPageViewModel() : base() {    }

    private void LoadCartItems()
    {
      if (CurrentUserService.UserId == null)
      {
        CartItems = new ObservableCollection<CartItemViewModel>();
        return;
      }

      using (var context = new DataBaseContext())
      {
        var itemsData = context.CartItems
          .Where(c => c.UserId == CurrentUserService.UserId)
          .Select(c => new
          {
            c.Id,
            c.Product,
            c.SelectedColor,
            c.SelectedSize
          })
          .ToList();

        var items = itemsData.Select(data =>
        {
          var item = new CartItemViewModel(RemoveCartItem)
          {
            CartItemId = data.Id,
            Product = data.Product,
            SelectedColor = data.SelectedColor,
            SelectedSize = data.SelectedSize,
            IsSelected = false,
          };

          if (favoriteService.IsFavorite(CurrentUserService.UserId ?? 0, data.Product.ProductId))
          {
            item.IsFavorite = true;
          }

          item.PropertyChanged += CartItem_PropertyChanged;

          return item;
        }).ToList();


        foreach (var item in items)
        {
          if (favoriteService.IsFavorite(CurrentUserService.UserId ?? 0, item.Product.ProductId) )
          {
            item.IsFavorite = true;
          }
        }

        foreach (var item in items)
        {
          item.PropertyChanged += CartItem_PropertyChanged;
        }

        CartItems = new ObservableCollection<CartItemViewModel>(items);
      }
    }


    private void CartItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == nameof(CartItemViewModel.IsSelected))
      {
        OnPropertyChanged(nameof(TotalPrice));
        OnPropertyChanged(nameof(TotalCount));
      }
    }



    private void DeleteSelected(object obj)
    {
      var toDelete = CartItems.Where(c => c.IsSelected).ToList();
      if (!toDelete.Any()) return;

      using (var context = new DataBaseContext())
      {
        foreach (var item in toDelete)
        {
          var entity = context.CartItems.Find(item.CartItemId);

          if (entity != null)
          {
            var clothingItem = context.Products.FirstOrDefault(ci => ci.ProductId == entity.ProductId);
            if (clothingItem != null)
            {
              clothingItem.Quantity += 1;
              _repository.AddOrUpdateClothingItemAsync(clothingItem);
            }

            context.CartItems.Remove(entity);
          }
        }

        context.SaveChanges();
      }

      foreach (var item in toDelete)
        CartItems.Remove(item);

      OnPropertyChanged(nameof(TotalPrice));
      OnPropertyChanged(nameof(TotalCount));
    }



    private void ProceedToCheckout(object obj)
    {

      var selectedItems = CartItems.Where(c => c.IsSelected).ToList();
      if (!selectedItems.Any())
      {
        CustomMessageBox.Show("Ошибка", "Выберите хотя бы один товар для оформления.");
        return;
      }

      _navigationService.NavigateTo(typeof(OrderCourierPage), selectedItems);
    }



    private void SelectAll(object obj)
    {
      foreach (var item in CartItems)
      {
        item.IsSelected = true;
      }
    }


    private void RemoveCartItem(CartItemViewModel item)
    {
      if (item == null) return;

      using (var context = new DataBaseContext())
      {
        var entity = context.CartItems.Find(item.CartItemId);
        if (entity != null)
        {
          var clothingItem = context.Products.FirstOrDefault(ci => ci.ProductId == entity.ProductId);
          if (clothingItem != null)
          {
            clothingItem.Quantity += 1;
            _repository.AddOrUpdateClothingItemAsync(clothingItem);
          }

          context.CartItems.Remove(entity);
          context.SaveChanges();
        }
      }

      CartItems.Remove(item);
      OnPropertyChanged(nameof(TotalPrice));
      OnPropertyChanged(nameof(TotalCount));
    }


  }

}
