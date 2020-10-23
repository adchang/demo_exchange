using System;
using System.Net.Http;
using DemoExchange.Api;
using DemoExchange.Contexts;
using DemoExchange.Interface;
using DemoExchange.Services;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;

namespace DemoExchange.OrderService {
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
      Config.ConnectionStrings connectionStrings = new Config.ConnectionStrings();
      Configuration.GetSection("ConnectionStrings").Bind(connectionStrings);
#if DEBUG
      logger.Debug("ConnectionString: " + connectionStrings.DemoExchangeDb);
#endif
      var httpHandler = new HttpClientHandler {
        ServerCertificateCustomValidationCallback =
        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
      };
      var channel = GrpcChannel.ForAddress("https://172.17.0.1:8091",
        new GrpcChannelOptions { HttpHandler = httpHandler });

      services.AddControllers();
      services.AddSwaggerGen(c => {
        c.SwaggerDoc("v1", new OpenApiInfo {
          Title = "DemoExchange.OrderService",
            Version = "v1",
            Description = "API for DemoExchange",
            TermsOfService = new Uri("https://er-x.io/terms"),
            Contact = new OpenApiContact {
              Name = "ER-X",
                Email = string.Empty,
                Url = new Uri("https://er-x.io"),
            },
            License = new OpenApiLicense {
              Name = "Use under LICX",
                Url = new Uri("https://er-x.io/license"),

            }
        });

        /* Set the comments path for the Swagger JSON and UI.
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);*/
      });
      services.AddSingleton<Config.ConnectionStrings>(connectionStrings);
      services.AddSingleton<IDemoExchangeDbContextFactory<OrderContext>, OrderContextFactory>();
      services.AddSingleton<IOrderService, DemoExchange.Services.OrderService>();
      services.AddSingleton<IAccountServiceRpcClient>(new AccountServiceRpcClient(channel));

      logger.Information("ConfigureServices done");
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
