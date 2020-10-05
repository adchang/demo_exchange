using System;
using DemoExchange.Interface;
using DemoExchange.Models;

namespace DemoExchange {
  public class TestUtils {
    public static Order NewBuyLimitDayOrder() {
      return NewLimitDayOrder("acct", "ERX", 100, 18.81M, OrderAction.BUY);
    }

    public static Order NewBuyLimitDayOrder(decimal strikePrice) {
      return NewLimitDayOrder("acct", "ERX", 100, strikePrice, OrderAction.BUY);
    }

    public static Order NewSellLimitDayOrder() {
      return NewLimitDayOrder("acct", "ERX", 100, 18.81M, OrderAction.SELL);
    }

    public static Order NewSellLimitDayOrder(decimal strikePrice) {
      return NewLimitDayOrder("acct", "ERX", 100, strikePrice, OrderAction.SELL);
    }

    public static Order NewLimitDayOrder(String accountId, String ticker, int quantity,
      decimal strikePrice, OrderAction action) {

      return OrderAction.BUY.Equals(action) ?
        new BuyLimitDayOrder(accountId, ticker, quantity, strikePrice) :
        new SellLimitDayOrder(accountId, ticker, quantity, strikePrice);
    }

    private TestUtils() {
      // Prevent instantiation
    }
  }

  public class TestOrder : Order {
    public new String OrderId {
      get { return ((ExchangeOrderEntity)this).OrderId; }
      set {
        ((ExchangeOrderEntity)this).OrderId = value;
      }
    }
    public new long CreatedTimestamp {
      get { return ((ExchangeOrderEntity)this).CreatedTimestamp; }
      set {
        ((ExchangeOrderEntity)this).CreatedTimestamp = value;
      }
    }
    public new String AccountId {
      get { return ((ExchangeOrderEntity)this).AccountId; }
      set {
        ((ExchangeOrderEntity)this).AccountId = value;
      }
    }
    public new OrderStatus Status {
      get { return ((ExchangeOrderEntity)this).Status; }
      set {
        ((ExchangeOrderEntity)this).Status = value;
      }
    }
    public new OrderAction Action {
      get { return ((ExchangeOrderEntity)this).Action; }
      set {
        ((ExchangeOrderEntity)this).Action = value;
      }
    }
    public new String Ticker {
      get { return ((ExchangeOrderEntity)this).Ticker; }
      set {
        ((ExchangeOrderEntity)this).Ticker = value;
      }
    }
    public new OrderType Type {
      get { return ((ExchangeOrderEntity)this).Type; }
      set {
        ((ExchangeOrderEntity)this).Type = value;
      }
    }
    public new int Quantity {
      get { return ((ExchangeOrderEntity)this).Quantity; }
      set {
        ((ExchangeOrderEntity)this).Quantity = value;
      }
    }
    public new int OpenQuantity {
      get { return ((ExchangeOrderEntity)this).OpenQuantity; }
      set {
        ((ExchangeOrderEntity)this).OpenQuantity = value;
      }
    }
    public new decimal OrderPrice {
      get { return ((ExchangeOrderEntity)this).OrderPrice; }
      set {
        ((ExchangeOrderEntity)this).OrderPrice = value;
      }
    }
    public new decimal StrikePrice {
      get { return ((ExchangeOrderEntity)this).StrikePrice; }
      set {
        ((ExchangeOrderEntity)this).StrikePrice = value;
      }
    }
    public new OrderTimeInForce TimeInForce {
      get { return ((ExchangeOrderEntity)this).TimeInForce; }
      set {
        ((ExchangeOrderEntity)this).TimeInForce = value;
      }
    }
    public new long ToBeCanceledTimestamp {
      get { return ((ExchangeOrderEntity)this).ToBeCanceledTimestamp; }
      set {
        ((ExchangeOrderEntity)this).ToBeCanceledTimestamp = value;
      }
    }
    public new long CanceledTimestamp {
      get { return ((ExchangeOrderEntity)this).CanceledTimestamp; }
      set {
        ((ExchangeOrderEntity)this).CanceledTimestamp = value;
      }
    }

    public TestOrder() { }

    public TestOrder(ExchangeOrderEntity entity) : base(entity) { }

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
