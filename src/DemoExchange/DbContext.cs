using System;
using DemoExchange.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoExchange {
  public interface IDemoExchangeContext {
    DbSet<ExchangeOrderEntity> Orders { get; set; }
    DbSet<ExchangeTransactionEntity> Transactions { get; set; }
  }

  public class DemoExchangeContext : DbContext, IDemoExchangeContext {
    public DbSet<ExchangeOrderEntity> Orders { get; set; }
    public DbSet<ExchangeTransactionEntity> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
      options.UseSqlServer("Server=loki,1143;Database=demo_exchange;User Id=demo_exchange;Password=Hello8!8;");
  }
}
