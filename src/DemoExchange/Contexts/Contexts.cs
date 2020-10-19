using System;
using System.Linq;
using DemoExchange.Api;
using DemoExchange.Models;
using Microsoft.EntityFrameworkCore;
using static DemoExchange.Models.Orders.Predicates;

namespace DemoExchange.Contexts {
  public interface IOrderContext : IDbContext {
    public DbSet<OrderEntity> Orders { get; }
    public DbSet<TransactionEntity> Transactions { get; }

    // Queries
    IQueryable<OrderEntity> GetAllOpenOrdersByTickerAndAction(String ticker,
      OrderAction type);
  }

  public class OrderContextFactory : DemoExchangeDbContextFactory,
    IDemoExchangeDbContextFactory<OrderContext> {

      public OrderContextFactory(ConnectionStrings connectionStrings) : base(connectionStrings) { }

      public override OrderContext Create() {
        return new OrderContext(options);
      }
    }

  public class OrderContext : DemoExchangeDbContext, IOrderContext {
    public DbSet<OrderEntity> Orders { get; private set; }
    public DbSet<TransactionEntity> Transactions { get; private set; }

    // Queries
    public IQueryable<OrderEntity> GetAllOpenOrdersByTickerAndAction(String ticker,
      OrderAction type) {
      // TODO: Test
      return Orders.Where(OpenByTickerAndAction(ticker, type)).AsQueryable();
    }

    public OrderContext(DbContextOptions<DemoExchangeDbContext> options):
      base(options) { }
  }

  public interface IAccountContext : IDbContext {
    public DbSet<AccountEntity> Accounts { get; }
    public DbSet<AddressEntity> Addresses { get; }

    public AccountEntity GetAccountById(String accountId);
  }

  public class AccountContextFactory : DemoExchangeDbContextFactory,
    IDemoExchangeDbContextFactory<AccountContext> {

      public AccountContextFactory(ConnectionStrings connectionStrings) : base(connectionStrings) { }

      public override AccountContext Create() {
        return new AccountContext(options);
      }
    }

  public class AccountContext : DemoExchangeDbContext, IAccountContext {
    public DbSet<AccountEntity> Accounts { get; private set; }
    public DbSet<AddressEntity> Addresses { get; private set; }

    // Queries
    public AccountEntity GetAccountById(String accountId) {
      // TODO: Test
      return Accounts.Find(Guid.Parse(accountId));
    }

    public AccountContext(DbContextOptions<DemoExchangeDbContext> options):
      base(options) { }
  }
}
