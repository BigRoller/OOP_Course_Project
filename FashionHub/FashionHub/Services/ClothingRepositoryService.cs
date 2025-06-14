using FashionHub.Models;
using System.Collections.Generic;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FashionHub.Services
{

  public interface IClothingRepository
  {
    Task<List<ClothingItem>> GetClothingItemsAsync();
    Task<bool> AddOrUpdateClothingItemAsync(ClothingItem item);
    Task<bool> RemoveClothingItemAsync(int productId);
    ClothingItem GetClothingItemById(int productId);
  }



  public class ClothingRepository : IClothingRepository
  {
    private readonly DataBaseContext _context;

    public ClothingRepository(DataBaseContext context)
    {
      _context = context;
    }

    public async Task<List<ClothingItem>> GetClothingItemsAsync()
    {
      try
      {
        return await _context.Products
          .Include("Comments.User.Profile")
          .ToListAsync();
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Ошибка загрузки данных из базы: {ex.Message}");
        return new List<ClothingItem>();
      }
    }


    public async Task<bool> AddOrUpdateClothingItemAsync(ClothingItem item)
    {
      using (var transaction = _context.Database.BeginTransaction())
      {
        try
        {
          var existingItem = await _context.Products
            .FirstOrDefaultAsync(p => p.ProductId == item.ProductId);

          if (existingItem != null)
          {
            _context.Entry(existingItem).CurrentValues.SetValues(item);
          }
          else
          {
            _context.Products.Add(item);
          }

          await _context.SaveChangesAsync();

          transaction.Commit();
          return true;
        }
        catch (Exception ex)
        {
          Console.WriteLine($"Ошибка при сохранении товара: {ex.Message}");
          transaction.Rollback();
          return false;
        }
      }
    }


    public async Task<bool> RemoveClothingItemAsync(int productId)
    {
      try
      {
        var item = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
        if (item == null) return false;

        _context.Products.Remove(item);
        await _context.SaveChangesAsync();
        return true;
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Ошибка при удалении товара: {ex.Message}");
        return false;
      }
    }


    public ClothingItem GetClothingItemById(int productId)
    {
      try
      {
        return _context.Products
            .Include("Comments.User.Profile")
            .FirstOrDefault(p => p.ProductId == productId);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Ошибка при получении товара по ID: {ex.Message}");
        return null;
      }
    }


  }


}