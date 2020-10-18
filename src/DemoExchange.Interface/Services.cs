using System;
using System.Collections.Generic;
using System.Threading;
using DemoExchange.Api;
using Grpc.Core;
using Grpc.Net.Client;

namespace DemoExchange.Interface {
  public interface IAccountServiceRpcClient {
    public AsyncUnaryCall<AccountList> ListAsync(Empty request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);

    public AsyncUnaryCall<AccountResponse> CreateAccountAsync(AccountRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);

    public AsyncUnaryCall<AddressResponse> CreateAddressAsync(AddressRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);

    public AsyncUnaryCall<CanFillOrderResponse> CanFillOrderAsync(CanFillOrderRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
  }

  public interface IAccountService {
    public List<IAccountModel> List();

    public IResponse<IAccountModel, AccountResponse> CreateAccount(IAccountModel request);
    public IResponse<IAddressModel, AddressResponse> CreateAddress(IAddressModel request);

    public bool CanFillOrder(Order order, int fillQuantity);
  }

  /// <summary>
  /// Grpc client to OrderService.
  /// </summary>
  public interface IOrderServiceRpcClient {
    public AsyncUnaryCall<BoolMessage> IsMarketOpenAsync(Empty request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);

    public AsyncUnaryCall<OrderResponse> SubmitOrderAsync(OrderRequest request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);

    public AsyncUnaryCall<OrderResponse> CancelOrderAsync(StringMessage request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);

    public AsyncUnaryCall<Level2> GetLevel2Async(StringMessage request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);

    public AsyncUnaryCall<Quote> GetQuoteAsync(StringMessage request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);

    public AsyncUnaryCall<Empty> InitializeServiceAsync(Empty request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);

    public AsyncUnaryCall<AddTickerResponse> AddTickerAsync(StringMessage request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);

    public AsyncUnaryCall<Empty> OpenMarketAsync(Empty request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);

    public AsyncUnaryCall<Empty> CloseMarketAsync(Empty request, Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
  }

  /// <summary>
  /// A service to manage orders.
  /// </summary>
  public interface IOrderService {
    public bool IsMarketOpen { get; }

    public IOrderModel GetOrder(String orderId);

    /// <summary>
    /// Processes the submitted order.
    /// </summary>
    public IResponse<IOrderModel, OrderResponse> SubmitOrder(IOrderModel request);

    /// <summary>
    /// Cancel the specified order.
    /// </summary>
    public IResponse<IOrderModel, OrderResponse> CancelOrder(String orderId);

    /// <summary>
    /// Get Level 2 for specified ticker.
    /// </summary>
    public Level2 GetLevel2(String ticker);

    /// <summary>
    /// Get quote for specified ticker.
    /// </summary>
    public Quote GetQuote(String ticker);

    public AddTickerResponse AddTicker(String ticker);

    public void OpenMarket();

    public void CloseMarket();
  }
}
