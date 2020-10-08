using System;
using System.Collections.Generic;
using System.Linq;
using DemoExchange.Interface;
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

  public class OrderContext : DemoExchangeDbContext, IOrderContext {
    public DbSet<OrderEntity> Orders { get; private set; }
    public DbSet<TransactionEntity> Transactions { get; private set; }

    // Queries
    public IQueryable<OrderEntity> GetAllOpenOrdersByTickerAndAction(String ticker,
      OrderAction type) {
      // TODO Test
      return Orders.Where(OpenByTickerAndAction(ticker, type)).AsQueryable();
    }
  }
}
