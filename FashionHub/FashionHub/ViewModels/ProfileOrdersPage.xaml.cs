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
using FashionHub.ViewModels;
using NavigationService = FashionHub.Services.NavigationService;
using FashionHub.Commands;
using FashionHub.Views;

namespace FashionHub.Views
{
  /// <summary>
  /// Логика взаимодействия для ProfilePage.xaml
  /// </summary>
  public partial class ProfileOrdersPage : Page
  {
    public ProfileOrdersPage()
    {
      InitializeComponent();
      LoadData();
    }

    private void LoadData()
    {
      var repository = ServiceLocator.GetService<IClothingRepository>(); // Получаем репозиторий
      var navigationService = ServiceLocator.NavigationService;
      DataContext = new ProfileOrdersPageViewModel(repository, navigationService);
    }
  }

}

namespace FashionHub.ViewModels
{
public class ProfileOrdersPageViewModel : RecomendationViewModel
  {
    new public event PropertyChangedEventHandler PropertyChanged;

    public ObservableCollection<OrderViewModel> UserOrders { get; }
    public ObservableCollection<OrderViewModel> AllOrders { get; set; } = new ObservableCollection<OrderViewModel>();
    public int CurrentOrdersCount => UserOrders.Count;
    public int TotalOrdersCount => AllOrders.Count;

    public ProfileOrdersPageViewModel(IClothingRepository repository, INavigationService navigationService) : base (repository, navigationService) 
    {
      UserOrders = new ObservableCollection<OrderViewModel>();
      LoadUserOrders();

      AllOrders.CollectionChanged += (s, e) =>
      {
        OnPropertyChanged(nameof(CurrentOrdersCount));
        OnPropertyChanged(nameof(TotalOrdersCount));
      };
    }

    public ProfileOrdersPageViewModel() : base() { }


    private void LoadUserOrders()
    {
      var userId = CurrentUserService.UserId ?? 0;

      using (var context = new DataBaseContext())
      {

        var orders = context.Orders
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.OrderDate)
            .Select(o => new
            {
              o.OrderId,
              o.OrderDate,
              o.DeliveryMethod,
              o.Address,
              o.Status,
              Products = o.OrderItems.Select(oi => new
              {
                oi.Product.ShortName,
                oi.Size,
                oi.Color,
                oi.Quantity,
                oi.Product.Images,
                oi.Product.Price
              }).ToList()
            })
            .ToList()
            .Select(o => new OrderViewModel(_navigationService)
            {
              OrderId = o.OrderId,
              OrderDate = o.OrderDate,
              DeliveryMethod = o.DeliveryMethod,
              Address = o.Address,
              Status = o.Status,
              Products = o.Products.Select(oi => new ProductInOrderViewModel
              {
                Name = oi.ShortName,
                Size = oi.Size,
                Color = oi.Color,
                Quantity = oi.Quantity,
                ImagePath = !string.IsNullOrWhiteSpace(oi.Images)
              ? oi.Images.Split(';').FirstOrDefault()
              : "C:\\Users\\glora\\Desktop\\ООП курсовой\\FashionHub\\FashionHub\\Resources\\icons\\no-image.png",
                PricePerItem = oi.Price
              }).ToList()
            })
            .ToList();


        foreach (var order in orders)
        {
          AllOrders.Add(order);
          UserOrders.Add(order);
        }
         
      }

    }

    protected override void OnPropertyChanged(string propertyName)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      if (propertyName == nameof(AllOrders))
      {
        OnPropertyChanged(nameof(CurrentOrdersCount));
        OnPropertyChanged(nameof(TotalOrdersCount));
      }
    }

  }
}
