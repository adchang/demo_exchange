using System;
using System.Collections.Generic;
using DemoExchange.Api;

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
  public interface IAccountModel {
    public String AccountId { get; }
    public long CreatedTimestamp { get; }
    public long LastUpdatedTimestamp { get; }
    public AccountStatus Status { get; }
    public String FirstName { get; }
    public String MiddleName { get; }
    public String LastName { get; }

    public List<IAddressModel> Addresses { get; }

    public Account ToMessage();
  }

  public interface IAddressModel {
    public String AddressId { get; }
    public String AccountId { get; }
    public long CreatedTimestamp { get; }
    public long LastUpdatedTimestamp { get; }
    public AddressType Type { get; }
    public String Line1 { get; }
    public String Line2 { get; }
    public String Subdistrict { get; }
    public String District { get; }
    public String City { get; }
    public String Province { get; }
    public String PostalCode { get; }
    public String Country { get; }

    public IAccountModel Account { get; }

    public Address ToMessage();
  }

  /// <summary>
  /// Validators for models.
  /// </summary>
  public interface IValidator<IModel> {
    public bool IsValid { get; }
  }
}
