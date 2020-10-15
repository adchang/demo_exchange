using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoExchange.Api.Order;
using DemoExchange.Interface;
using DemoExchange.Models;
using DemoExchange.Services;
using Serilog;
using static Utils.Time;

namespace DemoExchangeSimulator {
  public class Simulator {
    private readonly ILogger logger = Log.Logger;

    private readonly IOrderTestPerfService service;
    private readonly Random rnd = new Random();

    public Simulator(IOrderTestPerfService service) {
      this.service = service;
    }

    public void Execute(string[] args) {
      try {
        ExecuteSimulation(args);
      } catch (Exception e) {
        logger.Fatal("Simulator failed: " + e.Message);
        Console.WriteLine("Oops...something went wrong :( Sorry...");
      }
    }

    private void ExecuteSimulation(string[] args) {
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

      List<String> tickers = new List<String> { "ERX", "SPY", "DIA", "QQQ", "UPRO", "SPXU", "OILU", "OILD" };
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
            AddTickerResponse response = service.AddTicker(ticker);
            result = new Tuple<int, int>(response.BuyOrderCount, response.SellOrderCount);
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
          IResponse<IOrderModel, OrderResponse> response;
          if (orderType == 1) {
            response = service.SubmitOrder(NewBuyMarketOrder("mkt" + i, ticker, RandomQuantity));
          } else if (orderType == 2) {
            response = service.SubmitOrder(NewSellMarketOrder("mkt" + i, ticker, RandomQuantity));
          } else if (orderType == 3) {
            decimal price = Convert.ToDecimal(quote.Ask) + (sign * (rnd.Next(1, 10000) / 10000000M));
            response = service.SubmitOrder(NewBuyLimitDayOrder("lmt" + i,
              ticker, RandomQuantity, price));
          } else {
            decimal price = Convert.ToDecimal(quote.Bid) + (sign * (rnd.Next(1, 10000) / 10000000M));
            response = service.SubmitOrder(NewSellLimitDayOrder("lmt" + i,
              ticker, RandomQuantity, price));
          }
          if (response.HasErrors) {
            throw new Exception(response.Errors[0].Description);
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
          NewBuyLimitDayOrder(prefix + i,
            ticker, RandomQuantity, price) :
          NewSellLimitDayOrder(prefix + i,
            ticker, RandomQuantity, price));
      }

      return orders;
    }

    private static OrderBL NewBuyLimitDayOrder(String accountId, String ticker, int quantity, decimal orderPrice) {
      return NewLimitDayOrder(accountId, ticker, quantity, orderPrice, OrderAction.Buy);
    }

    private static OrderBL NewSellLimitDayOrder(String accountId, String ticker, int quantity, decimal orderPrice) {
      return NewLimitDayOrder(accountId, ticker, quantity, orderPrice, OrderAction.Sell);
    }

    private static OrderBL NewLimitDayOrder(String accountId, String ticker, int quantity,
      decimal orderPrice, OrderAction action) {
      return new OrderBL(accountId, action, ticker, OrderType.Limit, quantity,
        orderPrice, OrderTimeInForce.Day);
    }

    private static OrderBL NewBuyMarketOrder(String accountId, String ticker, int quantity) {
      return NewMarketOrder(accountId, OrderAction.Buy, ticker, quantity);
    }

    private static OrderBL NewSellMarketOrder(String accountId, String ticker, int quantity) {
      return NewMarketOrder(accountId, OrderAction.Sell, ticker, quantity);
    }

    private static OrderBL NewMarketOrder(String accountId, OrderAction type, String ticker, int quantity) {
      return new OrderBL(accountId, type, ticker, OrderType.Market, quantity,
        0, OrderTimeInForce.Day);
    }
  }
}
