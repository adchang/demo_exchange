using System;
using System.Collections.Generic;
using DemoExchange.Models;
using DemoExchange.Services;

namespace DemoExchangeSimulator {
  public class Simulator {
    readonly SimOrderService service = new SimOrderService();
    readonly Random rnd = new Random();

#pragma warning disable IDE0059
    public void Start(int buyMinOrders, int sellMinOrders, int numTrades) {
      List<String> tickers = new List<String> { "ERX", "SPY", "DIA" };
      foreach (String ticker in tickers) {
        service.AddTicker(ticker);
        SimOrderManager mgr = service.GetManager(ticker);
        int basePrice = rnd.Next(8, 19);
        SeedOrders(buyMinOrders, mgr.TestBuyBook, ticker, basePrice, true);
        SeedOrders(sellMinOrders, mgr.TestSellBook, ticker, basePrice, false);
      }

      Console.WriteLine("\n\n********** CLEAR MATCHES **********\n");
      foreach (String ticker in tickers) {
        service.SubmitOrder(new BuyLimitDayOrder("buy - START",
          ticker, 200, 1M));
      }

      Console.WriteLine("\n\n********** BEGIN TRADING **********\n");
      for (int i = 0; i < numTrades; i++) {
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
        Console.WriteLine(String.Format("Sim trading executed order in {0} ms\n", ((stop - start) / TimeSpan.TicksPerMillisecond)));
      }
    }
#pragma warning restore IDE0059

    private int RandomQuantity {
      get {
        return (rnd.Next(1, 3) == 1) ?
          rnd.Next(1, 6) * 100 :
          rnd.Next(50, 849);
      }
    }

    // Seed some orders in the order book; go through AddOrderNoSort to avoid sorting
    // for initial set; then add 1 more using AddOrder to trigger the sort
    private void SeedOrders(int minOrders, SimOrderBook book, String ticker,
      int basePrice, bool isBuy) {
      int numOrders = rnd.Next(minOrders, 2 * minOrders);
      String prefix = isBuy ? "BUY" : "SELL";
      int sign = isBuy ? -1 : +1;
      long start = System.Diagnostics.Stopwatch.GetTimestamp();
      for (int i = 0; i < numOrders; i++) {
        decimal price = basePrice + (sign * (rnd.Next(1, 10000) / 100000M));
        book.AddOrderNoSort(isBuy ?
          new BuyLimitDayOrder(prefix + i,
            ticker, RandomQuantity, price) :
          new SellLimitDayOrder(prefix + i,
            ticker, RandomQuantity, price));
      }
      long stop = System.Diagnostics.Stopwatch.GetTimestamp();
      Console.WriteLine(String.Format("Added {1} {2} {3} orders in {0} ms", ((stop - start) / TimeSpan.TicksPerMillisecond), numOrders, ticker, prefix));
      start = System.Diagnostics.Stopwatch.GetTimestamp();
      book.AddOrder(isBuy ?
        new BuyLimitDayOrder(prefix + " - sort",
          ticker, 200, 8.8888M) :
        new SellLimitDayOrder(prefix + " - sort",
          ticker, 200, 8.8888M));
      stop = System.Diagnostics.Stopwatch.GetTimestamp();
      Console.WriteLine(String.Format("Added a {1} order in {0} ms", ((stop - start) / TimeSpan.TicksPerMillisecond), prefix));
    }

    class SimOrderService : OrderService {
      public new void AddTicker(string ticker) {
        base.managers.Add(ticker, new SimOrderManager(ticker));
      }

      public SimOrderManager GetManager(String ticker) {
        return (SimOrderManager)base.managers[ticker];
      }
    }

    class SimOrderManager : OrderManager {
      public SimOrderManager(string ticker) : base(ticker) {
        base.BuyBook = new SimOrderBook(ticker, OrderAction.BUY);
        base.SellBook = new SimOrderBook(ticker, OrderAction.SELL);
      }

      public SimOrderBook TestBuyBook {
        get { return (SimOrderBook)base.BuyBook; }
      }

      public SimOrderBook TestSellBook {
        get { return (SimOrderBook)base.SellBook; }
      }
    }

    class SimOrderBook : OrderBook {
      public SimOrderBook(String ticker, OrderAction type) : base(ticker, type) { }

      public void AddOrderNoSort(Order order) {
        base.orderIds.Add(order.OrderId, order);
        base.orders.Add(order);
      }
    }
  }
}
