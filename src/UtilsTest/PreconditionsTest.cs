using System;
using Xunit;

namespace Utils {
  public class PreconditionsTest {
    [Fact]
    [Trait("Category", "Unit")]
    public void CheckArgumentTest() {
      Exception e = Assert.Throws<ArgumentException>(() =>
        Preconditions.CheckArgument(1 == 2));
      Assert.Equal("Invalid argument",
        e.Message);
      e = Assert.Throws<ArgumentException>(() =>
        Preconditions.CheckArgument(1 == 2 && 1 == 1, "bad"));
      Assert.Equal("bad",
        e.Message);
      Preconditions.CheckArgument(1 == 2 || 1 == 1);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void CheckNotNullTest() {
      Exception e = Assert.Throws<ArgumentNullException>(() =>
        Preconditions.CheckNotNull(null));
      Assert.Equal("Value cannot be null. (Parameter 'param')",
        e.Message);
      e = Assert.Throws<ArgumentNullException>(() =>
        Preconditions.CheckNotNull(null, paramName: "blah"));
      Assert.Equal("Value cannot be null. (Parameter 'blah')",
        e.Message);
      Preconditions.CheckNotNull("");
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void CheckNotNullOrWhitespaceTest() {
      Exception e = Assert.Throws<ArgumentException>(() =>
        Preconditions.CheckNotNullOrWhitespace(null));
      Assert.Equal("param is null, empty, or contains only white-space characters",
        e.Message);
      e = Assert.Throws<ArgumentException>(() =>
        Preconditions.CheckNotNullOrWhitespace("", paramName: "blah"));
      Assert.Equal("blah is null, empty, or contains only white-space characters",
        e.Message);
      e = Assert.Throws<ArgumentException>(() =>
        Preconditions.CheckNotNullOrWhitespace("\n\n", paramName: "blah", message: "fail {0}"));
      Assert.Equal("fail blah",
        e.Message);
      e = Assert.Throws<ArgumentException>(() =>
        Preconditions.CheckNotNullOrWhitespace("\t\n", paramName: "blah", message: "fail"));
      Assert.Equal("fail",
        e.Message);
      Preconditions.CheckNotNullOrWhitespace("Hi!");
    }
  }
}
