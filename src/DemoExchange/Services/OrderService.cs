using System;
using System.Collections.Generic;
using DemoExchange.Interface;
using DemoExchange.Models;
using static Utils.Preconditions;

namespace DemoExchange.Services {

  public class OrderService : IOrderService {
    /// <summary>
    /// VisibleForTesting
    /// </summary>
    protected readonly IDictionary<String, OrderManager> managers =
      new Dictionary<String, OrderManager>();

    public OrderService() {
      // TODO: OrderService DI
    }

    public void AddTicker(String ticker) {
      // TODO: AddTicker preconditions & tests
      managers.Add(ticker, new OrderManager(ticker));
    }

    public void SubmitOrder(IModelOrder data) {
      // TODO: Add tests
      CheckNotNull(data, paramName : nameof(data));

      Order order = (Order)data;
      managers[order.Ticker].SubmitOrder(order);
    }

    public void CancelOrder(String id) {
      throw new NotImplementedException();
    }

    public Quote GetQuote(String ticker) {
      // TODO: GetQuote preconditions & tests
      return managers[ticker].Quote;
    }

    public Level2 GetLevel2(String ticker) {
      // TODO: GetLevel2 preconditions & tests
      return managers[ticker].Level2;
    }
  }

  // QUESTION: Assumes order book always has at least 1 order, ie market maker
  public class OrderManager {
    public String Ticker { get; protected set; }
    /// <summary>
    /// VisibleForTesting
    /// </summary>
    protected internal OrderBook BuyBook { protected get; set; }
    /// <summary>
    /// VisibleForTesting
    /// </summary>
    protected internal OrderBook SellBook { protected get; set; }

    public Quote Quote {
      get { return new Quote(BuyBook.First.StrikePrice, SellBook.First.StrikePrice); }
    }

    public Level2 Level2 {
      get {
        return new Level2(BuyBook.Level2, SellBook.Level2);
      }
    }

    public OrderManager(String ticker) {
      Ticker = ticker;
      BuyBook = new OrderBook(ticker, OrderAction.BUY);
      SellBook = new OrderBook(ticker, OrderAction.SELL);
    }

    public void SubmitOrder(Order order) {
      if (!order.IsValid()) {
        throw new NotImplementedException(); // TODO
      }

      OrderBook book = OrderType.MARKET.Equals(order.Type) ?
        OrderAction.BUY.Equals(order.Action) ? SellBook : BuyBook :
        OrderAction.BUY.Equals(order.Action) ? BuyBook : SellBook;

      if (OrderType.MARKET.Equals(order.Type)) {
#pragma warning disable IDE0059
        OrderTransaction filled = FillMarketOrder(order, book);
#pragma warning restore IDE0059
#if DIAGNOSTICS
        WriteDetails(filled);
#endif
        // TODO: Persist filled as 1 db transaction
        return;
      }

      // TODO: Persist order insert
      book.AddOrder(order);
      bool done = false;
      while (!done) {
        if (BuyBook.IsEmpty || SellBook.IsEmpty)throw new SystemException("Order book is empty"); // TODO: Handle order book is empty
        if (BuyBook.First.StrikePrice >= SellBook.First.StrikePrice) {
#pragma warning disable IDE0059
          OrderTransaction filled = FillLimitOrder();
#pragma warning restore IDE0059
#if DIAGNOSTICS
          WriteDetails(filled);
#endif
        } else {
          done = true;
        }
      }
    }

    /// <summary>
    /// VisibleForTesting
    /// </summary>
    virtual protected OrderTransaction FillMarketOrder(Order order, OrderBook book) {
#if PERF
      long start = System.Diagnostics.Stopwatch.GetTimestamp();
#endif
      CheckArgument(!order.Action.Equals(book.Type), "Error: Wrong book");

      bool isBuy = OrderAction.BUY.Equals(order.Action);
      bool done = false;
      List<Order> filledOrders = new List<Order>();
      List<Transaction> transactions = new List<Transaction>();
      while (!done) {
        if (book.IsEmpty)throw new SystemException("Order book is empty"); // TODO: Handle order book is empty
        Order buyOrder = isBuy ? order : book.First;
        Order sellOrder = isBuy ? book.First : order;
        decimal executionPrice = isBuy ? sellOrder.StrikePrice : buyOrder.StrikePrice;
        int fillQuantity = Math.Min(buyOrder.OpenQuantity, sellOrder.OpenQuantity);
        if (!BuyerCanFillOrder(buyOrder.AccountId, fillQuantity, executionPrice)) {
          throw new NotImplementedException(); // TODO
        }
        if (!SellerCanFillOrder(buyOrder.AccountId, fillQuantity, executionPrice)) {
          throw new NotImplementedException(); // TODO
        }
        buyOrder.OpenQuantity -= fillQuantity;
        sellOrder.OpenQuantity -= fillQuantity;
        transactions.Add(new Transaction(buyOrder.Id, sellOrder.Id, Ticker,
          fillQuantity, executionPrice));
        filledOrders.Add(book.First);
        if (book.First.IsFilled) {
          Order filledOrder = book.First;
          book.RemoveOrder(filledOrder);
          filledOrder.Status = OrderStatus.COMPLETED;
        }
        if (order.IsFilled) {
          order.Status = OrderStatus.COMPLETED;
          filledOrders.Add(order);
          done = true;
        }
      }
#if PERF
      long stop = System.Diagnostics.Stopwatch.GetTimestamp();
      String msg = String.Format("Market order executed in {0} ms", ((stop - start) / TimeSpan.TicksPerMillisecond));
      Console.WriteLine(msg);
#endif

      return new OrderTransaction(filledOrders, transactions);
    }

    /// <summary>
    /// VisibleForTesting
    /// </summary>
    virtual protected OrderTransaction FillLimitOrder() {
#if PERF
      long start = System.Diagnostics.Stopwatch.GetTimestamp();
#endif
      Order buyOrder = BuyBook.First;
      Order sellOrder = SellBook.First;
      List<Order> filledOrders = new List<Order>();
      List<Transaction> transactions = new List<Transaction>();
      // TODO: Execute the limit order at sell price
      decimal executionPrice = sellOrder.StrikePrice;
      int fillQuantity = Math.Min(buyOrder.OpenQuantity, sellOrder.OpenQuantity);
      if (!BuyerCanFillOrder(buyOrder.AccountId, fillQuantity, executionPrice)) {
        throw new NotImplementedException(); // TODO
      }
      if (!SellerCanFillOrder(buyOrder.AccountId, fillQuantity, executionPrice)) {
        throw new NotImplementedException(); // TODO
      }
      buyOrder.OpenQuantity -= fillQuantity;
      sellOrder.OpenQuantity -= fillQuantity;
      transactions.Add(new Transaction(buyOrder.Id, sellOrder.Id, Ticker,
        fillQuantity, executionPrice));
      filledOrders.Add(SellBook.First);
      if (SellBook.First.IsFilled) {
        Order filledOrder = SellBook.First;
        SellBook.RemoveOrder(filledOrder);
        filledOrder.Status = OrderStatus.COMPLETED;
      }
      filledOrders.Add(BuyBook.First);
      if (BuyBook.First.IsFilled) {
        Order filledOrder = BuyBook.First;
        BuyBook.RemoveOrder(filledOrder);
        filledOrder.Status = OrderStatus.COMPLETED;
      }
#if PERF
      long stop = System.Diagnostics.Stopwatch.GetTimestamp();
      String msg = String.Format("Limit order executed in {0} ms", ((stop - start) / TimeSpan.TicksPerMillisecond));
      Console.WriteLine(msg);
#endif

      return new OrderTransaction(filledOrders, transactions);
    }

    /// <summary>
    /// VisibleForTesting
    /// </summary>
#pragma warning disable IDE0060, CA1822
    protected bool BuyerCanFillOrder(String accountId, int quanity, decimal price) {
      return true; // TODO
    }

    /// <summary>
    /// VisibleForTesting
    /// </summary>
    protected bool SellerCanFillOrder(String accountId, int quanity, decimal price) {
      return true; // TODO
    }
#pragma warning restore IDE0060, CA1822

#if DIAGNOSTICS
    private void WriteDetails(OrderTransaction tran) {
      Console.WriteLine("Order details:");
      foreach (Order filledOrder in tran.Orders) {
        Console.WriteLine("     " + filledOrder.ToString());
      }
      Console.WriteLine("Transaction details:");
      foreach (Transaction aTran in tran.Transactions) {
        Console.WriteLine("     " + aTran.ToString());
      }
      Console.WriteLine("  SPREAD: " + Quote);
      Console.WriteLine("  LEVEL 2:\n" + Level2);
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
}
