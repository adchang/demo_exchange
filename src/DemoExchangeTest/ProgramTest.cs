using System;
using Xunit;

namespace DemoExchange {
  public class ProgramTest {
    [Fact]
    public void DoSomethingTest() {
      Assert.False(Program.DoSomething());
    }
  }
}
