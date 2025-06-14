using GMap.NET.MapProviders;
using GMap.NET;
using System;
using System.Collections.Generic;
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
using System.Collections.ObjectModel;
using FashionHub.Models;
using NavigationService = FashionHub.Services.NavigationService;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;
using FashionHub.Services;
using GMap.NET.WindowsPresentation;
using FashionHub.Commands;
using FashionHub.Views;
using FashionHub.ViewModels;
using System.ComponentModel.DataAnnotations;


namespace FashionHub.Views
{
  /// <summary>
  /// Логика взаимодействия для OrderPointPage.xaml
  /// </summary>
  public partial class OrderCourierPage : Page
  {
    public OrderCourierPage()
    {
      InitializeComponent();
    }

    public OrderCourierPage(List<CartItemViewModel> items)
    {
      InitializeComponent();
      LoadData(items);
    }


    private void LoadData(IEnumerable<CartItemViewModel> items)
    {
      MapControl.MapProvider = GMapProviders.GoogleMap;
      MapControl.Position = new PointLatLng(53.9006, 27.5590);
      MapControl.Zoom = 10;

      MapControl.MinZoom = 2;
      MapControl.MaxZoom = 18;
      MapControl.ShowCenter = false;
      MapControl.CanDragMap = true;

      var navigationService = ServiceLocator.NavigationService;
      DataContext = new OrderCourierPageViewModel(navigationService, items);
    }
  }
}

namespace FashionHub.ViewModels
{
  public class OrderCourierPageViewModel : NavigationBarViewModel
  {
    public ObservableCollection<CartItemViewModel> SelectedItems { get; }

    private readonly DataBaseContext _context = new DataBaseContext();

    [Required(ErrorMessage = "Город обязателен")]
    [StringLength(50, ErrorMessage = "City must be less than 50 characters.")]
    public string City { get; set; }

    [Required(ErrorMessage = "Улица обязательна")]
    [StringLength(50, ErrorMessage = "Street must be less than 50 characters.")]
    public string Street { get; set; }

    [Required(ErrorMessage = "Дом обязателен")]
    [StringLength(50, ErrorMessage = "House must be less than 50 characters.")]
    public string House { get; set; }

    [Required(ErrorMessage = "Квартира обязательна")]
    [StringLength(50, ErrorMessage = "Apartment must be less than 50 characters.")]
    public string Apartment { get; set; }
    public string CourierComment { get; set; }

    public ICommand GoToPointPageCommand { get; }
    public ICommand GoToCourierPageCommand { get; }
    public ICommand PlaceCourierOrderCommand { get; }

    public Dictionary<string, string> OrderAddressErrors { get; } = new Dictionary<string, string>();

    public OrderCourierPageViewModel(INavigationService navigationService, IEnumerable<CartItemViewModel> selectedItems) : base(navigationService)
    {
      SelectedItems = new ObservableCollection<CartItemViewModel>(selectedItems);
      GoToPointPageCommand = new RelayCommand(GoToPointPage);
      GoToCourierPageCommand = new RelayCommand(GoToCourierPage);
      PlaceCourierOrderCommand = new RelayCommand(PlaceOrder);
    }

    private void GoToPointPage(object obj)
    {
      var items = SelectedItems.ToList();
      _navigationService.NavigateTo(typeof(OrderPointPage), items);
    }

    private void GoToCourierPage(object obj)
    {
      var items = SelectedItems.ToList();
      _navigationService.NavigateTo(typeof(OrderCourierPage), items);
    }

    private void PlaceOrder(object obj)
    {
      ClearValidationErrors();

      var validationContext = new ValidationContext(this);
      var validationResults = new List<ValidationResult>();

      if (!Validator.TryValidateObject(this, validationContext, validationResults, true))
      {
        SetValidationErrors(validationResults);
        return;
      }

      try
      {
        var fullAddress = $"{City}, ул. {Street}, д. {House}, кв. {Apartment}";
        var context = new DataBaseContext();
        var orderService = new OrderService(context);

        var userId = CurrentUserService.UserId ?? 0;

        orderService.PlaceOrder(
            userId: userId,
            deliveryMethod: "Курьером",
            address: fullAddress,
            cartItems: SelectedItems,
            NearestDeliveryDate: App.GetRandomDate()
        );

        foreach (var selectedItem in SelectedItems)
        {
          var itemToRemove = _context.CartItems.FirstOrDefault(ci =>
              ci.UserId == userId &&
              ci.ProductId == selectedItem.Product.ProductId);

          if (itemToRemove != null)
          {
            _context.CartItems.Remove(itemToRemove);
          }
        }

        _context.SaveChanges();

        CustomMessageBox.Show("Успешно", "Заказ оформлен!");
        _navigationService.NavigateTo(typeof(MainPage), null);
      }
      catch (Exception ex)
      {
        CustomMessageBox.Show("Ошибка", $"Не удалось оформить заказ: {ex.Message}");
      }
    }

    private void ClearValidationErrors()
    {
      OrderAddressErrors.Clear();
      OnPropertyChanged(nameof(OrderAddressErrors));
    }

    private void SetValidationErrors(List<ValidationResult> results)
    {
      foreach (var error in results)
      {
        foreach (var member in error.MemberNames)
        {
          OrderAddressErrors[member] = error.ErrorMessage;
        }
      }
      OnPropertyChanged(nameof(OrderAddressErrors));
    }




  }

}
