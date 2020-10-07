using System;
using DemoExchange.Interface;

namespace DemoExchange {
  public partial class Dependencies {
    public partial class Account {
      public class AccountModel : AccountBase {
        public AccountModel(String accountId) : base(accountId) { }
      }
    }
  }
}
