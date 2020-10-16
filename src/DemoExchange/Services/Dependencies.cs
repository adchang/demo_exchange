using System;
using DemoExchange.Api.Order;
using DemoExchange.Interface;
using Serilog;

namespace DemoExchange {
  public partial class Dependencies {
    public class AccountService : IAccountService {
      private static Serilog.ILogger logger => Serilog.Log.ForContext<AccountService>();

      public AccountService() { }

      public bool CanFillOrder(Order order) {
        logger.Here().Debug("TODO, should probably return a response instead of a bool");
        return true;
      }
    }
  }
}
