using System;
using DemoExchange.Models;
using Xunit;

namespace DemoExchange.Services {
  public class OrderServiceTest {
    [Fact]
    [Trait("Category", "Unit")]
    public void AddTickerTest() { // TODO

    }

    [Fact]
    [Trait("Category", "Unit")]
    public void SubmitOrderTest() { // TODO

    }

    [Fact]
    [Trait("Category", "Unit")]
    public void CancelOrderTest() { // TODO

    }
  }

  public class OrderManagerTest {
    [Fact]
    [Trait("Category", "Unit")]
    public void SubmitOrderTest() { // TODO

    }

    [Fact]
    [Trait("Category", "Unit")]
    public void FillMarketOrderTest_buy_market() {
      TestOrderManager mgr = new TestOrderManager("ERX");
      Order order1 = TestUtils.NewSellLimitDayOrder(10M);
      Order order2 = TestUtils.NewSellLimitDayOrder(11M);
      Order order3 = TestUtils.NewSellLimitDayOrder(12M);
      mgr.SellBook.AddOrder(order1);
      mgr.SellBook.AddOrder(order2);
      mgr.SellBook.AddOrder(order3);
      BuyMarketOrder order = new BuyMarketOrder("buyer", "ERX", 50);
      OrderTransaction filled = mgr.FillMarketOrder(order, mgr.SellBook);
      Assert.False(mgr.SellBook.First.IsFilled);
      Assert.Equal(50, mgr.SellBook.First.OpenQuantity);
      Assert.Equal(2, filled.Orders.Count);
      Assert.Equal(OrderStatus.COMPLETED, order.Status);
      Assert.True(order.IsFilled);
      Assert.Equal(OrderStatus.OPEN, order1.Status);
      Assert.Equal(50, order1.OpenQuantity);
      Assert.Single(filled.Transactions);
      Transaction tran = filled.Transactions[0];
      Assert.Equal(order.Id, tran.BuyOrderId);
      Assert.Equal(order1.Id, tran.SellOrderId);
      Assert.Equal("ERX", tran.Ticker);
      Assert.Equal(order.Quantity, tran.Quantity);
      Assert.Equal(order1.StrikePrice, tran.Price);

      order = new BuyMarketOrder("buyer", "ERX", 75);
      filled = mgr.FillMarketOrder(order, mgr.SellBook);
      Assert.False(mgr.SellBook.First.IsFilled);
      Assert.Equal(75, mgr.SellBook.First.OpenQuantity);
      Assert.Equal(3, filled.Orders.Count);
      Assert.Equal(OrderStatus.COMPLETED, order.Status);
      Assert.True(order.IsFilled);
      Assert.Equal(OrderStatus.COMPLETED, order1.Status);
      Assert.True(order1.IsFilled);
      Assert.Equal(OrderStatus.OPEN, order2.Status);
      Assert.Equal(75, order2.OpenQuantity);
      Assert.Equal(2, filled.Transactions.Count);
      tran = filled.Transactions[0];
      Assert.Equal(order.Id, tran.BuyOrderId);
      Assert.Equal(order1.Id, tran.SellOrderId);
      Assert.Equal("ERX", tran.Ticker);
      Assert.Equal(50, tran.Quantity);
      Assert.Equal(order1.StrikePrice, tran.Price);
      tran = filled.Transactions[1];
      Assert.Equal(order.Id, tran.BuyOrderId);
      Assert.Equal(order2.Id, tran.SellOrderId);
      Assert.Equal("ERX", tran.Ticker);
      Assert.Equal(25, tran.Quantity);
      Assert.Equal(order2.StrikePrice, tran.Price);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void FillMarketOrderTest_sell_market() {
      TestOrderManager mgr = new TestOrderManager("ERX");
      Order order1 = TestUtils.NewBuyLimitDayOrder(10M);
      Order order2 = TestUtils.NewBuyLimitDayOrder(11M);
      Order order3 = TestUtils.NewBuyLimitDayOrder(12M);
      mgr.BuyBook.AddOrder(order1);
      mgr.BuyBook.AddOrder(order2);
      mgr.BuyBook.AddOrder(order3);
      SellMarketOrder order = new SellMarketOrder("seller", "ERX", 100);
      OrderTransaction filled = mgr.FillMarketOrder(order, mgr.BuyBook);
      Assert.False(mgr.BuyBook.First.IsFilled);
      Assert.Equal(100, mgr.BuyBook.First.OpenQuantity);
      Assert.Equal(2, filled.Orders.Count);
      Assert.Equal(OrderStatus.COMPLETED, order.Status);
      Assert.True(order.IsFilled);
      Assert.Equal(OrderStatus.COMPLETED, order3.Status);
      Assert.True(order3.IsFilled);
      Assert.Single(filled.Transactions);
      Transaction tran = filled.Transactions[0];
      Assert.Equal(order3.Id, tran.BuyOrderId);
      Assert.Equal(order.Id, tran.SellOrderId);
      Assert.Equal("ERX", tran.Ticker);
      Assert.Equal(order.Quantity, tran.Quantity);
      Assert.Equal(order3.StrikePrice, tran.Price);

      order = new SellMarketOrder("buyer", "ERX", 200);
      filled = mgr.FillMarketOrder(order, mgr.BuyBook);
      Assert.True(mgr.BuyBook.IsEmpty);
      Assert.Equal(3, filled.Orders.Count);
      Assert.Equal(OrderStatus.COMPLETED, order.Status);
      Assert.True(order.IsFilled);
      Assert.Equal(OrderStatus.COMPLETED, order2.Status);
      Assert.True(order2.IsFilled);
      Assert.Equal(OrderStatus.COMPLETED, order1.Status);
      Assert.True(order1.IsFilled);
      Assert.Equal(2, filled.Transactions.Count);
      tran = filled.Transactions[0];
      Assert.Equal(order2.Id, tran.BuyOrderId);
      Assert.Equal(order.Id, tran.SellOrderId);
      Assert.Equal("ERX", tran.Ticker);
      Assert.Equal(100, tran.Quantity);
      Assert.Equal(order2.StrikePrice, tran.Price);
      tran = filled.Transactions[1];
      Assert.Equal(order1.Id, tran.BuyOrderId);
      Assert.Equal(order.Id, tran.SellOrderId);
      Assert.Equal("ERX", tran.Ticker);
      Assert.Equal(100, tran.Quantity);
      Assert.Equal(order1.StrikePrice, tran.Price);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void FillLimitOrderTest() { // TODO 

    }

    class TestOrderManager : OrderManager {
      public TestOrderManager(string ticker) : base(ticker) { }

      new public OrderTransaction FillMarketOrder(Order order, OrderBook book) {
        long start = System.Diagnostics.Stopwatch.GetTimestamp();
        OrderTransaction tran = base.FillMarketOrder(order, book);
        long stop = System.Diagnostics.Stopwatch.GetTimestamp();
        Console.WriteLine(String.Format("Market order executed in {0} ms", ((stop - start) / TimeSpan.TicksPerMillisecond)));

        return tran;
      }

      new public OrderBook BuyBook {
        get { return base.BuyBook; }
      }

      new public OrderBook SellBook {
        get { return base.SellBook; }
      }
    }
  }
}
