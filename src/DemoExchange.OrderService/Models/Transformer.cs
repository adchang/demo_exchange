using System;
using DemoExchange.Interface;
using DemoExchange.Models;

namespace DemoExchange.OrderService.Models {
  public class OrderTransformer {
    public static OrderModelView ToOrderModelView(OrderEntity entity) {
      return new OrderModelView {
        OrderId = entity.OrderId.ToString(),
          CreatedTimestamp = entity.CreatedTimestamp,
          AccountId = entity.AccountId,
          Status = entity.Status,
          Action = entity.Action,
          Ticker = entity.Ticker,
          Type = entity.Type,
          Quantity = entity.Quantity,
          OpenQuantity = entity.OpenQuantity,
          OrderPrice = entity.OrderPrice,
          StrikePrice = entity.StrikePrice,
          TimeInForce = entity.TimeInForce,
          ToBeCanceledTimestamp = entity.ToBeCanceledTimestamp,
          CanceledTimestamp = entity.CanceledTimestamp
      };
    }

    public static OrderModelView ToOrderModelView(NewOrderModelView view) {
      return new OrderModelView {
        AccountId = view.AccountId,
          Action = view.Action,
          Ticker = view.Ticker,
          Type = view.Type,
          Quantity = view.Quantity,
          OrderPrice = view.OrderPrice,
          TimeInForce = view.TimeInForce
      };
    }

    public static NewOrderModelView ToNewOrderModelView(NewMarketOrderModelView view) {
      return new NewOrderModelView {
        AccountId = view.AccountId,
          Action = view.Action,
          Ticker = view.Ticker,
          Type = OrderType.MARKET,
          Quantity = view.Quantity,
          OrderPrice = 0,
          TimeInForce = OrderTimeInForce.DAY
      };
    }
  }
}
