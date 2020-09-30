using System;
using Xunit;

namespace DemoExchange.Models {
  public class OrderTest {
    [Fact]
    public void OrderTest_constructor() {
      Order order = new Order(1, OrderType.MARKET);
      Assert.Equal(order.Quantity, order.OpenQuantity);
      Assert.Equal("{OrderId: , Quantity: 1, OpenQuantity: 1, OrderType: MARKET, }",
        order.ToString());
      Assert.False(order.IsFilled);
      order.OpenQuantity = 0;
      Assert.True(order.IsFilled);
    }
  }
}
