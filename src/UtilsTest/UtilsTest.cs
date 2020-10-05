using System;
using Xunit;

namespace Utils {
  public class StringsTest {
    [Fact]
    [Trait("Category", "Unit")]
    public void GetValueTest() {
      Assert.Equal("", Strings.GetValue("\n\n"));
      Assert.Equal("value", Strings.GetValue("value"));
      Assert.Equal("default", Strings.GetValue("\n\n", "default"));
      Assert.Equal("value", Strings.GetValue("value", "default"));
    }
  }

  public class TimeTest {
    [Fact]
    [Trait("Category", "Unit")]
    public void TimeTests() {
      long now = Time.Now;
      long stopwatch = Time.StopwatchNow;
      Assert.NotEqual(now, stopwatch);
      DateTime nowDt = Time.FromTicks(now);
      Assert.Equal(DateTimeKind.Utc, nowDt.Kind);
      Assert.Equal(DateTime.UtcNow.Date, nowDt.Date);
      Assert.Equal(DateTime.UtcNow.Hour, nowDt.Hour);
      Assert.Equal(DateTime.UtcNow.Date.AddDays(1).Date, Time.FromTicks(Time.MidnightUtc));
      Assert.Equal(DayOfWeek.Saturday, Time.FromTicks(Time.MidnightSaturdayUtc).DayOfWeek);
      Assert.Equal(Time.FromTicks(Time.MidnightUtc).AddHours(-1 * Time.OFFSET_ICT),
        Time.FromTicks(Time.MidnightIct));
      Assert.Equal(Time.FromTicks(Time.MidnightSaturdayUtc).AddHours(-1 * Time.OFFSET_ICT),
        Time.FromTicks(Time.MidnightSaturdayIct));
    }
  }
}
