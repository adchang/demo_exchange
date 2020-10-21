using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DemoExchange.Api;
using DemoExchange.Interface;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Grpc.HttpApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;

namespace DemoExchange.ApiGateway {
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

      ErxServicesConfig erx = new ErxServicesConfig();
      Configuration.GetSection("ErxServices").Bind(erx);
#if DEBUG
      Logger.Here().Debug("AccountUrlBase: " + erx.AccountUrlBase);
      Logger.Here().Debug("OrderUrlBase: " + erx.OrderUrlBase);
#endif

      var httpHandler = new HttpClientHandler();
      httpHandler.ServerCertificateCustomValidationCallback =
        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
      var accountChannel = GrpcChannel.ForAddress(erx.AccountUrlBase,
        new GrpcChannelOptions { HttpHandler = httpHandler });
      var orderChannel = GrpcChannel.ForAddress(erx.OrderUrlBase,
        new GrpcChannelOptions { HttpHandler = httpHandler });

      services.AddGrpc();
      services.AddGrpcHttpApi();

      services.AddSingleton<IAccountServiceRpcClient>(new AccountServiceRpcClient(accountChannel));
      services.AddSingleton<IOrderServiceRpcClient>(new OrderServiceRpcClient(orderChannel));

      services.AddSwaggerGen(c => {
        c.SwaggerDoc("v1", new OpenApiInfo {
          Title = "ER-X",
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

        /* Set the comments path
        for the Swagger JSON and UI.
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);*/
      });
      services.AddGrpcSwagger();

      Logger.Here().Information("ConfigureServices done");
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();

      app.UseEndpoints(endpoints => {
        endpoints.MapGrpcService<ApiGateway>();
      });

      app.UseSwagger();
      app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ER-X v1"));
    }
  }

  public class ErxServicesConfig {
    public String AccountUrlBase { get; set; }
    public String OrderUrlBase { get; set; }
  }
}
