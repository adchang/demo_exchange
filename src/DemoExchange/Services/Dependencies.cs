using System;
using DemoExchange.Api.Order;
using DemoExchange.Interface;
using Serilog;

namespace DemoExchange {
  public partial class Dependencies {
    public class AccountService : IAccountService {
      private readonly ILogger logger = Log.Logger;

      public AccountService() { }

      public bool CanFillOrder(Order order) {
        logger.Debug("TODO, should probably return a response instead of a bool");
        return true;
      }
    }
  }
}
