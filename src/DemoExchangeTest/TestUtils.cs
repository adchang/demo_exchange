using System;
using DemoExchange.Models;

namespace DemoExchange {
  public class TestUtils {
    public static Order NewBuyLimitDayOrder() {
      return NewLimitDayOrder("acct", "ERX", 100, 18.81M, OrderAction.BUY);
    }

    public static Order NewBuyLimitDayOrder(decimal strikePrice) {
      return NewLimitDayOrder("acct", "ERX", 100, strikePrice, OrderAction.BUY);
    }

    public static Order NewSellLimitDayOrder() {
      return NewLimitDayOrder("acct", "ERX", 100, 18.81M, OrderAction.SELL);
    }

    public static Order NewSellLimitDayOrder(decimal strikePrice) {
      return NewLimitDayOrder("acct", "ERX", 100, strikePrice, OrderAction.SELL);
    }

    public static Order NewLimitDayOrder(String accountId, String ticker, int quantity,
      decimal strikePrice, OrderAction action) {
      return OrderAction.BUY.Equals(action) ?
        new BuyLimitDayOrder(accountId, ticker, quantity, strikePrice) :
        new SellLimitDayOrder(accountId, ticker, quantity, strikePrice);
    }

    private TestUtils() {
      // Prevent instantiation
    }
  }
}
