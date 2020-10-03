using System;
using System.Collections.Generic;
using DemoExchange.Models;
using DemoExchange.Services;

namespace DemoExchangeSimulator {
  public class Simulator {
    readonly TestOrderService service = new TestOrderService();

#pragma warning disable IDE0059
    public void Start() {
      int minOrders = 3000;
      int trades = 1068;
      Random rnd = new Random();
      List<String> tickers = new List<String> { "ERX", "SPY", "DIA" };
      foreach (String ticker in tickers) {
        service.AddTicker(ticker);
        TestOrderManager mgr = service.GetManager(ticker);
        TestOrderBook book = mgr.TestBuyBook;
        int numOrders = rnd.Next(minOrders, 2 * minOrders);
        long start = System.Diagnostics.Stopwatch.GetTimestamp();
        int basePrice = rnd.Next(8, 10);
        for (int i = 0; i < numOrders; i++) {
          int quantity = (rnd.Next(1, 3) == 1) ?
            rnd.Next(1, 6) * 100 :
            rnd.Next(50, 849);
          decimal price = basePrice + rnd.Next(1, 10000) / 100000M;
          book.AddOrderNoSort(new BuyLimitDayOrder("buy" + i,
            ticker, quantity, price));
        }
        long stop = System.Diagnostics.Stopwatch.GetTimestamp();
        Console.WriteLine(String.Format("Added {1} {2} BUY orders in {0} ms", ((stop - start) / TimeSpan.TicksPerMillisecond), numOrders, ticker));
        start = System.Diagnostics.Stopwatch.GetTimestamp();
        book.AddOrder(new BuyLimitDayOrder("buy - ME",
          ticker, 200, 8.8888M));
        stop = System.Diagnostics.Stopwatch.GetTimestamp();
        Console.WriteLine(String.Format("Added a BUY order in {0} ms", ((stop - start) / TimeSpan.TicksPerMillisecond)));

        book = mgr.TestSellBook;
        numOrders = rnd.Next(minOrders, 2 * minOrders);
        start = System.Diagnostics.Stopwatch.GetTimestamp();
        for (int i = 0; i < numOrders; i++) {
          int quantity = (rnd.Next(1, 3) == 1) ?
            rnd.Next(1, 6) * 100 :
            rnd.Next(50, 849);
          decimal price = basePrice + rnd.Next(1, 10000) / 100000M;
          book.AddOrderNoSort(new SellLimitDayOrder("sell" + i,
            ticker, quantity, price));
        }
        stop = System.Diagnostics.Stopwatch.GetTimestamp();
        Console.WriteLine(String.Format("Added {1} {2} SELL orders in {0} ms", ((stop - start) / TimeSpan.TicksPerMillisecond), numOrders, ticker));
        start = System.Diagnostics.Stopwatch.GetTimestamp();
        book.AddOrder(new SellLimitDayOrder("sell - ME",
          ticker, 200, 8.8888M));
        stop = System.Diagnostics.Stopwatch.GetTimestamp();
        Console.WriteLine(String.Format("Added a SELL order in {0} ms", ((stop - start) / TimeSpan.TicksPerMillisecond)));
      }

      Console.WriteLine("\n\n********** CLEAR MATCHES **********\n");
      foreach (String ticker in tickers) {
        service.SubmitOrder(new BuyLimitDayOrder("buy - ME",
          ticker, 200, 1M));
      }

      Console.WriteLine("\n\n********** BEGIN TRADING **********\n");
      for (int i = 0; i < trades; i++) {
        int quantity = (rnd.Next(1, 3) == 1) ?
          rnd.Next(1, 4) * 100 :
          rnd.Next(50, 288);
        int orderType = (rnd.Next(1, 5));
        String ticker = tickers[rnd.Next(1, tickers.Count + 1) - 1];
        Quote quote = service.GetQuote(ticker);
        int sign = rnd.Next(1, 3) == 1 ? 1 : -1;
        long start = System.Diagnostics.Stopwatch.GetTimestamp();
        if (orderType == 1) {
          service.SubmitOrder(new BuyMarketOrder("mkt" + i, ticker, quantity));
        } else if (orderType == 2) {
          service.SubmitOrder(new SellMarketOrder("mkt" + i, ticker, quantity));
        } else if (orderType == 3) {
          decimal price = quote.Bid + (sign * (rnd.Next(1, 1000) / 100000M));
          service.SubmitOrder(new BuyLimitDayOrder("lmt" + i,
            ticker, quantity, price));
        } else {
          decimal price = quote.Ask + (sign * (rnd.Next(1, 1000) / 100000M));
          service.SubmitOrder(new SellLimitDayOrder("lmt" + i,
            ticker, quantity, price));
        }

        long stop = System.Diagnostics.Stopwatch.GetTimestamp();
        Console.WriteLine(String.Format("Executed order in {0} ms\n", ((stop - start) / TimeSpan.TicksPerMillisecond)));
      }
    }
#pragma warning restore IDE0059

    class TestOrderService : OrderService {
      public new void AddTicker(string ticker) {
        base.managers.Add(ticker, new TestOrderManager(ticker));
      }

      public TestOrderManager GetManager(String ticker) {
        return (TestOrderManager)base.managers[ticker];
      }
    }

    class TestOrderManager : OrderManager {
      public TestOrderManager(string ticker) : base(ticker) {
        base.BuyBook = new TestOrderBook(ticker, OrderAction.BUY);
        base.SellBook = new TestOrderBook(ticker, OrderAction.SELL);
      }

      protected override OrderTransaction FillMarketOrder(Order order, OrderBook book) {
        long start = System.Diagnostics.Stopwatch.GetTimestamp();
        OrderTransaction tran = base.FillMarketOrder(order, book);
        long stop = System.Diagnostics.Stopwatch.GetTimestamp();
        Console.WriteLine(String.Format("Market order executed in {0} ms", ((stop - start) / TimeSpan.TicksPerMillisecond)));
        WriteDetails(tran);
        return tran;
      }

      protected override OrderTransaction FillLimitOrder() {
        long start = System.Diagnostics.Stopwatch.GetTimestamp();
        OrderTransaction tran = base.FillLimitOrder();
        long stop = System.Diagnostics.Stopwatch.GetTimestamp();
        Console.WriteLine(String.Format("Limit order executed in {0} ms", ((stop - start) / TimeSpan.TicksPerMillisecond)));
        WriteDetails(tran);
        return tran;
      }

      private void WriteDetails(OrderTransaction tran) {
        Console.WriteLine("Order details:");
        foreach (Order filledOrder in tran.Orders) {
          Console.WriteLine("     " + filledOrder.ToString());
        }
        Console.WriteLine("Transaction details:");
        foreach (Transaction aTran in tran.Transactions) {
          Console.WriteLine("     " + aTran.ToString());
        }
        Console.WriteLine("  SPREAD: " + base.Quote);
        Console.WriteLine("  LEVEL 2:\n" + base.Level2);
      }

      public TestOrderBook TestBuyBook {
        get { return (TestOrderBook)base.BuyBook; }
      }

      public TestOrderBook TestSellBook {
        get { return (TestOrderBook)base.SellBook; }
      }
    }

    class TestOrderBook : OrderBook {
      public TestOrderBook(String ticker, OrderAction type) : base(ticker, type) { }

      public void AddOrderNoSort(Order order) {
        base.orderIds.Add(order.Id, order);
        base.orders.Add(order);
      }
    }
  }
}
