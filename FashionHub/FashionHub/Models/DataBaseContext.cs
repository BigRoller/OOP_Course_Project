using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace FashionHub.Models
{
  public class DataBaseContext : DbContext
  {
    public DataBaseContext() : base("name=MyConnectionString") { }

    public DbSet<ClothingItem> Products { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> Roles { get; set; }
    public DbSet<UserFavorite> UserFavorites { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<PickupPoint> PickupPoints { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }


    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataBaseContext, Migrations.Configuration>());

      modelBuilder.Entity<ClothingItem>()
          .ToTable("Products")
          .HasMany(c => c.Comments)
          .WithRequired(c => c.ClothingItem)
          .HasForeignKey(c => c.ProductId)
          .WillCascadeOnDelete(true);

      modelBuilder.Entity<Comment>()
          .ToTable("Comments")
          .HasRequired(c => c.User)
          .WithMany(u => u.Comments)
          .HasForeignKey(c => c.UserId)
          .WillCascadeOnDelete(true);

      modelBuilder.Entity<User>()
          .ToTable("Users")
          .HasRequired(u => u.UserRole)
          .WithMany()
          .HasForeignKey(u => u.RoleId)
          .WillCascadeOnDelete(true);

      modelBuilder.Entity<User>()
          .HasOptional(u => u.Profile)
          .WithRequired(p => p.User)
          .WillCascadeOnDelete(true);

      modelBuilder.Entity<UserProfile>()
          .ToTable("UserProfiles")
          .HasKey(p => p.UserId);

      modelBuilder.Entity<UserRole>()
          .ToTable("UserRoles");

      modelBuilder.Entity<UserFavorite>()
          .ToTable("UserFavorites")
          .HasKey(uf => new { uf.UserId, uf.ProductId });

      modelBuilder.Entity<UserFavorite>()
          .HasRequired(uf => uf.User)
          .WithMany(u => u.Favorites)
          .HasForeignKey(uf => uf.UserId)
          .WillCascadeOnDelete(true);

      modelBuilder.Entity<UserFavorite>()
          .HasRequired(uf => uf.ClothingItem)
          .WithMany()
          .HasForeignKey(uf => uf.ProductId)
          .WillCascadeOnDelete(true);

      modelBuilder.Entity<CartItem>()
          .HasRequired(ci => ci.Product)
          .WithMany() 
          .HasForeignKey(ci => ci.ProductId)
          .WillCascadeOnDelete(false);

      modelBuilder.Entity<CartItem>()
          .HasRequired(ci => ci.User)
          .WithMany(u => u.CartItems)
          .HasForeignKey(ci => ci.UserId)
          .WillCascadeOnDelete(true);

      modelBuilder.Entity<Order>()
          .ToTable("Orders")
          .HasKey(o => o.OrderId)
          .HasMany(o => o.OrderItems)
          .WithRequired(oi => oi.Order)
          .HasForeignKey(oi => oi.OrderId)
          .WillCascadeOnDelete(true);

      modelBuilder.Entity<Order>()
          .HasRequired(o => o.User)
          .WithMany(u => u.Orders)
          .HasForeignKey(o => o.UserId)
          .WillCascadeOnDelete(false);

      modelBuilder.Entity<OrderItem>()
          .ToTable("OrderItems")
          .HasKey(oi => oi.OrderItemId);

      modelBuilder.Entity<OrderItem>()
          .HasRequired(oi => oi.Product)
          .WithMany()
          .HasForeignKey(oi => oi.ProductId)
          .WillCascadeOnDelete(false);

      modelBuilder.Entity<PickupPoint>()
    .ToTable("PickupPoints")
    .HasKey(p => p.PickupPointId);


    }

  }
}
