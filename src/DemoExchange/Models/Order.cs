using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DemoExchange.Interface;
using static Utils.Preconditions;
using static Utils.Time;

namespace DemoExchange.Models {
  /// <summary>
  /// For persistence of an <c>Order</c>.
  /// </summary>
  [Table("ExchangeOrder")]
  public class ExchangeOrderEntity : IModelOrder {
    public virtual String OrderId { get; set; }
    public virtual long CreatedTimestamp { get; set; }
    public virtual String AccountId { get; set; }
    public virtual OrderStatus Status { get; set; }
    public virtual OrderAction Action { get; set; }
    public virtual String Ticker { get; set; }
    public virtual OrderType Type { get; set; }
    public virtual int Quantity { get; set; }
    public virtual int OpenQuantity { get; set; }
    public virtual decimal OrderPrice { get; set; }
    public virtual decimal StrikePrice { get; set; }
    public virtual OrderTimeInForce TimeInForce { get; set; }
    public virtual long ToBeCanceledTimestamp { get; set; }
    public virtual long CanceledTimestamp { get; set; }

    public virtual bool IsValid {
      get { throw new NotImplementedException(); }
    }

    public virtual ExchangeOrderEntity ShallowCopy() {
      return (ExchangeOrderEntity)this.MemberwiseClone();
    }

    public override String ToString() {
      return "{OrderId: " + OrderId + ", " +
        "CreatedTimestamp: " + CreatedTimestamp + ", " +
        "AccountId: " + AccountId + ", " +
        "Status: " + Status + ", " +
        "Action: " + Action + ", " +
        "Ticker: " + Ticker + ", " +
        "Type: " + Type + ", " +
        "Quantity: " + Quantity + ", " +
        "OpenQuantity: " + OpenQuantity + ", " +
        "OrderPrice: " + AppConstants.FormatPrice(OrderPrice) + ", " +
        "StrikePrice: " + AppConstants.FormatPrice(StrikePrice) + ", " +
        "TimeInForce: " + TimeInForce + ", " +
        "ToBeCanceledTimestamp: " + ToBeCanceledTimestamp + ", " +
        "CanceledTimestamp: " + CanceledTimestamp + ", " +
        "}";
    }

    public override bool Equals(object obj) {
      if (obj == null) { // Don't check for GetType
        return false;
      }

      return this.ToString().Equals(obj.ToString());
    }

    public override int GetHashCode() {
      return HashCode.Combine(ToString());
    }
  }

  /// <summary>
  /// Base Class representing an Order.
  /// </summary>
  public abstract class Order : ExchangeOrderEntity, IModelOrder {
    public const int TIME_IN_FORCE_TO_BE_CANCELLED_DAYS = 90;

    public const String ERROR_QUANTITY_IS_0 = "quantity must be greater than 0";
    public const String ERROR_ORDER_PRICE_MARKET_NOT_0 = "orderPrice should be 0 for Market orders";
    public const String ERROR_ORDER_PRICE_IS_0 = "orderPrice must be greater than 0";
    public const String ERROR_STATUS_NOT_OPEN = "Error Cancel: Status is not OPEN OrderId: {0}";

    public new String OrderId {
      get { return base.OrderId; }
#if DEBUG
      set { throw new InvalidOperationException(); }
#endif
    }
    public new long CreatedTimestamp {
      get { return base.CreatedTimestamp; }
#if DEBUG
      protected set { throw new InvalidOperationException(); }
#endif
    }
    public DateTime CreatedDateTime {
      get { return FromTicks(base.CreatedTimestamp); }
#if DEBUG
      protected set { throw new InvalidOperationException(); }
#endif
    }
    public new String AccountId {
      get { return base.AccountId; }
#if DEBUG
      protected set { throw new InvalidOperationException(); }
#endif
    }
    public new OrderStatus Status {
      get { return base.Status; }
#if DEBUG
      protected set { base.Status = value; }
#else
      private set { base.Status = value; }
#endif
    }
    public bool IsOpen {
      get { return OrderStatus.OPEN.Equals(Status); }
    }
    public bool IsCompleted {
      get { return OrderStatus.COMPLETED.Equals(Status); }
    }
    public bool IsUpdated {
      get { return OrderStatus.UPDATED.Equals(Status); }
    }
    public bool IsCancelled {
      get { return OrderStatus.CANCELLED.Equals(Status); }
    }
    public bool IsDeleted {
      get { return OrderStatus.DELETED.Equals(Status); }
    }
    public new OrderAction Action {
      get { return base.Action; }
#if DEBUG
      protected set { throw new InvalidOperationException(); }
#endif
    }
    public bool IsBuyOrder {
      get { return OrderAction.BUY.Equals(Action); }
    }
    public bool IsSellOrder {
      get { return OrderAction.SELL.Equals(Action); }
    }
    public new String Ticker {
      get { return base.Ticker; }
#if DEBUG
      protected set { throw new InvalidOperationException(); }
#endif
    }
    public new OrderType Type {
      get {
        return base.Type;
      }
#if DEBUG
      protected set { throw new InvalidOperationException(); }
#endif
    }
    public bool IsMarketOrder {
      get { return OrderType.MARKET.Equals(Type); }
    }
    public bool IsLimitOrder {
      get { return OrderType.LIMIT.Equals(Type); }
    }
    public bool IsStopMarketOrder {
      get { return OrderType.STOP_MARKET.Equals(Type); }
    }
    public bool IsStopLimitOrder {
      get { return OrderType.STOP_LIMIT.Equals(Type); }
    }
    public bool IsTrailingStopMarketOrder {
      get { return OrderType.TRAILING_STOP_MARKET.Equals(Type); }
    }
    public bool IsTrailingStopLimitOrder {
      get { return OrderType.TRAILING_STOP_LIMIT.Equals(Type); }
    }
    public bool IsFillOrKillOrder {
      get { return OrderType.FILL_OR_KILL.Equals(Type); }
    }
    public bool IsImmediateOrCancelOrder {
      get { return OrderType.IMMEDIATE_OR_CANCEL.Equals(Type); }
    }
    public new int Quantity {
      get { return base.Quantity; }
#if DEBUG
      protected set { throw new InvalidOperationException(); }
#endif
    }
    public new int OpenQuantity {
      get { return base.OpenQuantity; }
      set { base.OpenQuantity = value; }
    }
    public bool IsFilled {
      get { return OpenQuantity == 0; }
    }
    public virtual new decimal OrderPrice {
      get { return base.OrderPrice; }
#if DEBUG
      protected set { throw new InvalidOperationException(); }
#endif
    }
    public virtual new decimal StrikePrice {
      get { return base.StrikePrice; }
      set { base.StrikePrice = value; }
    }
    public new OrderTimeInForce TimeInForce {
      get { return base.TimeInForce; }
#if DEBUG
      protected set { throw new InvalidOperationException(); }
#endif
    }
    public bool IsDayOrder {
      get {
        return OrderTimeInForce.DAY.Equals(TimeInForce);
      }
    }
    public bool IsGoodTillCanceledOrder {
      get { return OrderTimeInForce.GOOD_TIL_CANCELED.Equals(TimeInForce); }
    }
    public bool IsMarketClose {
      get { return OrderTimeInForce.MARKET_CLOSE.Equals(TimeInForce); }
    }
    public new long ToBeCanceledTimestamp {
      get { return base.ToBeCanceledTimestamp; }
#if DEBUG
      protected set { throw new InvalidOperationException(); }
#endif
    }
    public DateTime ToBeCanceledDateTime {
      get { return FromTicks(ToBeCanceledTimestamp); }
    }
    public new long CanceledTimestamp {
      get { return base.CanceledTimestamp; }
#if DEBUG
      protected set { base.CanceledTimestamp = value; }
#else
      private set { base.CanceledTimestamp = value; }
#endif
    }
    public DateTime CanceledDateTime {
      get { return FromTicks(CanceledTimestamp); }
    }

    /// <summary>
    /// <c>Order</c> constructor.
    /// <br><c>Id</c>: Auto-gen GUID</br>
    /// <br><c>CreatedTimestamp</c>: Auto-gen UTC high fidelity timestamp</br>
    /// <br><c>Status</c>: Defaults to OPEN</br>
    /// <br><c>OpenQuantity</c>: Defaults to Quantity</br>
    /// <br><c>StrikePrice</c>: Defaults to OrderPrice</br>
    /// <br><c>ToBeCanceledTimestamp</c>: Defaults to 0 for Market orders, end of day for Day orders, and <c>TIME_IN_FORCE_TO_BE_CANCELLED_DAYS</c> for Good Til Canceled orders.</br>
    /// </summary>
    protected Order(String accountId, OrderAction action, String ticker, OrderType type,
      int quantity, decimal orderPrice, OrderTimeInForce timeInForce) {
      CheckNotNullOrWhitespace(accountId, paramName : nameof(accountId));
      CheckNotNullOrWhitespace(ticker, paramName : nameof(ticker));
      CheckArgument(quantity > 0, message : ERROR_QUANTITY_IS_0);
      if (OrderType.MARKET.Equals(type)) {
        if (orderPrice != 0) {
          throw new ArgumentException(ERROR_ORDER_PRICE_MARKET_NOT_0,
            paramName : nameof(orderPrice));
        }
      } else {
        if (orderPrice <= 0) {
          throw new ArgumentException(ERROR_ORDER_PRICE_IS_0,
            paramName : nameof(orderPrice));
        }
      }

      base.OrderId = Guid.NewGuid().ToString();
      base.CreatedTimestamp = Now;
      base.AccountId = accountId;
      base.Status = OrderStatus.OPEN;
      base.Action = action;
      base.Ticker = ticker;
      base.Type = type;
      base.Quantity = quantity;
      base.OpenQuantity = quantity;
      base.OrderPrice = orderPrice;
      base.StrikePrice = orderPrice;
      base.TimeInForce = timeInForce;
      if (OrderType.MARKET.Equals(type)) {
        base.ToBeCanceledTimestamp = 0;
      } else {
        long toBeCancel = 0;
        switch (TimeInForce) {
          case OrderTimeInForce.DAY:
            toBeCancel = MidnightIct;
            break;
          case OrderTimeInForce.GOOD_TIL_CANCELED:
            toBeCancel = base.CreatedTimestamp + (TimeSpan.TicksPerDay * TIME_IN_FORCE_TO_BE_CANCELLED_DAYS);
            break;
          case OrderTimeInForce.MARKET_CLOSE:
            toBeCancel = MidnightSaturdayIct;
            break;
        }

        base.ToBeCanceledTimestamp = toBeCancel;
      }
    }

    public Order(ExchangeOrderEntity entity) {
      base.OrderId = entity.OrderId;
      base.CreatedTimestamp = entity.CreatedTimestamp;
      base.AccountId = entity.AccountId;
      base.Status = entity.Status;
      base.Action = entity.Action;
      base.Ticker = entity.Ticker;
      base.Type = entity.Type;
      base.Quantity = entity.Quantity;
      base.OpenQuantity = entity.OpenQuantity;
      base.OrderPrice = entity.OrderPrice;
      base.StrikePrice = entity.StrikePrice;
      base.TimeInForce = entity.TimeInForce;
      base.ToBeCanceledTimestamp = entity.ToBeCanceledTimestamp;
      base.CanceledTimestamp = entity.CanceledTimestamp;
    }

    public override bool IsValid {
      get { return true; } // TODO: throw new NotImplementedException();
    }

    public void Cancel() {
      CheckArgument(IsOpen, String.Format(ERROR_STATUS_NOT_OPEN, OrderId));

      Status = OrderStatus.CANCELLED;
      CanceledTimestamp = Now;
    }

    public void Complete() {
      CheckArgument(IsOpen, String.Format(ERROR_STATUS_NOT_OPEN, OrderId));

      Status = OrderStatus.COMPLETED;
    }

#if DEBUG
    public Order() { }
#endif
  }

  public abstract class MarketOrder : Order {
    public new decimal OrderPrice {
      get { return ((ExchangeOrderEntity)this).OrderPrice; }
    }
    public new decimal StrikePrice {
      get { return ((ExchangeOrderEntity)this).StrikePrice; }
    }

    public MarketOrder(String accountId, OrderAction action, String ticker, int quantity):
      base(accountId, action, ticker, OrderType.MARKET, quantity, 0, OrderTimeInForce.DAY) { }
  }

  /// <summary>
  /// Convenience class to instantiate a Buy Market Order.
  /// </summary>
  public class BuyMarketOrder : MarketOrder {
    public BuyMarketOrder(String accountId, String ticker, int quantity):
      base(accountId, OrderAction.BUY, ticker, quantity) { }
  }

  /// <summary>
  /// Convenience class to instantiate a Buy Limit Day Order.
  /// </summary>
  public class BuyLimitDayOrder : Order {
    public BuyLimitDayOrder(String accountId, String ticker, int quantity, decimal orderPrice):
      base(accountId, OrderAction.BUY, ticker, OrderType.LIMIT, quantity, orderPrice,
        OrderTimeInForce.DAY) { }
  }

  /// <summary>
  /// Convenience class to instantiate a Buy Limit GTC Order.
  /// </summary>
  public class BuyLimitGoodTilCanceledOrder : Order {
    public BuyLimitGoodTilCanceledOrder(String accountId, String ticker, int quantity,
        decimal strikePrice):
      base(accountId, OrderAction.BUY, ticker, OrderType.LIMIT, quantity, strikePrice,
        OrderTimeInForce.GOOD_TIL_CANCELED) { }
  }

  /// <summary>
  /// Convenience class to instantiate a Sell Market Order.
  /// </summary>
  public class SellMarketOrder : MarketOrder {
    public SellMarketOrder(String accountId, String ticker, int quantity):
      base(accountId, OrderAction.SELL, ticker, quantity) { }
  }

  /// <summary>
  /// Convenience class to instantiate a Sell Limit Day Order.
  /// </summary>
  public class SellLimitDayOrder : Order {
    public SellLimitDayOrder(String accountId, String ticker, int quantity, decimal strikePrice):
      base(accountId, OrderAction.SELL, ticker, OrderType.LIMIT, quantity, strikePrice,
        OrderTimeInForce.DAY) { }
  }

  /// <summary>
  /// Convenience class to instantiate a Sell Limit GTC Order.
  /// </summary>
  public class SellLimitGoodTilCanceledOrder : Order {
    public SellLimitGoodTilCanceledOrder(String accountId, String ticker, int quantity,
        decimal strikePrice):
      base(accountId, OrderAction.SELL, ticker, OrderType.LIMIT, quantity, strikePrice,
        OrderTimeInForce.GOOD_TIL_CANCELED) { }
  }

  /// <summary>
  /// Convenience methods for <c>Order</c>.
  /// </summary>
  public class Orders {
    /// <summary>
    /// Use for SELL order books.
    /// </summary>
    public static readonly Comparer<Order> STRIKE_PRICE_ASCENDING_COMPARER =
      new StrikePriceAscendingComparer();

    /// <summary>
    /// Use for BUY order books.
    /// </summary>
    public static readonly Comparer<Order> STRIKE_PRICE_DESCENDING_COMPARER =
      new StrikePriceDescendingComparer();

    public static Predicate<Order> ById(String orderId) {
      return order => order.OrderId.Equals(orderId);
    }

    private Orders() {
      // Prevent instantiation;
    }

    /// <summary>
    /// Price-Time ascending <c>Comparer</c>.
    /// </summary>
    private class StrikePriceAscendingComparer : Comparer<Order> {
      public override int Compare(Order o1, Order o2) {
        if (o1.StrikePrice > o2.StrikePrice)
          return 1;
        if (o1.StrikePrice < o2.StrikePrice)
          return -1;
        if (o1.CreatedTimestamp > o2.CreatedTimestamp)
          return 1;
        if (o1.CreatedTimestamp < o2.CreatedTimestamp)
          return -1;

        return 0;
      }
    }

    /// <summary>
    /// Price-Time descending <c>Comparer</c>.
    /// </summary>
    private class StrikePriceDescendingComparer : Comparer<Order> {
      public override int Compare(Order o1, Order o2) {
        if (o1.StrikePrice < o2.StrikePrice)
          return 1;
        if (o1.StrikePrice > o2.StrikePrice)
          return -1;
        if (o1.CreatedTimestamp > o2.CreatedTimestamp)
          return 1;
        if (o1.CreatedTimestamp < o2.CreatedTimestamp)
          return -1;

        return 0;
      }
    }
  }

  /// <summary>
  /// Market <c>Order</c> validator.
  /// </summary>
  public class MarketOrderValidator : IValidator<IModelOrder> {
    public bool IsValid {
      get { throw new NotImplementedException(); }
    }
  }
}
