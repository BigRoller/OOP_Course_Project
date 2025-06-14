using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionHub.Services;

namespace FashionHub.Models
{
  public class UserProfile : BaseViewModel
  {
    [Key, ForeignKey("User")]
    public int UserId { get; set; }

    [StringLength(50)]
    public string LastName { get; set; }

    [StringLength(50)]
    public string FirstName { get; set; }

    [StringLength(50)]
    public string MiddleName { get; set; }

    [StringLength(20)]
    [Phone]
    public string PhoneNumber { get; set; }

    public bool? Gender { get; set; }

    private string _avatarPath;
    public string AvatarPath
    {
      get => _avatarPath ?? CurrentUserService.DefaultAvatarPath;
      set
      {
        _avatarPath = value;
        OnPropertyChanged(nameof(AvatarPath));
      }
    }

    [DataType(DataType.Date)]
    public DateTime? BirthDate { get; set; }

    public virtual User User { get; set; }
  }
}
