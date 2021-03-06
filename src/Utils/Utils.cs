using System;
using System.Collections.Generic;

namespace Utils {
  public class Strings {
    public static String GetValue(String value) {
      return GetValue(value, "");
    }

    public static String GetValue(String value, String nullEmptyValue) {
      return String.IsNullOrWhiteSpace(value) ? nullEmptyValue : value;
    }

    private Strings() {
      // Prevent instantiation
    }
  }

  public class Time {
    public const int OFFSET_ICT = 7;

    public static long Now {
      get { return DateTime.UtcNow.Ticks; }
    }

    public static long MidnightUtc {
      get { return DateTime.UtcNow.AddDays(1).Date.Ticks; }
    }

    public static long MidnightIct {
      get { return MidnightUtc - (TimeSpan.TicksPerHour * OFFSET_ICT); }
    }

    public static long MidnightSaturdayUtc {
      get {
        DateTime now = DateTime.UtcNow;
        return now.AddDays(DayOfWeek.Saturday - now.DayOfWeek).Date.Ticks;
      }
    }

    public static long MidnightSaturdayIct {
      get { return MidnightSaturdayUtc - (TimeSpan.TicksPerHour * OFFSET_ICT); }
    }

    public static long StopwatchNow {
      get { return System.Diagnostics.Stopwatch.GetTimestamp(); }
    }

    public static DateTime FromTicks(long ticks) {
      return new DateTime(ticks, DateTimeKind.Utc);
    }

    public static long ToTop10Second(long ticks) {
      DateTime current = FromTicks(ticks);
      int top = (int) (current.Second / 10) * 10;
      return new DateTime(current.Year, current.Month, current.Day, current.Hour, current.Minute, top, DateTimeKind.Utc).Ticks;
    }

    private Time() {
      // Prevent instantiation
    }
  }
}
