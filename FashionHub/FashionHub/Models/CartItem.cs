using FashionHub.Commands;
using FashionHub.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FashionHub.Models
{
  public class CartItem
  {
    public int Id { get; set; }

    public int ProductId { get; set; }
    public virtual ClothingItem Product { get; set; }

    public string SelectedSize { get; set; }
    public string SelectedColor { get; set; }

    public int UserId { get; set; }
    public virtual User User { get; set; }
  }
}
