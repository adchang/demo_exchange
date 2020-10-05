using System;
using System.Collections.Generic;
using System.Threading;
using DemoExchange.Interface;
using DemoExchange.Models;
using static Utils.Preconditions;
using static Utils.Time;

namespace DemoExchange.Services {
  public class OrderService : IOrderService {
    public const String MARKET_CLOSED = "Market is closed.";

    private readonly DemoExchangeContext db; // How to do DI? IDemoExchangeContext db;
    private readonly IDictionary<String, OrderManager> managers =
      new Dictionary<String, OrderManager>();
    private bool marketOpen = false;

    public OrderService() {
      // TODO: OrderService DI
      //db = new DemoExchangeContext();
      // int numRetries = 0;
      // while (!db.Database.CanConnect() && numRetries < 10) {
      //   Thread.Sleep(5000);
      //   numRetries++;
      // }
      // if (!db.Database.CanConnect())throw new InvalidOperationException("Cannot start db");
    }

    private void MarketIsOpen() {
      if (!marketOpen)throw new InvalidOperationException(MARKET_CLOSED);
    }

    public void OpenMarket() {
#if DEBUG
      marketOpen = true;
#else
      // TODO If Market hours, set marketOpen to true;
      throw new InvalidOperationException(MARKET_CLOSED);
#endif
    }

    public void CloseMarket() {
#if DEBUG
      marketOpen = false;
#else
      // TODO If !Market hours, set marketOpen to false;
      throw new InvalidOperationException(MARKET_CLOSED);
#endif
    }

    public void AddTicker(String ticker) {
      // TODO: AddTicker preconditions & tests
      lock(managers) {
        managers.Add(ticker, new OrderManager(db, ticker));
      }
    }

    public void SubmitOrder(IModelOrder data) {
      // TODO: MarketIsOpen(); This is not correct; should still be able to submit 
      //       orders if market is not open; Just can't trade.
      // TODO: Add tests
      CheckNotNull(data, paramName : nameof(data));

      Order order = (Order)data;
      OrderManager manager = managers[order.Ticker];
      lock(manager) {
        manager.SubmitOrder(order);
      }
    }

    public void CancelOrder(String orderId) {
      throw new NotImplementedException();
    }

    public IQuote GetQuote(String ticker) {
      // TODO: GetQuote preconditions & tests
      return managers[ticker].Quote;
    }

    public ILevel2 GetLevel2(String ticker) {
      // TODO: GetLevel2 preconditions & tests
      return managers[ticker].Level2;
    }

#if PERF
    public void TestPerfAddTicker(String ticker,
      List<Order> buyOrders, List<Order> sellOrders) {
      lock(managers) {
        OrderManager manager = new OrderManager(db, ticker);
        manager.TestPerfAddOrder(buyOrders, sellOrders);
        managers.Add(ticker, manager);
      }
    }
#endif
  }

  // QUESTION: Assumes order book always has at least 1 order, ie market maker
  public class OrderManager {
    private readonly DemoExchangeContext db;
    private readonly OrderBook BuyBook;
    private readonly OrderBook SellBook;

    public Quote Quote {
      get { return new Quote(BuyBook.First.StrikePrice, SellBook.First.StrikePrice); }
    }
    public Level2 Level2 {
      get { return new Level2(BuyBook.Level2, SellBook.Level2); }
    }

    public OrderManager(IDemoExchangeContext dbContext, String ticker) {
      db = (DemoExchangeContext)dbContext;
      BuyBook = new OrderBook(ticker, OrderAction.BUY);
      SellBook = new OrderBook(ticker, OrderAction.SELL);
    }

    public void SubmitOrder(Order order) {
#if PERF
      long start = Now;
#endif
      if (!order.IsValid) {
        throw new NotImplementedException(); // TODO
      }

      OrderBook book = OrderType.MARKET.Equals(order.Type) ?
        OrderAction.BUY.Equals(order.Action) ? SellBook : BuyBook :
        OrderAction.BUY.Equals(order.Action) ? BuyBook : SellBook;

      if (OrderType.MARKET.Equals(order.Type)) {
        OrderTransaction filled = FillMarketOrder(order, book);
#if DIAGNOSTICS
        DiagnosticsWriteDetails(filled);
#endif
        // foreach (Order o in filled.Orders) {
        //   db.Orders.Add(o);
        // }
        // foreach (Transaction t in filled.Transactions) {
        //   db.Transactions.Add(t);
        // }
        // HERE db.SaveChanges();
        // TODO: Persist filled as 1 db transaction
#if PERF
        Console.WriteLine(String.Format("SubmitOrder: Market order executed in {0} ms", ((Now - start) / TimeSpan.TicksPerMillisecond)));
#endif
        return;
      }

      // TODO: Persist order insert
      book.AddOrder(order);
      bool done = false;
      while (!done) {
        if (BuyBook.IsEmpty || SellBook.IsEmpty)throw new SystemException("Order book is empty"); // TODO: Handle order book is empty
        if (BuyBook.First.StrikePrice >= SellBook.First.StrikePrice) {
          OrderTransaction filled = TryFillOrderBook();
#if DIAGNOSTICS
          DiagnosticsWriteDetails(filled);
#endif
        } else {
          done = true;
        }
      }
#if PERF
      Console.WriteLine(String.Format("SubmitOrder: Processed in {0} ms", ((Now - start) / TimeSpan.TicksPerMillisecond)));
#endif
    }

    private OrderTransaction FillMarketOrder(Order order, OrderBook book) {
#if PERF_FINEST
      long start = Now;
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
        transactions.Add(new Transaction(buyOrder, sellOrder, buyOrder.Ticker,
          fillQuantity, executionPrice));
        filledOrders.Add(book.First);
        if (book.First.IsFilled) {
          Order filledOrder = book.First;
          book.RemoveOrder(filledOrder);
          filledOrder.Complete();
        }
        if (order.IsFilled) {
          order.Complete();
          filledOrders.Add(order);
          done = true;
        }
      }
#if PERF_FINEST
      Console.WriteLine(String.Format("Market order executed in {0} ms", ((Now - start) / TimeSpan.TicksPerMillisecond)));
#endif

      return new OrderTransaction(filledOrders, transactions);
    }

    private OrderTransaction TryFillOrderBook() {
#if PERF_FINEST
      long start = Now;
#endif
      Order buyOrder = BuyBook.First;
      Order sellOrder = SellBook.First;
      List<Order> filledOrders = new List<Order>();
      List<Transaction> transactions = new List<Transaction>();
      // QUESTION: Execute the limit order at sell price benefits BUYER fat finger
      //           What to do if SELLER fat finger? Shouldn't that give the SELLER
      //           the higher BID price?
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
      transactions.Add(new Transaction(buyOrder, sellOrder, buyOrder.Ticker,
        fillQuantity, executionPrice));
      filledOrders.Add(SellBook.First);
      if (SellBook.First.IsFilled) {
        Order filledOrder = SellBook.First;
        SellBook.RemoveOrder(filledOrder);
        filledOrder.Complete();
      }
      filledOrders.Add(BuyBook.First);
      if (BuyBook.First.IsFilled) {
        Order filledOrder = BuyBook.First;
        BuyBook.RemoveOrder(filledOrder);
        filledOrder.Complete();
      }
#if PERF_FINEST
      Console.WriteLine(String.Format("TryFillOrderBook executed in {0} ms", ((Now - start) / TimeSpan.TicksPerMillisecond)));
#endif

      return new OrderTransaction(filledOrders, transactions);
    }

#pragma warning disable IDE0060, CA1822
    private bool BuyerCanFillOrder(String accountId, int quanity, decimal price) {
      return true; // TODO
    }

    private bool SellerCanFillOrder(String accountId, int quanity, decimal price) {
      return true; // TODO
    }
#pragma warning restore IDE0060, CA1822

#if DEBUG

#endif

#if DIAGNOSTICS
    private void DiagnosticsWriteDetails(OrderTransaction tran) {
      Console.WriteLine("Order details:");
      foreach (Order filledOrder in tran.Orders) {
        Console.WriteLine("     " + filledOrder.ToString());
      }
      Console.WriteLine("Transaction details:");
      foreach (Transaction aTran in tran.Transactions) {
        Console.WriteLine("     " + aTran.ToString());
      }
      Quote q = (Quote)Quote;
      Console.WriteLine("  SPREAD: " + q.ToString());
      Level2 l2 = (Level2)Level2;
      Console.WriteLine("  LEVEL 2:\n" + l2.ToString());
    }
#endif

#if PERF
    public void TestPerfAddOrder(List<Order> buyOrders, List<Order> sellOrders) {
      foreach (Order order in buyOrders) {
        // TODO persist order
        BuyBook.TestPerfAddOrderNoSort(order);
        BuyBook.TestPerfSort();
      }
      foreach (Order order in sellOrders) {
        // TODO persist order
        SellBook.TestPerfAddOrderNoSort(order);
        SellBook.TestPerfSort();
      }
    }
#endif
  }
}
