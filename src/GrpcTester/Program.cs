using System;
using System.Net.Http;
using System.Threading.Tasks;
using DemoExchange.Api;
using DemoExchange.Api.Order;
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
      var channel = GrpcChannel.ForAddress("https://loki:8091",
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
      Display("Initializing service...");
      try {
        await client.InitializeServiceAsync(new Empty());
      } catch (Exception e) {
        Display("An error occurred: " + e.Message);
      }
      Display("Service initialized");

      // Quote quote = await client.GetQuoteAsync(new StringMessage { Value = "ERX" });
      // Display(quote.ToString());

      Display("Submitting order...");
      OrderResponse response = null;
      try {
        response = await client.SubmitOrderAsync(new OrderRequest {
          AccountId = "gRPC-BUY",
            Action = OrderAction.Buy,
            Ticker = "ERX",
            Type = OrderType.Market,
            Quantity = 100,
            OrderPrice = 0,
            TimeInForce = OrderTimeInForce.Day
        });
      } catch (Exception e) {
        Display("An error occurred: " + e.Message);
      }
      Display("Order submitted: " + response.ToString());
    }

    private static void Display(String message) {
      // logger.Information(message);
      Console.WriteLine(message);
    }
  }
}
