using System;
using DemoExchange.Interface;
using DemoExchange.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoExchange.Contexts {
  public interface IDbContext : IDisposable {
    public int SaveChanges();
    public Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry Entry(object entity);
  }

  // TODO: DI & https://docs.microsoft.com/en-us/ef/core/miscellaneous/context-pooling
  public class DemoExchangeDbContext : DbContext, IDbContext {
    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
      options.UseSqlServer("Server=loki,1433;Database=demo_exchange;User Id=demo_exchange_user;Password=PASSWORD;"); // HACK: Move connection string to config file

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      base.OnModelCreating(modelBuilder);
      modelBuilder
        .Entity<OrderEntity>()
        .Property(e => e.Status)
        .HasConversion(
          v => v.ToString(),
          v => (OrderStatus)Enum.Parse(typeof(OrderStatus), v));
      modelBuilder
        .Entity<OrderEntity>()
        .Property(e => e.Action)
        .HasConversion(
          v => v.ToString(),
          v => (OrderAction)Enum.Parse(typeof(OrderAction), v));
      modelBuilder
        .Entity<OrderEntity>()
        .Property(e => e.Type)
        .HasConversion(
          v => v.ToString(),
          v => (OrderType)Enum.Parse(typeof(OrderType), v));
      modelBuilder
        .Entity<OrderEntity>()
        .Property(e => e.OrderPrice)
        .HasPrecision(28, 8);
      modelBuilder
        .Entity<OrderEntity>()
        .Property(e => e.StrikePrice)
        .HasPrecision(28, 8);
      modelBuilder
        .Entity<OrderEntity>()
        .Property(e => e.TimeInForce)
        .HasConversion(
          v => v.ToString(),
          v => (OrderTimeInForce)Enum.Parse(typeof(OrderTimeInForce), v));

      modelBuilder
        .Entity<TransactionEntity>()
        .Property(e => e.Price)
        .HasPrecision(28, 8);
    }
  }
}
