using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoExchange.Api.Order;
using DemoExchange.Interface;
using static Utils.Preconditions;
using static Utils.Time;

namespace DemoExchange.Models {
  /// <summary>
  /// For persistence of an <c>Order</c>.
  /// </summary>
  [Table("ExchangeOrder")]
  public class OrderEntity {
    [Key]
    public Guid OrderId { get; set; }

    [Required]
    public long CreatedTimestamp { get; set; }

    [Required]
    public String AccountId { get; set; } // TODO: Change this to Guid

    [Required]
    public OrderStatus Status { get; set; }

    [Required]
    public OrderAction Action { get; set; }

    [Required]
    public String Ticker { get; set; }

    [Required]
    public OrderType Type { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public int OpenQuantity { get; set; }

    [Required]
    public decimal OrderPrice { get; set; }

    [Required]
    public decimal StrikePrice { get; set; }

    [Required]
    public OrderTimeInForce TimeInForce { get; set; }

    [Required]
    public long ToBeCanceledTimestamp { get; set; }

    [Required]
    public long CanceledTimestamp { get; set; }

    public virtual OrderEntity ShallowCopy() {
      return (OrderEntity)this.MemberwiseClone();
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

    public override bool Equals(object other) {
      if (other == null) { // Don't check for GetType
        return false;
      }

      return this.ToString().Equals(other.ToString());
    }

    public override int GetHashCode() {
      return HashCode.Combine(ToString());
    }
  }

  /// <summary>
  /// Base Class representing an Order.
  /// </summary>
  public class OrderBL : OrderEntity, IOrderModel, IIsValid {
    public const int TIME_IN_FORCE_TO_BE_Cancelled_DAYS = 90;

    public const String ERROR_STATUS_NOT_Open = "Error Cancel: Status is not Open OrderId: {0}";

    public new String OrderId {
      get { return base.OrderId.ToString(); }
    }
    public new long CreatedTimestamp {
      get { return base.CreatedTimestamp; }
    }
    public DateTime CreatedDateTime {
      get { return FromTicks(base.CreatedTimestamp); }
    }
    public new String AccountId {
      get { return base.AccountId; }
    }
    public new OrderStatus Status {
      get { return base.Status; }
      private set { base.Status = value; }
    }
    public bool IsOpen {
      get { return OrderStatus.Open.Equals(Status); }
    }
    public bool IsCompleted {
      get { return OrderStatus.Completed.Equals(Status); }
    }
    public bool IsUpdated {
      get { return OrderStatus.Updated.Equals(Status); }
    }
    public bool IsCancelled {
      get { return OrderStatus.Cancelled.Equals(Status); }
    }
    public bool IsDeleted {
      get { return OrderStatus.Deleted.Equals(Status); }
    }
    public new OrderAction Action {
      get { return base.Action; }
    }
    public bool IsBuyOrder {
      get { return OrderAction.Buy.Equals(Action); }
    }
    public bool IsSellOrder {
      get { return OrderAction.Sell.Equals(Action); }
    }
    public new String Ticker {
      get { return base.Ticker; }
    }
    public new OrderType Type {
      get {
        return base.Type;
      }
    }
    public bool IsMarketOrder {
      get { return OrderType.Market.Equals(Type); }
    }
    public bool IsLimitOrder {
      get { return OrderType.Limit.Equals(Type); }
    }
    public bool IsStopMarketOrder {
      get { return OrderType.StopMarket.Equals(Type); }
    }
    public bool IsStopLimitOrder {
      get { return OrderType.StopLimit.Equals(Type); }
    }
    public bool IsTrailingStopMarketOrder {
      get { return OrderType.TrailingStopMarket.Equals(Type); }
    }
    public bool IsTrailingStopLimitOrder {
      get { return OrderType.TrailingStopLimit.Equals(Type); }
    }
    public bool IsFillOrKillOrder {
      get { return OrderType.FillOrKill.Equals(Type); }
    }
    public bool IsImmediateOrCancelOrder {
      get { return OrderType.ImmediateOrCancel.Equals(Type); }
    }
    public new int Quantity {
      get { return base.Quantity; }
    }
    // OrderEntity.OpenQuantity does not need to be hidden
    public bool IsFilled {
      get { return OpenQuantity == 0; }
    }
    public new decimal OrderPrice {
      get { return base.OrderPrice; }
    }
    // OrderEntity.StrikePrice does not need to be hidden
    public new OrderTimeInForce TimeInForce {
      get { return base.TimeInForce; }
    }
    public bool IsDayOrder {
      get {
        return OrderTimeInForce.Day.Equals(TimeInForce);
      }
    }
    public bool IsGoodTillCanceledOrder {
      get { return OrderTimeInForce.GoodTilCanceled.Equals(TimeInForce); }
    }
    public bool IsMarketClose {
      get { return OrderTimeInForce.MarketClose.Equals(TimeInForce); }
    }
    public new long ToBeCanceledTimestamp {
      get { return base.ToBeCanceledTimestamp; }
    }
    public DateTime ToBeCanceledDateTime {
      get {
        return FromTicks(ToBeCanceledTimestamp);
      }
    }
    public new long CanceledTimestamp {
      get { return base.CanceledTimestamp; }
      private set { base.CanceledTimestamp = value; }
    }
    public DateTime CanceledDateTime {
      get {
        return FromTicks(CanceledTimestamp);
      }
    }

    /// <summary>
    /// <c>Order</c> constructor.
    /// <br><c>Id</c>: Auto-gen GUID</br>
    /// <br><c>CreatedTimestamp</c>: Auto-gen UTC high fidelity timestamp</br>
    /// <br><c>Status</c>: Defaults to Open</br>
    /// <br><c>OpenQuantity</c>: Defaults to Quantity</br>
    /// <br><c>StrikePrice</c>: Defaults to OrderPrice</br>
    /// <br><c>ToBeCanceledTimestamp</c>: Defaults to 0 for Market orders, end of day for Day orders, and <c>TIME_IN_FORCE_TO_BE_Cancelled_DAYS</c> for Good Til Canceled orders.</br>
    /// </summary>
    public OrderBL(String accountId, OrderAction action, String ticker, OrderType type,
      int quantity, decimal orderPrice, OrderTimeInForce timeInForce) {
      CheckNotNullOrWhitespace(accountId, paramName : nameof(accountId));
      CheckNotNullOrWhitespace(ticker, paramName : nameof(ticker));
      CheckArgument(quantity > 0, message : IOrderModel.ERROR_QUANTITY_IS_0);
      if (OrderType.Market.Equals(type)) {
        if (orderPrice != 0) {
          throw new ArgumentException(IOrderModel.ERROR_ORDER_PRICE_MARKET_NOT_0);
        }
      } else {
        if (orderPrice <= 0) {
          throw new ArgumentException(IOrderModel.ERROR_ORDER_PRICE_IS_0);
        }
      }

      base.OrderId = Guid.NewGuid();
      base.CreatedTimestamp = Now;
      base.AccountId = accountId;
      base.Status = OrderStatus.Open;
      base.Action = action;
      base.Ticker = ticker;
      base.Type = type;
      base.Quantity = quantity;
      base.OpenQuantity = quantity;
      base.OrderPrice = orderPrice;
      base.StrikePrice = orderPrice;
      base.TimeInForce = timeInForce;
      if (OrderType.Market.Equals(type)) {
        base.ToBeCanceledTimestamp = 0;
      } else {
        long toBeCancel = 0;
        switch (TimeInForce) {
          case OrderTimeInForce.Day:
            toBeCancel = MidnightIct;
            break;
          case OrderTimeInForce.GoodTilCanceled:
            toBeCancel = base.CreatedTimestamp + (TimeSpan.TicksPerDay * TIME_IN_FORCE_TO_BE_Cancelled_DAYS);
            break;
          case OrderTimeInForce.MarketClose:
            toBeCancel = MidnightSaturdayIct;
            break;
        }

        base.ToBeCanceledTimestamp = toBeCancel;
      }
    }

    public OrderBL(OrderEntity entity) {
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

    public bool IsValid {
      get { return true; } // TODO: throw new NotImplementedException();
    }

    public void Cancel() {
      CheckArgument(IsOpen, String.Format(ERROR_STATUS_NOT_Open, OrderId));

      Status = OrderStatus.Cancelled;
      CanceledTimestamp = Now;
    }

    public void Complete() {
      CheckArgument(IsOpen, String.Format(ERROR_STATUS_NOT_Open, OrderId));

      Status = OrderStatus.Completed;
    }

    public Order ToMessage() => new
    Order {
      OrderId = this.OrderId,
      CreatedTimestamp = this.CreatedTimestamp,
      AccountId = this.AccountId,
      Status = this.Status,
      Action = this.Action,
      Ticker = this.Ticker,
      Type = this.Type,
      Quantity = this.Quantity,
      OpenQuantity = this.OpenQuantity,
      OrderPrice = Convert.ToDouble(this.OrderPrice),
      StrikePrice = Convert.ToDouble(this.StrikePrice),
      TimeInForce = this.TimeInForce,
      ToBeCanceledTimestamp = this.ToBeCanceledTimestamp,
      CanceledTimestamp = this.CanceledTimestamp
    };

#if DEBUG
    protected OrderBL() { }
#endif
  }

  /// <summary>
  /// Convenience methods for <c>Order</c>.
  /// </summary>
  public class Orders {
    /// <summary>
    /// Use for Sell order books.
    /// </summary>
    public static readonly Comparer<IOrderModel> STRIKE_PRICE_ASCENDING_COMPARER =
      new StrikePriceAscendingComparer();

    /// <summary>
    /// Use for Buy order books.
    /// </summary>
    public static readonly Comparer<IOrderModel> STRIKE_PRICE_DESCENDING_COMPARER =
      new StrikePriceDescendingComparer();

    public class Predicates {
      public static Predicate<IOrderModel> ById(String orderId) {
        return order => order.OrderId.Equals(orderId);
      }

      public static Func<OrderEntity, bool> OpenByTickerAndAction(String ticker,
        OrderAction action) {
        return order => (order.Status == OrderStatus.Open) &&
          (order.Ticker.Equals(ticker)) &&
          (order.Action == action);
      }

      private Predicates() {
        // Prevent instantiation
      }
    }

    private Orders() {
      // Prevent instantiation;
    }

    /// <summary>
    /// Price-Time ascending <c>Comparer</c>.
    /// </summary>
    private class StrikePriceAscendingComparer : Comparer<IOrderModel> {
      public override int Compare(IOrderModel o1, IOrderModel o2) {
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
    private class StrikePriceDescendingComparer:
      Comparer<IOrderModel> {
        public override int Compare(IOrderModel o1, IOrderModel o2) {
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
  public class MarketOrderValidator : IValidator<IOrderModel> {
    public bool IsValid {
      get { throw new NotImplementedException(); }
    }
  }
}
