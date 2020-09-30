using System;

namespace DemoExchange.Models {
  public abstract class Order {
    const int TIME_IN_FORCE_TO_BE_CANCELLED_DAYS = 90;

    public String Id { get; }
    public long CreatedTimestamp { get; protected set; }
    public DateTime CreatedDateTime {
      get { return new DateTime(CreatedTimestamp); }
    }
    public String AccountId { get; }
    public OrderStatus Status { get; set; }
    public bool IsOpen {
      get { return Status.Equals(OrderStatus.OPEN); }
    }
    public bool IsCompleted {
      get { return Status.Equals(OrderStatus.COMPLETED); }
    }
    public bool IsUpdated {
      get { return Status.Equals(OrderStatus.UPDATED); }
    }
    public bool IsCancelled {
      get { return Status.Equals(OrderStatus.CANCELLED); }
    }
    public bool IsDeleted {
      get { return Status.Equals(OrderStatus.DELETED); }
    }
    public OrderAction Action { get; }
    public bool IsBuyOrder {
      get { return Action.Equals(OrderAction.BUY); }
    }
    public bool IsSellOrder {
      get { return Action.Equals(OrderAction.SELL); }
    }
    public String Ticker { get; }
    public OrderType Type { get; }
    public bool IsMarketOrder {
      get { return Type.Equals(OrderType.MARKET); }
    }
    public bool IsLimitOrder {
      get { return Type.Equals(OrderType.LIMIT); }
    }
    public bool IsStopMarketOrder {
      get { return Type.Equals(OrderType.STOP_MARKET); }
    }
    public bool IsStopLimitOrder {
      get { return Type.Equals(OrderType.STOP_LIMIT); }
    }
    public bool IsTrailingStopMarketOrder {
      get { return Type.Equals(OrderType.TRAILING_STOP_MARKET); }
    }
    public bool IsTrailingStopLimitOrder {
      get { return Type.Equals(OrderType.TRAILING_STOP_LIMIT); }
    }
    public bool IsFillOrKillOrder {
      get { return Type.Equals(OrderType.FILL_OR_KILL); }
    }
    public bool IsImmediateOrCancelOrder {
      get { return Type.Equals(OrderType.IMMEDIATE_OR_CANCEL); }
    }
    public decimal StrikePrice { get; set; }
    public int Quantity { get; }
    public int OpenQuantity { get; set; }
    public bool IsFilled {
      get { return OpenQuantity == 0; }
    }
    public OrderTimeInForce TimeInForce { get; }
    public bool IsDayOrder {
      get { return TimeInForce.Equals(OrderTimeInForce.DAY); }
    }
    public bool IsGoodTillCanceledOrder {
      get { return TimeInForce.Equals(OrderTimeInForce.GOOD_TIL_CANCELED); }
    }
    public long ToBeCanceledTimestamp { get; }
    public DateTime ToBeCanceledDateTime {
      get { return new DateTime(ToBeCanceledTimestamp); }
    }

    public Order(String accountId, OrderAction action, String ticker, OrderType orderType,
      decimal strikePrice, int quantity, OrderTimeInForce timeInForce) {
      Id = Guid.NewGuid().ToString();
      CreatedTimestamp = System.Diagnostics.Stopwatch.GetTimestamp();
      AccountId = accountId;
      Status = OrderStatus.OPEN;
      Action = action;
      Ticker = ticker;
      Type = orderType;
      StrikePrice = strikePrice;
      Quantity = quantity;
      OpenQuantity = quantity;
      TimeInForce = timeInForce;
      ToBeCanceledTimestamp = timeInForce.Equals(OrderTimeInForce.DAY) ?
        0 : CreatedTimestamp + (TimeSpan.TicksPerDay * TIME_IN_FORCE_TO_BE_CANCELLED_DAYS);
    }

    public override String ToString() {
      return "{Id: " + Id + ", " +
        "CreatedTimestamp: " + CreatedTimestamp + ", " +
        "AccountId: " + AccountId + ", " +
        "Status: " + Status + ", " +
        "Action: " + Action + ", " +
        "Ticker: " + Ticker + ", " +
        "Type: " + Type + ", " +
        "StrikePrice: " + StrikePrice + ", " +
        "Quantity: " + Quantity + ", " +
        "OpenQuantity: " + OpenQuantity + ", " +
        "TimeInForce: " + TimeInForce + ", " +
        "ToBeCanceledTimestamp: " + ToBeCanceledTimestamp + ", " +
        "}";
    }

    public override bool Equals(object obj) {
      if (obj == null || GetType() != obj.GetType()) {
        return false;
      }

      return this.ToString().Equals(obj.ToString());
    }

    public override int GetHashCode() {
      return HashCode.Combine(ToString());
    }
  }

  public class BuyMarketOrder : Order {
    public BuyMarketOrder(String accountId, String ticker, int quantity) : base(accountId,
      OrderAction.BUY, ticker, OrderType.MARKET, 0, quantity, OrderTimeInForce.DAY) { }
  }

  public class SellMarketOrder : Order {
    public SellMarketOrder(String accountId, String ticker, int quantity) : base(accountId,
      OrderAction.SELL, ticker, OrderType.MARKET, 0, quantity, OrderTimeInForce.DAY) { }
  }

  public enum OrderStatus {
    OPEN,
    COMPLETED,
    UPDATED,
    CANCELLED,
    DELETED
  }

  public enum OrderAction {
    BUY,
    SELL
  }

  public enum OrderType {
    MARKET,
    LIMIT,
    STOP_MARKET,
    STOP_LIMIT,
    TRAILING_STOP_MARKET,
    TRAILING_STOP_LIMIT,
    FILL_OR_KILL,
    IMMEDIATE_OR_CANCEL
  }

  public enum OrderTimeInForce {
    DAY,
    GOOD_TIL_CANCELED
  }
}
