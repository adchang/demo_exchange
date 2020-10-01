using System;
using Xunit;

namespace DemoExchange.Models {
  public class OrderTest {
    [Fact]
    [Trait("Category", "Unit")]
    public void OrderTest_constructor() {
      OrderEntity test = new OrderEntity();
      test.Id = "123";
      Order test2 = (Order)test;
      Assert.Equal("123", test.Id);
      Assert.Equal("123", test2.Id);

      Exception e = Assert.Throws<ArgumentException>(() =>
        new TestOrder(null, null, OrderType.MARKET, 0, 0));
      Assert.Equal("AccountId is null, empty, or contains only white-space characters",
        e.Message);
      e = Assert.Throws<ArgumentException>(() =>
        new TestOrder("accountId", null, OrderType.MARKET, 0, 0));
      Assert.Equal("Ticker is null, empty, or contains only white-space characters",
        e.Message);
      e = Assert.Throws<ArgumentException>(() =>
        new TestOrder("accountId", "ERX", OrderType.MARKET, 0, 0));
      Assert.Equal("Quantity must be greater than 0",
        e.Message);
      e = Assert.Throws<ArgumentException>(() =>
        new TestOrder("accountId", "ERX", OrderType.MARKET, -100, 0));
      Assert.Equal("Quantity must be greater than 0",
        e.Message);
      e = Assert.Throws<ArgumentException>(() =>
        new TestOrder("accountId", "ERX", OrderType.MARKET, 100, -1));
      Assert.Equal("StrikePrice should be 0 for Market orders",
        e.Message);
      e = Assert.Throws<ArgumentException>(() =>
        new TestOrder("accountId", "ERX", OrderType.LIMIT, 100, -1));
      Assert.Equal("StrikePrice must be greater than 0",
        e.Message);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void OrderEntityTest() {
      OrderEntity order = new OrderEntity();
      order.Id = "Id";
      order.CreatedTimestamp = 1;
      order.AccountId = "accountId";
      order.Status = OrderStatus.OPEN;
      order.Action = OrderAction.BUY;
      order.Ticker = "ERX";
      order.Type = OrderType.MARKET;
      order.Quantity = 100;
      order.OpenQuantity = 200;
      order.StrikePrice = 0;
      order.TimeInForce = OrderTimeInForce.DAY;
      order.ToBeCanceledTimestamp = 8;
      Assert.Equal("Id", order.Id);
      Assert.Equal(1, order.CreatedTimestamp);
      Assert.Equal("accountId", order.AccountId);
      Assert.Equal(OrderStatus.OPEN, order.Status);
      Assert.Equal(OrderAction.BUY, order.Action);
      Assert.Equal("ERX", order.Ticker);
      Assert.Equal(OrderType.MARKET, order.Type);
      Assert.Equal(100, order.Quantity);
      Assert.Equal(200, order.OpenQuantity);
      Assert.Equal(0, order.StrikePrice);
      Assert.Equal(OrderTimeInForce.DAY, order.TimeInForce);
      Assert.Equal(8, order.ToBeCanceledTimestamp);
      Order baseOrder = (Order)order;
      Assert.Equal("Id", baseOrder.Id);
      Assert.Equal(1, baseOrder.CreatedTimestamp);
      Assert.Equal("accountId", baseOrder.AccountId);
      Assert.Equal(OrderStatus.OPEN, baseOrder.Status);
      Assert.Equal(OrderAction.BUY, baseOrder.Action);
      Assert.Equal("ERX", baseOrder.Ticker);
      Assert.Equal(OrderType.MARKET, baseOrder.Type);
      Assert.Equal(100, baseOrder.Quantity);
      Assert.Equal(200, baseOrder.OpenQuantity);
      Assert.Equal(0, baseOrder.StrikePrice);
      Assert.Equal(OrderTimeInForce.DAY, baseOrder.TimeInForce);
      Assert.Equal(8, baseOrder.ToBeCanceledTimestamp);
      OrderEntity order2 = new OrderEntity();
      order2.Id = "Id";
      order2.CreatedTimestamp = 1;
      order2.AccountId = "accountId";
      order2.Status = OrderStatus.OPEN;
      order2.Action = OrderAction.BUY;
      order2.Ticker = "ERX";
      order2.Type = OrderType.MARKET;
      order2.Quantity = 100;
      order2.OpenQuantity = 200;
      order2.StrikePrice = 0;
      order2.TimeInForce = OrderTimeInForce.DAY;
      order2.ToBeCanceledTimestamp = 8;
      Assert.False(order == order2);
      Assert.False(order.Equals(null));
      Assert.True(order.Equals(order2));
      Assert.Equal(order.GetHashCode(), order2.GetHashCode());
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void MarketOrderTest() {
      Order order = new BuyMarketOrder("accountId", "ERX", 100);
      Assert.False(String.IsNullOrWhiteSpace(order.Id));
      Assert.Equal(new DateTime(order.CreatedTimestamp), order.CreatedDateTime);
      Assert.True(order.IsOpen);
      Assert.False(order.IsCompleted);
      Assert.False(order.IsUpdated);
      Assert.False(order.IsCancelled);
      Assert.False(order.IsDeleted);
      Assert.True(order.IsBuyOrder);
      Assert.False(order.IsSellOrder);
      Assert.True(order.IsMarketOrder);
      Assert.False(order.IsLimitOrder);
      Assert.False(order.IsStopMarketOrder);
      Assert.False(order.IsStopLimitOrder);
      Assert.False(order.IsTrailingStopMarketOrder);
      Assert.False(order.IsTrailingStopLimitOrder);
      Assert.False(order.IsFillOrKillOrder);
      Assert.False(order.IsImmediateOrCancelOrder);
      Assert.Equal(order.Quantity, order.OpenQuantity);
      Assert.Equal(0, order.StrikePrice);
      Assert.True(order.IsDayOrder);
      Assert.False(order.IsGoodTillCanceledOrder);
      Assert.Equal(new DateTime(order.ToBeCanceledTimestamp), order.ToBeCanceledDateTime);
      Assert.Equal("{Id: " + order.Id + ", CreatedTimestamp: " + order.CreatedTimestamp + ", " +
        "AccountId: accountId, Status: OPEN, Action: BUY, Ticker: ERX, Type: MARKET, " +
        "Quantity: 100, OpenQuantity: 100, StrikePrice: 0, " +
        "TimeInForce: DAY, ToBeCanceledTimestamp: 0, }",
        order.ToString());

      Assert.False(order.IsFilled);
      order.OpenQuantity = 0;
      Assert.True(order.IsFilled);

      order = new SellMarketOrder("accountId", "ERX", 100);
      Assert.Equal("accountId", order.AccountId);
      Assert.True(order.IsOpen);
      Assert.True(order.IsSellOrder);
      Assert.Equal("ERX", order.Ticker);
      Assert.True(order.IsMarketOrder);
      Assert.Equal(100, order.Quantity);
      Assert.Equal(0, order.StrikePrice);
      Assert.True(order.IsDayOrder);
    }

    class TestOrder : Order {
      public TestOrder(String accountId, String ticker, OrderType type,
        int quantity, decimal strikePrice) : base(accountId,
        OrderAction.BUY, ticker, type, quantity, strikePrice, OrderTimeInForce.DAY) { }
    }
  }
}
