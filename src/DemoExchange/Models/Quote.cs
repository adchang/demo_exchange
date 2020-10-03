using System;
using System.Collections.Generic;
using System.Text;

namespace DemoExchange.Models {
  public class Quote {
    public decimal Bid { get; }
    public decimal Ask { get; }
    public decimal Last { get; }
    public int Volume { get; }

    public Quote(decimal bid, decimal ask) {
      Bid = bid;
      Ask = ask;
    }

    public Quote(decimal bid, decimal ask, decimal last, int volume) {
      Bid = bid;
      Ask = ask;
      Last = last;
      Volume = volume;
    }

    public override String ToString() {
      return QuoteType.BID + ": " + AppConstants.FormatPrice(Bid) + " / " +
        QuoteType.ASK + ": " + AppConstants.FormatPrice(Ask) +
        ((Last > 0 && Volume > 0) ?
          " Last: " + AppConstants.FormatPrice(Last) + " Volume: " + Volume :
          "");
    }
  }

  public class Level2Quote {
    public decimal Price { get; }
    public int Quantity { get; }

    public Level2Quote(decimal price, int quantity) {
      Price = price;
      Quantity = quantity;
    }

    public override String ToString() {
      return Quantity + " @ " + AppConstants.FormatPrice(Price);
    }
  }

  public class Level2 {
    public List<Level2Quote> Bid { get; }
    public List<Level2Quote> Ask { get; }

    public Level2(List<Level2Quote> bid, List<Level2Quote> ask) {
      Bid = bid;
      Ask = ask;
    }

    public override String ToString() {
      StringBuilder sb = new StringBuilder();
      sb.Append(QuoteType.BID + ":\n");
      foreach (Level2Quote quote in Bid) {
        sb.Append("  " + quote.ToString() + "\n");
      }
      sb.Append(QuoteType.ASK + ":\n");
      foreach (Level2Quote quote in Ask) {
        sb.Append("  " + quote.ToString() + "\n");
      }
      sb.Append('\n');

      return sb.ToString();
    }
  }

  public enum QuoteType {
    BID,
    ASK
  }
}
