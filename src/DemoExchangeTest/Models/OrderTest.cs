using System;
using DemoExchange.Api;
using DemoExchange.Interface;
using Xunit;
using static Utils.Time;

namespace DemoExchange.Models {
  public class OrderTest {
    [Fact]
    [Trait("Category", "Unit")]
    public void OrderEntityTest() {
      Guid orderId = Guid.NewGuid();
      Guid accountId = Guid.NewGuid();
      OrderEntity orderEntity = new OrderEntity {
        OrderId = orderId,
        CreatedTimestamp = 1,
        AccountId = accountId,
        Status = OrderStatus.OrderOpen,
        Action = OrderAction.OrderBuy,
        Ticker = "ERX",
        Type = OrderType.OrderMarket,
        Quantity = 100,
        OpenQuantity = 200,
        OrderPrice = 1.12345678M,
        StrikePrice = 2.12345678M,
        TimeInForce = OrderTimeInForce.OrderDay,
        ToBeCanceledTimestamp = 8,
        CanceledTimestamp = 18
      };
      Assert.Equal(orderId, orderEntity.OrderId);
      Assert.Equal(1, orderEntity.CreatedTimestamp);
      Assert.Equal(accountId, orderEntity.AccountId);
      Assert.Equal(OrderStatus.OrderOpen, orderEntity.Status);
      Assert.Equal(OrderAction.OrderBuy, orderEntity.Action);
      Assert.Equal("ERX", orderEntity.Ticker);
      Assert.Equal(OrderType.OrderMarket, orderEntity.Type);
      Assert.Equal(100, orderEntity.Quantity);
      Assert.Equal(200, orderEntity.OpenQuantity);
      Assert.Equal(1.12345678M, orderEntity.OrderPrice);
      Assert.Equal(2.12345678M, orderEntity.StrikePrice);
      Assert.Equal(OrderTimeInForce.OrderDay, orderEntity.TimeInForce);
      Assert.Equal(8, orderEntity.ToBeCanceledTimestamp);
      Assert.Equal(18, orderEntity.CanceledTimestamp);
      OrderBL order = new TestOrder(orderEntity);
      Assert.Equal(1, order.CreatedTimestamp);
      Assert.Equal(accountId.ToString(), order.AccountId);
      Assert.Equal(OrderStatus.OrderOpen, order.Status);
      Assert.Equal(OrderAction.OrderBuy, order.Action);
      Assert.Equal("ERX", order.Ticker);
      Assert.Equal(OrderType.OrderMarket, order.Type);
      Assert.Equal(100, order.Quantity);
      Assert.Equal(200, order.OpenQuantity);
      Assert.Equal(1.12345678M, order.OrderPrice);
      Assert.Equal(2.12345678M, order.StrikePrice);
      Assert.Equal(OrderTimeInForce.OrderDay, order.TimeInForce);
      Assert.Equal(8, order.ToBeCanceledTimestamp);
      Assert.Equal(18, order.CanceledTimestamp);
      OrderEntity orderEntity2 = new OrderEntity {
        OrderId = orderId,
        CreatedTimestamp = 1,
        AccountId = accountId,
        Status = OrderStatus.OrderOpen,
        Action = OrderAction.OrderBuy,
        Ticker = "ERX",
        Type = OrderType.OrderMarket,
        Quantity = 100,
        OpenQuantity = 200,
        OrderPrice = 1.12345678M,
        StrikePrice = 2.12345678M,
        TimeInForce = OrderTimeInForce.OrderDay,
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
      Assert.Equal(accountId, up.AccountId);
      Assert.Equal(OrderStatus.OrderOpen, up.Status);
      Assert.Equal(OrderAction.OrderBuy, up.Action);
      Assert.Equal("ERX", up.Ticker);
      Assert.Equal(OrderType.OrderMarket, up.Type);
      Assert.Equal(100, up.Quantity);
      Assert.Equal(200, up.OpenQuantity);
      Assert.Equal(1.12345678M, up.OrderPrice);
      Assert.Equal(2.12345678M, up.StrikePrice);
      Assert.Equal(OrderTimeInForce.OrderDay, up.TimeInForce);
      Assert.Equal(8, up.ToBeCanceledTimestamp);
      Assert.Equal(18, up.CanceledTimestamp);
      up.CreatedTimestamp += 10;;
      up.Status = OrderStatus.OrderCompleted;
      up.Action = OrderAction.OrderSell;
      up.Ticker += "-Up";
      up.Type = OrderType.OrderLimit;
      up.Quantity += 1;
      up.OpenQuantity += 1;
      up.OrderPrice += 10;
      up.StrikePrice += 10;
      up.TimeInForce = OrderTimeInForce.OrderGoodTilCanceled;
      up.ToBeCanceledTimestamp += 10;
      up.CanceledTimestamp += 10;
      Assert.Equal(11, up.CreatedTimestamp);
      Assert.Equal(OrderStatus.OrderCompleted, up.Status);
      Assert.Equal(OrderAction.OrderSell, up.Action);
      Assert.Equal("ERX-Up", up.Ticker);
      Assert.Equal(OrderType.OrderLimit, up.Type);
      Assert.Equal(101, up.Quantity);
      Assert.Equal(201, up.OpenQuantity);
      Assert.Equal(11.12345678M, up.OrderPrice);
      Assert.Equal(12.12345678M, up.StrikePrice);
      Assert.Equal(OrderTimeInForce.OrderGoodTilCanceled, up.TimeInForce);
      Assert.Equal(18, up.ToBeCanceledTimestamp);
      Assert.Equal(28, up.CanceledTimestamp);
      Assert.True(up.Equals(order));
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void OrderTest_constructor() {
      Exception e = Assert.Throws<ArgumentException>(() =>
        new TestOrder(null, null, OrderType.OrderMarket, 0, 0));
      Assert.Equal("accountId is null, empty, or contains only white-space characters",
        e.Message);
      String accountId = Guid.NewGuid().ToString();
      e = Assert.Throws<ArgumentException>(() =>
        new TestOrder(accountId, null, OrderType.OrderMarket, 0, 0));
      Assert.Equal("ticker is null, empty, or contains only white-space characters",
        e.Message);
      e = Assert.Throws<ArgumentException>(() =>
        new TestOrder(accountId, "ERX", OrderType.OrderMarket, 0, 0));
      Assert.Equal(IOrderModel.ERROR_QUANTITY_IS_0,
        e.Message);
      e = Assert.Throws<ArgumentException>(() =>
        new TestOrder(accountId, "ERX", OrderType.OrderMarket, -100, 0));
      Assert.Equal(IOrderModel.ERROR_QUANTITY_IS_0,
        e.Message);
      e = Assert.Throws<ArgumentException>(() =>
        new TestOrder(accountId, "ERX", OrderType.OrderMarket, 100, -1));
      Assert.Equal(IOrderModel.ERROR_ORDER_PRICE_MARKET_NOT_0,
        e.Message);
      e = Assert.Throws<ArgumentException>(() =>
        new TestOrder(accountId, "ERX", OrderType.OrderLimit, 100, -1));
      Assert.Equal(IOrderModel.ERROR_ORDER_PRICE_IS_0,
        e.Message);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void CancelTest() {
      Guid orderId = Guid.NewGuid();
      OrderBL order = new TestOrder {
        OrderId = orderId.ToString(),
        Status = OrderStatus.OrderCompleted
      };
      Exception e = Assert.Throws<ArgumentException>(() =>
        order.Cancel());
      Assert.Equal("Error Cancel: Status is not Open OrderId: " + orderId.ToString(),
        e.Message);
      OrderBL open = TestUtils.NewBuyLimitDayOrder();
      Assert.Equal(0, open.CanceledTimestamp);
      open.Cancel();
      Assert.Equal(OrderStatus.OrderCancelled, open.Status);
      Assert.NotEqual(0, open.CanceledTimestamp);
    }
  }

  public class OrdersPredicatesTest {
    [Fact]
    [Trait("Category", "Unit")]
    public void OpenByTickerAndActionTest() {
      TestOrder order = new TestOrder() {
        Status = OrderStatus.OrderCancelled,
        Ticker = "ER",
        Action = OrderAction.OrderSell
      };
      Assert.False(Orders.Predicates.OpenByTickerAndAction("ERX", OrderAction.OrderBuy).Invoke(order));
      order.Status = OrderStatus.OrderOpen;
      Assert.False(Orders.Predicates.OpenByTickerAndAction("ERX", OrderAction.OrderBuy).Invoke(order));
      order.Ticker = "ERX";
      Assert.False(Orders.Predicates.OpenByTickerAndAction("ERX", OrderAction.OrderBuy).Invoke(order));
      order.Action = OrderAction.OrderBuy;
      Assert.True(Orders.Predicates.OpenByTickerAndAction("ERX", OrderAction.OrderBuy).Invoke(order));
    }
  }
}
