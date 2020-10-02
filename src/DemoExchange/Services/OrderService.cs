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
    }
  }

  // QUESTION: Assumes order book always has at least 1 order, ie market maker
  public class OrderManager {
    public String Ticker { get; }
    /// <summary>
    /// VisibleForTesting
    /// </summary>
    protected readonly OrderBook BuyBook;
    /// <summary>
    /// VisibleForTesting
    /// </summary>
    protected readonly OrderBook SellBook;

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
#pragma warning disable IDE0059
        OrderTransaction filled = FillMarketOrder(order, book);
#pragma warning restore IDE0059
        // TODO: Persist filled as 1 db transaction
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

    /// <summary>
    /// VisibleForTesting
    /// </summary>
    protected OrderTransaction FillMarketOrder(Order order, OrderBook book) {
      CheckArgument(!order.Action.Equals(book.Type), "Error: Wrong book");

      bool isBuy = OrderAction.BUY.Equals(order.Action);
      bool done = false;
      List<Order> filledOrders = new List<Order>();
      List<Transaction> transactions = new List<Transaction>();
      while (!done) {
        if (book.IsEmpty)throw new SystemException("Order book is empty"); // TODO: Handle order book is empty
        Order buyOrder = isBuy ? order : book.First;
        Order sellOrder = isBuy ? book.First : order;
        decimal executionPrice = isBuy ? sellOrder.StrikePrice : buyOrder.StrikePrice;
        int fillQuantity = Math.Min(buyOrder.OpenQuantity, sellOrder.OpenQuantity);
        if (!BuyerCanFillOrder(buyOrder.AccountId, fillQuantity, executionPrice)) {
          throw new NotImplementedException(); // TODO
        }
        if (!SellerCanFillOrder(buyOrder.AccountId, fillQuantity, executionPrice)) {
          throw new NotImplementedException(); // TODO
        }
        buyOrder.OpenQuantity -= fillQuantity;
        sellOrder.OpenQuantity -= fillQuantity;
        transactions.Add(new Transaction(buyOrder.Id, sellOrder.Id, order.Ticker,
          fillQuantity, executionPrice));
        filledOrders.Add(book.First);
        if (book.First.IsFilled) {
          Order filledOrder = book.First;
          book.RemoveOrder(filledOrder);
          filledOrder.Status = OrderStatus.COMPLETED;
        }
        if (order.IsFilled) {
          order.Status = OrderStatus.COMPLETED;
          filledOrders.Add(order);
          done = true;
        }
      }

      return new OrderTransaction(filledOrders, transactions);
    }

    /// <summary>
    /// VisibleForTesting
    /// </summary>
#pragma warning disable IDE0060, CA1822
    protected bool BuyerCanFillOrder(String accountId, int quanity, decimal price) {
      return true; // TODO
    }

    /// <summary>
    /// VisibleForTesting
    /// </summary>
    protected bool SellerCanFillOrder(String accountId, int quanity, decimal price) {
      return true; // TODO
    }
#pragma warning restore IDE0060, CA1822
  }

  public class OrderTransaction {
    public List<Order> Orders { get; }
    public List<Transaction> Transactions { get; }

    public OrderTransaction(List<Order> orders, List<Transaction> transactions) {
      Orders = orders;
      Transactions = transactions;
    }
  }
}
