using System;
using DemoExchange.Api;
using DemoExchange.Api.Order;

namespace DemoExchange.Interface {
  public interface IIsValid {
    public bool IsValid { get; }
  }

  /// <summary>
  /// Model for an order.
  /// </summary>
  public interface IOrderModel {
    public const String ORDER_ID_NEW = "NEW";

    public const String ERROR_STRING_EMTPY = "Cannot be empty";
    public const String ERROR_QUANTITY_IS_0 = "quantity must be greater than 0";
    public const String ERROR_OPEN_QUANITY_GREATER_THAN_QUANITY = "openQuantity cannot be greater than original quantity";
    public const String ERROR_ORDER_PRICE_MARKET_NOT_0 = "orderPrice should be 0 for Market orders";
    public const String ERROR_ORDER_PRICE_IS_0 = "orderPrice must be greater than 0";

    public String OrderId { get; }
    public long CreatedTimestamp { get; }
    public String AccountId { get; }
    public OrderStatus Status { get; }
    public OrderAction Action { get; }
    public String Ticker { get; }
    public OrderType Type { get; }
    public int Quantity { get; }
    public int OpenQuantity { get; }
    public decimal OrderPrice { get; }
    public decimal StrikePrice { get; }
    public OrderTimeInForce TimeInForce { get; }
    public long ToBeCanceledTimestamp { get; }
    public long CanceledTimestamp { get; }

    public Order ToMessage();
  }

  /// <summary>
  /// Model for an account.
  /// </summary>
  public interface IAccountModel : IIsValid {
    public String AccountId { get; }
  }

  public class AccountBase : IAccountModel {
    public String AccountId { get; }

    public virtual bool IsValid {
      get { return true; }
    }

    public AccountBase(String accountId) {
      AccountId = accountId;
    }
  }

  /// <summary>
  /// Validators for models.
  /// </summary>
  public interface IValidator<IModel> {
    public bool IsValid { get; }
  }
}
