using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DemoExchange.Api;
using DemoExchange.Interface;
using Grpc.Net.Client;
// using Serilog;

namespace GrpcTester {
  class Program {
    // private readonly ILogger logger = Log.Logger;

    static async Task Main(string[] args) {
      // HACK for dev certs
      var httpHandler = new HttpClientHandler();
      httpHandler.ServerCertificateCustomValidationCallback =
        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
      var channel = GrpcChannel.ForAddress("https://loki:8092",
        new GrpcChannelOptions { HttpHandler = httpHandler });
      IOrderServiceRpcClient client = new OrderServiceRpcClient(channel);

      /*var reply = await client.EchoAsync(
        new StringMessage { Value = "ER-X" });
      Console.WriteLine("Greeting: " + reply.Value);*/

      await DoSomething(client);

      Console.WriteLine("Press any key to exit...");
      Console.ReadKey();
    }

    private static async Task DoSomething(IOrderServiceRpcClient client) {
      Random rnd = new Random();
      List<String> tickers = new List<String> { "ERX", "SPY", "DIA", "QQQ", "UPRO", "SPXU", "OILU", "OILD" };

      Display("Initializing service...");
      try {
        await client.InitializeServiceAsync(new Empty());
      } catch (Exception e) {
        Display("An error occurred: " + e.Message);
      }
      Display("Service initialized");

      // Quote quote = await client.GetQuoteAsync(new StringMessage { Value = "ERX" });
      // Display(quote.ToString());

      int numThreads = 10;
      int numTrades = 100;
      ParallelOptions opt = new ParallelOptions() {
        MaxDegreeOfParallelism = numThreads
      };
      Parallel.For(0, numTrades, opt, async i => {
        String ticker = tickers[rnd.Next(1, tickers.Count + 1) - 1];
        Display("Submitting order...");
        OrderResponse response = null;
        try {
          response = await client.SubmitOrderAsync(new OrderRequest {
            AccountId = "gRPC-BUY",
              Action = OrderAction.OrderBuy,
              Ticker = ticker,
              Type = OrderType.OrderMarket,
              Quantity = 10,
              OrderPrice = 0,
              TimeInForce = OrderTimeInForce.OrderDay
          });
        } catch (Exception e) {
          Display("An error occurred: " + e.Message);
        }
        Display("Order submitted: " + response.ToString());
      });
    }

    private static void Display(String message) {
      // logger.Information(message);
      Console.WriteLine(message);
    }
  }
}
