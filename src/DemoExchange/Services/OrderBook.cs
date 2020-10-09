using System;
using System.Collections.Generic;
using System.Linq;
using DemoExchange.Contexts;
using DemoExchange.Interface;
using DemoExchange.Models;
using Serilog;
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
    private readonly Comparer<IOrderModel> comparer;

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

    public OrderBook(IOrderContext context, String ticker, OrderAction type) {
      Ticker = ticker;
      Type = type;
      comparer = OrderAction.BUY.Equals(type) ?
        Orders.STRIKE_PRICE_DESCENDING_COMPARER :
        Orders.STRIKE_PRICE_ASCENDING_COMPARER;
      LoadOrders(context);
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

    private void LoadOrders(IOrderContext context) {
      context.GetAllOpenOrdersByTickerAndAction(Ticker, Type).ToList()
        .ForEach(entity => {
          Order order = new Order(entity);
          orderIds.Add(order.OrderId, order);
          orders.Add(order);
        });

      orders.Sort(comparer);
    }

#if DEBUG
    public OrderBook(String ticker, OrderAction type) {
      Ticker = ticker;
      Type = type;
      comparer = OrderAction.BUY.Equals(type) ?
        Orders.STRIKE_PRICE_DESCENDING_COMPARER :
        Orders.STRIKE_PRICE_ASCENDING_COMPARER;
    }

    public IDictionary<String, Order> TestOrderIds {
      get { return orderIds; }
    }

    public List<Order> TestOrders {
      get { return orders; }
    }

    public Comparer<IOrderModel> TestComparer {
      get { return comparer; }
    }
#endif

#if PERF
    public OrderBook(String ticker, OrderAction type, bool perf) {
      Ticker = ticker;
      Type = type;
      comparer = OrderAction.BUY.Equals(type) ?
        Orders.STRIKE_PRICE_DESCENDING_COMPARER :
        Orders.STRIKE_PRICE_ASCENDING_COMPARER;
    }

    public void TestPerfAddOrderNoSort(Order order) {
      orderIds.Add(order.OrderId, order);
      orders.Add(order);
    }

    public void TestPerfSort() {
      orders.Sort(comparer);
    }

    public void TestPerfLoadOrders(IOrderContext context) {
      orderIds.Clear();
      orders.Clear();
      LoadOrders(context);
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

  public class OrderTransactionResponse : ResponseBase<OrderTransaction> {
    public OrderTransactionResponse() { }
    public OrderTransactionResponse(OrderTransaction data) : this(Constants.Response.OK, data) { }
    public OrderTransactionResponse(int code, OrderTransaction data) : base(code, data) { }
    public OrderTransactionResponse(int code, OrderTransaction data, Error error) : base(code, data, error) { }
    public OrderTransactionResponse(int code, OrderTransaction data, List<IError> errors) : base(code, data, errors) { }
  }
}
