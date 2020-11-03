using System;
using System.Collections.Generic;
using System.Threading;
using DemoExchange.Api;
using Grpc.Core;
using Grpc.Net.Client;

namespace DemoExchange.Interface {
  public class Constants {
    public const String FORMAT_PRICE = "0.0000000000";

    public class Identity {
      public const String BEARER = "Bearer";

      public const String API_NAME = "ER-X API";
      public const String API_SCOPE = "api";
      public const String CLIENT_ID = "erx.client";
      public const String SECRET = "TODO in keystore";

      public const String POLICY_SCOPE_NAME = "ApiScope";
      public const String CLAIM_NAME = "scope";

      private Identity() {
        // Prevent instantiation
      }
    }

    public class Response {
      public const int OK = 200;
      public const int CREATED = 201;
      public const int ACCEPTED = 202;

      public const int BAD_REQUEST = 400;
      public const int UNAUTHORIZED = 401;
      public const int FORBIDDEN = 403;
      public const int NOT_FOUND = 404;

      public const int INTERNAL_SERVER_ERROR = 500;

      private Response() {
        // Prevent instantiation
      }
    }

    public class Redis {
      public const String CONSUMER_GROUP = "CONSUMER_GROUP";
      public const String STREAM = "STREAM";
      public const String TIMESTAMP = "TIMESTAMP_";

      public const String QUOTE = "QUOTE_";
      public const String QUOTE_CONSUMER_GROUP = QUOTE + CONSUMER_GROUP;
      public const String QUOTE_STREAM = QUOTE + STREAM;
      public const String QUOTE_TIMESTAMP = QUOTE + TIMESTAMP;
      public const String QUOTE_LEVEL2 = QUOTE + "LEVEL2_";

      private Redis() {
        // Prevent instantiation
      }
    }

    public class PubSub {
      public const String TOPIC_TRANSACTION_PROCESSED = "TransactionProcessed";

      private PubSub() {
        // Prevent instantiation
      }
    }

    private Constants() {
      // Prevent instantiation
    }
  }

  public class Config {
    public class ErxServices {
      public const String SECTION = "ErxServices";

      public String IdentityUrlBase { get; set; }
      public String AccountUrlBase { get; set; }
      public String OrderUrlBase { get; set; }
      public String QuoteUrlBase { get; set; }
    }

    public class ConnectionStrings {
      public const String SECTION = "ConnectionStrings";

      public String DemoExchangeDb { get; set; }
      public String Redis { get; set; }
    }

    private Config() {
      // Prevent instantiation
    }
  }

  public interface IResponse<T, V> {
    public int Code { get; }
    public T Data { get; }
    public bool HasErrors { get; }
    public List<Error> Errors { get; }
    public V ToMessage();
  }

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

  public interface IQuoteServiceRpcClient {
    public AsyncUnaryCall<Level2> GetLevel2Async(StringMessage request, 
      Metadata headers = null, DateTime? deadline = null, 
      CancellationToken cancellationToken = default);

    public AsyncServerStreamingCall<Level2> GetLevel2Streams(StringMessage request,
      Metadata headers = null, DateTime? deadline = null, 
      CancellationToken cancellationToken = default);

    public AsyncUnaryCall<Quote> GetQuoteAsync(StringMessage request, 
      Metadata headers = null, DateTime? deadline = null, 
      CancellationToken cancellationToken = default);
  }
}
