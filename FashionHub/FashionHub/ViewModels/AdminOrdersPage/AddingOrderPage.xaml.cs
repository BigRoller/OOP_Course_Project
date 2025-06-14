using FashionHub.Commands;
using FashionHub.Components;
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
using System.Data.Entity;
using NavigationService = FashionHub.Services.NavigationService;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;
using Order = FashionHub.Models.Order;
using System.IO.Packaging;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using FashionHub.ViewModels;
using FashionHub.Views;

namespace FashionHub.Views
{
  /// <summary>
  /// Логика взаимодействия для AddingOrderPageViewModel.xaml
  /// </summary>
  public partial class AddingOrderPage : Page
  {
    public AddingOrderPage(OrderViewModel order)
    {
      InitializeComponent();
      LoadData(order);
    }

    public AddingOrderPage()
    {
      InitializeComponent();
      LoadData(null);
    }

    private void LoadData(OrderViewModel order)
    {

      var navigationService = ServiceLocator.NavigationService;
      if (order != null)
      {
        DataContext = new AddingOrderPageViewModel(navigationService, order);
      }
      else
      {
        DataContext = new AddingOrderPageViewModel(navigationService);
      }
    }
  }
}

  namespace FashionHub.ViewModels
  { 
    public class AddingOrderPageViewModel : NavigationBarViewModel
    {
      public PageStateEnum PageState { get; set; }

      public ObservableCollection<User> Users { get; set; }
      public ObservableCollection<ClothingItem> Products { get; set; }

      public OrderViewModel Order { get; set; }

      public string Address { get; set; }
      public string Status { get; set; }

      public ObservableCollection<string> AvailableColors { get; set; } = new ObservableCollection<string>();
      public ObservableCollection<string> AvailableSizes { get; set; } = new ObservableCollection<string>();

      public Dictionary<string, string> OrderErrors { get; } = new Dictionary<string, string>();

      private string selectedColor;
      public string SelectedColor
      {
        get => selectedColor;
        set
        {
          selectedColor = value;
          OnPropertyChanged(nameof(SelectedColor));
        }
      }

      private string selectedSize;
      public string SelectedSize
      {
        get => selectedSize;
        set
        {
          selectedSize = value;
          OnPropertyChanged(nameof(SelectedSize));
        }
      }

      private User selectedUser;
      public User SelectedUser
      {
        get => selectedUser;
        set
        {
          selectedUser = value;
          OnPropertyChanged(nameof(SelectedUser));
        }
      }

      private string selectedOrderStatus;
      public string SelectedOrderStatus
      {
        get => selectedOrderStatus;
        set
        {
          selectedOrderStatus = value;
          OnPropertyChanged(nameof(SelectedOrderStatus));
        }
      }

      private ClothingItem selectedProduct;
      public ClothingItem SelectedProduct
      {
        get => selectedProduct;
        set
        {
          selectedProduct = value;
          OnPropertyChanged(nameof(SelectedProduct));
          UpdateAvailableColorsAndSizes();
        }
      }
          
      private string buttonText;
      public string ButtonText
      {
        get => buttonText;
        set 
        {
          buttonText = value;
          OnPropertyChanged(nameof(ButtonText));
        }
      }

      private string _productSearchText;
      public string ProductSearchText
      {
        get => _productSearchText;
        set
        {
          if (_productSearchText != value)
          {
            _productSearchText = value;
            OnPropertyChanged(nameof(ProductSearchText));

            if (string.IsNullOrWhiteSpace(_productSearchText))
            {
              SelectedProduct = null;
            }

            UpdateFilteredProducts();
          }
        }
      }

      private string selectedDeliveryMethod;
      public string SelectedDeliveryMethod
      {
        get => selectedDeliveryMethod;
        set
        {
          selectedDeliveryMethod = value;
          OnPropertyChanged(nameof(SelectedDeliveryMethod));
        }
      }

      public ObservableCollection<ClothingItem> FilteredProducts { get; set; } = new ObservableCollection<ClothingItem>();


      public ObservableCollection<CartItemViewModel> SelectedProducts { get; set; }
      public ObservableCollection<string> OrderStatuses { get; set; }
      public ObservableCollection<string> DeliveryMethods { get; set; }

      public ICommand AddProductToOrderCommand { get; }
      public ICommand FinishCommand { get; }

      public AddingOrderPageViewModel(INavigationService navigationService, OrderViewModel order) : base(navigationService)
      {
        FinishCommand = new RelayCommand(FinishOrder);
        AddProductToOrderCommand = new RelayCommand(AddProductToOrder);
        LoadData(order);
      }

      public AddingOrderPageViewModel(INavigationService navigationService) : base(navigationService)
      {
        FinishCommand = new RelayCommand(FinishOrder);
        AddProductToOrderCommand = new RelayCommand(AddProductToOrder);
        LoadData(null);
      }

      public AddingOrderPageViewModel() : base() { }

      public enum PageStateEnum
      {
        ADDITION,
        EDITING
      }

      private void LoadData(object parameter)
      {
        using (var db = new DataBaseContext())
        {
          Users = new ObservableCollection<User>(db.Users.ToList());
          Products = new ObservableCollection<ClothingItem>(db.Products.ToList());

          UpdateFilteredProducts();

          OrderStatuses = new ObservableCollection<string>
        {
            "Оформлен",
            "Обрабатывается",
            "В пути",
            "Доставлен",
            "Отменен"
        };
          DeliveryMethods = new ObservableCollection<string>
        {
          "Пункт выдачи",
          "Курьером",
        };

          if (parameter is OrderViewModel orderViewModel)
          {
            PageState = PageStateEnum.EDITING;
            ButtonText = "Изменить заказ";
            var fullOrder = db.Orders
                              .Include(o => o.User)
                              .Include(o => o.OrderItems)
                              .FirstOrDefault(o => o.OrderId == orderViewModel.OrderId);

            SelectedUser = Users.FirstOrDefault(u => u.UserId == fullOrder.UserId);
            SelectedOrderStatus = fullOrder.Status;

            if (fullOrder != null)
            {
              Order = new OrderViewModel()
              {
                OrderId = fullOrder.OrderId,
                Address = fullOrder.Address,
                DeliveryMethod = fullOrder.DeliveryMethod,
                Status = fullOrder.Status,
                OrderDate = fullOrder.OrderDate,
                User = SelectedUser
              };

              SelectedDeliveryMethod = Order.DeliveryMethod;
              Address = Order.Address;
              Status = Order.Status;
            }

            SelectedProducts = new ObservableCollection<CartItemViewModel>();
            foreach (var item in fullOrder.OrderItems)
            {
              var product = Products.FirstOrDefault(p => p.ProductId == item.ProductId);
              if (product != null)
              {
                SelectedProducts.Add(new CartItemViewModel(DeleteCartItem)
                {
                  Product = product,
                  SelectedColor = item.Color,
                  SelectedSize = item.Size
                });
              }
            }
          }
          else
          {
            SelectedProducts = new ObservableCollection<CartItemViewModel>();
            PageState = PageStateEnum.ADDITION;
            ButtonText = "Добавить заказ";
            Order = new OrderViewModel();
          }
        }
      }

      private void UpdateFilteredProducts()
      {
        if (Products == null) return;

        var filtered = string.IsNullOrWhiteSpace(ProductSearchText)
            ? Products
            : Products.Where(p => p.ShortName != null &&
                                  p.ShortName.IndexOf(ProductSearchText, StringComparison.OrdinalIgnoreCase) >= 0);

        FilteredProducts.Clear();

        foreach (var product in filtered.Distinct())
        {
          FilteredProducts.Add(product);
        }
      }

      private void UpdateAvailableColorsAndSizes()
      {
        AvailableColors.Clear();
        AvailableSizes.Clear();

        if (SelectedProduct != null)
        {
          var colors = SelectedProduct.Color?.Split(',').Select(c => c.Trim()).Distinct();
          var sizes = SelectedProduct.Size?.Split(',').Select(s => s.Trim()).Distinct();

          if (colors != null)
            foreach (var color in colors)
              AvailableColors.Add(color);

          if (sizes != null)
            foreach (var size in sizes)
              AvailableSizes.Add(size);

          SelectedColor = null;
          SelectedSize = null;
        }
      }

      private void AddProductToOrder(object parameter)
      {
        bool hasError = false;
        OrderErrors.Remove("Product");
        OrderErrors.Remove("Color");
        OrderErrors.Remove("Size");

        if (SelectedProduct == null)
        {
          OrderErrors["Product"] = "Выберите продукт.";
          hasError = true;
        }

        if (string.IsNullOrWhiteSpace(SelectedColor))
        {
          OrderErrors["Color"] = "Выберите цвет.";
          hasError = true;
        }

        if (string.IsNullOrWhiteSpace(SelectedSize))
        {
          OrderErrors["Size"] = "Выберите размер.";
          hasError = true;
        }

        OnPropertyChanged(nameof(OrderErrors));

        if (hasError) return;

        SelectedProducts.Add(new CartItemViewModel(DeleteCartItem)
        {
          Product = SelectedProduct,
          SelectedColor = SelectedColor,
          SelectedSize = SelectedSize,
        });

        SelectedProduct = null;
        ProductSearchText = string.Empty;
        SelectedColor = null;
        SelectedSize = null;

        AvailableColors.Clear();
        AvailableSizes.Clear();

        OnPropertyChanged(nameof(SelectedProducts));
      }



      private void FinishOrder(object parameter)
      {
        ClearValidationErrors();

        var orderToValidate = new Order
        {
          UserId = SelectedUser?.UserId ?? 0,
          Address = Address,
          Status = SelectedOrderStatus,
          OrderDate = DateTime.Now,
          User = SelectedUser,
          DeliveryMethod = SelectedDeliveryMethod,
        };

        var context = new ValidationContext(orderToValidate);
        var results = new List<ValidationResult>();

        bool isValid = Validator.TryValidateObject(orderToValidate, context, results, true);

        if (!isValid || !SelectedProducts.Any())
        {
          SetValidationErrors(results);

          if (!SelectedProducts.Any())
          {
            OrderErrors["Products"] = "Добавьте хотя бы один товар.";
          }

          OnPropertyChanged(nameof(OrderErrors));
          return;
        }

        using (var db = new DataBaseContext())
        {
          if (PageState == PageStateEnum.ADDITION)
          {
            var newOrder = new Order
            {
              UserId = SelectedUser.UserId,
              DeliveryMethod = SelectedDeliveryMethod,
              Address = Address,
              User = SelectedUser,
              Status = SelectedOrderStatus,
              OrderDate = DateTime.Now,
              OrderItems = SelectedProducts.Select(p => new OrderItem
              {
                ProductId = p.Product.ProductId,
                Quantity = 1
              }).ToList()
            };

            db.Orders.Add(newOrder);
            try
            {
              db.SaveChanges();
              CustomMessageBox.Show("Успех!", "Заказ добавлен.");
            }
            catch (DbEntityValidationException ex)
            {
              foreach (var validationErrors in ex.EntityValidationErrors)
              {
                foreach (var validationError in validationErrors.ValidationErrors)
                {
                  Console.WriteLine($"Свойство: {validationError.PropertyName} — Ошибка: {validationError.ErrorMessage}");
                }
              }

              throw;
            }
          }
          else if (PageState == PageStateEnum.EDITING)
          {
            var existingOrder = db.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefault(o => o.OrderId == Order.OrderId);

            if (existingOrder != null)
            {
              existingOrder.UserId = SelectedUser.UserId;
              existingOrder.DeliveryMethod = SelectedDeliveryMethod;
              existingOrder.Address = Address;
              existingOrder.Status = Status;

              db.OrderItems.RemoveRange(existingOrder.OrderItems);
              existingOrder.OrderItems = SelectedProducts.Select(p => new OrderItem
              {
                ProductId = p.Product.ProductId,
                Quantity = 1
              }).ToList();

              db.SaveChanges();
              CustomMessageBox.Show("Успех!", "Заказ обновлён.");
              _navigationService.NavigateTo(typeof(AdminOrdersPage), null);
            }
            else
            {
              CustomMessageBox.Show("Ошибка", "Заказа не существует.");
            }
          }
        }
      }
  

      private void ClearValidationErrors()
      {
        OrderErrors.Clear();
        OnPropertyChanged(nameof(OrderErrors));
      }

      private void SetValidationErrors(List<ValidationResult> results)
      {
        foreach (var error in results)
        {
          foreach (var member in error.MemberNames)
          {
            OrderErrors[member] = error.ErrorMessage;
          }
        }
        OnPropertyChanged(nameof(OrderErrors));
      }

      private void DeleteCartItem(CartItemViewModel item)
      {
        if (SelectedProducts.Contains(item))
          SelectedProducts.Remove(item);
      }

    }

  }
