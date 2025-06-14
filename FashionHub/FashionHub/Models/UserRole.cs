using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionHub.Models
{
  public class UserRole
  {
    [Key]
    public int RoleId { get; set; }
    public string RoleName { get; set; }

    public override string ToString()
    {
      return RoleName;
    }
  }
}
