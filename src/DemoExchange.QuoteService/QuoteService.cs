using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoExchange.Api;
using DemoExchange.Interface;
using Grpc.Core;
using Serilog;
using StackExchange.Redis;

namespace DemoExchange.QuoteService {
  public class QuoteServiceGrpc : DemoExchange.Api.QuoteService.QuoteServiceBase {
    private static Serilog.ILogger Logger => Serilog.Log.ForContext<QuoteServiceGrpc>();

    private readonly IDatabase redis;
    private readonly IOrderServiceRpcClient orderService;

    public QuoteServiceGrpc(IDatabase redis, IOrderServiceRpcClient orderService) {
      this.redis = redis;
      this.orderService = orderService;
    }

    public override Task<Level2> GetLevel2(StringMessage request, ServerCallContext context) {
      Logger.Here().Debug(redis.StringGet("mykey"));
      return null; //Task.FromResult(service.GetLevel2(request.Value));
    }

    public override Task<Quote> GetQuote(StringMessage request, ServerCallContext context) {
      return null; //Task.FromResult(service.GetQuote(request.Value));
    }
  }
}
