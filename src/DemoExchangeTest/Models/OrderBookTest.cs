using System;
using Xunit;

namespace DemoExchange.Models {
  public class OrderBookTest {
    [Fact]
    [Trait("Category", "Unit")]
    public void AddOrderTest() {
      OrderBook book = new OrderBook("ERX", OrderAction.BUY);
      Exception e = Assert.Throws<ArgumentException>(() =>
        book.AddOrder(new BuyMarketOrder("acct", "E", 100)));
      Assert.Equal(OrderBook.ERROR_MARKET_ORDER,
        e.Message);
      BuyLimitDayOrder buyLimit = new BuyLimitDayOrder("acct", "E", 100, (decimal)0.50);
      e = Assert.Throws<ArgumentException>(() =>
        book.AddOrder(buyLimit));
      Assert.Equal(String.Format(OrderBook.ERROR_TICKER, "ERX", buyLimit.Id),
        e.Message);
      SellLimitDayOrder sellLimit = new SellLimitDayOrder("acct", "ERX", 100, (decimal)18.81);
      e = Assert.Throws<ArgumentException>(() =>
        book.AddOrder(sellLimit));
      Assert.Equal(String.Format(OrderBook.ERROR_ACTION, OrderAction.BUY, sellLimit.Action),
        e.Message);

      BuyLimitDayOrder order1 = new BuyLimitDayOrder("acct", "ERX", 100, (decimal)18.81);
      BuyLimitDayOrder order2 = new BuyLimitDayOrder("acct", "ERX", 100, (decimal)18.82);
      BuyLimitDayOrder order3 = new BuyLimitDayOrder("acct", "ERX", 100, (decimal)18.83);
      BuyLimitDayOrder order4 = new BuyLimitDayOrder("acct", "ERX", 100, (decimal)18.84);
      BuyLimitDayOrder order5 = new BuyLimitDayOrder("acct", "ERX", 100, (decimal)18.85);
      book.AddOrder(order3);
      book.AddOrder(order5);
      book.AddOrder(order1);
      book.AddOrder(order2);
      book.AddOrder(order4);

      e = Assert.Throws<ArgumentException>(() =>
        book.AddOrder(order1));
      Assert.Equal(String.Format(OrderBook.ERROR_ORDER_EXISTS, order1.Id),
        e.Message);
    }
  }
}
