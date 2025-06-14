using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionHub.Models
{
  public class Comment
  {
    [Key]
    public int CommentId { get; set; }

    [Required(ErrorMessage = "Товар обязателен")]
    public int ProductId { get; set; }

    [Required(ErrorMessage = "Пользователь обязателен")]
    public int? UserId { get; set; }

    [Required(ErrorMessage = "Введите текст комментария")]
    public string Text { get; set; }

    [Range(1, 5, ErrorMessage = "Рейтинг должен быть от 1 до 5")]
    public decimal Rate { get; set; }

    public ClothingItem ClothingItem { get; set; }

    public virtual User User { get; set; }
  }
}
