using System;
using Xunit;

namespace DemoExchange {
  public class ProgramTest {
    [Fact]
    public void Test1() {
      Assert.False(Program.DoSomething());
    }
  }
}
