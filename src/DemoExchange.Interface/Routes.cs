using System;

namespace DemoExchange.Interface {
  public class Routes {
    public class Orders {
      public const String DEFAULT_VERSION = "v1/";

      public const String ORDERS = "orders";
      public const String DEFAULT_ORDERS = DEFAULT_VERSION + ORDERS;

      public const String MARKET_ORDER = "market-order";
      public const String DEFAULT_MARKET_ORDER = DEFAULT_VERSION + MARKET_ORDER;

      private Orders() {
        // Prevent instantiation
      }
    }

    private Routes() {
      // Prevent instantiation
    }
  }
}
