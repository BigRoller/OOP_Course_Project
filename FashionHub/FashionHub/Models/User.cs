using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionHub.Models
{
  public class User
  {
    [Key]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Email обязателен.")]
    [EmailAddress(ErrorMessage = "Некорректный формат email.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Login обязателен.")]
    [StringLength(50, MinimumLength = 4, ErrorMessage = "Login должен быть длиной от 5 до 50 символов.")]
    public string Login { get; set; }

    [Required(ErrorMessage = "Пароль обязателен.")]
    [StringLength(100, ErrorMessage = "Пароль должен содержать не более 100 символов.")]
    public string PasswordHash { get; set; }

    [Required(ErrorMessage = "Роль обязательна")]
    [Range(1, 3, ErrorMessage = "RoleId может быть только 1 (ADMIN), 2 (USER) или 3 (MANAGER).")]
    public int RoleId { get; set; }

    public UserRole UserRole { get; set; }
    public virtual UserProfile Profile { get; set; }

    public virtual ICollection<Comment> Comments { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ICollection<UserFavorite> Favorites { get; set; } = new List<UserFavorite>();

    public virtual List<Order> Orders { get; set; } = new List<Order>();



    public enum Role
    {
      ADMIN = 1,
      USER,
      MANAGER,
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      var results = new List<ValidationResult>();

      Validator.TryValidateProperty(Login, new ValidationContext(this, null, null) { MemberName = nameof(Login) }, results);
      Validator.TryValidateProperty(PasswordHash, new ValidationContext(this, null, null) { MemberName = nameof(PasswordHash) }, results);

      return results;
    }

    public override string ToString()
    {
      return Login;
    }
  }
}
