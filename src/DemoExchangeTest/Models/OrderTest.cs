using System;
using Xunit;

namespace DemoExchange.Models {
  public class OrderTest {
    [Fact]
    public void MarketOrderTest() {
      Order order = new BuyMarketOrder("accountId", "ERX", 100);
      Assert.True(order.IsOpen);
      Assert.False(order.IsCompleted);
      Assert.False(order.IsUpdated);
      Assert.False(order.IsCancelled);
      Assert.False(order.IsDeleted);
      Assert.True(order.IsBuyOrder);
      Assert.False(order.IsSellOrder);
      Assert.True(order.IsMarketOrder);
      Assert.False(order.IsLimitOrder);
      Assert.False(order.IsStopMarketOrder);
      Assert.False(order.IsStopLimitOrder);
      Assert.False(order.IsTrailingStopMarketOrder);
      Assert.False(order.IsTrailingStopLimitOrder);
      Assert.False(order.IsFillOrKillOrder);
      Assert.False(order.IsImmediateOrCancelOrder);
      Assert.Equal(0, order.StrikePrice);
      Assert.Equal(order.Quantity, order.OpenQuantity);
      Assert.True(order.IsDayOrder);
      Assert.False(order.IsGoodTillCanceledOrder);
      Assert.Equal("{Id: " + order.Id + ", CreatedTimestamp: " + order.CreatedTimestamp + ", " +
        "AccountId: accountId, Status: OPEN, Action: BUY, Ticker: ERX, Type: MARKET, " +
        "StrikePrice: 0, Quantity: 100, OpenQuantity: 100, " +
        "TimeInForce: DAY, ToBeCanceledTimestamp: 0, }",
        order.ToString());

      Assert.False(order.IsFilled);
      order.OpenQuantity = 0;
      Assert.True(order.IsFilled);

      order = new SellMarketOrder("accountId", "ERX", 100);
      Assert.Equal("accountId", order.AccountId);
      Assert.True(order.IsOpen);
      Assert.True(order.IsSellOrder);
      Assert.Equal("ERX", order.Ticker);
      Assert.True(order.IsMarketOrder);
      Assert.Equal(0, order.StrikePrice);
      Assert.Equal(100, order.Quantity);
      Assert.True(order.IsDayOrder);
    }
  }
}
