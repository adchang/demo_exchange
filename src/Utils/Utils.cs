using System;

namespace Utils {
  public class Utils {
    public static String GetValue(String value) {
      return GetValue(value, "");
    }

    public static String GetValue(String value, String nullEmptyValue) {
      return String.IsNullOrWhiteSpace(value) ? nullEmptyValue : value;
    }

    private Utils() {
      // Prevent instantiation
    }
  }
}
