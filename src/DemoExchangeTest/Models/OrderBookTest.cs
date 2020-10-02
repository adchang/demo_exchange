using System;
using System.Collections.Generic;
using Xunit;

namespace DemoExchange.Models {
  public class OrderBookTest {
    [Fact]
    [Trait("Category", "Unit")]
    public void AddOrderTest() {
      OrderBook book = new OrderBook("ERX", OrderAction.BUY);
      Exception e = Assert.Throws<ArgumentNullException>(() =>
        book.AddOrder(null));
      Assert.Equal("Value cannot be null. (Parameter 'order')",
        e.Message);
      BuyMarketOrder buyMarket = new BuyMarketOrder("acct", "E", 100);
      e = Assert.Throws<ArgumentException>(() =>
        book.AddOrder(buyMarket));
      Assert.Equal(String.Format(OrderBook.ERROR_MARKET_ORDER, buyMarket.Id),
        e.Message);
      BuyLimitDayOrder buyLimit = new BuyLimitDayOrder("acct", "E", 100, 0.50M) {
        Status = OrderStatus.CANCELLED
      };
      e = Assert.Throws<ArgumentException>(() =>
        book.AddOrder(buyLimit));
      Assert.Equal(String.Format(OrderBook.ERROR_NOT_OPEN_ORDER, buyLimit.Id),
        e.Message);
      buyLimit.Status = OrderStatus.OPEN;
      e = Assert.Throws<ArgumentException>(() =>
        book.AddOrder(buyLimit));
      Assert.Equal(String.Format(OrderBook.ERROR_TICKER, "ERX", buyLimit.Id),
        e.Message);
      SellLimitDayOrder sellLimit = new SellLimitDayOrder("acct", "ERX", 100, 18.81M);
      e = Assert.Throws<ArgumentException>(() =>
        book.AddOrder(sellLimit));
      Assert.Equal(String.Format(OrderBook.ERROR_ACTION, OrderAction.BUY, sellLimit.Action),
        e.Message);

      BuyLimitDayOrder order1 = new BuyLimitDayOrder("acct", "ERX", 100, 18.81M);
      BuyLimitDayOrder order2 = new BuyLimitDayOrder("acct", "ERX", 100, 18.82M);
      BuyLimitDayOrder order3 = new BuyLimitDayOrder("acct", "ERX", 100, 18.83M);
      BuyLimitDayOrder order4 = new BuyLimitDayOrder("acct", "ERX", 100, 18.84M);
      BuyLimitDayOrder order5 = new BuyLimitDayOrder("acct", "ERX", 100, 18.85M);
      book.AddOrder(order3);
      book.AddOrder(order5);
      book.AddOrder(order1);
      book.AddOrder(order2);
      book.AddOrder(order4);

      e = Assert.Throws<ArgumentException>(() =>
        book.AddOrder(order1));
      Assert.Equal(String.Format(OrderBook.ERROR_ORDER_EXISTS, order1.Id),
        e.Message);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void OrderQueueTest_Buy() {
      TestOrderBook book = new TestOrderBook("ERX", OrderAction.BUY);
      List<Order> orders = book.Orders();
      Comparer<Order> comparer = book.Comparer();
      OrderEntity order1 = new OrderEntity {
        Id = "1",
        Ticker = "ERX",
        Type = OrderType.LIMIT,
        StrikePrice = 1,
        CreatedTimestamp = 1
      };
      OrderEntity order2 = (OrderEntity)order1.ShallowCopy();
      order2.Id = "2";
      order2.StrikePrice = 2;
      order2.CreatedTimestamp = 2;
      OrderEntity order3 = (OrderEntity)order1.ShallowCopy();
      order3.Id = "3";
      order3.Ticker = "ERX";
      order3.Type = OrderType.LIMIT;
      order3.StrikePrice = 3;
      order3.CreatedTimestamp = 3;
      book.AddOrder(order1);
      book.AddOrder(order2);
      book.AddOrder(order3);
      Assert.Equal("3", orders[0].Id);
      Assert.Equal("2", orders[1].Id);
      Assert.Equal("1", orders[2].Id);
      order1.StrikePrice = 8;
      orders.Sort(comparer);
      Assert.Equal("1", orders[0].Id);
      Assert.Equal("3", orders[1].Id);
      Assert.Equal("2", orders[2].Id);
      order1.StrikePrice = 8;
      order1.CreatedTimestamp = 1000;
      order2.StrikePrice = 8;
      order2.CreatedTimestamp = 100;
      order3.StrikePrice = 8;
      order3.CreatedTimestamp = 10;
      orders.Sort(comparer);
      Assert.Equal("3", orders[0].Id);
      Assert.Equal("2", orders[1].Id);
      Assert.Equal("1", orders[2].Id);
      order3.StrikePrice = 6;
      orders.Sort(comparer);
      Assert.Equal("2", orders[0].Id);
      Assert.Equal("1", orders[1].Id);
      Assert.Equal("3", orders[2].Id);
      order1.StrikePrice = 6;
      order2.StrikePrice = 6;
      orders.Sort(comparer);
      Assert.Equal("3", orders[0].Id);
      Assert.Equal("2", orders[1].Id);
      Assert.Equal("1", orders[2].Id);
      order3.CreatedTimestamp = 800;
      orders.Sort(comparer);
      Assert.Equal("2", orders[0].Id);
      Assert.Equal("3", orders[1].Id);
      Assert.Equal("1", orders[2].Id);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void OrderQueueTest_Sell() {
      TestOrderBook book = new TestOrderBook("ERX", OrderAction.SELL);
      List<Order> orders = book.Orders();
      Comparer<Order> comparer = book.Comparer();
      OrderEntity order1 = new OrderEntity {
        Id = "1",
        Ticker = "ERX",
        Action = OrderAction.SELL,
        Type = OrderType.LIMIT,
        StrikePrice = 1,
        CreatedTimestamp = 1
      };
      OrderEntity order2 = (OrderEntity)order1.ShallowCopy();
      order2.Id = "2";
      order2.StrikePrice = 2;
      order2.CreatedTimestamp = 2;
      OrderEntity order3 = (OrderEntity)order1.ShallowCopy();
      order3.Id = "3";
      order3.Ticker = "ERX";
      order3.Type = OrderType.LIMIT;
      order3.StrikePrice = 3;
      order3.CreatedTimestamp = 3;
      book.AddOrder(order1);
      book.AddOrder(order2);
      book.AddOrder(order3);
      Assert.Equal("1", orders[0].Id);
      Assert.Equal("2", orders[1].Id);
      Assert.Equal("3", orders[2].Id);
      order1.StrikePrice = 8;
      orders.Sort(comparer);
      Assert.Equal("2", orders[0].Id);
      Assert.Equal("3", orders[1].Id);
      Assert.Equal("1", orders[2].Id);
      order1.StrikePrice = 8;
      order1.CreatedTimestamp = 1000;
      order2.StrikePrice = 8;
      order2.CreatedTimestamp = 100;
      order3.StrikePrice = 8;
      order3.CreatedTimestamp = 10;
      orders.Sort(comparer);
      Assert.Equal("3", orders[0].Id);
      Assert.Equal("2", orders[1].Id);
      Assert.Equal("1", orders[2].Id);
      order3.StrikePrice = 10;
      orders.Sort(comparer);
      Assert.Equal("2", orders[0].Id);
      Assert.Equal("1", orders[1].Id);
      Assert.Equal("3", orders[2].Id);
      order1.StrikePrice = 10;
      order2.StrikePrice = 10;
      orders.Sort(comparer);
      Assert.Equal("3", orders[0].Id);
      Assert.Equal("2", orders[1].Id);
      Assert.Equal("1", orders[2].Id);
      order3.CreatedTimestamp = 800;
      orders.Sort(comparer);
      Assert.Equal("2", orders[0].Id);
      Assert.Equal("3", orders[1].Id);
      Assert.Equal("1", orders[2].Id);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("   ")]
    [InlineData("\n")]
    [Trait("Category", "Unit")]
    public void CancelOrderTest_parameter(String input) {
      String eMsg = "Id is null, empty, or contains only white-space characters";
      OrderBook book = new OrderBook("ERX", OrderAction.BUY);
      Exception e = Assert.Throws<ArgumentException>(() =>
        book.CancelOrder(input));
      Assert.Equal(eMsg, e.Message);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void CancelOrderTest() {
      TestOrderBook book = new TestOrderBook("ERX", OrderAction.BUY);
      Exception e = Assert.Throws<ArgumentException>(() =>
        book.CancelOrder("someId"));
      Assert.Equal("Error Order Not Exists : Order Id: someId", e.Message);

      IDictionary<String, Order> orderIds = book.OrderIds();
      List<Order> orders = book.Orders();

      for (int i = 0; i < 10; i++) {
        book.AddOrder(TestUtils.NewBuyLimitDayOrder(100M + i));
      }
      Order order = TestUtils.NewBuyLimitDayOrder(101.101M);
      book.AddOrder(order);
      Assert.Equal(11, book.Count);
      Assert.True(orderIds.ContainsKey(order.Id));
      book.CancelOrder(order.Id);
      Assert.Equal(OrderStatus.CANCELLED, order.Status);
      Assert.NotEqual(0, order.CanceledTimestamp);
      Assert.Equal(10, book.Count);
      Assert.False(orderIds.ContainsKey(order.Id));
      Assert.Null(orders.Find(Orders.ById(order.Id)));

    }

    [Fact]
    [Trait("Category", "Unit")]
    public void OrderBookPropertiesTest() {
      OrderBook book = new OrderBook("ERX", OrderAction.BUY);
      Assert.Equal("ERX BUY", book.Name);
      Assert.Equal(0, book.Count);
      book.AddOrder(new BuyLimitDayOrder("acct", "ERX", 100, 18.81M));
      Assert.Equal(1, book.Count);
    }
  }

  class TestOrderBook : OrderBook {
    public TestOrderBook(String ticker, OrderAction type) : base(ticker, type) { }

    public IDictionary<String, Order> OrderIds() {
      return orderIds;
    }

    public List<Order> Orders() {
      return orders;
    }

    public Comparer<Order> Comparer() {
      return comparer;
    }
  }
}
