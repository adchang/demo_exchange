using System;

namespace DemoExchange {
  public class AppConstants {
    public const String FMT_PRICE = "0.0000000000";
    public static readonly Func<decimal, String> FormatPrice = price => price.ToString(FMT_PRICE);

    public const int LEVEL_2_QUOTE_SIZE = 10;

    private AppConstants() {
      // Prevent instantiation
    }
  }
}
