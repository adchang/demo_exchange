using System;
using DemoExchange.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoExchange.Contexts {
  public interface IOrderContext : IDbContext {
    public DbSet<OrderEntity> Orders { get; }
    public DbSet<TransactionEntity> Transactions { get; }
  }

  public class OrderContext : DemoExchangeDbContext, IOrderContext {
    public DbSet<OrderEntity> Orders { get; private set; }
    public DbSet<TransactionEntity> Transactions { get; private set; }
  }
}
