using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoExchange.Api.Order;
using Grpc.Core;
using Serilog;

namespace DemoExchange.OrderServiceGrpc {
  public class OrderServiceGrpc : OrderService.OrderServiceBase {
    private readonly ILogger logger = Log.Logger;

    public override Task<StringData> Echo(StringData request, ServerCallContext context) {
      logger.Information("Echo started");

      StringData response = new StringData {
        Data = "Hello " + request.Data
      };

      logger.Information("Echoing..." + response.Data);
      logger.Information("Echo done");

      return Task.FromResult(response);
    }
  }
}
