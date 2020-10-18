using System;
using System.Collections.Generic;
using System.Linq;
using DemoExchange.Api;
using DemoExchange.Contexts;
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

    private readonly IDictionary<String, OrderBL> orderIds =
      new Dictionary<String, OrderBL>();
    private readonly List<OrderBL> orders = new List<OrderBL>();
    private readonly Comparer<IOrderModel> comparer;

    public String Ticker { get; }
    public OrderAction Type { get; }
    public String Name {
      get { return Ticker + " " + Type; }
    }
    public OrderBL First {
      get { return orders[0]; }
    }
    public int Count {
      get { return orders.Count; }
    }
    public bool IsEmpty {
      get { return orders.Count == 0; }
    }
    public List<Level2Quote> Level2 {
      get {
        List<Level2Quote> quotes = new List<Level2Quote>();
        int numQuotes = Math.Min(AppConstants.LEVEL_2_QUOTE_SIZE, orders.Count);
        for (int i = 0; i < numQuotes; i++) {
          quotes.Add(new Level2Quote {
            Price = Convert.ToDouble(orders[i].StrikePrice),
              Quantity = orders[i].OpenQuantity
          });
        }

        return quotes;
      }
    }

    public OrderBook(IOrderContext context, String ticker, OrderAction type) {
      Ticker = ticker;
      Type = type;
      comparer = OrderAction.OrderBuy.Equals(type) ?
        Orders.STRIKE_PRICE_DESCENDING_COMPARER :
        Orders.STRIKE_PRICE_ASCENDING_COMPARER;
      LoadOrders(context);
    }

    public void AddOrder(OrderBL order) {
      CheckNotNull(order, paramName : nameof(order));
      CheckArgument(!OrderType.OrderMarket.Equals(order.Type),
        String.Format(ERROR_MARKET_ORDER, order.OrderId));
      CheckArgument(OrderStatus.OrderOpen.Equals(order.Status),
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

    public OrderBL CancelOrder(String orderId) {
      CheckNotNullOrWhitespace(orderId, paramName : nameof(orderId));

      CheckArgument(orderIds.ContainsKey(orderId),
        String.Format(ERROR_ORDER_NOT_EXISTS, orderId));

      OrderBL order = orderIds[orderId];
      RemoveOrder(order);
      order.Cancel();

      return order;
    }

    public void RemoveOrder(OrderBL order) {
      CheckNotNull(order);

      CheckArgument(orderIds.ContainsKey(order.OrderId),
        String.Format(ERROR_ORDER_NOT_EXISTS, order.OrderId));

      orderIds.Remove(order.OrderId);
      orders.Remove(order);
    }

    private void LoadOrders(IOrderContext context) {
      context.GetAllOpenOrdersByTickerAndAction(Ticker, Type).ToList()
        .ForEach(entity => {
          OrderBL order = new OrderBL(entity);
          orderIds.Add(order.OrderId, order);
          orders.Add(order);
        });

      orders.Sort(comparer);
    }

#if DEBUG
    public OrderBook(String ticker, OrderAction type) {
      Ticker = ticker;
      Type = type;
      comparer = OrderAction.OrderBuy.Equals(type) ?
        Orders.STRIKE_PRICE_DESCENDING_COMPARER :
        Orders.STRIKE_PRICE_ASCENDING_COMPARER;
    }

    public IDictionary<String, OrderBL> TestOrderIds {
      get { return orderIds; }
    }

    public List<OrderBL> TestOrders {
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
      comparer = OrderAction.OrderBuy.Equals(type) ?
        Orders.STRIKE_PRICE_DESCENDING_COMPARER :
        Orders.STRIKE_PRICE_ASCENDING_COMPARER;
    }

    public void TestPerfAddOrderNoSort(OrderBL order) {
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

  public class OrderTransactionBL {
    public List<OrderBL> Orders { get; }
    public List<Transaction> Transactions { get; }

    public OrderTransactionBL(List<OrderBL> orders, List<Transaction> transactions) {
      Orders = orders;
      Transactions = transactions;
    }
  }

  public class OrderTransactionResponseBL : ResponseBase<OrderTransactionBL, String> {
    public OrderTransactionResponseBL() { }
    public OrderTransactionResponseBL(OrderTransactionBL data) : base(data) { }
    public OrderTransactionResponseBL(int code, OrderTransactionBL data) : base(code, data) { }
    public OrderTransactionResponseBL(int code, OrderTransactionBL data, Error error) : base(code, data, error) { }
    public OrderTransactionResponseBL(int code, OrderTransactionBL data, List<Error> errors) : base(code, data, errors) { }
    public override String ToMessage() {
      return "" + Code;
    }
  }
}
