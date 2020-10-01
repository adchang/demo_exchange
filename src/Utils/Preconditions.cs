using System;

namespace Utils {
  public class Preconditions {
    public static void CheckArgument(Boolean expression,
      String message = "Invalid argument") {
      if (!expression) {
        throw new ArgumentException(message);
      }
    }

    public static void CheckNotNull(Object obj,
      String paramName = "param") {
      if (obj == null) {
        throw new ArgumentNullException(paramName);
      }
    }

    public static void CheckNotNullOrWhitespace(String value,
      String message = "{0} is null, empty, or contains only white-space characters",
      String paramName = "param") {
      if (String.IsNullOrWhiteSpace(value)) {
        throw new ArgumentException(String.Format(message, paramName));
      }
    }

    private Preconditions() {
      // Prevent instantiation
    }
  }
}
