using System;
using System.Collections.Generic;
using DemoExchange.Interface;
using DemoExchange.Models;
using static Utils.Preconditions;

namespace DemoExchange.Services {

  public class OrderService : IOrderService {
    private readonly IDictionary<String, OrderManager> managers =
      new Dictionary<String, OrderManager>();

    public OrderService() {
      // TODO: OrderService DI
    }

    public void AddTicker(string ticker) {
      // TODO: AddTicker preconditions & tests
      managers.Add(ticker, new OrderManager(ticker));
    }

    public void SubmitOrder(IModelOrder data) {
      // TODO: Add tests
      CheckNotNull(data, paramName : nameof(data));

      Order order = (Order)data;
      managers[order.Ticker].SubmitOrder(order);
    }

    public void CancelOrder(String id) {
      throw new NotImplementedException();
      // TODO: Persist order cancel update
    }
  }

  public class OrderManager {
    public String Ticker { get; }
    private readonly OrderBook BuyBook;
    private readonly OrderBook SellBook;

    public OrderManager(String ticker) {
      Ticker = ticker;
      BuyBook = new OrderBook(ticker, OrderAction.BUY);
      SellBook = new OrderBook(ticker, OrderAction.SELL);
    }

    public void SubmitOrder(Order order) {
      if (!order.IsValid()) {
        throw new NotImplementedException(); // TODO
      }

      OrderBook book = OrderType.MARKET.Equals(order.Type) ?
        OrderAction.BUY.Equals(order.Action) ? SellBook : BuyBook :
        OrderAction.BUY.Equals(order.Action) ? BuyBook : SellBook;

      if (OrderType.MARKET.Equals(order.Type)) {
        // TODO: Execute the market order
        return;
      }

      // TODO: Persist order insert
      book.AddOrder(order);
      bool done = false;
      while (!done) {
        Order buyOrder = BuyBook.First;
        Order sellOrder = SellBook.First;
        if (buyOrder.StrikePrice >= sellOrder.StrikePrice) {
          // TODO: Execute the limit order at sell price

        } else {
          done = true;
        }
      }
    }
  }
}
