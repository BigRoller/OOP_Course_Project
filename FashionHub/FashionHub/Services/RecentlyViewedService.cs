using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionHub.Models;


namespace FashionHub.Services
{
  public class RecentlyViewedService
  {
    private static readonly ObservableCollection<ClothingItem> _recentlyViewed = new ObservableCollection<ClothingItem>();
    private const int MaxItems = 4;

    public static ObservableCollection<ClothingItem> GetRecentlyViewed() => _recentlyViewed;

    public static void AddToRecentlyViewed(ClothingItem product)
    {
      var existing = _recentlyViewed.FirstOrDefault(p => p.ProductId == product.ProductId);
      if (existing != null)
      {
        _recentlyViewed.Remove(existing);
      }

      _recentlyViewed.Insert(0, product);

      while (_recentlyViewed.Count > MaxItems)
      {
        _recentlyViewed.RemoveAt(_recentlyViewed.Count - 1);
      }
    }
  }
}
