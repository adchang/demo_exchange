using System;
using System.Collections.Generic;

namespace DemoExchange.Interface {
  public interface IQuote {
    decimal Bid { get; }
    decimal Ask { get; }
    decimal Last { get; }
    int Volume { get; }
  }

  public interface ILevel2Quote {
    decimal Price { get; }
    int Quantity { get; }
  }

  public interface ILevel2 {
    List<ILevel2Quote> Bid { get; }
    List<ILevel2Quote> Ask { get; }
  }
}
