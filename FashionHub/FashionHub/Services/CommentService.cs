using FashionHub.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FashionHub.Services
{
  public static class CommentService
  {
    public static void AddComment(Comment comment)
    {
      using (var context = new DataBaseContext())
      {
        context.Comments.Add(comment);
        context.SaveChanges();
      }
    }

    public static List<Comment> GetCommentsForItem(int itemId)
    {
      using (var context = new DataBaseContext())
      {
        return context.Comments
            .Where(c => c.ProductId == itemId)
            .Include("User.Profile")
            .OrderByDescending(c => c.Rate)
            .ToList();
      }
    }
  }
}