using System;
using Xunit;

namespace DemoExchange {
  public class UtilsTest {
    [Fact]
    public void GetValTest() {
      Assert.Equal("", Utils.GetVal("\n\n"));
      Assert.Equal("value", Utils.GetVal("value"));
      Assert.Equal("default", Utils.GetVal("\n\n", "default"));
      Assert.Equal("value", Utils.GetVal("value", "default"));
    }
  }
}
