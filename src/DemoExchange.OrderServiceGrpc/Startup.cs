using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoExchange.Contexts;
using DemoExchange.Interface;
using DemoExchange.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace DemoExchange.OrderServiceGrpc {
  public class Startup {
    private ILogger logger;

    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration) {
      Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services) {
      Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(Configuration)
        .CreateLogger();
      logger = Log.Logger;
      logger.Information("Logger created");
      ConnectionStrings connectionStrings = new ConnectionStrings();
      Configuration.GetSection("ConnectionStrings").Bind(connectionStrings);
#if DEBUG
      logger.Debug("ConnectionString: " + connectionStrings.DemoExchangeDb);
#endif

      services.AddGrpc();
      services.AddSingleton<ConnectionStrings>(connectionStrings);
      services.AddSingleton<IDemoExchangeDbContextFactory<OrderContext>, OrderContextFactory>();
      services.AddSingleton<IOrderService, DemoExchange.Services.OrderService>();
      services.AddSingleton<IAccountService, Dependencies.AccountService>();

      logger.Information("ConfigureServices done");
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();

      app.UseEndpoints(endpoints => {
        endpoints.MapGrpcService<OrderServiceGrpc>();
      });
    }
  }
}
