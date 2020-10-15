using System;
using DemoExchange.Api.Order;
using DemoExchange.Models;

namespace DemoExchange.OrderService.Models {
  public class OrderTransformer {
    public static Order ToOrder(OrderEntity entity) {
      return new Order {
        OrderId = entity.OrderId.ToString(),
          CreatedTimestamp = entity.CreatedTimestamp,
          AccountId = entity.AccountId,
          Status = entity.Status,
          Action = entity.Action,
          Ticker = entity.Ticker,
          Type = entity.Type,
          Quantity = entity.Quantity,
          OpenQuantity = entity.OpenQuantity,
          OrderPrice = Convert.ToDouble(entity.OrderPrice),
          StrikePrice = Convert.ToDouble(entity.StrikePrice),
          TimeInForce = entity.TimeInForce,
          ToBeCanceledTimestamp = entity.ToBeCanceledTimestamp,
          CanceledTimestamp = entity.CanceledTimestamp
      };
    }

    public static OrderBL ToOrderBL(OrderRequest request) {
      return new OrderBL(
        request.AccountId,
        request.Action,
        request.Ticker,
        request.Type,
        request.Quantity,
        Convert.ToDecimal(request.OrderPrice),
        request.TimeInForce
      );
    }

    public static OrderRequest ToOrderRequest(MarketOrderRequest request) {
      return new OrderRequest {
        AccountId = request.AccountId,
          Action = request.Action,
          Ticker = request.Ticker,
          Type = OrderType.Market,
          Quantity = request.Quantity,
          OrderPrice = 0,
          TimeInForce = OrderTimeInForce.Day
      };
    }
  }
}
