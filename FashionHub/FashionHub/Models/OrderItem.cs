using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionHub.Models
{
  public class OrderItem
  {
    public int OrderItemId { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public string Size { get; set; }
    public string Color { get; set; }
    public int Quantity { get; set; }

    public Order Order { get; set; }
    public ClothingItem Product { get; set; }
  }


  public class ProductInOrderViewModel
  {
    public string Name { get; set; }
    public string Size { get; set; }
    public string Color { get; set; }
    public int Quantity { get; set; }
    public string ImagePath { get; set; }
    public decimal PricePerItem { get; set; }
  }
}