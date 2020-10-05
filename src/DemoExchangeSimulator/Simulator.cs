using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoExchange.Models;
using DemoExchange.Services;
using Microsoft.VisualBasic.CompilerServices;
using static Utils.Time;

namespace DemoExchangeSimulator {
  public class Simulator {
    readonly OrderService service = new OrderService();
    readonly Random rnd = new Random();

#pragma warning disable IDE0059
    public void Start(int buyMinOrders, int sellMinOrders, int numTrades, int numThreads) {
      List<String> tickers = new List<String> { "ERX", "SPY", "DIA" };
      Console.WriteLine("\n\n********** SEEDING ORDER BOOK **********\n");
      foreach (String ticker in tickers) {
        long seedStart = Now;
        int basePrice = rnd.Next(8, 19);
        List<Order> buyOrders = GenerateInitialOrders(buyMinOrders, ticker, basePrice, true);
        List<Order> sellOrders = GenerateInitialOrders(sellMinOrders, ticker, basePrice, false);
        service.TestPerfAddTicker(ticker, buyOrders, sellOrders);
        Console.WriteLine(String.Format("{3}: Added {1} buy orders and {2} sell orders in {0} ms", Stop(seedStart), buyOrders.Count, sellOrders.Count, ticker));
      }

      Console.WriteLine("\n\n********** BEGIN TRADING **********\n");
      long tradeStart = Now;
      ParallelOptions opt = new ParallelOptions() {
        MaxDegreeOfParallelism = numThreads
      };
      Parallel.For(0, numTrades, opt, i => {
        int orderType = (rnd.Next(1, 5));
        String ticker = tickers[rnd.Next(1, tickers.Count + 1) - 1];
        Quote quote = (Quote)service.GetQuote(ticker);
        int sign = rnd.Next(1, 3) == 1 ? 1 : -1;
        long orderStart = Now;
        if (orderType == 1) {
          service.SubmitOrder(new BuyMarketOrder("mkt" + i, ticker, RandomQuantity));
        } else if (orderType == 2) {
          service.SubmitOrder(new SellMarketOrder("mkt" + i, ticker, RandomQuantity));
        } else if (orderType == 3) {
          decimal price = quote.Bid + (sign * (rnd.Next(1, 1000) / 100000M));
          service.SubmitOrder(new BuyLimitDayOrder("lmt" + i,
            ticker, RandomQuantity, price));
        } else {
          decimal price = quote.Ask + (sign * (rnd.Next(1, 1000) / 100000M));
          service.SubmitOrder(new SellLimitDayOrder("lmt" + i,
            ticker, RandomQuantity, price));
        }
        Console.WriteLine(String.Format("Executed order in {0} ms\n", Stop(orderStart)));
      });
      Console.WriteLine("\n\n********** BEGIN TRADING **********\n");
      Console.WriteLine(String.Format("Executed {1} trades in {0} ms",
        Stop(tradeStart), numTrades));
    }
#pragma warning restore IDE0059

    private int RandomQuantity {
      get {
        return (rnd.Next(1, 3) == 1) ?
          rnd.Next(1, 6) * 100 :
          rnd.Next(50, 849);
      }
    }

    private static long Stop(long start) {
      return ((Now - start) / TimeSpan.TicksPerMillisecond);
    }

    private List<Order> GenerateInitialOrders(int minOrders, String ticker, int basePrice,
      bool isBuy) {
      List<Order> orders = new List<Order>();
      int numOrders = rnd.Next(minOrders, 2 * minOrders);
      String prefix = isBuy ? "BUY" : "SELL";
      int sign = isBuy ? -1 : +1;
      for (int i = 0; i < numOrders; i++) {
        decimal price = basePrice + (sign * (rnd.Next(1, 10000) / 100000M));
        orders.Add(isBuy ?
          new BuyLimitDayOrder(prefix + i,
            ticker, RandomQuantity, price) :
          new SellLimitDayOrder(prefix + i,
            ticker, RandomQuantity, price));
      }
      return orders;
    }
  }
}
