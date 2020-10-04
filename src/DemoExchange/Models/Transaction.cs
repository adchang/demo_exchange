using System;

namespace DemoExchange.Models {
  public class Transaction {

    public String TransactionId { get; }
    public long CreatedTimestamp { get; protected set; }
    public DateTime CreatedDateTime {
      get { return new DateTime(CreatedTimestamp); }
    }
    public String BuyOrderId { get; }
    public String SellOrderId { get; }
    public String Ticker { get; }
    public int Quantity { get; }
    public decimal Price { get; }

    public Transaction(string buyOrderId, string sellOrderId, string ticker, int quantity, decimal price) {
      TransactionId = Guid.NewGuid().ToString();
      CreatedTimestamp = System.Diagnostics.Stopwatch.GetTimestamp();
      BuyOrderId = buyOrderId;
      SellOrderId = sellOrderId;
      Ticker = ticker;
      Quantity = quantity;
      Price = price;
    }

    public override String ToString() {
      return "{TransactionId: " + TransactionId + ", " +
        "CreatedTimestamp: " + CreatedTimestamp + ", " +
        "BuyOrderId: " + BuyOrderId + ", " +
        "SellOrderId: " + SellOrderId + ", " +
        "Ticker: " + Ticker + ", " +
        "Quantity: " + Quantity + ", " +
        "Price: " + Price.ToString("0.0000000000") + ", " +
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
}
