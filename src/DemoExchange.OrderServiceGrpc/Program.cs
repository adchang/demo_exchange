using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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
          .UseKestrel(options => {
            options.Listen(IPAddress.Loopback, 8081);
            options.Listen(IPAddress.Loopback, 8091, listenOptions => {
              listenOptions.UseHttps("localhost.pfx", "");
            });
          })
          .UseSerilog()
          .UseStartup<Startup>();
      });
  }
}
