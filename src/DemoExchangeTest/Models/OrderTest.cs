using System;
using DemoExchange.Interface;
using Xunit;
using static Utils.Time;

namespace DemoExchange.Models {
  public class OrderTest {
    [Fact]
    [Trait("Category", "Unit")]
    public void OrderEntityTest() {
      Guid orderId = Guid.NewGuid();
      OrderEntity orderEntity = new OrderEntity {
        OrderId = orderId,
        CreatedTimestamp = 1,
        AccountId = "accountId",
        Status = OrderStatus.OPEN,
        Action = OrderAction.BUY,
        Ticker = "ERX",
        Type = OrderType.MARKET,
        Quantity = 100,
        OpenQuantity = 200,
        OrderPrice = 1.12345678M,
        StrikePrice = 2.12345678M,
        TimeInForce = OrderTimeInForce.DAY,
        ToBeCanceledTimestamp = 8,
        CanceledTimestamp = 18
      };
      Assert.Equal(orderId, orderEntity.OrderId);
      Assert.Equal(1, orderEntity.CreatedTimestamp);
      Assert.Equal("accountId", orderEntity.AccountId);
      Assert.Equal(OrderStatus.OPEN, orderEntity.Status);
      Assert.Equal(OrderAction.BUY, orderEntity.Action);
      Assert.Equal("ERX", orderEntity.Ticker);
      Assert.Equal(OrderType.MARKET, orderEntity.Type);
      Assert.Equal(100, orderEntity.Quantity);
      Assert.Equal(200, orderEntity.OpenQuantity);
      Assert.Equal(1.12345678M, orderEntity.OrderPrice);
      Assert.Equal(2.12345678M, orderEntity.StrikePrice);
      Assert.Equal(OrderTimeInForce.DAY, orderEntity.TimeInForce);
      Assert.Equal(8, orderEntity.ToBeCanceledTimestamp);
      Assert.Equal(18, orderEntity.CanceledTimestamp);
      Order order = new TestOrder(orderEntity);
      Assert.Equal(1, order.CreatedTimestamp);
      Assert.Equal("accountId", order.AccountId);
      Assert.Equal(OrderStatus.OPEN, order.Status);
      Assert.Equal(OrderAction.BUY, order.Action);
      Assert.Equal("ERX", order.Ticker);
      Assert.Equal(OrderType.MARKET, order.Type);
      Assert.Equal(100, order.Quantity);
      Assert.Equal(200, order.OpenQuantity);
      Assert.Equal(1.12345678M, order.OrderPrice);
      Assert.Equal(2.12345678M, order.StrikePrice);
      Assert.Equal(OrderTimeInForce.DAY, order.TimeInForce);
      Assert.Equal(8, order.ToBeCanceledTimestamp);
      Assert.Equal(18, order.CanceledTimestamp);
      OrderEntity orderEntity2 = new OrderEntity {
        OrderId = orderId,
        CreatedTimestamp = 1,
        AccountId = "accountId",
        Status = OrderStatus.OPEN,
        Action = OrderAction.BUY,
        Ticker = "ERX",
        Type = OrderType.MARKET,
        Quantity = 100,
        OpenQuantity = 200,
        OrderPrice = 1.12345678M,
        StrikePrice = 2.12345678M,
        TimeInForce = OrderTimeInForce.DAY,
        ToBeCanceledTimestamp = 8,
        CanceledTimestamp = 18
      };
      Assert.False(orderEntity == orderEntity2);
      Assert.False(orderEntity.Equals(null));
      Assert.Equal(orderEntity.ToString(), order.ToString());
      Assert.True(order.Equals(orderEntity));
      Assert.Equal(orderEntity.ToString(), orderEntity2.ToString());
      Assert.True(orderEntity.Equals(orderEntity2));
      Assert.Equal(orderEntity.GetHashCode(), orderEntity2.GetHashCode());
      OrderEntity up = order;
      Assert.Equal(orderId, up.OrderId);
      Assert.Equal(1, up.CreatedTimestamp);
      Assert.Equal("accountId", up.AccountId);
      Assert.Equal(OrderStatus.OPEN, up.Status);
      Assert.Equal(OrderAction.BUY, up.Action);
      Assert.Equal("ERX", up.Ticker);
      Assert.Equal(OrderType.MARKET, up.Type);
      Assert.Equal(100, up.Quantity);
      Assert.Equal(200, up.OpenQuantity);
      Assert.Equal(1.12345678M, up.OrderPrice);
      Assert.Equal(2.12345678M, up.StrikePrice);
      Assert.Equal(OrderTimeInForce.DAY, up.TimeInForce);
      Assert.Equal(8, up.ToBeCanceledTimestamp);
      Assert.Equal(18, up.CanceledTimestamp);
      up.CreatedTimestamp += 10;;
      up.AccountId += "-Up";
      up.Status = OrderStatus.COMPLETED;
      up.Action = OrderAction.SELL;
      up.Ticker += "-Up";
      up.Type = OrderType.LIMIT;
      up.Quantity += 1;
      up.OpenQuantity += 1;
      up.OrderPrice += 10;
      up.StrikePrice += 10;
      up.TimeInForce = OrderTimeInForce.GOOD_TIL_CANCELED;
      up.ToBeCanceledTimestamp += 10;
      up.CanceledTimestamp += 10;
      Assert.Equal(11, up.CreatedTimestamp);
      Assert.Equal("accountId-Up", up.AccountId);
      Assert.Equal(OrderStatus.COMPLETED, up.Status);
      Assert.Equal(OrderAction.SELL, up.Action);
      Assert.Equal("ERX-Up", up.Ticker);
      Assert.Equal(OrderType.LIMIT, up.Type);
      Assert.Equal(101, up.Quantity);
      Assert.Equal(201, up.OpenQuantity);
      Assert.Equal(11.12345678M, up.OrderPrice);
      Assert.Equal(12.12345678M, up.StrikePrice);
      Assert.Equal(OrderTimeInForce.GOOD_TIL_CANCELED, up.TimeInForce);
      Assert.Equal(18, up.ToBeCanceledTimestamp);
      Assert.Equal(28, up.CanceledTimestamp);
      Assert.True(up.Equals(order));
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void OrderTest_constructor() {
      Exception e = Assert.Throws<ArgumentException>(() =>
        new TestOrder(null, null, OrderType.MARKET, 0, 0));
      Assert.Equal("accountId is null, empty, or contains only white-space characters",
        e.Message);
      e = Assert.Throws<ArgumentException>(() =>
        new TestOrder("accountId", null, OrderType.MARKET, 0, 0));
      Assert.Equal("ticker is null, empty, or contains only white-space characters",
        e.Message);
      e = Assert.Throws<ArgumentException>(() =>
        new TestOrder("accountId", "ERX", OrderType.MARKET, 0, 0));
      Assert.Equal(IModelOrder.ERROR_QUANTITY_IS_0,
        e.Message);
      e = Assert.Throws<ArgumentException>(() =>
        new TestOrder("accountId", "ERX", OrderType.MARKET, -100, 0));
      Assert.Equal(IModelOrder.ERROR_QUANTITY_IS_0,
        e.Message);
      e = Assert.Throws<ArgumentException>(() =>
        new TestOrder("accountId", "ERX", OrderType.MARKET, 100, -1));
      Assert.Equal(IModelOrder.ERROR_ORDER_PRICE_MARKET_NOT_0,
        e.Message);
      e = Assert.Throws<ArgumentException>(() =>
        new TestOrder("accountId", "ERX", OrderType.LIMIT, 100, -1));
      Assert.Equal(IModelOrder.ERROR_ORDER_PRICE_IS_0,
        e.Message);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void MarketOrderTest() {
      /* TODO Move to InterfaceTest project
      MarketOrder order = new BuyMarketOrder("accountId", "ERX", 100);
      Assert.False(String.IsNullOrWhiteSpace(order.OrderId));
      Assert.Equal(new DateTime(order.CreatedTimestamp), order.CreatedDateTime);
      Assert.Equal("accountId", order.AccountId);
      Assert.True(order.IsOpen);
      Assert.False(order.IsCompleted);
      Assert.False(order.IsUpdated);
      Assert.False(order.IsCancelled);
      Assert.False(order.IsDeleted);
      Assert.True(order.IsBuyOrder);
      Assert.False(order.IsSellOrder);
      Assert.Equal("ERX", order.Ticker);
      Assert.True(order.IsMarketOrder);
      Assert.False(order.IsLimitOrder);
      Assert.False(order.IsStopMarketOrder);
      Assert.False(order.IsStopLimitOrder);
      Assert.False(order.IsTrailingStopMarketOrder);
      Assert.False(order.IsTrailingStopLimitOrder);
      Assert.False(order.IsFillOrKillOrder);
      Assert.False(order.IsImmediateOrCancelOrder);
      Assert.Equal(order.Quantity, order.OpenQuantity);
      Assert.Equal(0, order.OrderPrice);
      Assert.Equal(0, order.StrikePrice);
      Assert.True(order.IsDayOrder);
      Assert.False(order.IsGoodTillCanceledOrder);
      Assert.Equal(new DateTime(order.ToBeCanceledTimestamp), order.ToBeCanceledDateTime);
      Assert.Equal("{OrderId: " + order.OrderId + ", " +
        "CreatedTimestamp: " + order.CreatedTimestamp + ", " +
        "AccountId: accountId, Status: OPEN, Action: BUY, Ticker: ERX, Type: MARKET, " +
        "Quantity: 100, OpenQuantity: 100, " +
        "OrderPrice: 0.0000000000, StrikePrice: 0.0000000000, " +
        "TimeInForce: DAY, ToBeCanceledTimestamp: 0, CanceledTimestamp: 0, }",
        order.ToString());

      Assert.False(order.IsFilled);
      Exception e = Assert.Throws<InvalidOperationException>(() =>
        order.OpenQuantity = 1000);
      Assert.Equal(Order.ERROR_OPEN_QUANITY_GREATER_THAN_QUANITY,
        e.Message);

      order.OpenQuantity = 0;
      Assert.Equal(0, order.OpenQuantity);
      Assert.True(order.IsFilled);

      order = new SellMarketOrder("accountId", "ERX", 100);
      Assert.False(String.IsNullOrWhiteSpace(order.OrderId));
      Assert.Equal("accountId", order.AccountId);
      Assert.True(order.IsOpen);
      Assert.True(order.IsSellOrder);
      Assert.Equal("ERX", order.Ticker);
      Assert.True(order.IsMarketOrder);
      Assert.Equal(100, order.Quantity);
      Assert.Equal(0, order.OrderPrice);
      Assert.Equal(0, order.StrikePrice);
      Assert.True(order.IsDayOrder);
      */
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void LimitOrderTest() {
      /* TODO Move to InterfaceTest project
      Order order = new BuyLimitDayOrder("accountId", "ERX", 100, 18.81018M);
      Assert.False(String.IsNullOrWhiteSpace(order.OrderId));
      Assert.Equal("accountId", order.AccountId);
      Assert.True(order.IsOpen);
      Assert.True(order.IsBuyOrder);
      Assert.Equal("ERX", order.Ticker);
      Assert.True(order.IsLimitOrder);
      Assert.Equal(100, order.Quantity);
      Assert.Equal(18.81018M, order.OrderPrice);
      Assert.Equal(18.81018M, order.StrikePrice);
      Assert.True(order.IsDayOrder);
      Assert.Equal(MidnightIct, order.ToBeCanceledTimestamp);

      order = new BuyLimitGoodTilCanceledOrder("accountId", "ERX", 100, 18.81018M);
      Assert.False(String.IsNullOrWhiteSpace(order.OrderId));
      Assert.Equal("accountId", order.AccountId);
      Assert.True(order.IsOpen);
      Assert.True(order.IsBuyOrder);
      Assert.True(order.IsLimitOrder);
      Assert.Equal("ERX", order.Ticker);
      Assert.Equal(100, order.Quantity);
      Assert.Equal(18.81018M, order.OrderPrice);
      Assert.Equal(18.81018M, order.StrikePrice);
      Assert.True(order.IsGoodTillCanceledOrder);
      Assert.Equal(order.CreatedTimestamp +
        (TimeSpan.TicksPerDay * Order.TIME_IN_FORCE_TO_BE_CANCELLED_DAYS),
        order.ToBeCanceledTimestamp);

      order = new SellLimitDayOrder("accountId", "ERX", 100, 18.81018M);
      Assert.False(String.IsNullOrWhiteSpace(order.OrderId));
      Assert.Equal("accountId", order.AccountId);
      Assert.True(order.IsOpen);
      Assert.True(order.IsSellOrder);
      Assert.Equal("ERX", order.Ticker);
      Assert.True(order.IsLimitOrder);
      Assert.Equal(100, order.Quantity);
      Assert.Equal(18.81018M, order.OrderPrice);
      Assert.Equal(18.81018M, order.StrikePrice);
      Assert.True(order.IsDayOrder);
      Assert.Equal(MidnightIct, order.ToBeCanceledTimestamp);

      order = new SellLimitGoodTilCanceledOrder("accountId", "ERX", 100, 18.81018M);
      Assert.False(String.IsNullOrWhiteSpace(order.OrderId));
      Assert.Equal("accountId", order.AccountId);
      Assert.True(order.IsOpen);
      Assert.True(order.IsSellOrder);
      Assert.Equal("ERX", order.Ticker);
      Assert.True(order.IsLimitOrder);
      Assert.Equal(100, order.Quantity);
      Assert.Equal(18.81018M, order.OrderPrice);
      Assert.Equal(18.81018M, order.StrikePrice);
      Assert.True(order.IsGoodTillCanceledOrder);
      Assert.Equal(order.CreatedTimestamp +
        (TimeSpan.TicksPerDay * Order.TIME_IN_FORCE_TO_BE_CANCELLED_DAYS),
        order.ToBeCanceledTimestamp);
        */
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void CancelTest() {
      Guid orderId = Guid.NewGuid();
      Order order = new TestOrder {
        OrderId = orderId.ToString(),
        Status = OrderStatus.COMPLETED
      };
      Exception e = Assert.Throws<ArgumentException>(() =>
        order.Cancel());
      Assert.Equal("Error Cancel: Status is not OPEN OrderId: " + orderId.ToString(),
        e.Message);
      Order open = TestUtils.NewBuyLimitDayOrder();
      Assert.Equal(0, open.CanceledTimestamp);
      open.Cancel();
      Assert.Equal(OrderStatus.CANCELLED, open.Status);
      Assert.NotEqual(0, open.CanceledTimestamp);
    }
  }
}
