using System;
using System.Collections.Generic;
using System.Linq;
using DemoExchange.Contexts;
using DemoExchange.Interface;
using DemoExchange.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace DemoExchange.Services {
  public class OrderBookTest {
    [Fact]
    [Trait("Category", "Unit")]
    public void AddOrderTest() {
      OrderBook book = new OrderBook("ERX", OrderAction.BUY);
      Exception e = Assert.Throws<ArgumentNullException>(() =>
        book.AddOrder(null));
      Assert.Equal("Value cannot be null. (Parameter 'order')",
        e.Message);
      Order buyMarket = TestUtils.NewBuyMarketOrder("acct", "E", 100);
      e = Assert.Throws<ArgumentException>(() =>
        book.AddOrder(buyMarket));
      Assert.Equal(String.Format(OrderBook.ERROR_MARKET_ORDER, buyMarket.OrderId),
        e.Message);
      Order buyLimit = TestUtils.NewBuyLimitDayOrder("acct", "E", 100, 0.50M);
      buyLimit.Cancel();
      e = Assert.Throws<ArgumentException>(() =>
        book.AddOrder(buyLimit));
      Assert.Equal(String.Format(OrderBook.ERROR_NOT_OPEN_ORDER, buyLimit.OrderId),
        e.Message);
      buyLimit = TestUtils.NewBuyLimitDayOrder("acct", "E", 100, 0.50M);
      e = Assert.Throws<ArgumentException>(() =>
        book.AddOrder(buyLimit));
      Assert.Equal(String.Format(OrderBook.ERROR_TICKER, "ERX", buyLimit.OrderId),
        e.Message);
      Order sellLimit = TestUtils.NewSellLimitDayOrder("acct", "ERX", 100, 18.81M);
      e = Assert.Throws<ArgumentException>(() =>
        book.AddOrder(sellLimit));
      Assert.Equal(String.Format(OrderBook.ERROR_ACTION, OrderAction.BUY, sellLimit.Action),
        e.Message);

      Order order1 = TestUtils.NewBuyLimitDayOrder("acct", "ERX", 100, 18.81M);
      Order order2 = TestUtils.NewBuyLimitDayOrder("acct", "ERX", 100, 18.82M);
      Order order3 = TestUtils.NewBuyLimitDayOrder("acct", "ERX", 100, 18.83M);
      Order order4 = TestUtils.NewBuyLimitDayOrder("acct", "ERX", 100, 18.84M);
      Order order5 = TestUtils.NewBuyLimitDayOrder("acct", "ERX", 100, 18.85M);
      book.AddOrder(order3);
      book.AddOrder(order5);
      book.AddOrder(order1);
      book.AddOrder(order2);
      book.AddOrder(order4);

      e = Assert.Throws<ArgumentException>(() =>
        book.AddOrder(order1));
      Assert.Equal(String.Format(OrderBook.ERROR_ORDER_EXISTS, order1.OrderId),
        e.Message);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void OrderQueueTest_Buy() {
      OrderBook book = new OrderBook("ERX", OrderAction.BUY);
      List<Order> orders = book.TestOrders;
      Comparer<IOrderModel> comparer = book.TestComparer;
      TestOrder order1 = new TestOrder("accountId", "ERX", OrderType.LIMIT, 100, 1);
      String order1Id = order1.OrderId;
      order1.StrikePrice = 1;
      order1.CreatedTimestamp = 1;
      TestOrder order2 = new TestOrder("accountId", "ERX", OrderType.LIMIT, 100, 1);
      String order2Id = order2.OrderId;
      order2.StrikePrice = 2;
      order2.CreatedTimestamp = 2;
      TestOrder order3 = new TestOrder("accountId", "ERX", OrderType.LIMIT, 100, 1);
      String order3Id = order3.OrderId;
      order3.StrikePrice = 3;
      order3.CreatedTimestamp = 3;
      orders.Add(order1);
      orders.Add(order2);
      orders.Add(order3);
      orders.Sort(comparer);
      Assert.Equal(order3Id, book.First.OrderId);
      Assert.Equal(order2Id, orders[1].OrderId);
      Assert.Equal(order1Id, orders[2].OrderId);
      order1.StrikePrice = 8;
      orders.Sort(comparer);
      Assert.Equal(order1Id, book.First.OrderId);
      Assert.Equal(order3Id, orders[1].OrderId);
      Assert.Equal(order2Id, orders[2].OrderId);
      order1.StrikePrice = 8;
      order1.CreatedTimestamp = 1000;
      order2.StrikePrice = 8;
      order2.CreatedTimestamp = 100;
      order3.StrikePrice = 8;
      order3.CreatedTimestamp = 10;
      orders.Sort(comparer);
      Assert.Equal(order3Id, book.First.OrderId);
      Assert.Equal(order2Id, orders[1].OrderId);
      Assert.Equal(order1Id, orders[2].OrderId);
      order3.StrikePrice = 6;
      orders.Sort(comparer);
      Assert.Equal(order2Id, book.First.OrderId);
      Assert.Equal(order1Id, orders[1].OrderId);
      Assert.Equal(order3Id, orders[2].OrderId);
      order1.StrikePrice = 6;
      order2.StrikePrice = 6;
      orders.Sort(comparer);
      Assert.Equal(order3Id, book.First.OrderId);
      Assert.Equal(order2Id, orders[1].OrderId);
      Assert.Equal(order1Id, orders[2].OrderId);
      order3.CreatedTimestamp = 800;
      orders.Sort(comparer);
      Assert.Equal(order2Id, book.First.OrderId);
      Assert.Equal(order3Id, orders[1].OrderId);
      Assert.Equal(order1Id, orders[2].OrderId);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void OrderQueueTest_Sell() {
      OrderBook book = new OrderBook("ERX", OrderAction.SELL);
      List<Order> orders = book.TestOrders;
      Comparer<IOrderModel> comparer = book.TestComparer;
      TestOrder order1 = new TestOrder("accountId", OrderAction.SELL, "ERX", OrderType.LIMIT,
        100, 1);
      String order1Id = order1.OrderId;
      order1.StrikePrice = 1;
      order1.CreatedTimestamp = 1;
      TestOrder order2 = new TestOrder("accountId", OrderAction.SELL, "ERX", OrderType.LIMIT,
        100, 1);
      String order2Id = order2.OrderId;
      order2.StrikePrice = 2;
      order2.CreatedTimestamp = 2;
      TestOrder order3 = new TestOrder("accountId", OrderAction.SELL, "ERX", OrderType.LIMIT,
        100, 1);
      String order3Id = order3.OrderId;
      order3.StrikePrice = 3;
      order3.CreatedTimestamp = 3;
      orders.Add(order1);
      orders.Add(order2);
      orders.Add(order3);
      orders.Sort(comparer);
      Assert.Equal(order1Id, book.First.OrderId);
      Assert.Equal(order2Id, orders[1].OrderId);
      Assert.Equal(order3Id, orders[2].OrderId);
      order1.StrikePrice = 8;
      orders.Sort(comparer);
      Assert.Equal(order2Id, book.First.OrderId);
      Assert.Equal(order3Id, orders[1].OrderId);
      Assert.Equal(order1Id, orders[2].OrderId);
      order1.StrikePrice = 8;
      order1.CreatedTimestamp = 1000;
      order2.StrikePrice = 8;
      order2.CreatedTimestamp = 100;
      order3.StrikePrice = 8;
      order3.CreatedTimestamp = 10;
      orders.Sort(comparer);
      Assert.Equal(order3Id, book.First.OrderId);
      Assert.Equal(order2Id, orders[1].OrderId);
      Assert.Equal(order1Id, orders[2].OrderId);
      order3.StrikePrice = 10;
      orders.Sort(comparer);
      Assert.Equal(order2Id, book.First.OrderId);
      Assert.Equal(order1Id, orders[1].OrderId);
      Assert.Equal(order3Id, orders[2].OrderId);
      order1.StrikePrice = 10;
      order2.StrikePrice = 10;
      orders.Sort(comparer);
      Assert.Equal(order3Id, book.First.OrderId);
      Assert.Equal(order2Id, orders[1].OrderId);
      Assert.Equal(order1Id, orders[2].OrderId);
      order3.CreatedTimestamp = 800;
      orders.Sort(comparer);
      Assert.Equal(order2Id, book.First.OrderId);
      Assert.Equal(order3Id, orders[1].OrderId);
      Assert.Equal(order1Id, orders[2].OrderId);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("   ")]
    [InlineData("\n")]
    [Trait("Category", "Unit")]
    public void CancelOrderTest_parameter(String input) {
      String eMsg = "orderId is null, empty, or contains only white-space characters";
      OrderBook book = new OrderBook("ERX", OrderAction.BUY);
      Exception e = Assert.Throws<ArgumentException>(() =>
        book.CancelOrder(input));
      Assert.Equal(eMsg, e.Message);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void CancelOrderTest() {
      OrderBook book = new OrderBook("ERX", OrderAction.BUY);
      Exception e = Assert.Throws<ArgumentException>(() =>
        book.CancelOrder("someId"));
      Assert.Equal("Error Order Not Exists : OrderId: someId", e.Message);

      IDictionary<String, Order> orderIds = book.TestOrderIds;
      List<Order> orders = book.TestOrders;

      for (int i = 0; i < 10; i++) {
        book.AddOrder(TestUtils.NewBuyLimitDayOrder(100M + i));
      }
      Order order = TestUtils.NewBuyLimitDayOrder(101.101M);
      book.AddOrder(order);
      Assert.Equal(11, book.Count);
      Assert.True(orderIds.ContainsKey(order.OrderId));
      book.CancelOrder(order.OrderId);
      Assert.Equal(OrderStatus.CANCELLED, order.Status);
      Assert.NotEqual(0, order.CanceledTimestamp);
      Assert.Equal(10, book.Count);
      Assert.False(orderIds.ContainsKey(order.OrderId));
      Assert.Null(orders.Find(Orders.Predicates.ById(order.OrderId)));
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void OrderBookPropertiesTest() {
      OrderBook book = new OrderBook("ERX", OrderAction.BUY);
      Assert.Equal("ERX BUY", book.Name);
      Assert.Equal(0, book.Count);
      book.AddOrder(TestUtils.NewBuyLimitDayOrder("acct", "ERX", 100, 18.81M));
      Assert.Equal(1, book.Count);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void LoadOrdersTest() {
      OrderBook book = new OrderBook("ERX", OrderAction.BUY);;
      var data = new List<OrderEntity> {
        (OrderEntity)TestUtils.NewBuyLimitDayOrder("acctI", "ERX", 100, 1M),
        (OrderEntity)TestUtils.NewBuyLimitDayOrder("acctI", "ERX", 100, 2M),
        (OrderEntity)TestUtils.NewBuyLimitDayOrder("acctI", "ERX", 100, 3M)
      }.AsQueryable();

      var context = new Mock<IOrderContext>();
      context.Setup(c => c.GetAllOpenOrdersByTickerAndAction("ERX", OrderAction.BUY))
        .Returns(data);

      book.LoadOrders(context.Object);
      Assert.Equal(3, book.TestOrders[0].StrikePrice);
      Assert.Equal(2, book.TestOrders[1].StrikePrice);
      Assert.Equal(1, book.TestOrders[2].StrikePrice);
    }
  }
}
// Mocking DbSet
// var mockOrders = new Mock<DbSet<OrderEntity>>();
// mockOrders.As<IQueryable<OrderEntity>>().Setup(m => m.Provider).Returns(data.Provider);
// mockOrders.As<IQueryable<OrderEntity>>().Setup(m => m.Expression).Returns(data.Expression);
// mockOrders.As<IQueryable<OrderEntity>>().Setup(m => m.ElementType).Returns(data.ElementType);
// mockOrders.As<IQueryable<OrderEntity>>().Setup(m => m.GetEnumerator())
//   .Returns(data.GetEnumerator());
