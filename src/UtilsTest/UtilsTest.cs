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
      /*long stopwatch = Time.StopwatchNow;
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
        Time.FromTicks(Time.MidnightSaturdayIct));*/
      long top10Second = 
        Time.ToTop10Second(new DateTime(2020, 10, 28, 8, 18, 9, DateTimeKind.Utc).Ticks);
      Assert.Equal(0, Time.FromTicks(top10Second).Second);
      top10Second = 
        Time.ToTop10Second(new DateTime(2020, 10, 28, 8, 18, 13, DateTimeKind.Utc).Ticks);
      Assert.Equal(10, Time.FromTicks(top10Second).Second);
      top10Second = 
        Time.ToTop10Second(new DateTime(2020, 10, 28, 8, 18, 26, DateTimeKind.Utc).Ticks);
      Assert.Equal(20, Time.FromTicks(top10Second).Second);
      top10Second = 
        Time.ToTop10Second(new DateTime(2020, 10, 28, 8, 18, 38, DateTimeKind.Utc).Ticks);
      Assert.Equal(30, Time.FromTicks(top10Second).Second);
      top10Second = 
        Time.ToTop10Second(new DateTime(2020, 10, 28, 8, 18, 44, DateTimeKind.Utc).Ticks);
      Assert.Equal(40, Time.FromTicks(top10Second).Second);
      top10Second = 
        Time.ToTop10Second(new DateTime(2020, 10, 28, 8, 18, 55, DateTimeKind.Utc).Ticks);
      Assert.Equal(50, Time.FromTicks(top10Second).Second);
    }
  }
}
