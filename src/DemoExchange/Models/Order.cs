using System;

namespace DemoExchange.Models {
  public class Order {
    public String OrderId { get; }
    public int Quantity { get; }
    public int OpenQuantity { get; set; }
    public bool IsFilled {
      get {
        return OpenQuantity == 0;
      }
    }
    public OrderType OrderType { get; }

    public Order(int quantity, OrderType orderType) {
      this.Quantity = quantity;
      this.OpenQuantity = quantity;
      this.OrderType = orderType;
    }

    public override String ToString() {
      return "{OrderId: " + Utils.GetVal(OrderId) + ", " +
        "Quantity: " + Quantity + ", " +
        "OpenQuantity: " + OpenQuantity + ", " +
        "OrderType: " + OrderType + ", " +
        "}";
    }

    public override bool Equals(object obj) {
      if (obj == null || GetType() != obj.GetType()) {
        return false;
      }

      return this.ToString() == obj.ToString();
    }

    public override int GetHashCode() {
      return HashCode.Combine(OrderId, Quantity, OpenQuantity, OrderType);
    }
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
}
