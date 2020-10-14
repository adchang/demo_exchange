using System;
using System.Net.Http;
using System.Threading.Tasks;
using DemoExchange.Api.Order;
using Grpc.Net.Client;

namespace GrpcTester {
  class Program {
    static async Task Main(string[] args) {
      // HACK for dev certs
      var httpHandler = new HttpClientHandler();
      httpHandler.ServerCertificateCustomValidationCallback =
        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
      var channel = GrpcChannel.ForAddress("https://loki:8091",
        new GrpcChannelOptions { HttpHandler = httpHandler });
      var client = new OrderService.OrderServiceClient(channel);
      var reply = await client.EchoAsync(
        new StringData { Data = "ER-X" });
      Console.WriteLine("Greeting: " + reply.Data);
      Console.WriteLine("Press any key to exit...");
      Console.ReadKey();
    }
  }
}
