using System;
using DemoExchange.Interface;

namespace DemoExchange {
  public partial class Dependencies {
    public partial class Account {
      public class ModelAccount : BaseAccount {
        public ModelAccount(String accountId) : base(accountId) { }
      }
    }
  }
}
