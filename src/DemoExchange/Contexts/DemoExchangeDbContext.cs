using System;
using DemoExchange.Api;
using DemoExchange.Interface;
using DemoExchange.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoExchange.Contexts {
  public interface IDbContext : IDisposable {
    public int SaveChanges();
    public Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry Entry(object entity);
  }

  public interface IDemoExchangeDbContextFactory<T> where T : DemoExchangeDbContext {
    public T Create();
  }

  public class ConnectionStrings {
    public String DemoExchangeDb { get; set; }
  }

  public class DemoExchangeDbContextFactory : IDemoExchangeDbContextFactory<DemoExchangeDbContext> {
    protected readonly DbContextOptions<DemoExchangeDbContext> options;

    public DemoExchangeDbContextFactory(ConnectionStrings connectionStrings) {
      var optionsBuilder = new DbContextOptionsBuilder<DemoExchangeDbContext>();
      options = optionsBuilder.UseSqlServer(connectionStrings.DemoExchangeDb).Options;
    }

    public virtual DemoExchangeDbContext Create() {
      return new DemoExchangeDbContext(options);
    }
  }

  public class DemoExchangeDbContext : DbContext, IDbContext {
    public DemoExchangeDbContext(DbContextOptions options):
      base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      base.OnModelCreating(modelBuilder);
      modelBuilder
        .Entity<AccountEntity>()
        .Property(e => e.Status)
        .HasConversion(
          v => v.ToString(),
          v => (AccountStatus)Enum.Parse(typeof(AccountStatus), v));

      modelBuilder
        .Entity<AddressEntity>()
        .Property(e => e.Type)
        .HasConversion(
          v => v.ToString(),
          v => (AddressType)Enum.Parse(typeof(AddressType), v));

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
