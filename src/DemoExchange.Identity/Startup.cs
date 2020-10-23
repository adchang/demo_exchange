using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace DemoExchange.Identity {
  public class Startup {
    private static Serilog.ILogger Logger => Serilog.Log.ForContext<Startup>();

    public IConfiguration Configuration { get; }
    public IWebHostEnvironment Environment { get; }

    public Startup(IConfiguration configuration, IWebHostEnvironment environment) {
      Configuration = configuration;
      Environment = environment;
    }

    public void ConfigureServices(IServiceCollection services) {
      Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(Configuration)
        .CreateLogger();
      Logger.Here().Information("Logger created");

      var builder = services.AddIdentityServer()
        .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
        .AddInMemoryClients(IdentityConfiguration.Clients);

      // TODO: not recommended for production - you need to store your key material somewhere secure
      builder.AddDeveloperSigningCredential();

      Logger.Here().Information("END");
    }

    public void Configure(IApplicationBuilder app) {
      if (Environment.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
      }

      // uncomment if you want to add MVC
      //app.UseStaticFiles();
      //app.UseRouting();

      app.UseIdentityServer();

      // uncomment, if you want to add MVC
      //app.UseAuthorization();
      //app.UseEndpoints(endpoints =>
      //{
      //    endpoints.MapDefaultControllerRoute();
      //});
    }
  }
}
