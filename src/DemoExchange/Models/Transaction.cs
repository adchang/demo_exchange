using System;
using System.ComponentModel.DataAnnotations.Schema;
using DemoExchange.Interface;
using static Utils.Time;

namespace DemoExchange.Models {
  /// <summary>
  /// Model for a transaction.
  /// </summary>
  public interface IModelTransaction : IModel {
    string TransactionId { get; set; }
    long CreatedTimestamp { get; set; }
    IModelOrder BuyOrder { get; set; }
    IModelOrder SellOrder { get; set; }
    string Ticker { get; set; }
    int Quantity { get; set; }
    decimal Price { get; set; }
  }

  /// <summary>
  /// For persistence of a <c>Transaction</c>.
  /// </summary>
  [Table("ExchangeTransaction")]
  public class ExchangeTransactionEntity : IModelTransaction {
    public virtual String TransactionId { get; set; }
    public virtual long CreatedTimestamp { get; set; }
    public virtual IModelOrder BuyOrder { get; set; }
    public virtual IModelOrder SellOrder { get; set; }
    public virtual String Ticker { get; set; }
    public virtual int Quantity { get; set; }
    public virtual decimal Price { get; set; }

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

  public class Transaction : ExchangeTransactionEntity {
    public new String TransactionId {
      get { return base.TransactionId; }
    }
    public new long CreatedTimestamp {
      get { return base.CreatedTimestamp; }
    }
    public DateTime CreatedDateTime {
      get { return FromTicks(CreatedTimestamp); }
    }
    public new Order BuyOrder {
      get { return (Order)base.BuyOrder; }
    }
    public new Order SellOrder {
      get { return (Order)base.SellOrder; }
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

    public Transaction(Order buyOrder, Order sellOrder, string ticker, int quantity,
      decimal price) {
      // TODO Precondtions
      base.TransactionId = Guid.NewGuid().ToString();
      base.CreatedTimestamp = Now;
      base.BuyOrder = buyOrder;
      base.SellOrder = sellOrder;
      base.Ticker = ticker;
      base.Quantity = quantity;
      base.Price = price;
    }
  }
}
