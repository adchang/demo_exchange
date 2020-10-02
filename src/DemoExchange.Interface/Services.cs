using System;

namespace DemoExchange.Interface {
  /// <summary>
  /// A service to manage orders.
  /// </summary>
  public interface IOrderService {
    /// <summary>
    /// Add specified ticker to be managed by this service instance.
    /// </summary>
    public void AddTicker(String ticker);

    /// <summary>
    /// Processes the submitted order.
    /// </summary>
    public void SubmitOrder(IModelOrder order);

    /// <summary>
    /// Cancel the specified order.
    /// </summary>
    public void CancelOrder(String Id);
  }
}
