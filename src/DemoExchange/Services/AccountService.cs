using System;
using System.Collections.Generic;
using System.Linq;
using DemoExchange.Api;
using DemoExchange.Contexts;
using DemoExchange.Interface;
using DemoExchange.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DemoExchange.Services {
  public class AccountService : IAccountService {
    private static Serilog.ILogger Logger => Serilog.Log.ForContext<AccountService>();

    private readonly IDemoExchangeDbContextFactory<AccountContext> accountContextFactory;

    public AccountService(IDemoExchangeDbContextFactory<AccountContext> accountContextFactory) {
      this.accountContextFactory = accountContextFactory;
    }

    public List<IAccountModel> List() {
      Logger.Here().Information("BGN");
      using AccountContext context = accountContextFactory.Create();
      List<AccountEntity> data = context.Accounts.ToList();

      List<IAccountModel> response = new List<IAccountModel>();
      data.ForEach(account => response.Add(new AccountBL(account)));
      Logger.Here().Information("END");
      return response;
    }

    public IResponse<IAccountModel, AccountResponse> CreateAccount(IAccountModel request) {
      Logger.Here().Information("BGN");
      using AccountContext context = accountContextFactory.Create();
      context.Accounts.Add((AccountEntity)request);
      try {
        context.SaveChanges();
      } catch (Exception e) {
        Logger.Here().Warning("Failed: " + e.Message);
        return new AccountResponseBL(Constants.Response.INTERNAL_SERVER_ERROR,
          request, new Error {
            Description = e.Message
          });
      }

      Logger.Here().Information("END");
      return new AccountResponseBL(request);
    }

    public IResponse<IAddressModel, AddressResponse> CreateAddress(IAddressModel request) {
      Logger.Here().Information("BGN");

      Logger.Here().Information("END");
      return null;
    }

    public bool CanFillOrder(Order order, int fillQuantity) {
      Logger.Here().Information("BGN");
      using AccountContext context = accountContextFactory.Create();

      AccountBL account = new AccountBL(context.GetAccountById(order.AccountId));
      if (!account.IsActive) {
        Logger.Here().Information("Account is not Active");
        return false;
      }
      Logger.Here().Information("END");
      return true;
    }
  }

  public class AccountResponseBL : ResponseBase<IAccountModel, AccountResponse> {
    public AccountResponseBL() { }
    public AccountResponseBL(IAccountModel data) : base(data) { }
    public AccountResponseBL(int code, IAccountModel data) : base(code, data) { }
    public AccountResponseBL(int code, IAccountModel data, Error error) : base(code, data, error) { }
    public AccountResponseBL(int code, IAccountModel data, List<Error> errors) : base(code,
      data, errors) { }

    public override AccountResponse ToMessage() {
      AccountResponse response = new AccountResponse {
        Code = this.Code,
        Data = this.Data.ToMessage()
      };
      if (this.HasErrors) {
        this.Errors.ForEach(error => response.Errors.Add(error));
      }

      return response;
    }
  }

  public class AddressResponseBL : ResponseBase<IAddressModel, AddressResponse> {
    public AddressResponseBL() { }
    public AddressResponseBL(IAddressModel data) : base(data) { }
    public AddressResponseBL(int code, IAddressModel data) : base(code, data) { }
    public AddressResponseBL(int code, IAddressModel data, Error error) : base(code, data, error) { }
    public AddressResponseBL(int code, IAddressModel data, List<Error> errors) : base(code,
      data, errors) { }

    public override AddressResponse ToMessage() {
      AddressResponse response = new AddressResponse {
        Code = this.Code,
        Data = this.Data.ToMessage()
      };
      if (this.HasErrors) {
        this.Errors.ForEach(error => response.Errors.Add(error));
      }

      return response;
    }
  }
}
