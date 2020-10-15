using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoExchange.Api;
using DemoExchange.Api.Order;
using DemoExchange.Interface;
// using DemoExchange.Models;
using Grpc.Core;
using Serilog;

namespace DemoExchange.OrderServiceGrpc {
  public class OrderServiceGrpc : OrderService.OrderServiceBase {
    private readonly ILogger logger = Log.Logger;

    public override Task<StringMessage> Echo(StringMessage request, ServerCallContext context) {
      logger.Information("Echo started");

      StringMessage response = new StringMessage {
        Value = "Hello " + request.Value
      };

      logger.Information("Echoing..." + response.Value);
      logger.Information("Echo done");

      return Task.FromResult(response);
    }

    public class Transformer {
      // public static OrderBL ToOrderBL(OrderRequest request) =>
      //   new OrderBL(request.AccountId, request.Action, request.Ticker, request.Type,
      //     request.Quantity, Convert.ToDecimal(request.OrderPrice), request.TimeInForce);

      private Transformer() {
        // Prevent instantiation
      }
    }
  }
}
