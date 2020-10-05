using System;

namespace DemoExchange.Interface {
  /// <summary>
  /// Is a model.
  /// </summary>
  public interface IModel { }

  public interface IIsValid {
    public bool IsValid { get; }
  }

  /// <summary>
  /// Model for an order.
  /// </summary>
  public interface IModelOrder : IModel, IIsValid {
    string OrderId { get; set; }
    long CreatedTimestamp { get; set; }
    string AccountId { get; set; }
    OrderStatus Status { get; set; }
    OrderAction Action { get; set; }
    string Ticker { get; set; }
    OrderType Type { get; set; }
    int Quantity { get; set; }
    int OpenQuantity { get; set; }
    decimal OrderPrice { get; set; }
    decimal StrikePrice { get; set; }
    OrderTimeInForce TimeInForce { get; set; }
    long ToBeCanceledTimestamp { get; set; }
    long CanceledTimestamp { get; set; }
  }

  public enum OrderStatus {
    OPEN, // Default
    COMPLETED,
    UPDATED,
    CANCELLED,
    DELETED
  }

  public enum OrderAction {
    BUY, // Default
    SELL
  }

  public enum OrderType {
    MARKET, // Default
    LIMIT,
    STOP_MARKET,
    STOP_LIMIT,
    TRAILING_STOP_MARKET,
    TRAILING_STOP_LIMIT,
    FILL_OR_KILL,
    IMMEDIATE_OR_CANCEL
  }

  public enum OrderTimeInForce {
    DAY, // Default
    GOOD_TIL_CANCELED,
    MARKET_CLOSE
  }

  /// <summary>
  /// Validators for models.
  /// </summary>
  public interface IValidator<IModel> {
    public bool IsValid { get; }
  }
}
