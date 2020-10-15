using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DemoExchange.Api;
using DemoExchange.Api.Order;
using DemoExchange.Contexts;
using DemoExchange.Interface;
using DemoExchange.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using static Utils.Preconditions;
using static Utils.Time;

namespace DemoExchange.Services {
#if (PERF || PERF_FINE || PERF_FINEST)
    public interface IOrderTestPerfService : IOrderService {
      public void TestPerfAddOrder(String ticker,
        List<IOrderModel> buyOrders, List<IOrderModel> sellOrders);
      public Tuple<int, int> TestPerfLoadOrderBook(String ticker);
    }
#endif

#if (PERF || PERF_FINE || PERF_FINEST)
    public class OrderService : IOrderTestPerfService {
#else
      public class OrderService : IOrderService {
#endif
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

        public AddTickerResponse AddTicker(String ticker) {
          // TODO: AddTicker preconditions & tests
          lock(managers) {
            using OrderContext context = orderContextFactory.Create();
            OrderManager manager = new OrderManager(context, ticker);
            managers.Add(ticker, manager);

            AddTickerResponse response = new AddTickerResponse();
            response.BuyOrderCount = manager.Count.Item1;
            response.SellOrderCount = manager.Count.Item2;

            return response;
          }
        }

        public IResponse<IOrderModel, OrderResponse> SubmitOrder(IOrderModel request) {
          // TODO: CheckMarketIsOpen(); This is not correct; should still be able to submit 
          //       orders if market is not open; Just can't trade.
          // TODO: Add tests
          CheckNotNull(request, paramName : nameof(request));

          OrderManager manager = managers[request.Ticker];
          lock(manager) {
            using OrderContext context = orderContextFactory.Create();
            return manager.SubmitOrder(context, accountService, (OrderBL)request);
          }
        }

        public IResponse<IOrderModel, OrderResponse> CancelOrder(String orderId) {
          throw new NotImplementedException();
        }

        public Quote GetQuote(String ticker) {
          // TODO: GetQuote preconditions & tests
          return managers[ticker].Quote;
        }

        public Level2 GetLevel2(String ticker) {
          // TODO: GetLevel2 preconditions & tests
          return managers[ticker].Level2;
        }

        /*private static OrderResponse NewOrder(IOrderModel request) {
          try {
            return new OrderResponse(new Order(request));
          } catch (Exception e) {
            return new OrderResponse(Constants.Response.BAD_REQUEST, request,
              new Error("", "", e.Message));
          }
        }*/

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
          get {
            return new Quote {
              Bid = Convert.ToDouble(BuyBook.First.StrikePrice),
                Ask = Convert.ToDouble(SellBook.First.StrikePrice)
            };
          }
        }
        public Level2 Level2 {
          get {
            Level2 level2 = new Level2();
            BuyBook.Level2.ForEach(level2Quote => level2.Bids.Add(level2Quote));
            SellBook.Level2.ForEach(level2Quote => level2.Asks.Add(level2Quote));

            return level2;
          }
        }

        public OrderManager(IOrderContext context, String ticker) {
          BuyBook = new OrderBook(context, ticker, OrderAction.Buy);
          SellBook = new OrderBook(context, ticker, OrderAction.Sell);
        }

        // QUESTION: Consider making async: https://docs.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/task-based-asynchronous-pattern-tap
        public OrderResponseBL SubmitOrder(IOrderContext context,
          IAccountService accountService, OrderBL order) {
#if (PERF || PERF_FINE || PERF_FINEST)
          long start = Now;
#endif
          OrderResponseBL insertResponse = InsertOrder(context, order);
          if (insertResponse.HasErrors) {
            return insertResponse;
          }

          OrderBook book = order.IsMarketOrder ?
            order.IsBuyOrder ? SellBook : BuyBook :
            order.IsBuyOrder ? BuyBook : SellBook;
          if (order.IsMarketOrder) {
            // QUESTION: Could potentially optimize to not do an initial save of a Market order
            //           but can we allow order entry during market close hours? What's MarketOnOpen?
            OrderTransactionResponseBL fillResponse = FillMarketOrder(accountService, order, book);
            if (fillResponse.HasErrors) {
              return new OrderResponseBL(fillResponse.Code, order, fillResponse.Errors);
            }
            fillResponse = SaveOrderTransaction(context, fillResponse.Data);
            if (fillResponse.HasErrors) {
              return new OrderResponseBL(fillResponse.Code, order, fillResponse.Errors);
            }
#if DIAGNOSTICS
            DiagnosticsWriteDetails(fillResponse.Data);
#endif
#if (PERF || PERF_FINE || PERF_FINEST)
            logger.Information(String.Format("SubmitOrder: Market order executed in {0} milliseconds", ((Now - start) / TimeSpan.TicksPerMillisecond)));
#endif
            return new OrderResponseBL(order);
          }

          book.AddOrder(order); // HACK: Technically, this should return back as Submitted, and the TryFillOrderBook is a separate process that runs continuously
          bool done = false;
          while (!done) {
            if (BuyBook.IsEmpty || SellBook.IsEmpty)throw new SystemException("Order book is empty"); // TODO: Handle order book is empty
            if (BuyBook.First.StrikePrice >= SellBook.First.StrikePrice) {
              OrderTransactionResponseBL fillResponse = TryFillOrderBook(accountService);
              if (fillResponse.HasErrors) {
                return new OrderResponseBL(fillResponse.Code, order, fillResponse.Errors);
              }
              fillResponse = SaveOrderTransaction(context, fillResponse.Data);
              if (fillResponse.HasErrors) {
                return new OrderResponseBL(fillResponse.Code, order, fillResponse.Errors);
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

        private static OrderTransactionResponseBL FillMarketOrder(IAccountService accountService,
          OrderBL order, OrderBook book) {
#if PERF_FINEST
          long start = Now;
#endif
          CheckArgument(!order.Action.Equals(book.Type), "Error: Wrong book");

          bool done = false;
          List<OrderBL> filledOrders = new List<OrderBL>();
          List<Transaction> transactions = new List<Transaction>();
          while (!done) {
            if (book.IsEmpty)throw new SystemException("Order book is empty"); // TODO: Handle order book is empty
            OrderBL buyOrder = order.IsBuyOrder ? order : book.First;
            OrderBL sellOrder = order.IsSellOrder ? order : book.First;
            decimal executionPrice = order.IsBuyOrder ? sellOrder.StrikePrice : buyOrder.StrikePrice;
            int fillQuantity = Math.Min(buyOrder.OpenQuantity, sellOrder.OpenQuantity);
            // QUESTION: Many issues here, partial fill, etc
            if (!accountService.CanFillOrder(buyOrder.ToMessage())) {
              throw new NotImplementedException(); // TODO
            }
            if (!accountService.CanFillOrder(sellOrder.ToMessage())) {
              throw new NotImplementedException(); // TODO
            }

            buyOrder.OpenQuantity -= fillQuantity;
            sellOrder.OpenQuantity -= fillQuantity;
            transactions.Add(new Transaction(buyOrder, sellOrder, buyOrder.Ticker,
              fillQuantity, executionPrice));
            filledOrders.Add(book.First);
            if (book.First.IsFilled) {
              OrderBL filledOrder = book.First;
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

          return new OrderTransactionResponseBL(Constants.Response.CREATED,
            new OrderTransactionBL(filledOrders, transactions));
        }

        private OrderTransactionResponseBL TryFillOrderBook(IAccountService accountService) {
#if PERF_FINEST
          long start = Now;
#endif

          OrderBL buyOrder = BuyBook.First;
          OrderBL sellOrder = SellBook.First;
          List<OrderBL> filledOrders = new List<OrderBL>();
          List<Transaction> transactions = new List<Transaction>();
          // QUESTION: Execute the limit order at sell price benefits BUYER fat finger
          //           What to do if SELLER fat finger? Shouldn't that give the SELLER
          //           the higher BID price?
          decimal executionPrice = sellOrder.StrikePrice;
          int fillQuantity = Math.Min(buyOrder.OpenQuantity, sellOrder.OpenQuantity);
          if (!accountService.CanFillOrder(buyOrder.ToMessage())) {
            throw new NotImplementedException(); // TODO
          }
          if (!accountService.CanFillOrder(sellOrder.ToMessage())) {
            throw new NotImplementedException(); // TODO
          }

          buyOrder.OpenQuantity -= fillQuantity;
          sellOrder.OpenQuantity -= fillQuantity;
          transactions.Add(new Transaction(buyOrder, sellOrder, buyOrder.Ticker,
            fillQuantity, executionPrice));
          filledOrders.Add(SellBook.First);
          if (SellBook.First.IsFilled) {
            OrderBL filledOrder = SellBook.First;
            SellBook.RemoveOrder(filledOrder);
            filledOrder.Complete();
          }
          filledOrders.Add(BuyBook.First);
          if (BuyBook.First.IsFilled) {
            OrderBL filledOrder = BuyBook.First;
            BuyBook.RemoveOrder(filledOrder);
            filledOrder.Complete();
          }
#if PERF_FINEST
          logger.Information(String.Format("TryFillOrderBook executed in {0} milliseconds", ((Now - start) / TimeSpan.TicksPerMillisecond)));
#endif

          return new OrderTransactionResponseBL(Constants.Response.CREATED,
            new OrderTransactionBL(filledOrders, transactions));
        }

        private OrderResponseBL InsertOrder(IOrderContext context, OrderBL order) {
          context.Orders.Add((OrderEntity)order);
          try {
            context.SaveChanges();
          } catch (Exception e) {
            logger.Warning("InsertOrder failed", e);
            return new OrderResponseBL(Constants.Response.INTERNAL_SERVER_ERROR,
              order, new Error {
                Description = e.Message
              });
          }

          return new OrderResponseBL(order);
        }

        private OrderTransactionResponseBL SaveOrderTransaction(IOrderContext context,
          OrderTransactionBL data) {
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
            return new OrderTransactionResponseBL(Constants.Response.INTERNAL_SERVER_ERROR,
              data, new Error {
                Description = e.Message
              });
          }

          return new OrderTransactionResponseBL(data);
        }

        public static String QuoteToString(Quote quote) {
          return QuoteType.Bid + ": " + quote.Bid.ToString(Constants.FORMAT_PRICE) + " / " +
            QuoteType.Ask + ": " + quote.Ask.ToString(Constants.FORMAT_PRICE) +
            ((quote.Last > 0 && quote.Volume > 0) ?
              " Last: " + quote.Last.ToString(Constants.FORMAT_PRICE) + " Volume: " + quote.Volume :
              "");
        }

        public static String Level2QuoteToString(Level2Quote quote) {
          return quote.Quantity + " @ " + quote.Price.ToString(Constants.FORMAT_PRICE);
        }

        public static String Level2ToString(Level2 level2) {
          StringBuilder sb = new StringBuilder();
          sb.Append(QuoteType.Bid + ":\n");
          foreach (Level2Quote quote in level2.Bids) {
            sb.Append("  " + Level2QuoteToString(quote) + "\n");
          }
          sb.Append(QuoteType.Ask + ":\n");
          foreach (Level2Quote quote in level2.Asks) {
            sb.Append("  " + Level2QuoteToString(quote) + "\n");
          }
          sb.Append('\n');

          return sb.ToString();
        }
#if DEBUG

#endif

#if DIAGNOSTICS
        private void DiagnosticsWriteDetails(OrderTransactionBL transaction) {
          StringBuilder sb = new StringBuilder();
          sb.Append("Order details:\n");
          transaction.Orders.ForEach(order => sb.Append("     " + order.ToString() + "\n"));
          sb.Append("Transaction details:\n");
          transaction.Transactions.ForEach(transaction => sb.Append("     " + transaction.ToString() + "\n"));
          sb.Append("\n  SPREAD: " + QuoteToString(Quote));
          sb.Append("\n  LEVEL 2:\n" + Level2ToString(Level2));
          logger.Verbose(sb.ToString());
        }
#endif

#if (PERF || PERF_FINE || PERF_FINEST)
        public OrderManager(String ticker) {
          BuyBook = new OrderBook(ticker, OrderAction.Buy, true);
          SellBook = new OrderBook(ticker, OrderAction.Sell, true);
        }

        public void TestPerfAddOrder(List<IOrderModel> buyOrders, List<IOrderModel> sellOrders,
          IDemoExchangeDbContextFactory<OrderContext> orderContextFactory) {
          int i = 0;
          using OrderContext buy = orderContextFactory.Create();
          foreach (IOrderModel request in buyOrders) {
            OrderBL order = new OrderBL(request.AccountId, request.Action, request.Ticker, request.Type, request.Quantity, request.OrderPrice, request.TimeInForce);
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
            OrderBL order = new OrderBL(request.AccountId, request.Action, request.Ticker, request.Type, request.Quantity, request.OrderPrice, request.TimeInForce);
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

      public class OrderResponseBL : ResponseBase<IOrderModel, OrderResponse> {
        public OrderResponseBL() { }
        public OrderResponseBL(IOrderModel data) : base(data) { }
        public OrderResponseBL(int code, IOrderModel data) : base(code, data) { }
        public OrderResponseBL(int code, IOrderModel data, Error error) : base(code, data, error) { }
        public OrderResponseBL(int code, IOrderModel data, List<Error> errors) : base(code,
          data, errors) { }

        public override OrderResponse ToMessage() {
          OrderResponse response = new OrderResponse {
            Code = this.Code,
            Data = this.Data.ToMessage()
          };
          if (this.HasErrors) {
            this.Errors.ForEach(error => response.Errors.Add(error));
          }

          return response;
        }
      }
    }
