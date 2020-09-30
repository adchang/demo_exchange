using System;

namespace Utils {
  public class Utils {
    public static String GetVal(String data) {
      return GetVal(data, "");
    }

    public static String GetVal(String data, String nullEmptyValue) {
      return String.IsNullOrWhiteSpace(data) ? nullEmptyValue : data;
    }

    private Utils() {
      // Prevent instantiation
    }
  }
}
