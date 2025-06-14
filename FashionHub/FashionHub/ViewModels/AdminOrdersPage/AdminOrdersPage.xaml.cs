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
using FashionHub.Models;
using FashionHub.Services;
using FashionHub.ViewModels;
using FashionHub.Views;
using System.Data.Entity;
using NavigationService = FashionHub.Services.NavigationService;
using FashionHub.Commands;
using FashionHub.Migrations;
using System.Data.Entity.Infrastructure.Design;

namespace FashionHub.Views
{
  /// <summary>
  /// Логика взаимодействия для AdminOrdersPage.xaml
  /// </summary>
  public partial class AdminOrdersPage : Page
  {
    public AdminOrdersPage()
    {
      InitializeComponent();
      LoadData();
    }

    private void LoadData()
    {
      var repository = ServiceLocator.GetService<IClothingRepository>(); // Получаем репозиторий
      var navigationService = ServiceLocator.NavigationService;
      DataContext = new AdminOrdersPageViewModel(repository, navigationService);
    }
  }

}

namespace FashionHub.ViewModels
{
  public class AdminOrdersPageViewModel : NavigationBarViewModel
  {
    public ObservableCollection<OrderViewModel> Orders { get; set; }

    public ICommand AddOrderCommand { get; }
    public ICommand EditOrderCommand { get; }
    public ICommand DeleteOrderCommand { get; }
    public ICommand FulfillOrderCommand { get; }

    public AdminOrdersPageViewModel(IClothingRepository repository,INavigationService navigationService) : base(repository, navigationService)
    {
      DeleteOrderCommand = new RelayCommand(DeleteOrder);
      AddOrderCommand = new RelayCommand(AddOrder);
      EditOrderCommand = new RelayCommand(EditOrder);
      FulfillOrderCommand = new RelayCommand(FulfillOrder);
      LoadOrder();
    }

    public AdminOrdersPageViewModel() : base() { }

    private void LoadOrder()
    {
      using (var dbcontext = new DataBaseContext())
      {
        var ordersFromDb = dbcontext.Orders
            .Include("OrderItems.Product")
            .Include(o => o.User)
            .ToList();

        Orders = new ObservableCollection<OrderViewModel>(
            ordersFromDb.Select(order => new OrderViewModel
            {
              OrderId = order.OrderId,
              OrderDate = order.OrderDate,
              DeliveryMethod = order.DeliveryMethod,
              Address = order.Address,
              Status = order.Status,
              User = order.User,
              NearestDeliveryDate = order.NearestDeliveryDate,
              Products = order.OrderItems.Select(oi => new ProductInOrderViewModel
              {
                Name = oi.Product.ShortName,
                Quantity = oi.Quantity,
                PricePerItem = oi.Product.Price
              }).ToList()
            }));
      }
    }


    private void DeleteOrder(object parameter)
    {
      if (parameter is OrderViewModel orderViewModel)
      {
        if (orderViewModel == null) return;

        var result = CustomMessageBox.Show("Подтверждение", "Вы уверены, что хотите удалить этот заказ?", true);
        if (result != true) return;

        using (var dbcontext = new DataBaseContext())
        {
          var orderToDelete = dbcontext.Orders
              .Include("OrderItems")
              .FirstOrDefault(o => o.OrderId == orderViewModel.OrderId);

          if (orderToDelete != null)
          {
            foreach (var item in orderToDelete.OrderItems)
            {
              var product = dbcontext.Products.FirstOrDefault(p => p.ProductId == item.ProductId);
              if (product != null)
              {
                product.Quantity += 1;
                _repository.AddOrUpdateClothingItemAsync(product);
              }
            }

            dbcontext.OrderItems.RemoveRange(orderToDelete.OrderItems);
            dbcontext.Orders.Remove(orderToDelete);
            dbcontext.SaveChanges();

            Orders.Remove(orderViewModel);

            CustomMessageBox.Show("Успех", "Заказ успешно удалён");
          }
        }
      }
    }


    private void EditOrder(object parameter)
    {
      if (parameter is OrderViewModel order)
      {
        _navigationService.NavigateTo(typeof(AddingOrderPage), order);
      }
    }
    
    private void AddOrder(object parameter)
    {
      if (parameter == null)
      {
        _navigationService.NavigateTo(typeof(AddingOrderPage), null);
      }
    }

    private void FulfillOrder(object parameter)
    {
      if (parameter is OrderViewModel orderViewModel)
      {
        var result = CustomMessageBox.Show("Подтверждение", "Вы уверены, что хотите отметить заказ как выполненный?", true);
        if (result != true) return;

        using (var dbcontext = new DataBaseContext())
        {
          var dbOrder = dbcontext.Orders
              .Include("OrderItems.Product")
              .FirstOrDefault(o => o.OrderId == orderViewModel.OrderId);

          if (dbOrder != null)
          {
            dbOrder.Status = "Выполнен";

            foreach (var item in dbOrder.OrderItems)
            {
              var product = item.Product;
              if (product != null)
              {
                product.SalesCount += item.Quantity;
                _repository.AddOrUpdateClothingItemAsync(product);
              }
            }

            dbcontext.SaveChanges();

            var targetOrder = Orders.FirstOrDefault(o => o.OrderId == dbOrder.OrderId);
            if (targetOrder != null)
            {
              targetOrder.Status = "Выполнен";
            }

            CustomMessageBox.Show("Успех", "Заказ помечен как выполненный");
          }
        }
      }
    }


  }
}
