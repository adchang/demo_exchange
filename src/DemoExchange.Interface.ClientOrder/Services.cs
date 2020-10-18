using System;
using System.Collections.Generic;
using System.Threading;
using DemoExchange.Api;
using Grpc.Core;
using Grpc.Net.Client;

namespace DemoExchange.Interface {
  /// <summary>
  /// Grpc client to OrderService.
  /// </summary>
  public class OrderServiceRpcClient : OrderService.OrderServiceClient, IOrderServiceRpcClient {
    public OrderServiceRpcClient(GrpcChannel channel) : base(channel) { }
  }
}
