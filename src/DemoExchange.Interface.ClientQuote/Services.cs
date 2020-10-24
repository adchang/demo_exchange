using System;
using System.Collections.Generic;
using System.Threading;
using DemoExchange.Api;
using Grpc.Core;
using Grpc.Net.Client;

namespace DemoExchange.Interface {
  public class QuoteServiceRpcClient : QuoteService.QuoteServiceClient,
    IQuoteServiceRpcClient {
      public QuoteServiceRpcClient(GrpcChannel channel) : base(channel) { }
    }
}
