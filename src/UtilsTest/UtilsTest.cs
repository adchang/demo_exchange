using System;
using Xunit;

namespace Utils {
  public class UtilsTest {
    [Fact]
    [Trait("Category", "Unit")]
    public void GetValueTest() {
      Assert.Equal("", Utils.GetValue("\n\n"));
      Assert.Equal("value", Utils.GetValue("value"));
      Assert.Equal("default", Utils.GetValue("\n\n", "default"));
      Assert.Equal("value", Utils.GetValue("value", "default"));
    }
  }
}
