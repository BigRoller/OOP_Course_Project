using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionHub.Services
{
  static public class CategoryFilterService
  {
    public static string ManCategory { get; }
    public static string WomanCategory { get; set; }
    public static string KidsCategory { get; set; }
    public static string AllCategory { get; set; }

    static CategoryFilterService()
    {
      ManCategory = "Man";
      WomanCategory = "Woman";
      KidsCategory = "Kids";
      AllCategory = "All";
    }
  }
}