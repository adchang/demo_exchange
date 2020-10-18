using System;
using System.Collections.Generic;
using System.Threading;
using DemoExchange.Api;
using Grpc.Core;
using Grpc.Net.Client;

namespace DemoExchange.Interface {
  public class AccountServiceRpcClient : AccountService.AccountServiceClient,
    IAccountServiceRpcClient {
      public AccountServiceRpcClient(GrpcChannel channel) : base(channel) { }
    }
}
