using FashionHub.Commands;
using FashionHub.Components;
using FashionHub.Services;
using FashionHub.ViewModels;
using FashionHub.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace FashionHub.Models
{
  public class Order
  {
    public int OrderId { get; set; }
    public int UserId { get; set; }

    [Required(ErrorMessage = "Метод доставки обязателен.")]
    public string DeliveryMethod { get; set; }

    [Required(ErrorMessage = "Адрес обязателен.")]
    [MinLength(5, ErrorMessage = "Адрес должен содержать минимум 5 символов.")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Дата обязательна.")]
    public DateTime OrderDate { get; set; }

    public DateTime? NearestDeliveryDate { get; set; }

    [Required(ErrorMessage = "Статус обязателен.")]
    public string Status { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; }

    [Required(ErrorMessage = "Пользователь обязателен.")]
    public virtual User User { get; set; }

  }


  public class OrderViewModel : NavigationServiceViewModel
  {
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public string DeliveryMethod { get; set; }
    public DateTime? NearestDeliveryDate { get; set; }
    
    public string Address { get; set; }

    public string _status;
    public string Status
    {
      get => _status;
      set
      {
        if (_status != value)
        {
          _status = value;
          OnPropertyChanged(nameof(Status));
        }
      }
    }

    public List<ProductInOrderViewModel> Products { get; set; }
    public string ProductNames => string.Join(", ", Products?.Select(p => p.Name) ?? Enumerable.Empty<string>());

    public virtual User User { get; set; }
    public decimal TotalOrderPrice => Products?.Sum(p => p.PricePerItem * p.Quantity) ?? 0;

    public ICommand MoreInformationCommand { get; }

    public OrderViewModel(INavigationService navigationService) : base(navigationService) 
    {
      MoreInformationCommand = new RelayCommand(MoreInforamtion);
    }

    public OrderViewModel() : base() { }

    private void MoreInforamtion(object parameter)
    {
      _navigationService.NavigateTo(typeof(OrderDetailsPage), this);
    }
  }
}
