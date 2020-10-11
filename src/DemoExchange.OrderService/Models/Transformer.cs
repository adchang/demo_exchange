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
  }
}
