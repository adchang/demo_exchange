using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoExchange;
using DemoExchange.Interface;
using DemoExchange.Models;
using DemoExchange.Services;
using Microsoft.VisualBasic.CompilerServices;
using static Utils.Time;

namespace DemoExchangeSimulator {
  public class Simulator {
    readonly Random rnd = new Random();

#pragma warning disable IDE0059
    public void Start(int minOrders, int numTrades, int numThreads) {
      OrderService service;
      List<String> tickers = new List<String> { "ERX", "SPY", "DIA", "QQQ", "UPRO", "SPXU", "OILU", "OILD" };
      if (minOrders > 0) {
        service = new OrderService(new Dependencies.AccountService());
        Console.WriteLine("\n\n********** SEEDING ORDER BOOK **********\n");
        ParallelOptions opt = new ParallelOptions() {
          MaxDegreeOfParallelism = tickers.Count
        };
        Parallel.For(0, tickers.Count, opt, i => {
          String ticker = tickers[i];
          long seedStart = Now;
          int basePrice = rnd.Next(8, 19);
          List<IModelOrder> buyOrders = GenerateInitialOrders(minOrders, ticker,
            basePrice, true);
          List<IModelOrder> sellOrders = GenerateInitialOrders(minOrders, ticker,
            basePrice, false);
          service.TestPerfAddOrder(ticker, buyOrders, sellOrders);
          Console.WriteLine(String.Format("{3}: Added {1} buy orders and {2} sell orders in {0} milliseconds", Stop(seedStart), buyOrders.Count, sellOrders.Count, ticker));
        });
      }

      if (numTrades > 0) {
        service = new OrderService(new Dependencies.AccountService());
        tickers.ForEach(ticker => service.AddTicker(ticker));
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
            decimal price = quote.Ask + (sign * (rnd.Next(1, 10000) / 10000000M));
            service.SubmitOrder(new BuyLimitDayOrder("lmt" + i,
              ticker, RandomQuantity, price));
          } else {
            decimal price = quote.Bid + (sign * (rnd.Next(1, 10000) / 10000000M));
            service.SubmitOrder(new SellLimitDayOrder("lmt" + i,
              ticker, RandomQuantity, price));
          }
          Console.WriteLine(String.Format("Executed order in {0} milliseconds\n", Stop(orderStart)));
        });
        Console.WriteLine(String.Format("Executed {1} trades with {2} threads in {0} milliseconds",
          Stop(tradeStart), numTrades, numThreads));
      }
      Console.WriteLine("\n\n********** END TRADING **********\n");
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

    private List<IModelOrder> GenerateInitialOrders(int minOrders, String ticker, int basePrice,
      bool isBuy) {
      List<IModelOrder> orders = new List<IModelOrder>();
      int numOrders = rnd.Next(minOrders, 2 * minOrders);
      String prefix = isBuy ? "BUY" : "SELL";
      int sign = isBuy ? -1 : +1;
      for (int i = 0; i < numOrders; i++) {
        decimal price = basePrice + (sign * (rnd.Next(1, 10000) / 10000000M));
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
