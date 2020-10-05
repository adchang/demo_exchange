using System;
using System.Collections.Generic;
using System.Text;
using DemoExchange.Interface;
using DemoExchange.Models;
using static Utils.Preconditions;

// QUESTION: Use timer callbacks to manage GTC order?
namespace DemoExchange.Services {
  public class OrderBook {
    public const String ERROR_MARKET_ORDER = "Error Type: Market OrderId: {0}";
    public const String ERROR_NOT_OPEN_ORDER = "Error Status: Not Open OrderId: {0}";
    public const String ERROR_TICKER = "Error Ticker: OrderBook {0} received OrderId: {1}";
    public const String ERROR_ACTION = "Error Action: OrderBook {0} received OrderId: {1}";
    public const String ERROR_ORDER_EXISTS = "Error Order Exists : OrderId: {0}";
    public const String ERROR_ORDER_NOT_EXISTS = "Error Order Not Exists : OrderId: {0}";

    private readonly IDictionary<String, Order> orderIds =
      new Dictionary<String, Order>();
    private readonly List<Order> orders = new List<Order>();
    private readonly Comparer<Order> comparer;

    public String Ticker { get; }
    public OrderAction Type { get; }
    public String Name {
      get { return Ticker + " " + Type; }
    }
    public Order First {
      get { return orders[0]; }
    }
    public int Count {
      get { return orders.Count; }
    }
    public bool IsEmpty {
      get { return orders.Count == 0; }
    }
    public List<ILevel2Quote> Level2 {
      get {
        List<ILevel2Quote> quotes = new List<ILevel2Quote>();
        int numQuotes = Math.Min(AppConstants.LEVEL_2_QUOTE_SIZE, orders.Count);
        for (int i = 0; i < numQuotes; i++) {
          quotes.Add(new Level2Quote(orders[i].StrikePrice,
            orders[i].OpenQuantity));
        }

        return quotes;
      }
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
        String.Format(ERROR_MARKET_ORDER, order.OrderId));
      CheckArgument(OrderStatus.OPEN.Equals(order.Status),
        String.Format(ERROR_NOT_OPEN_ORDER, order.OrderId));
      CheckArgument(Ticker.Equals(order.Ticker),
        String.Format(ERROR_TICKER, Ticker, order.OrderId));
      CheckArgument(Type.Equals(order.Action),
        String.Format(ERROR_ACTION, Type, order.Action));

      CheckArgument(!orderIds.ContainsKey(order.OrderId),
        String.Format(ERROR_ORDER_EXISTS, order.OrderId));

      orderIds.Add(order.OrderId, order);
      orders.Add(order);
      orders.Sort(comparer);
    }

    public Order CancelOrder(String orderId) {
      CheckNotNullOrWhitespace(orderId, paramName : nameof(orderId));

      CheckArgument(orderIds.ContainsKey(orderId),
        String.Format(ERROR_ORDER_NOT_EXISTS, orderId));

      Order order = orderIds[orderId];
      RemoveOrder(order);
      order.Cancel();

      return order;
    }

    public void RemoveOrder(Order order) {
      CheckNotNull(order);

      CheckArgument(orderIds.ContainsKey(order.OrderId),
        String.Format(ERROR_ORDER_NOT_EXISTS, order.OrderId));

      orderIds.Remove(order.OrderId);
      orders.Remove(order);
    }

#if DEBUG
    public IDictionary<String, Order> TestOrderIds {
      get { return orderIds; }
    }

    public List<Order> TestOrders {
      get { return orders; }
    }

    public Comparer<Order> TestComparer {
      get { return comparer; }
    }
#endif

#if PERF
    public void TestPerfAddOrderNoSort(Order order) {
      orderIds.Add(order.OrderId, order);
      orders.Add(order);
    }

    public void TestPerfSort() {
      orders.Sort(comparer);
    }
#endif
  }

  public class OrderTransaction {
    public List<Order> Orders { get; }
    public List<Transaction> Transactions { get; }

    public OrderTransaction(List<Order> orders, List<Transaction> transactions) {
      Orders = orders;
      Transactions = transactions;
    }
  }

  public class Quote : IQuote {
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

    public new String ToString() {
      return QuoteType.BID + ": " + AppConstants.FormatPrice(Bid) + " / " +
        QuoteType.ASK + ": " + AppConstants.FormatPrice(Ask) +
        ((Last > 0 && Volume > 0) ?
          " Last: " + AppConstants.FormatPrice(Last) + " Volume: " + Volume :
          "");
    }
  }

  public class Level2Quote : ILevel2Quote {
    public decimal Price { get; }
    public int Quantity { get; }

    public Level2Quote(decimal price, int quantity) {
      Price = price;
      Quantity = quantity;
    }

    public new String ToString() {
      return Quantity + " @ " + AppConstants.FormatPrice(Price);
    }
  }

  public class Level2 : ILevel2 {
    public List<ILevel2Quote> Bid { get; }
    public List<ILevel2Quote> Ask { get; }

    public Level2(List<ILevel2Quote> bid, List<ILevel2Quote> ask) {
      Bid = bid;
      Ask = ask;
    }

    public new String ToString() {
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