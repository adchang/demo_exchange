using System;
using DemoExchange;
using DemoExchange.Interface;
using DemoExchange.Services;
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

      return new ServiceCollection()
        .AddSingleton(config)
        .AddSingleton<Simulator>()
        .AddSingleton<IOrderInternalService, OrderService>()
        .AddSingleton<IAccountService, Dependencies.AccountService>()
        .BuildServiceProvider();
    }
  }
}
