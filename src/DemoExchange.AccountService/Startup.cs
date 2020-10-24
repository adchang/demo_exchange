using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DemoExchange.Api;
using DemoExchange.Contexts;
using DemoExchange.Interface;
using DemoExchange.Services;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace DemoExchange.AccountService {
  public class Startup {
    private static Serilog.ILogger Logger => Serilog.Log.ForContext<Startup>();

    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration) {
      Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services) {
      Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(Configuration)
        .CreateLogger();
      Logger.Here().Information("Logger created");

      Config.ConnectionStrings connectionStrings = new Config.ConnectionStrings();
      Configuration.GetSection(Config.ConnectionStrings.SECTION).Bind(connectionStrings);
#if DEBUG
      Logger.Here().Debug("ConnectionString: " + connectionStrings.DemoExchangeDb);
#endif

      services.AddGrpc();
      services.AddSingleton<Config.ConnectionStrings>(connectionStrings);
      services.AddSingleton<IDemoExchangeDbContextFactory<AccountContext>, AccountContextFactory>();
      services.AddSingleton<IAccountService, DemoExchange.Services.AccountService>();

      Logger.Here().Information("END");
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();

      app.UseEndpoints(endpoints => {
        endpoints.MapGrpcService<AccountServiceGrpc>();
      });
    }
  }
}
