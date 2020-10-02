using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static Utils.Preconditions;

namespace DemoExchange.Models {
  public class OrderBook {
    public const String ERROR_MARKET_ORDER = "Error Type: Market Order Id: {0}";
    public const String ERROR_NOT_OPEN_ORDER = "Error Status: Not Open Order Id: {0}";
    public const String ERROR_TICKER = "Error Ticker: OrderBook {0} received Order Id: {1}";
    public const String ERROR_ACTION = "Error Action: OrderBook {0} received Order Id: {1}";
    public const String ERROR_ORDER_EXISTS = "Error Order Exists : Order Id: {0}";

    public String Ticker { get; }
    public OrderAction Type { get; }

    private readonly ISet<String> orderIds = new HashSet<String>();
    protected readonly List<Order> orders = new List<Order>(); // VisibleForTesting
    protected readonly Comparer<Order> comparer; // VisibleForTesting

    public OrderBook(String ticker, OrderAction type) {
      Ticker = ticker;
      Type = type;
      comparer = OrderAction.BUY.Equals(type) ?
        OrderComparers.STRIKE_PRICE_DESCENDING : OrderComparers.STRIKE_PRICE_ASCENDING;
    }

    public void AddOrder(Order order) {
      CheckArgument(!OrderType.MARKET.Equals(order.Type),
        String.Format(ERROR_MARKET_ORDER, order.Id));
      CheckArgument(OrderStatus.OPEN.Equals(order.Status),
        String.Format(ERROR_NOT_OPEN_ORDER, order.Id));
      CheckArgument(Ticker.Equals(order.Ticker),
        String.Format(ERROR_TICKER, Ticker, order.Id));
      CheckArgument(Type.Equals(order.Action),
        String.Format(ERROR_ACTION, Type, order.Action));

      CheckArgument(!orderIds.Contains(order.Id),
        String.Format(ERROR_ORDER_EXISTS, order.Id));

      orderIds.Add(order.Id);
      orders.Add(order);
      orders.Sort(comparer);
    }
  }
}
