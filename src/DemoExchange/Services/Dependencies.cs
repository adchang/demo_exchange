using System;
using DemoExchange.Interface;

namespace DemoExchange {
  public partial class Dependencies {
    public class AccountService : IAccountService {
      public bool CanFillOrder(IOrderModel order) {
        return true;
      }
    }
  }
}
