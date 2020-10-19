using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoExchange.Api;
using DemoExchange.Interface;
using DemoExchange.Models;
using Grpc.Core;
using Serilog;

namespace DemoExchange.AccountService {
  public class AccountServiceGrpc : DemoExchange.Api.AccountService.AccountServiceBase {
    private static Serilog.ILogger Logger => Serilog.Log.ForContext<AccountServiceGrpc>();

    private readonly IAccountService service;

    public AccountServiceGrpc(IAccountService service) {
      this.service = service;
    }

    public override Task<AccountList> List(Empty request, ServerCallContext context) {
      Logger.Here().Information("BGN");
      List<IAccountModel> data;
      try {
        data = service.List();
      } catch (Exception e) {
        Logger.Here().Warning("Error creating account E: " + e.Message);
        throw new RpcException(new Status(StatusCode.Internal, e.Message));
      }
      AccountList response = new AccountList();
      data.ForEach(account => response.Accounts.Add(account.ToMessage()));
      Logger.Here().Information("END");
      return Task.FromResult(response);
    }

    public override Task<AccountResponse> CreateAccount(AccountRequest request, ServerCallContext context) {
      Logger.Here().Information("BGN");
      IResponse<IAccountModel, AccountResponse> response;
      try {
        response = service.CreateAccount(new AccountBL(request));
      } catch (Exception e) {
        Logger.Here().Warning("Error creating account E: " + e.Message);
        throw new RpcException(new Status(StatusCode.Internal, e.Message));
      }
      Logger.Here().Information("END");
      return Task.FromResult(response.ToMessage());
    }

    public override Task<AddressResponse> CreateAddress(AddressRequest request, ServerCallContext context) {
      Logger.Here().Information("BGN");
      IResponse<IAddressModel, AddressResponse> response;
      try {
        response = service.CreateAddress(new AddressBL(request));
      } catch (Exception e) {
        Logger.Here().Warning("Error creating address E: " + e.Message);
        throw new RpcException(new Status(StatusCode.Internal, e.Message));
      }
      Logger.Here().Information("END");
      return Task.FromResult(response.ToMessage());
    }

    public override Task<CanFillOrderResponse> CanFillOrder(CanFillOrderRequest request, ServerCallContext context) {
      Logger.Here().Information("BGN");
      bool response;
      try {
        response = service.CanFillOrder(request.Order, request.FillQuantity);
      } catch (Exception e) {
        Logger.Here().Warning("Error E: " + e.Message);
        throw new RpcException(new Status(StatusCode.Internal, e.Message));
      }
      Logger.Here().Information("END");
      return Task.FromResult(new CanFillOrderResponse {
        Value = response
      });
    }
  }
}
