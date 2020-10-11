using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoExchange;
using DemoExchange.Contexts;
using DemoExchange.Interface;
using DemoExchange.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;

namespace DemoExchange.OrderService {
  public class Startup {
    private ILogger logger;

    public Startup(IConfiguration configuration) {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services) {
      Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(Configuration)
        .CreateLogger();
      logger = Log.Logger;
      logger.Debug("Logger created");
      ConnectionStrings connectionStrings = new ConnectionStrings();
      Configuration.GetSection("ConnectionStrings").Bind(connectionStrings);
#if DEBUG
      logger.Debug("Connection String: " + connectionStrings.DemoExchangeDb);
#endif

      services.AddControllers();
      services.AddSwaggerGen(c => {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "DemoExchange.OrderService", Version = "v1" });
      });
      services.AddSingleton<ConnectionStrings>(connectionStrings);
      services.AddSingleton<IDemoExchangeDbContextFactory<OrderContext>, OrderContextFactory>();
      services.AddSingleton<IOrderInternalService, DemoExchange.Services.OrderService>();
      services.AddSingleton<IAccountService, Dependencies.AccountService>();
      logger.Debug("ConfigureServices done");
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DemoExchange.OrderService v1"));
      }

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints => {
        endpoints.MapControllers();
      });
    }
  }
}
