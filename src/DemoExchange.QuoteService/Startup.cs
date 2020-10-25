using System.Net.Http;
using DemoExchange.Interface;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using StackExchange.Redis;

namespace DemoExchange.QuoteService {
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
      Logger.Here().Debug("Redis: " + connectionStrings.Redis);
#endif
      ConnectionMultiplexer muxer = ConnectionMultiplexer.Connect(connectionStrings.Redis);
      IDatabase redis = muxer.GetDatabase();
      ISubscriber subscriber = muxer.GetSubscriber();

      Logger.Here().Information("Subscribing to " + Constants.PubSub.TOPIC_TRANSACTION_PROCESSED);
      subscriber.Subscribe(Constants.PubSub.TOPIC_TRANSACTION_PROCESSED,
        Handlers.TransactionProcessHandler(Logger, redis));

      Config.ErxServices erx = new Config.ErxServices();
      Configuration.GetSection(Config.ErxServices.SECTION).Bind(erx);
#if DEBUG
      Logger.Here().Debug("OrderUrlBase: " + erx.OrderUrlBase);
#endif

      var httpHandler = new HttpClientHandler() {
        ServerCertificateCustomValidationCallback =
        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
      };
      var channel = GrpcChannel.ForAddress(erx.OrderUrlBase,
        new GrpcChannelOptions { HttpHandler = httpHandler });

      services.AddGrpc();
      services.AddSingleton<IDatabase>(redis);
      services.AddSingleton<IOrderServiceRpcClient>(new OrderServiceRpcClient(channel));

      Logger.Here().Information("END");
    }

    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();

      app.UseEndpoints(endpoints => {
        endpoints.MapGrpcService<QuoteServiceGrpc>();
      });
    }

  }
}
