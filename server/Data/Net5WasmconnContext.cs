using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

using Net5Wasm.Models.Net5Wasmconn;

namespace Net5Wasm.Data
{
  public partial class Net5WasmconnContext : Microsoft.EntityFrameworkCore.DbContext
  {
    public Net5WasmconnContext(DbContextOptions<Net5WasmconnContext> options):base(options)
    {
    }

    public Net5WasmconnContext()
    {
    }

    partial void OnModelBuilding(ModelBuilder builder);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.OrdersProduct>().HasKey(table => new {
          table.OrderId, table.ProductId
        });
        builder.Entity<Net5Wasm.Models.Net5Wasmconn.ShoppingCartsProduct>().HasKey(table => new {
          table.ShoppingCartId, table.ProductId
        });
        builder.Entity<Net5Wasm.Models.Net5Wasmconn.WishlistsProduct>().HasKey(table => new {
          table.WishlistId, table.ProductId
        });
        builder.Entity<Net5Wasm.Models.Net5Wasmconn.Order>()
              .HasOne(i => i.Address)
              .WithMany(i => i.Orders)
              .HasForeignKey(i => i.DeliveryAddressId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<Net5Wasm.Models.Net5Wasmconn.OrdersProduct>()
              .HasOne(i => i.Order)
              .WithMany(i => i.OrdersProducts)
              .HasForeignKey(i => i.OrderId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<Net5Wasm.Models.Net5Wasmconn.OrdersProduct>()
              .HasOne(i => i.Product)
              .WithMany(i => i.OrdersProducts)
              .HasForeignKey(i => i.ProductId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<Net5Wasm.Models.Net5Wasmconn.Product>()
              .HasOne(i => i.Category)
              .WithMany(i => i.Products)
              .HasForeignKey(i => i.CategoryId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<Net5Wasm.Models.Net5Wasmconn.ShoppingCartsProduct>()
              .HasOne(i => i.ShoppingCart)
              .WithMany(i => i.ShoppingCartsProducts)
              .HasForeignKey(i => i.ShoppingCartId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<Net5Wasm.Models.Net5Wasmconn.ShoppingCartsProduct>()
              .HasOne(i => i.Product)
              .WithMany(i => i.ShoppingCartsProducts)
              .HasForeignKey(i => i.ProductId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<Net5Wasm.Models.Net5Wasmconn.WishlistsProduct>()
              .HasOne(i => i.Wishlist)
              .WithMany(i => i.WishlistsProducts)
              .HasForeignKey(i => i.WishlistId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<Net5Wasm.Models.Net5Wasmconn.WishlistsProduct>()
              .HasOne(i => i.Product)
              .WithMany(i => i.WishlistsProducts)
              .HasForeignKey(i => i.ProductId)
              .HasPrincipalKey(i => i.Id);


        builder.Entity<Net5Wasm.Models.Net5Wasmconn.Address>()
              .Property(p => p.CreatedOn)
              .HasColumnType("datetime2");

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.Address>()
              .Property(p => p.ModifiedOn)
              .HasColumnType("datetime2");

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.Address>()
              .Property(p => p.DeletedOn)
              .HasColumnType("datetime2");

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.Category>()
              .Property(p => p.CreatedOn)
              .HasColumnType("datetime2");

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.Category>()
              .Property(p => p.ModifiedOn)
              .HasColumnType("datetime2");

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.Category>()
              .Property(p => p.DeletedOn)
              .HasColumnType("datetime2");

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.Order>()
              .Property(p => p.CreatedOn)
              .HasColumnType("datetime2");

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.Order>()
              .Property(p => p.ModifiedOn)
              .HasColumnType("datetime2");

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.OrdersProduct>()
              .Property(p => p.CreatedOn)
              .HasColumnType("datetime2");

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.OrdersProduct>()
              .Property(p => p.ModifiedOn)
              .HasColumnType("datetime2");

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.Product>()
              .Property(p => p.CreatedOn)
              .HasColumnType("datetime2");

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.Product>()
              .Property(p => p.ModifiedOn)
              .HasColumnType("datetime2");

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.Product>()
              .Property(p => p.DeletedOn)
              .HasColumnType("datetime2");

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.ShoppingCart>()
              .Property(p => p.CreatedOn)
              .HasColumnType("datetime2");

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.ShoppingCart>()
              .Property(p => p.ModifiedOn)
              .HasColumnType("datetime2");

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.ShoppingCartsProduct>()
              .Property(p => p.CreatedOn)
              .HasColumnType("datetime2");

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.ShoppingCartsProduct>()
              .Property(p => p.ModifiedOn)
              .HasColumnType("datetime2");

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.Wishlist>()
              .Property(p => p.CreatedOn)
              .HasColumnType("datetime2");

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.Wishlist>()
              .Property(p => p.ModifiedOn)
              .HasColumnType("datetime2");

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.WishlistsProduct>()
              .Property(p => p.CreatedOn)
              .HasColumnType("datetime2");

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.WishlistsProduct>()
              .Property(p => p.ModifiedOn)
              .HasColumnType("datetime2");

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.Address>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.Category>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.Order>()
              .Property(p => p.DeliveryAddressId)
              .HasPrecision(10, 0);

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.OrdersProduct>()
              .Property(p => p.ProductId)
              .HasPrecision(10, 0);

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.OrdersProduct>()
              .Property(p => p.Quantity)
              .HasPrecision(10, 0);

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.Product>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.Product>()
              .Property(p => p.Quantity)
              .HasPrecision(10, 0);

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.Product>()
              .Property(p => p.Price)
              .HasPrecision(18, 2);

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.Product>()
              .Property(p => p.CategoryId)
              .HasPrecision(10, 0);

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.ShoppingCart>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.ShoppingCartsProduct>()
              .Property(p => p.ShoppingCartId)
              .HasPrecision(10, 0);

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.ShoppingCartsProduct>()
              .Property(p => p.ProductId)
              .HasPrecision(10, 0);

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.ShoppingCartsProduct>()
              .Property(p => p.Quantity)
              .HasPrecision(10, 0);

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.Wishlist>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.WishlistsProduct>()
              .Property(p => p.WishlistId)
              .HasPrecision(10, 0);

        builder.Entity<Net5Wasm.Models.Net5Wasmconn.WishlistsProduct>()
              .Property(p => p.ProductId)
              .HasPrecision(10, 0);
        this.OnModelBuilding(builder);
    }


    public DbSet<Net5Wasm.Models.Net5Wasmconn.Address> Addresses
    {
      get;
      set;
    }

    public DbSet<Net5Wasm.Models.Net5Wasmconn.Category> Categories
    {
      get;
      set;
    }

    public DbSet<Net5Wasm.Models.Net5Wasmconn.Order> Orders
    {
      get;
      set;
    }

    public DbSet<Net5Wasm.Models.Net5Wasmconn.OrdersProduct> OrdersProducts
    {
      get;
      set;
    }

    public DbSet<Net5Wasm.Models.Net5Wasmconn.Product> Products
    {
      get;
      set;
    }

    public DbSet<Net5Wasm.Models.Net5Wasmconn.ShoppingCart> ShoppingCarts
    {
      get;
      set;
    }

    public DbSet<Net5Wasm.Models.Net5Wasmconn.ShoppingCartsProduct> ShoppingCartsProducts
    {
      get;
      set;
    }

    public DbSet<Net5Wasm.Models.Net5Wasmconn.Wishlist> Wishlists
    {
      get;
      set;
    }

    public DbSet<Net5Wasm.Models.Net5Wasmconn.WishlistsProduct> WishlistsProducts
    {
      get;
      set;
    }
  }
}
