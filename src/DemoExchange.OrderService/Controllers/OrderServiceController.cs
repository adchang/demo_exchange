//#define DIAGNOSTICS // HACK: Move to CD pipeline
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoExchange.Contexts;
using DemoExchange.Interface;
using DemoExchange.Models;
using DemoExchange.OrderService.Models;
using DemoExchange.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

// https://docs.microsoft.com/en-us/dotnet/architecture/microservices/multi-container-microservice-net-applications/data-driven-crud-microservice
namespace DemoExchange.OrderService.Controllers {
  [ApiController]
  public class OrderServiceController : ControllerBase {
    // TODO: hateoas
    private readonly ILogger logger = Log.Logger;

    private readonly List<String> tickers = new List<String> { "ERX", "SPY", "DIA", "QQQ", "UPRO", "SPXU", "OILU", "OILD" };

    private readonly IDemoExchangeDbContextFactory<OrderContext> orderContextFactory;
    private readonly IOrderInternalService service;

    public OrderServiceController(IDemoExchangeDbContextFactory<OrderContext> orderContextFactory,
      IOrderInternalService service) {
      this.orderContextFactory = orderContextFactory;
      this.service = service;
    }

    // TODO scopeRequiredByApi

    [HttpGet(Routes.Orders.DEFAULT_ORDERS + "/{orderId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<OrderModelView>> GetOrder(String orderId) {
      logger.Information("GetOrder: " + orderId);

      // TODO: Authorization: User is admin or creator of the order

      using OrderContext context = orderContextFactory.Create();
      OrderEntity entity = null;
      try {
        entity = await context.Orders.FindAsync(Guid.Parse(orderId));
      } catch (Exception e) {
        logger.Warning("GetOrder: " + orderId + " Exception: " + e.Message);
        return BadRequest();
      }
      if (entity == null) {
        logger.Warning("GetOrder: " + orderId + " not found");
        return BadRequest();
      }

      return Ok(OrderTransformer.ToOrderModelView(entity));
    }

    [HttpPost(Routes.Orders.DEFAULT_ORDERS)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IOrderResponse> SubmitOrder(NewOrderModelView request) {
      logger.Information("SubmitOrder");
#if DIAGNOSTICS
      logger.Verbose("SubmitOrder: " + request.ToString());
#endif
      IOrderResponse response = null;
      try {
        response = service.SubmitOrder(OrderTransformer.ToOrderModelView(request));
      } catch (Exception e) {
        logger.Error("SubmitOrder Server Error: " + request.ToString() +
          " Exception: " + e.Message +
          (response == null ? "" : " Response Exception: " + response.Errors[0].Description));
        return StatusCode(StatusCodes.Status500InternalServerError);
      }

#if DIAGNOSTICS
      logger.Verbose("SubmitOrder response: " + response.ToString());
#endif
      logger.Information("SubmitMarketOrder done");

      if (response.HasErrors) {
        if (response.Code == Constants.Response.UNAUTHORIZED) {
          logger.Warning("SubmitOrder Unauthorized: " + request.ToString() +
            " Exception: " + response.Errors[0].Description);
          return Unauthorized();
        } else if (response.Code == Constants.Response.INTERNAL_SERVER_ERROR) {
          logger.Error("SubmitOrder Server Error: " + request.ToString() +
            " Exception: " + response.Errors[0].Description);
          return StatusCode(StatusCodes.Status500InternalServerError);
        } else {
          return BadRequest(response);
        }
      }

      return Ok(response);
    }

    [HttpPost(Routes.Orders.DEFAULT_MARKET_ORDER)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IOrderResponse> SubmitMarketOrder(NewMarketOrderModelView request) {
      logger.Information("SubmitMarketOrder");
      return SubmitOrder(OrderTransformer.ToNewOrderModelView(request));
    }

    [HttpGet(Routes.Orders.DEFAULT_ORDERS + "/start")]
    public void StartOrderService() {
      logger.Information("StartOrderService");

      // TODO: Authorization: User is admin

      tickers.ForEach(ticker => {
        logger.Information("StartOrderService adding " + ticker);
        service.AddTicker(ticker);
        logger.Information("StartOrderService adding " + ticker + " done");
      });
    }
  }
}
