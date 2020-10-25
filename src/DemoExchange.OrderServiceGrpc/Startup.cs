using System.Net.Http;
using DemoExchange.Contexts;
using DemoExchange.Interface;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using StackExchange.Redis;

namespace DemoExchange.OrderService {
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
      Logger.Here().Debug("DemoExchangeDb: " + connectionStrings.DemoExchangeDb);
      Logger.Here().Debug("Redis: " + connectionStrings.Redis);
#endif
      ConnectionMultiplexer muxer = ConnectionMultiplexer.Connect(connectionStrings.Redis);
      ISubscriber subscriber = muxer.GetSubscriber();

      Config.ErxServices erx = new Config.ErxServices();
      Configuration.GetSection(Config.ErxServices.SECTION).Bind(erx);
#if DEBUG
      Logger.Here().Debug("AccountUrlBase: " + erx.AccountUrlBase);
#endif

      var httpHandler = new HttpClientHandler() {
        ServerCertificateCustomValidationCallback =
        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
      };
      var channel = GrpcChannel.ForAddress(erx.AccountUrlBase,
        new GrpcChannelOptions { HttpHandler = httpHandler });

      services.AddGrpc();
      services.AddSingleton<Config.ConnectionStrings>(connectionStrings);
      services.AddSingleton<IDemoExchangeDbContextFactory<OrderContext>, OrderContextFactory>();
      services.AddSingleton<ISubscriber>(subscriber);
      services.AddSingleton<IOrderService, DemoExchange.Services.OrderService>();
      services.AddSingleton<IAccountServiceRpcClient>(new AccountServiceRpcClient(channel));

      Logger.Here().Information("END");
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
