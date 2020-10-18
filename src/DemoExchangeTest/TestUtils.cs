using System;
using DemoExchange.Api;
using DemoExchange.Interface;
using DemoExchange.Models;

namespace DemoExchange {
  public class TestUtils {
    public static OrderBL NewBuyMarketOrder(String accountId, String ticker, int quantity) {
      return new OrderBL(accountId, OrderAction.OrderBuy, ticker, OrderType.OrderMarket, quantity,
        0, OrderTimeInForce.OrderDay);
    }

    public static OrderBL NewBuyLimitDayOrder() {
      return NewLimitDayOrder(Guid.NewGuid().ToString(), "ERX", 100, 18.81M, OrderAction.OrderBuy);
    }

    public static OrderBL NewBuyLimitDayOrder(decimal strikePrice) {
      return NewLimitDayOrder(Guid.NewGuid().ToString(), "ERX", 100, strikePrice, OrderAction.OrderBuy);
    }

    public static OrderBL NewBuyLimitDayOrder(String accountId, String ticker, int quantity,
      decimal strikePrice) {
      return NewLimitDayOrder(accountId, ticker, quantity, strikePrice, OrderAction.OrderBuy);
    }

    public static OrderBL NewSellLimitDayOrder() {
      return NewLimitDayOrder(Guid.NewGuid().ToString(), "ERX", 100, 18.81M, OrderAction.OrderSell);
    }

    public static OrderBL NewSellLimitDayOrder(decimal strikePrice) {
      return NewLimitDayOrder(Guid.NewGuid().ToString(), "ERX", 100, strikePrice, OrderAction.OrderSell);
    }

    public static OrderBL NewSellLimitDayOrder(String accountId, String ticker, int quantity,
      decimal strikePrice) {
      return NewLimitDayOrder(accountId, ticker, quantity, strikePrice, OrderAction.OrderSell);
    }

    public static OrderBL NewLimitDayOrder(String accountId, String ticker, int quantity,
      decimal orderPrice, OrderAction action) {
      return new OrderBL(accountId, action, ticker, OrderType.OrderLimit, quantity,
        orderPrice, OrderTimeInForce.OrderDay);
    }

    private TestUtils() {
      // Prevent instantiation
    }
  }

  public class TestOrder : OrderBL {
    public new String OrderId {
      get { return ((OrderEntity)this).OrderId.ToString(); }
      set {
        ((OrderEntity)this).OrderId = Guid.Parse(value);
      }
    }
    public new long CreatedTimestamp {
      get { return ((OrderEntity)this).CreatedTimestamp; }
      set {
        ((OrderEntity)this).CreatedTimestamp = value;
      }
    }
    public new String AccountId {
      get { return ((OrderEntity)this).AccountId.ToString(); }
      set {
        ((OrderEntity)this).AccountId = Guid.Parse(value);
      }
    }
    public new OrderStatus Status {
      get { return ((OrderEntity)this).Status; }
      set {
        ((OrderEntity)this).Status = value;
      }
    }
    public new OrderAction Action {
      get { return ((OrderEntity)this).Action; }
      set {
        ((OrderEntity)this).Action = value;
      }
    }
    public new String Ticker {
      get { return ((OrderEntity)this).Ticker; }
      set {
        ((OrderEntity)this).Ticker = value;
      }
    }
    public new OrderType Type {
      get { return ((OrderEntity)this).Type; }
      set {
        ((OrderEntity)this).Type = value;
      }
    }
    public new int Quantity {
      get { return ((OrderEntity)this).Quantity; }
      set {
        ((OrderEntity)this).Quantity = value;
      }
    }
    public new int OpenQuantity {
      get { return ((OrderEntity)this).OpenQuantity; }
      set {
        ((OrderEntity)this).OpenQuantity = value;
      }
    }
    public new decimal OrderPrice {
      get { return ((OrderEntity)this).OrderPrice; }
      set {
        ((OrderEntity)this).OrderPrice = value;
      }
    }
    public new decimal StrikePrice {
      get { return ((OrderEntity)this).StrikePrice; }
      set {
        ((OrderEntity)this).StrikePrice = value;
      }
    }
    public new OrderTimeInForce TimeInForce {
      get { return ((OrderEntity)this).TimeInForce; }
      set {
        ((OrderEntity)this).TimeInForce = value;
      }
    }
    public new long ToBeCanceledTimestamp {
      get { return ((OrderEntity)this).ToBeCanceledTimestamp; }
      set {
        ((OrderEntity)this).ToBeCanceledTimestamp = value;
      }
    }
    public new long CanceledTimestamp {
      get { return ((OrderEntity)this).CanceledTimestamp; }
      set {
        ((OrderEntity)this).CanceledTimestamp = value;
      }
    }

    public TestOrder() { }

    public TestOrder(OrderEntity entity) : base(entity) { }

    public TestOrder(String accountId, String ticker, OrderType type,
      int quantity, decimal strikePrice) : this(accountId,
      OrderAction.OrderBuy, ticker, type, quantity, strikePrice) { }

    public TestOrder(String accountId, OrderAction action, String ticker, OrderType type,
      int quantity, decimal strikePrice) : base(accountId,
      action, ticker, type, quantity, strikePrice, OrderTimeInForce.OrderDay) { }

    public new TestOrder ShallowCopy() {
      return (TestOrder)this.MemberwiseClone();
    }
  }
}
