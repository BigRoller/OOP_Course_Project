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
using FashionHub.Services;
using GMap.NET.WindowsPresentation;
using FashionHub.Commands;
using FashionHub.Views;
using FashionHub.ViewModels;
using System.Security;

namespace FashionHub.Views
{
  /// <summary>
  /// Логика взаимодействия для OrderPointPage.xaml
  /// </summary>
  public partial class OrderPointPage : Page
  {
    public OrderPointPage()
    {
      InitializeComponent();
    }

    public OrderPointPage(List<CartItemViewModel> items)
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
      DataContext = new OrderPointPageViewModel(navigationService, items);
    }
  }
}

namespace FashionHub.ViewModels
{
  public class OrderPointPageViewModel : NavigationBarViewModel
  {
    public ObservableCollection<CartItemViewModel> SelectedItems { get; }
    public ObservableCollection<PickupPoint> PickupPoints { get; }

    private readonly DataBaseContext _context = new DataBaseContext();

    private PickupPoint _selectedPickupPoint;
    public PickupPoint SelectedPickupPoint
    {
      get => _selectedPickupPoint;
      set
      {
        _selectedPickupPoint = value;
        OnPropertyChanged(nameof(SelectedPickupPoint));
      }
    }

    public ICommand GoToPointPageCommand { get; }
    public ICommand GoToCourierPageCommand { get; }
    public ICommand SelectPickupCommand { get; }


    public OrderPointPageViewModel(INavigationService navigationService, IEnumerable<CartItemViewModel> selectedItems) : base(navigationService)
    {
      SelectedItems = new ObservableCollection<CartItemViewModel>(selectedItems);
      PickupPoints = new ObservableCollection<PickupPoint>(_context.PickupPoints.ToList());

      GoToPointPageCommand = new RelayCommand(GoToPointPage);
      GoToCourierPageCommand = new RelayCommand(GoToCourierPage);
      SelectPickupCommand = new RelayCommand(SelectPickup);
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
    private void SelectPickup(object obj)
    {
      if (SelectedPickupPoint == null)
      {
        CustomMessageBox.Show("Ошибка", "Выберите пункт выдачи.");
        return;
      }

      var userId = CurrentUserService.UserId ?? 0;

      var orderService = new OrderService(_context);
      orderService.PlaceOrder(
          userId: userId,
          deliveryMethod: "Пункт выдачи",
          address: SelectedPickupPoint.FullAddress,
          cartItems: SelectedItems,
          NearestDeliveryDate: SelectedPickupPoint.NearestDeliveryDate
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



  }

}
