using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DemoExchange.Contexts;
using DemoExchange.Interface;
using DemoExchange.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using static Utils.Preconditions;
using static Utils.Time;

namespace DemoExchange.Services {
  public interface IOrderInternalService : IOrderService {
    public Tuple<int, int> AddTicker(String ticker);
    public void OpenMarket();
    public void CloseMarket();

#if (PERF || PERF_FINE || PERF_FINEST)
    public void TestPerfAddOrder(String ticker,
      List<IOrderModel> buyOrders, List<IOrderModel> sellOrders);
    public Tuple<int, int> TestPerfLoadOrderBook(String ticker);
#endif
  }

  public class OrderService : IOrderInternalService {
    private readonly ILogger logger = Log.Logger;

    public const String MARKET_CLOSED = "Market is closed.";

    // Question: ConcurrentDictionary instead? https://docs.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentdictionary-2?view=net-5.0
    private readonly IDictionary<String, OrderManager> managers =
      new Dictionary<String, OrderManager>();
    private readonly IDemoExchangeDbContextFactory<OrderContext> orderContextFactory;
    private readonly IAccountService accountService;

    public bool IsMarketOpen { get; private set; }

    public OrderService(IDemoExchangeDbContextFactory<OrderContext> orderContextFactory,
      IAccountService accountService) {
      this.orderContextFactory = orderContextFactory;
      this.accountService = accountService;
    }

    private void CheckMarketIsOpen() {
      if (!IsMarketOpen) {
        throw new InvalidOperationException(MARKET_CLOSED);
      }
    }

    public void OpenMarket() {
#if DEBUG
      IsMarketOpen = true;
#else
      // TODO If Market hours, set marketOpen to true;
      throw new InvalidOperationException(MARKET_CLOSED);
#endif
    }

    public void CloseMarket() {
#if DEBUG
      IsMarketOpen = false;
#else
      // TODO If !Market hours, set marketOpen to false;
      throw new InvalidOperationException(MARKET_CLOSED);
#endif
    }

    public Tuple<int, int> AddTicker(String ticker) {
      // TODO: AddTicker preconditions & tests
      lock(managers) {
        using OrderContext context = orderContextFactory.Create();
        OrderManager manager = new OrderManager(context, ticker);
        managers.Add(ticker, manager);
        return manager.Count;
      }
    }

    public IOrderResponse SubmitOrder(IOrderModel orderRequest) {
      // TODO: CheckMarketIsOpen(); This is not correct; should still be able to submit 
      //       orders if market is not open; Just can't trade.
      // TODO: Add tests
      CheckNotNull(orderRequest, paramName : nameof(orderRequest));
      OrderResponse response = NewOrder(orderRequest);
      if (response.HasErrors)return response;

      OrderManager manager = managers[response.Data.Ticker];
      lock(manager) {
        using OrderContext context = orderContextFactory.Create();
        return manager.SubmitOrder(context, accountService, (Order)response.Data);
      }
    }

    public IOrderResponse CancelOrder(String orderId) {
      throw new NotImplementedException();
    }

    public IQuote GetQuote(String ticker) {
      // TODO: GetQuote preconditions & tests
      return managers[ticker].Quote;
    }

    public ILevel2 GetLevel2(String ticker) {
      // TODO: GetLevel2 preconditions & tests
      return managers[ticker].Level2;
    }

    private static OrderResponse NewOrder(IOrderModel request) {
      try {
        return new OrderResponse(new Order(request));
      } catch (Exception e) {
        return new OrderResponse(Constants.Response.BAD_REQUEST, request,
          new Error("", "", e.Message));
      }
    }

#if (PERF || PERF_FINE || PERF_FINEST)
    public void TestPerfAddOrder(String ticker,
      List<IOrderModel> buyOrders, List<IOrderModel> sellOrders) {
      OrderManager manager = new OrderManager(ticker);
      manager.TestPerfAddOrder(buyOrders, sellOrders, orderContextFactory);
      managers.Add(ticker, manager);
    }

    public Tuple<int, int> TestPerfLoadOrderBook(String ticker) {
      OrderManager manager = managers[ticker];
      lock(manager) {
        using OrderContext context = orderContextFactory.Create();
        return manager.TestPerfLoadOrderBook(context);
      }
    }
#endif
  }

  // QUESTION: Assumes order book always has at least 1 order, ie market maker
  public class OrderManager {
    private readonly ILogger logger = Log.Logger;

    private readonly OrderBook BuyBook;
    private readonly OrderBook SellBook;

    public Tuple<int, int> Count {
      get { return new Tuple<int, int>(BuyBook.Count, SellBook.Count); }
    }
    public Quote Quote {
      get { return new Quote(BuyBook.First.StrikePrice, SellBook.First.StrikePrice); }
    }
    public Level2 Level2 {
      get { return new Level2(BuyBook.Level2, SellBook.Level2); }
    }

    public OrderManager(IOrderContext context, String ticker) {
      BuyBook = new OrderBook(context, ticker, OrderAction.BUY);
      SellBook = new OrderBook(context, ticker, OrderAction.SELL);
    }

    public OrderResponse SubmitOrder(IOrderContext context,
      IAccountService accountService, Order order) {
#if (PERF || PERF_FINE || PERF_FINEST)
      long start = Now;
#endif
      OrderResponse insertResponse = InsertOrder(context, order);
      if (insertResponse.HasErrors) {
        return insertResponse;
      }

      OrderBook book = order.IsMarketOrder ?
        order.IsBuyOrder ? SellBook : BuyBook :
        order.IsBuyOrder ? BuyBook : SellBook;
      if (order.IsMarketOrder) {
        // QUESTION: Could potentially optimize to not do an initial save of a Market order
        //           but can we allow order entry during market close hours? What's MarketOnOpen?
        OrderTransactionResponse fillResponse = FillMarketOrder(accountService, order, book);
        if (fillResponse.HasErrors) {
          return new OrderResponse(fillResponse.Code, order, fillResponse.Errors);
        }
        fillResponse = SaveOrderTransaction(context, fillResponse.Data);
        if (fillResponse.HasErrors) {
          return new OrderResponse(fillResponse.Code, order, fillResponse.Errors);
        }
#if DIAGNOSTICS
        DiagnosticsWriteDetails(fillResponse.Data);
#endif
#if (PERF || PERF_FINE || PERF_FINEST)
        logger.Information(String.Format("SubmitOrder: Market order executed in {0} milliseconds", ((Now - start) / TimeSpan.TicksPerMillisecond)));
#endif
        return new OrderResponse(order);
      }

      book.AddOrder(order); // HACK: Technically, this should return back as Submitted, and the TryFillOrderBook is a separate process that runs continuously
      bool done = false;
      while (!done) {
        if (BuyBook.IsEmpty || SellBook.IsEmpty)throw new SystemException("Order book is empty"); // TODO: Handle order book is empty
        if (BuyBook.First.StrikePrice >= SellBook.First.StrikePrice) {
          OrderTransactionResponse fillResponse = TryFillOrderBook(accountService);
          if (fillResponse.HasErrors) {
            return new OrderResponse(fillResponse.Code, order, fillResponse.Errors);
          }
          fillResponse = SaveOrderTransaction(context, fillResponse.Data);
          if (fillResponse.HasErrors) {
            return new OrderResponse(fillResponse.Code, order, fillResponse.Errors);
          }
#if DIAGNOSTICS
          DiagnosticsWriteDetails(fillResponse.Data);
#endif
        } else {
          done = true;
        }
      }
#if (PERF || PERF_FINE || PERF_FINEST)
      logger.Information(String.Format("SubmitOrder: Processed in {0} milliseconds", ((Now - start) / TimeSpan.TicksPerMillisecond)));
#endif
      return insertResponse;
    }

    private OrderTransactionResponse FillMarketOrder(IAccountService accountService,
      Order order, OrderBook book) {
#if PERF_FINEST
      long start = Now;
#endif
      CheckArgument(!order.Action.Equals(book.Type), "Error: Wrong book");

      bool done = false;
      List<Order> filledOrders = new List<Order>();
      List<Transaction> transactions = new List<Transaction>();
      while (!done) {
        if (book.IsEmpty)throw new SystemException("Order book is empty"); // TODO: Handle order book is empty
        Order buyOrder = order.IsBuyOrder ? order : book.First;
        Order sellOrder = order.IsSellOrder ? order : book.First;
        decimal executionPrice = order.IsBuyOrder ? sellOrder.StrikePrice : buyOrder.StrikePrice;
        int fillQuantity = Math.Min(buyOrder.OpenQuantity, sellOrder.OpenQuantity);
        // QUESTION: Many issues here, partial fill, etc
        if (!accountService.CanFillOrder(buyOrder)) {
          throw new NotImplementedException(); // TODO
        }
        if (!accountService.CanFillOrder(sellOrder)) {
          throw new NotImplementedException(); // TODO
        }

        buyOrder.OpenQuantity -= fillQuantity;
        sellOrder.OpenQuantity -= fillQuantity;
        transactions.Add(new Transaction(buyOrder, sellOrder, buyOrder.Ticker,
          fillQuantity, executionPrice));
        filledOrders.Add(book.First);
        if (book.First.IsFilled) {
          Order filledOrder = book.First;
          book.RemoveOrder(filledOrder);
          filledOrder.Complete();
        }
        if (order.IsFilled) {
          order.Complete();
          filledOrders.Add(order);
          done = true;
        }
      }
#if PERF_FINEST
      logger.Information(String.Format("Market order executed in {0} milliseconds", ((Now - start) / TimeSpan.TicksPerMillisecond)));
#endif

      return new OrderTransactionResponse(Constants.Response.CREATED,
        new OrderTransaction(filledOrders, transactions));
    }

    private OrderTransactionResponse TryFillOrderBook(IAccountService accountService) {
#if PERF_FINEST
      long start = Now;
#endif

      Order buyOrder = BuyBook.First;
      Order sellOrder = SellBook.First;
      List<Order> filledOrders = new List<Order>();
      List<Transaction> transactions = new List<Transaction>();
      // QUESTION: Execute the limit order at sell price benefits BUYER fat finger
      //           What to do if SELLER fat finger? Shouldn't that give the SELLER
      //           the higher BID price?
      decimal executionPrice = sellOrder.StrikePrice;
      int fillQuantity = Math.Min(buyOrder.OpenQuantity, sellOrder.OpenQuantity);
      if (!accountService.CanFillOrder(buyOrder)) {
        throw new NotImplementedException(); // TODO
      }
      if (!accountService.CanFillOrder(sellOrder)) {
        throw new NotImplementedException(); // TODO
      }

      buyOrder.OpenQuantity -= fillQuantity;
      sellOrder.OpenQuantity -= fillQuantity;
      transactions.Add(new Transaction(buyOrder, sellOrder, buyOrder.Ticker,
        fillQuantity, executionPrice));
      filledOrders.Add(SellBook.First);
      if (SellBook.First.IsFilled) {
        Order filledOrder = SellBook.First;
        SellBook.RemoveOrder(filledOrder);
        filledOrder.Complete();
      }
      filledOrders.Add(BuyBook.First);
      if (BuyBook.First.IsFilled) {
        Order filledOrder = BuyBook.First;
        BuyBook.RemoveOrder(filledOrder);
        filledOrder.Complete();
      }
#if PERF_FINEST
      logger.Information(String.Format("TryFillOrderBook executed in {0} milliseconds", ((Now - start) / TimeSpan.TicksPerMillisecond)));
#endif

      return new OrderTransactionResponse(Constants.Response.CREATED,
        new OrderTransaction(filledOrders, transactions));
    }

    private OrderResponse InsertOrder(IOrderContext context, Order order) {
      context.Orders.Add((OrderEntity)order);
      try {
        context.SaveChanges();
      } catch (Exception e) {
        logger.Warning("InsertOrder failed", e);
        return new OrderResponse(Constants.Response.INTERNAL_SERVER_ERROR,
          order, new Error("", "", e.Message));
      }
      return new OrderResponse(order);
    }

    private OrderTransactionResponse SaveOrderTransaction(IOrderContext context,
      OrderTransaction data) {
      data.Orders.ForEach(order => {
        context.Orders.Add((OrderEntity)order);
        context.Entry(order).State = EntityState.Modified;
      });
      data.Transactions.ForEach(transaction => {
        context.Transactions.Add((TransactionEntity)transaction);
      });

      try {
        context.SaveChanges();
      } catch (Exception e) {
        // QUESTION: What to do here in terms of transaction integrity?
        logger.Warning("SaveOrderTransaction failed", e);
        return new OrderTransactionResponse(Constants.Response.INTERNAL_SERVER_ERROR,
          data, new Error("", "", e.Message));
      }
      return new OrderTransactionResponse(data);
    }

#if DEBUG

#endif

#if DIAGNOSTICS
    private void DiagnosticsWriteDetails(OrderTransaction transaction) {
      StringBuilder sb = new StringBuilder();
      sb.Append("Order details:\n");
      transaction.Orders.ForEach(order => sb.Append("     " + order.ToString() + "\n"));
      sb.Append("Transaction details:\n");
      transaction.Transactions.ForEach(transaction => sb.Append("     " + transaction.ToString() + "\n"));
      Quote q = (Quote)Quote;
      sb.Append("\n  SPREAD: " + q.ToString());
      Level2 l2 = (Level2)Level2;
      sb.Append("\n  LEVEL 2:\n" + l2.ToString());
      logger.Information(sb.ToString());
    }
#endif

#if (PERF || PERF_FINE || PERF_FINEST)
    public OrderManager(String ticker) {
      BuyBook = new OrderBook(ticker, OrderAction.BUY, true);
      SellBook = new OrderBook(ticker, OrderAction.SELL, true);
    }

    public void TestPerfAddOrder(List<IOrderModel> buyOrders, List<IOrderModel> sellOrders,
      IDemoExchangeDbContextFactory<OrderContext> orderContextFactory) {
      int i = 0;
      using OrderContext buy = orderContextFactory.Create();
      foreach (IOrderModel request in buyOrders) {
        Order order = new Order(request);
        buy.Orders.Add((OrderEntity)order);
        BuyBook.TestPerfAddOrderNoSort(order);
        if ((i % 1000) == 0) {
          buy.SaveChanges();
        }
      }
      buy.SaveChanges();
      BuyBook.TestPerfSort();

      i = 0;
      using OrderContext sell = orderContextFactory.Create();
      foreach (IOrderModel request in sellOrders) {
        Order order = new Order(request);
        sell.Orders.Add((OrderEntity)order);
        SellBook.TestPerfAddOrderNoSort(order);
        if ((i % 1000) == 0) {
          sell.SaveChanges();
        }
      }
      sell.SaveChanges();
      SellBook.TestPerfSort();
    }

    public Tuple<int, int> TestPerfLoadOrderBook(IOrderContext context) {
      BuyBook.TestPerfLoadOrders(context);
      SellBook.TestPerfLoadOrders(context);

      return Count;
    }
#endif
  }

  public class OrderResponse : ResponseBase<IOrderModel>, IOrderResponse {
    public OrderResponse() { }
    public OrderResponse(IOrderModel data) : this(Constants.Response.OK, data) { }
    public OrderResponse(int code, IOrderModel data) : base(code, data) { }
    public OrderResponse(int code, IOrderModel data, Error error) : base(code, data, error) { }
    public OrderResponse(int code, IOrderModel data, List<IError> errors) : base(code,
      data, errors) { }
  }
}
