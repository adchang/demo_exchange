using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace DemoExchange.OrderServiceGrpc {
  public class Program {
    public static void Main(string[] args) {
      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
      .ConfigureWebHostDefaults(webBuilder => {
        webBuilder
          // Uncomment useKestrel for non-docker run to use the localhost.pfx, which will override launchsettings.json in developlment
          // .UseKestrel(options => {
          //   options.Listen(IPAddress.Loopback, 8081);
          //   options.Listen(IPAddress.Loopback, 8091, listenOptions => {
          //     listenOptions.UseHttps("localhost.pfx", "");
          //   });
          // })
          .UseSerilog()
          .UseStartup<Startup>();
      });
  }
}
