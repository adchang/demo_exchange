using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoExchange.Interface;
using DemoExchange.Services;
using Serilog;
using static Utils.Time;

namespace DemoExchangeSimulator {
  public class Simulator {
    private readonly ILogger logger = Log.Logger;

    private readonly IOrderInternalService service;
    private readonly Random rnd = new Random();

    public Simulator(IOrderInternalService service) {
      this.service = service;
    }

    public void Execute(string[] args) {
      String msg = "";
      int minOrders = 1;
      int numTrades = 1;
      bool limitOrders = true;
      int numThreads = 1;

      Console.WriteLine("\nHello! I am a simulator for DemoExchange\n");
      Console.WriteLine("How many orders to seed?: ");
      logger.Information("Orders to seed: " + minOrders);
      Console.WriteLine("How many trades to execute?: ");
      logger.Information("Trades to execute: " + numTrades);
      Console.WriteLine("Include limit orders?: ");
      logger.Information("Limit orders: " + limitOrders);
      Console.WriteLine("How many concurrent threads?: ");
      logger.Information("Concurrent threads: " + numThreads);
      Console.WriteLine("");

      // List<String> tickers = new List<String> { "ERX", "SPY", "DIA", "QQQ", "UPRO", "SPXU", "OILU", "OILD" };
      List<String> tickers = new List<String> { "UPRO" };
      logger.Information("Tickers: " + String.Join(", ", tickers));
      if (minOrders > 0) {
        msg = "\n\n********** SEEDING ORDER BOOK **********\n";
        Console.WriteLine(msg);
        logger.Information(msg);
        ParallelOptions opt = new ParallelOptions() {
          MaxDegreeOfParallelism = tickers.Count
        };
        Parallel.For(0, tickers.Count, opt, i => {
          String ticker = tickers[i];
          long seedStart = Now;
          int basePrice = rnd.Next(8, 19);
          List<IOrderModel> buyOrders = GenerateInitialOrders(minOrders, ticker,
            basePrice, true);
          List<IOrderModel> sellOrders = GenerateInitialOrders(minOrders, ticker,
            basePrice, false);
          service.TestPerfAddOrder(ticker, buyOrders, sellOrders);
          msg = String.Format("{3}: Added {1} buy orders and {2} sell orders in {0} milliseconds", Stop(seedStart), buyOrders.Count, sellOrders.Count, ticker);
          Console.WriteLine(msg);
          logger.Information(msg);
        });
      }

      if (numTrades > 0) {
        msg = "\n\n********** LOADING ORDERS **********\n";
        Console.WriteLine(msg);
        logger.Information(msg);
        long loadStart = Now;
        ParallelOptions loadOpt = new ParallelOptions() {
          MaxDegreeOfParallelism = tickers.Count
        };
        bool addTicker = minOrders == 0;
        Parallel.For(0, tickers.Count, loadOpt, i => {
          String ticker = tickers[i];
          Tuple<int, int> result;
          long loadStart = Now;
          if (addTicker) {
            result = service.AddTicker(ticker);
          } else {
            result = service.TestPerfLoadOrderBook(ticker);
          }
          msg = String.Format("Loaded {1} BUY and {2} {3} orders in {0} milliseconds",
            Stop(loadStart), result.Item1, result.Item2, ticker);
          Console.WriteLine(msg);
          logger.Information(msg);

        });

        msg = "\n\n********** BEGIN TRADING **********\n";
        Console.WriteLine(msg);
        logger.Information(msg);
        long tradeStart = Now;
        ParallelOptions opt = new ParallelOptions() {
          MaxDegreeOfParallelism = numThreads
        };
        Parallel.For(0, numTrades, opt, i => {
          int orderType = (rnd.Next(1, limitOrders ? 5 : 3));
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
          msg = String.Format("Executed order in {0} milliseconds\n", Stop(orderStart));
          Console.WriteLine(msg);
          logger.Information(msg);
        });
        msg = String.Format("Executed {1} trades with {2} threads in {0} milliseconds",
          Stop(tradeStart), numTrades, numThreads);
        Console.WriteLine(msg);
        logger.Information(msg);
      }
      msg = "\n\n********** END TRADING **********\n";
      Console.WriteLine(msg);
      logger.Information(msg);
    }

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

    private List<IOrderModel> GenerateInitialOrders(int minOrders, String ticker, int basePrice,
      bool isBuy) {
      List<IOrderModel> orders = new List<IOrderModel>();
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
