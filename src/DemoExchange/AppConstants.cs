using System;

namespace DemoExchange {
  public class AppConstants {
    public const string CONFIG_FILE = "appsettings.json";

    public const string DB_DEMO_EXCHANGE = "DemoExchangeDb";

    public const String FMT_PRICE = DemoExchange.Interface.Constants.FORMAT_PRICE;
    public static readonly Func<decimal, String> FormatPrice = price => price.ToString(FMT_PRICE);

    public const int LEVEL_2_QUOTE_SIZE = 10;

    private AppConstants() {
      // Prevent instantiation
    }
  }
}
