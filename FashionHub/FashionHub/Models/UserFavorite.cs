using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionHub.Models
{
  public class UserFavorite
  {
    [Key]
    [Column(Order = 1)]
    public int? UserId { get; set; }

    [Key]
    [Column(Order = 2)]
    public int ProductId { get; set; }

    [ForeignKey("UserId")]
    public virtual User User { get; set; }

    [ForeignKey("ProductId")]
    public virtual ClothingItem ClothingItem { get; set; }
  }
}
