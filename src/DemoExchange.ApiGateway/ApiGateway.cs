using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoExchange.Api;
using DemoExchange.Interface;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Serilog;

namespace DemoExchange.ApiGateway {
  public class ApiGateway : ErxService.ErxServiceBase {
    private static Serilog.ILogger Logger => Serilog.Log.ForContext<ApiGateway>();

    private readonly IAccountServiceRpcClient accountService;
    private readonly IOrderServiceRpcClient orderService;
    private readonly IQuoteServiceRpcClient quoteService;

    public ApiGateway(IAccountServiceRpcClient accountService,
      IOrderServiceRpcClient orderService, IQuoteServiceRpcClient quoteService) {
      this.accountService = accountService;
      this.orderService = orderService;
      this.quoteService = quoteService;
    }

    //[Authorize]
    public override async Task<AccountList> ListAccounts(Empty request, ServerCallContext context) {
      Logger.Here().Information("BGN");
      // TODO: authentication
      try {
        var response = accountService.ListAsync(request);

        Logger.Here().Information("END");
        return await response;
      } catch (Exception e) {
        Logger.Here().Warning(e.Message);
        throw new RpcException(new Status(StatusCode.Internal, e.Message));
      }
    }

    public override async Task<AccountResponse> CreateAccount(AccountRequest request, ServerCallContext context) {
      Logger.Here().Information("BGN");
      try {
        var response = accountService.CreateAccountAsync(request);

        Logger.Here().Information("END");
        return await response;
      } catch (Exception e) {
        Logger.Here().Warning(e.Message);
        throw new RpcException(new Status(StatusCode.Internal, e.Message));
      }
    }

    public override async Task<OrderResponse> SubmitOrder(OrderRequest request, ServerCallContext context) {
      Logger.Here().Information("BGN");
      try {
        var response = orderService.SubmitOrderAsync(request);

        Logger.Here().Information("END");
        return await response;
      } catch (Exception e) {
        Logger.Here().Warning(e.Message);
        throw new RpcException(new Status(StatusCode.Internal, e.Message));
      }
    }

    public override async Task<Level2> GetLevel2(StringMessage request, ServerCallContext context) {
      Logger.Here().Information("BGN");
      try {
        var response = quoteService.GetLevel2Async(request);

        Logger.Here().Information("END");
        return await response;
      } catch (Exception e) {
        Logger.Here().Warning(e.Message);
        throw new RpcException(new Status(StatusCode.Internal, e.Message));
      }
    }

    public override async Task GetLevel2Streams(StringMessage request, 
      IServerStreamWriter<Level2> responseStream, ServerCallContext context){
      Logger.Here().Information("BGN");
      try {
        var response = quoteService.GetLevel2Streams(request).ResponseStream;
        while (await response.MoveNext()) {
          await responseStream.WriteAsync(response.Current);
        }
      } catch (Exception e) {
        Logger.Here().Warning(e.Message);
        throw new RpcException(new Status(StatusCode.Internal, e.Message));
      }
    }

    public override async Task<Quote> GetQuote(StringMessage request, ServerCallContext context) {
      Logger.Here().Information("BGN");
      try {
        var response = quoteService.GetQuoteAsync(request);

        Logger.Here().Information("END");
        return await response;
      } catch (Exception e) {
        Logger.Here().Warning(e.Message);
        throw new RpcException(new Status(StatusCode.Internal, e.Message));
      }
    }

    public override async Task GetHistoricalPriceStreams(HistoricalPriceRequest request,
      IServerStreamWriter<HistoricalPrice> responseStream, ServerCallContext context) {
      Logger.Here().Information("BGN");
      try {
        var response = quoteService.GetHistoricalPriceStreams(request).ResponseStream;
        while (await response.MoveNext()) {
          await responseStream.WriteAsync(response.Current);
        }
      } catch (Exception e) {
        Logger.Here().Warning(e.Message);
        throw new RpcException(new Status(StatusCode.Internal, e.Message));
      }
    }

    public override async Task<Empty> InitializeService(Empty request, ServerCallContext context) {
      Logger.Here().Information("BGN");
      try {
        var response = orderService.InitializeServiceAsync(request);

        Logger.Here().Information("END");
        return await response;
      } catch (Exception e) {
        Logger.Here().Warning(e.Message);
        throw new RpcException(new Status(StatusCode.Internal, e.Message));
      }
    }
  }
}
