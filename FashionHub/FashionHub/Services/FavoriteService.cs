using FashionHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionHub.Services
{
  public class FavoriteService
  {
    private readonly DataBaseContext _db;

    public FavoriteService(DataBaseContext db)
    {
      _db = db;
    }

    public bool IsFavorite(int userId, int productId)
    {
      return _db.UserFavorites
          .Any(uf => uf.UserId == userId && uf.ProductId == productId);
    }

    public void ToggleFavorite(int userId, int productId)
    {
      var existing = _db.UserFavorites
          .FirstOrDefault(uf => uf.UserId == userId && uf.ProductId == productId);

      if (existing != null)
      {
        _db.UserFavorites.Remove(existing);
      }
      else
      {
        _db.UserFavorites.Add(new UserFavorite
        {
          UserId = userId,
          ProductId = productId,
        });
      }

      _db.SaveChanges();
    }
  }
}
