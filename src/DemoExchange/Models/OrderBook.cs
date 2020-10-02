using System;
using System.Collections.Generic;
using static Utils.Preconditions;

// QUESTION: Use timer callbacks to manage GTC order?
namespace DemoExchange.Models {
  public class OrderBook {
    public const String ERROR_MARKET_ORDER = "Error Type: Market Order Id: {0}";
    public const String ERROR_NOT_OPEN_ORDER = "Error Status: Not Open Order Id: {0}";
    public const String ERROR_TICKER = "Error Ticker: OrderBook {0} received Order Id: {1}";
    public const String ERROR_ACTION = "Error Action: OrderBook {0} received Order Id: {1}";
    public const String ERROR_ORDER_EXISTS = "Error Order Exists : Order Id: {0}";
    public const String ERROR_ORDER_NOT_EXISTS = "Error Order Not Exists : Order Id: {0}";

    public String Ticker { get; }
    public OrderAction Type { get; }
    public String Name {
      get { return Ticker + " " + Type; }
    }
    public int Count {
      get { return orders.Count; }
    }
    public bool IsEmpty {
      get { return orders.Count == 0; }
    }

    /// <summary>
    /// VisibleForTesting
    /// </summary>
    protected readonly IDictionary<String, Order> orderIds = new Dictionary<String, Order>();
    /// <summary>
    /// VisibleForTesting
    /// </summary>
    protected readonly List<Order> orders = new List<Order>();
    /// <summary>
    /// VisibleForTesting
    /// </summary>
    protected readonly Comparer<Order> comparer;

    public Order First {
      get { return orders[0]; }
    }

    public OrderBook(String ticker, OrderAction type) {
      Ticker = ticker;
      Type = type;
      comparer = OrderAction.BUY.Equals(type) ?
        Orders.STRIKE_PRICE_DESCENDING_COMPARER :
        Orders.STRIKE_PRICE_ASCENDING_COMPARER;
    }

    public void AddOrder(Order order) {
      CheckNotNull(order, paramName : nameof(order));
      CheckArgument(!OrderType.MARKET.Equals(order.Type),
        String.Format(ERROR_MARKET_ORDER, order.Id));
      CheckArgument(OrderStatus.OPEN.Equals(order.Status),
        String.Format(ERROR_NOT_OPEN_ORDER, order.Id));
      CheckArgument(Ticker.Equals(order.Ticker),
        String.Format(ERROR_TICKER, Ticker, order.Id));
      CheckArgument(Type.Equals(order.Action),
        String.Format(ERROR_ACTION, Type, order.Action));

      CheckArgument(!orderIds.ContainsKey(order.Id),
        String.Format(ERROR_ORDER_EXISTS, order.Id));

      orderIds.Add(order.Id, order);
      orders.Add(order);
      orders.Sort(comparer);
    }

    public Order CancelOrder(String id) {
      CheckNotNullOrWhitespace(id, paramName: "Id");

      CheckArgument(orderIds.ContainsKey(id),
        String.Format(ERROR_ORDER_NOT_EXISTS, id));

      Order order = orderIds[id];
      RemoveOrder(order);
      order.Cancel();

      return order;
    }

    public void RemoveOrder(Order order) {
      CheckNotNull(order);

      CheckArgument(orderIds.ContainsKey(order.Id),
        String.Format(ERROR_ORDER_NOT_EXISTS, order.Id));

      orderIds.Remove(order.Id);
      orders.Remove(order);
    }
  }
}
