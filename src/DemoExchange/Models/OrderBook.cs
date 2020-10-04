using System;
using System.Collections.Generic;
using System.Text;
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

    public List<Level2Quote> Level2 {
      get {
        List<Level2Quote> quotes = new List<Level2Quote>();
        int numQuotes = Math.Min(AppConstants.LEVEL_2_QUOTE_SIZE, orders.Count);
        for (int i = 0; i < numQuotes; i++) {
          quotes.Add(new Level2Quote(orders[i].StrikePrice,
            orders[i].OpenQuantity));
        }

        return quotes;
      }
    }

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

  public class Quote {
    public decimal Bid { get; }
    public decimal Ask { get; }
    public decimal Last { get; }
    public int Volume { get; }

    public Quote(decimal bid, decimal ask) {
      Bid = bid;
      Ask = ask;
    }

    public Quote(decimal bid, decimal ask, decimal last, int volume) {
      Bid = bid;
      Ask = ask;
      Last = last;
      Volume = volume;
    }

    public override String ToString() {
      return QuoteType.BID + ": " + AppConstants.FormatPrice(Bid) + " / " +
        QuoteType.ASK + ": " + AppConstants.FormatPrice(Ask) +
        ((Last > 0 && Volume > 0) ?
          " Last: " + AppConstants.FormatPrice(Last) + " Volume: " + Volume :
          "");
    }
  }

  public class Level2Quote {
    public decimal Price { get; }
    public int Quantity { get; }

    public Level2Quote(decimal price, int quantity) {
      Price = price;
      Quantity = quantity;
    }

    public override String ToString() {
      return Quantity + " @ " + AppConstants.FormatPrice(Price);
    }
  }

  public class Level2 {
    public List<Level2Quote> Bid { get; }
    public List<Level2Quote> Ask { get; }

    public Level2(List<Level2Quote> bid, List<Level2Quote> ask) {
      Bid = bid;
      Ask = ask;
    }

    public override String ToString() {
      StringBuilder sb = new StringBuilder();
      sb.Append(QuoteType.BID + ":\n");
      foreach (Level2Quote quote in Bid) {
        sb.Append("  " + quote.ToString() + "\n");
      }
      sb.Append(QuoteType.ASK + ":\n");
      foreach (Level2Quote quote in Ask) {
        sb.Append("  " + quote.ToString() + "\n");
      }
      sb.Append('\n');

      return sb.ToString();
    }
  }

  public enum QuoteType {
    BID,
    ASK
  }
}
