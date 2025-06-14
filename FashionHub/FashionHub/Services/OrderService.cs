using FashionHub.Components;
using FashionHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order = FashionHub.Models.Order;

namespace FashionHub.Services
{
  public class OrderService
  {
    private readonly DataBaseContext _context;

    public OrderService(DataBaseContext context)
    {
      _context = context;
    }

    public void PlaceOrder(int userId, string deliveryMethod, string address, IEnumerable<CartItemViewModel> cartItems, DateTime? NearestDeliveryDate)
    {
      var user = _context.Users.Find(userId);
      if (user == null)
      {
        throw new Exception("Пользователь не найден");
      }


      var order = new Order
      {
        UserId = userId,
        DeliveryMethod = deliveryMethod,
        Address = address,
        OrderDate = DateTime.Now,
        Status = "Оформлен",
        OrderItems = new List<OrderItem>(),
        User = user,
        NearestDeliveryDate = NearestDeliveryDate
      };

      foreach (var item in cartItems)
      {
        var orderItem = new OrderItem
        {
          ProductId = item.Product.ProductId,
          Size = item.SelectedSize,
          Color = item.SelectedColor,
          Quantity = 1
        };
        order.OrderItems.Add(orderItem);
      }

      try
      {
        _context.Orders.Add(order);

        _context.SaveChanges();
      }
      catch (DbEntityValidationException dbEx)
      {
        var errorMessages = dbEx.EntityValidationErrors
            .SelectMany(e => e.ValidationErrors)
            .Select(e => $"Свойство: {e.PropertyName} — Ошибка: {e.ErrorMessage}");

        var fullErrorMessage = string.Join(Environment.NewLine, errorMessages);
        var exceptionMessage = $"Ошибка валидации при сохранении заказа:{Environment.NewLine}{fullErrorMessage}";

        throw new Exception(exceptionMessage, dbEx);
      }
      catch (Exception ex)
      {
        throw new Exception($"Неизвестная ошибка при оформлении заказа: {ex.Message}", ex);
      }

    }
  }
}
