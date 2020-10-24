using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoExchange.Api;
using DemoExchange.Interface;
using Google.Protobuf;
using Grpc.Core;
using Microsoft.VisualBasic.CompilerServices;
using Serilog;
using StackExchange.Redis;
using static Utils.Time;

namespace DemoExchange.QuoteService {
  public class QuoteServiceGrpc : DemoExchange.Api.QuoteService.QuoteServiceBase {
    private static Serilog.ILogger Logger => Serilog.Log.ForContext<QuoteServiceGrpc>();

    private readonly IDatabase redis;
    private readonly IOrderServiceRpcClient orderService;

    private const int POS_TIMESTAMP = 0;
    private const int POS_QUOTE = 1;
    private const int POS_LEVEL2 = 2;

    public QuoteServiceGrpc(IDatabase redis, IOrderServiceRpcClient orderService) {
      this.redis = redis;
      this.orderService = orderService;
    }

    public override Task<Level2> GetLevel2(StringMessage request, ServerCallContext context) {
      Logger.Here().Information("BGN");
      byte[] bytes;
      var data = redis.StringGet(GetKeySet(request.Value));
      RedisValue level2 = data[POS_LEVEL2];
      if (level2.HasValue) {
        bytes = (RedisValue)level2;
      } else {
        List<KeyValuePair<RedisKey, RedisValue>> result = GetAndSetLevel2(request);
        bytes = (RedisValue)result[POS_LEVEL2].Value;
      }
      Level2 level2Result = Level2.Parser.ParseFrom(bytes);

      Logger.Here().Information("END");
      return Task.FromResult(level2Result);
    }

    public override Task<Quote> GetQuote(StringMessage request, ServerCallContext context) {
      Logger.Here().Information("BGN");
      byte[] bytes;
      var data = redis.StringGet(GetKeySet(request.Value));
      RedisValue quote = data[POS_QUOTE];
      if (quote.HasValue) {
        bytes = (RedisValue)quote;
      } else {
        List<KeyValuePair<RedisKey, RedisValue>> result = GetAndSetLevel2(request);
        bytes = (RedisValue)result[POS_QUOTE].Value;
      }
      Quote quoteResult = Quote.Parser.ParseFrom(bytes);

      Logger.Here().Information("END");
      return Task.FromResult(quoteResult);
    }

    private static RedisKey[] GetKeySet(String ticker) {
      return new RedisKey[] {
        Constants.Redis.QUOTE_TIMESTAMP + ticker,
          Constants.Redis.QUOTE + ticker,
          Constants.Redis.QUOTE_LEVEL2 + ticker
      };
    }

    private List<KeyValuePair<RedisKey, RedisValue>> GetAndSetLevel2(StringMessage request) {
      Logger.Here().Information("BGN");
      String ticker = request.Value;
      try {
        Task<Level2> level2Response = orderService.GetLevel2Async(request).ResponseAsync;
        Level2 level2 = level2Response.Result;
        Quote quote = new Quote() {
          Bid = level2.Bids[0].Price,
          Ask = level2.Asks[0].Price,
          Last = 0,
          Volume = 0
        };
        List<KeyValuePair<RedisKey, RedisValue>> batch =
          new List<KeyValuePair<RedisKey, RedisValue>>(3) {
            new KeyValuePair<RedisKey, RedisValue>(
            Constants.Redis.QUOTE_TIMESTAMP + ticker, Now),
            new KeyValuePair<RedisKey, RedisValue>(
            Constants.Redis.QUOTE + ticker, quote.ToByteArray()),
            new KeyValuePair<RedisKey, RedisValue>(
            Constants.Redis.QUOTE_LEVEL2 + ticker, level2.ToByteArray())
          };
        redis.StringSet(batch.ToArray());

        Logger.Here().Information("END");
        return batch;
      } catch (Exception e) {
        Logger.Here().Error(e.Message);
        throw e;
      }
    }
  }
}
