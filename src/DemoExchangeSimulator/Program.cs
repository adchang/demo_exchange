using System;
using System.Net.Http;
using DemoExchange;
using DemoExchange.Api;
using DemoExchange.Contexts;
using DemoExchange.Interface;
using DemoExchange.Services;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace DemoExchangeSimulator {
  class Program {
    public static void Main(string[] args) {
      var serviceProvider = ConfigureServices();
      var app = serviceProvider.GetRequiredService<Simulator>();
      app.Execute(args);
    }

    private static IServiceProvider ConfigureServices() {
      IConfiguration config = new ConfigurationBuilder()
        .AddJsonFile(AppConstants.CONFIG_FILE)
        .Build();
      Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(config)
        .CreateLogger();
      ConnectionStrings connectionStrings = new ConnectionStrings();
      config.GetSection("ConnectionStrings").Bind(connectionStrings);
      var httpHandler = new HttpClientHandler();
      httpHandler.ServerCertificateCustomValidationCallback =
        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
      var accountChannel = GrpcChannel.ForAddress("https://loki:8091",
        new GrpcChannelOptions { HttpHandler = httpHandler });
      var orderChannel = GrpcChannel.ForAddress("https://loki:8092",
        new GrpcChannelOptions { HttpHandler = httpHandler });

      return new ServiceCollection()
        .AddSingleton<Simulator>()
        .AddSingleton<ConnectionStrings>(connectionStrings)
        .AddSingleton<IDemoExchangeDbContextFactory<OrderContext>, OrderContextFactory>()
        .AddSingleton<IOrderTestPerfService, DemoExchange.Services.OrderService>()
        .AddSingleton<IOrderServiceRpcClient>(new OrderServiceRpcClient(orderChannel))
        .AddSingleton<IAccountServiceRpcClient>(new AccountServiceRpcClient(accountChannel))
        .BuildServiceProvider();
    }
  }
}
