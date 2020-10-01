using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static Utils.Preconditions;

// NTH: Investigate [assembly : InternalsVisibleTo("DemoExchangeTest")]
//       Add to snippet when figured out

namespace DemoExchange.Models {
  public class OrderBook {
    public const String ERROR_MARKET_ORDER = "Order book should not have Market orders ";
    public const String ERROR_TICKER = "Error Ticker: OrderBook {0} received Order Id: {1}";
    public const String ERROR_ACTION = "Error Action: OrderBook {0} received Order Id: {1}";
    public const String ERROR_ORDER_EXISTS = "Error Order Exists : Order Id: {0}";

    public String Ticker { get; }
    public OrderAction Type { get; }

    private ISet<String> orderIds = new HashSet<String>();

    public OrderBook(String ticker, OrderAction type) {
      Ticker = ticker;
      Type = type;
    }

    public void AddOrder(Order order) {
      CheckArgument(!OrderType.MARKET.Equals(order.Type), ERROR_MARKET_ORDER);
      CheckArgument(Ticker.Equals(order.Ticker),
        String.Format(ERROR_TICKER, Ticker, order.Id));
      CheckArgument(Type.Equals(order.Action),
        String.Format(ERROR_ACTION, Type, order.Action));
      CheckArgument(!orderIds.Contains(order.Id),
        String.Format(ERROR_ORDER_EXISTS, order.Id));

      orderIds.Add(order.Id);
      // FIXME: Add to list
    }
  }
}
