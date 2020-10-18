using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Utils.Time;

namespace DemoExchange.Models {
  /// <summary>
  /// For persistence of a <c>Transaction</c>.
  /// </summary>
  [Table("ExchangeTransaction")]
  public class TransactionEntity {
    [Key]
    public Guid TransactionId { get; set; }

    [Required]
    public long CreatedTimestamp { get; set; }

    [Required]
    public OrderEntity BuyOrder { get; set; }

    [Required]
    public OrderEntity SellOrder { get; set; }

    [Required]
    public String Ticker { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public decimal Price { get; set; }

    public override String ToString() {
      return "{TransactionId: " + TransactionId + ", " +
        "CreatedTimestamp: " + CreatedTimestamp + ", " +
        "BuyOrder: " + BuyOrder.ToString() + ", " +
        "SellOrder: " + SellOrder.ToString() + ", " +
        "Ticker: " + Ticker + ", " +
        "Quantity: " + Quantity + ", " +
        "Price: " + Price.ToString("0.0000000000") + ", " +
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

  public class Transaction : TransactionEntity {
    public new String TransactionId {
      get { return base.TransactionId.ToString(); }
    }
    public new long CreatedTimestamp {
      get { return base.CreatedTimestamp; }
    }
    public DateTime CreatedDateTime {
      get { return FromTicks(CreatedTimestamp); }
    }
    public new OrderBL BuyOrder {
      get { return (OrderBL)base.BuyOrder; }
    }
    public new OrderBL SellOrder {
      get { return (OrderBL)base.SellOrder; }
    }
    public new String Ticker {
      get { return base.Ticker; }
    }
    public new int Quantity {
      get { return base.Quantity; }
    }
    public new decimal Price {
      get { return base.Price; }
    }

    public Transaction(OrderBL buyOrder, OrderBL sellOrder, string ticker, int quantity,
      decimal price) {
      // TODO: Precondtions
      base.TransactionId = Guid.NewGuid();
      base.CreatedTimestamp = Now;
      base.BuyOrder = buyOrder;
      base.SellOrder = sellOrder;
      base.Ticker = ticker;
      base.Quantity = quantity;
      base.Price = price;
    }
  }
}
