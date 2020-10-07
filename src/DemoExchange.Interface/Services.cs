using System;
using System.Collections.Generic;

namespace DemoExchange.Interface {

  /// <summary>
  /// A service to manage orders.
  /// </summary>
  public interface IOrderService {
    public bool IsMarketOpen { get; }

    /// <summary>
    /// Processes the submitted order.
    /// </summary>
    public IOrderResponse SubmitOrder(IOrderModel order);

    /// <summary>
    /// Cancel the specified order.
    /// </summary>
    public IOrderResponse CancelOrder(String Id);

    /// <summary>
    /// Get quote for specified ticker.
    /// </summary>
    public IQuote GetQuote(String ticker);

    /// <summary>
    /// Get Level 2 for specified ticker.
    /// </summary>
    public ILevel2 GetLevel2(String ticker);
  }

  public interface IOrderResponse : IResponse<IOrderModel> { }

  public class OrderServiceResponse : IOrderResponse {
    public int Code { get; }
    public IOrderModel Data { get; }
    public bool HasErrors { get { return Errors != null && Errors.Count > 0; } }
    public List<IError> Errors { get; }

    public OrderServiceResponse(int code, IOrderModel data) {
      Code = code;
      Data = data;
    }

    public OrderServiceResponse(int code, IOrderModel data, List<IError> errors):
      this(code, data) {
        Errors = errors;
      }
  }

  public interface IAccountService {
    public bool CanFillOrder(IOrderModel order);
  }
}
