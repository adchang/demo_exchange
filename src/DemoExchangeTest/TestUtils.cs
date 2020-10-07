using System;
using DemoExchange.Interface;
using DemoExchange.Models;

namespace DemoExchange {
  public class TestUtils {
    public static Order NewBuyMarketOrder(String accountId, String ticker, int quantity) {
      return new Order(new BuyMarketOrder(accountId, ticker, quantity));
    }

    public static Order NewBuyLimitDayOrder() {
      return NewLimitDayOrder("acct", "ERX", 100, 18.81M, OrderAction.BUY);
    }

    public static Order NewBuyLimitDayOrder(decimal strikePrice) {
      return NewLimitDayOrder("acct", "ERX", 100, strikePrice, OrderAction.BUY);
    }

    public static Order NewBuyLimitDayOrder(String accountId, String ticker, int quantity,
      decimal strikePrice) {
      return NewLimitDayOrder(accountId, ticker, quantity, strikePrice, OrderAction.BUY);
    }

    public static Order NewSellLimitDayOrder() {
      return NewLimitDayOrder("acct", "ERX", 100, 18.81M, OrderAction.SELL);
    }

    public static Order NewSellLimitDayOrder(decimal strikePrice) {
      return NewLimitDayOrder("acct", "ERX", 100, strikePrice, OrderAction.SELL);
    }

    public static Order NewSellLimitDayOrder(String accountId, String ticker, int quantity,
      decimal strikePrice) {
      return NewLimitDayOrder(accountId, ticker, quantity, strikePrice, OrderAction.SELL);
    }

    public static Order NewLimitDayOrder(String accountId, String ticker, int quantity,
      decimal strikePrice, OrderAction action) {
      return new Order(OrderAction.BUY.Equals(action) ?
        new BuyLimitDayOrder(accountId, ticker, quantity, strikePrice) :
        new SellLimitDayOrder(accountId, ticker, quantity, strikePrice));
    }

    private TestUtils() {
      // Prevent instantiation
    }
  }

  public class TestOrder : Order {
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
      get { return ((OrderEntity)this).AccountId; }
      set {
        ((OrderEntity)this).AccountId = value;
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
      OrderAction.BUY, ticker, type, quantity, strikePrice) { }

    public TestOrder(String accountId, OrderAction action, String ticker, OrderType type,
      int quantity, decimal strikePrice) : base(accountId,
      action, ticker, type, quantity, strikePrice, OrderTimeInForce.DAY) { }

    public new TestOrder ShallowCopy() {
      return (TestOrder)this.MemberwiseClone();
    }
  }
}
