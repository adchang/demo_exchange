using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoExchange.Api;
using DemoExchange.Api.Order;
using DemoExchange.Interface;
using DemoExchange.Models;
using Grpc.Core;
using Serilog;

namespace DemoExchange.OrderServiceGrpc {
  public class OrderServiceGrpc : OrderService.OrderServiceBase {
    private static Serilog.ILogger logger => Serilog.Log.ForContext<OrderServiceGrpc>();

    private readonly List<String> tickers = new List<String> { "ERX", "SPY", "DIA", "QQQ", "UPRO", "SPXU", "OILU", "OILD" };

    private readonly IOrderService service;

    public OrderServiceGrpc(IOrderService service) {
      this.service = service;
    }

    public override Task<BoolMessage> IsMarketOpen(Empty request, ServerCallContext context) {
      return base.IsMarketOpen(request, context);
    }

    public override Task<Order> GetOrder(StringMessage request, ServerCallContext context) {
      return base.GetOrder(request, context);
    }

    public override Task<OrderResponse> SubmitOrder(OrderRequest request, ServerCallContext context) {
      logger.Here().Information("SubmitOrder BGN");
      IResponse<IOrderModel, OrderResponse> response;
      try {
        response = service.SubmitOrder(new OrderBL(request));
      } catch (Exception e) {
        logger.Here().Warning("Error submitting order E: " + e.Message);
        throw new RpcException(new Status(StatusCode.Internal, e.Message));
      }
      logger.Here().Information("SubmitOrder END");
      return Task.FromResult(response.ToMessage());
    }

    public override Task<OrderResponse> CancelOrder(StringMessage request, ServerCallContext context) {
      return base.CancelOrder(request, context);
    }

    public override Task<Level2> GetLevel2(StringMessage request, ServerCallContext context) {
      return Task.FromResult(service.GetLevel2(request.Value));
    }

    public override Task<Quote> GetQuote(StringMessage request, ServerCallContext context) {
      return Task.FromResult(service.GetQuote(request.Value));
    }

    public override Task<Empty> InitializeService(Empty request, ServerCallContext context) {
      logger.Here().Information("InitializeService BGN");
      tickers.ForEach(ticker => {
        logger.Here().Information("Adding ticker: " + ticker);
        try {
          AddTickerResponse response = service.AddTicker(ticker);
          logger.Here().Information(String.Format("Added {0} BUY and {1} SELL open orders for {2}",
            response.BuyOrderCount, response.SellOrderCount, ticker));
        } catch (Exception e) {
          logger.Here().Warning("Error adding ticker " + ticker + " E: " + e.Message);
          throw new RpcException(new Status(StatusCode.Internal, e.Message));
        }
      });

      logger.Here().Information("InitializeService END");
      return Task.FromResult(new Empty());
    }

    public override Task<AddTickerResponse> AddTicker(StringMessage request, ServerCallContext context) {
      return Task.FromResult(service.AddTicker(request.Value));
    }

    public override Task<Empty> OpenMarket(Empty request, ServerCallContext context) {
      service.OpenMarket();

      return Task.FromResult(new Empty());
    }

    public override Task<Empty> CloseMarket(Empty request, ServerCallContext context) {
      service.CloseMarket();

      return Task.FromResult(new Empty());
    }

    public override Task<StringMessage> Echo(StringMessage request, ServerCallContext context) {
      logger.Here().Information("Echo started");

      StringMessage response = new StringMessage {
        Value = "Hello " + request.Value
      };

      logger.Here().Information("Echoing..." + response.Value);
      logger.Here().Information("Echo done");

      return Task.FromResult(response);
    }
  }
}
