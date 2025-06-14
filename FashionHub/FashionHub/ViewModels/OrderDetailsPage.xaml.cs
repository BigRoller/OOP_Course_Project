using FashionHub.Commands;
using FashionHub.Components;
using FashionHub.Models;
using FashionHub.Services;
using FashionHub.ViewModels;
using FashionHub.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Proxies;
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
using NavigationService = FashionHub.Services.NavigationService;

namespace FashionHub.Views
{
  /// <summary>
  /// Логика взаимодействия для OrderDetailsPage.xaml
  /// </summary>
  public partial class OrderDetailsPage : Page
  {
    public OrderDetailsPage(OrderViewModel order)
    {
      InitializeComponent();
      LoadData(order);
    }

    private void LoadData(OrderViewModel order)
    {
      var navigationService = ServiceLocator.NavigationService;
      DataContext = new OrderDetailsPageViewModel(navigationService, order);
    }

  }
}

namespace FashionHub.ViewModels
{
  public class OrderDetailsPageViewModel : NavigationBarViewModel
  {
    private OrderViewModel order;

    public ObservableCollection<CartItemViewModel> OrderItems { get; set; }

    public DateTime OrderDate => order.OrderDate;
    public string DeliveryMethod => order.DeliveryMethod ?? "Не указано";
    public string Status => order.Status ?? "Не указан";
    public string Address => order.Address;
    public int TotalCount => OrderItems?.Count ?? 0;
    public decimal TotalPrice => OrderItems?.Sum(p => p.Product.Price) ?? 0;

    public ICommand CancelOrderCommand { get; }

    public OrderDetailsPageViewModel(INavigationService navigationService, OrderViewModel order) : base(navigationService)
    {
      this.order = order;

      OrderItems = new ObservableCollection<CartItemViewModel>(
          order.Products.Select(p => new CartItemViewModel(p))
      );

      CancelOrderCommand = new RelayCommand(CancelOrderExecute);
    }

    public OrderDetailsPageViewModel() : base() { }

    private void CancelOrderExecute(object parameter)
    {
      var result = CustomMessageBox.Show("Подтверждение", "Вы уверены, что хотите отменить заказ?", true);
      if (result != true) return;

      try
      {
        using (var context = new DataBaseContext())
        {
          var dbOrder = context.Orders.FirstOrDefault(o => o.OrderId == order.OrderId);
          if (dbOrder != null)
          {
            dbOrder.Status = "Отменён";
            context.SaveChanges();
          }
        }

        CustomMessageBox.Show("Успех", "Заказ успешно отменён.");
        _navigationService.GoBack();
      }
      catch (Exception ex)
      {
        CustomMessageBox.Show("Ошибка", $"Ошибка при отмене заказа: {ex.Message}");
      }
    }


  }

}
