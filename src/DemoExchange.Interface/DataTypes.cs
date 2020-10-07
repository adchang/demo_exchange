using System;
using System.Collections.Generic;
using System.Text;

namespace DemoExchange.Interface {
  public class Constants {
    public const String FORMAT_PRICE = "0.0000000000";

    private Constants() {
      // Prevent instantiation
    }

    public class Response {
      public const int OK = 200;
      public const int CREATED = 201;
      public const int ACCEPTED = 202;

      public const int BAD_REQUEST = 400;
      public const int UNAUTHORIZED = 401;
      public const int FORBIDDEN = 403;
      public const int NOT_FOUND = 404;

      public const int INTERNAL_SERVER_ERROR = 500;

      private Response() {
        // Prevent instantiation
      }
    }
  }

  public interface IQuote {
    decimal Bid { get; }
    decimal Ask { get; }
    decimal Last { get; }
    int Volume { get; }
  }

  public enum QuoteType {
    BID,
    ASK
  }

  public class Quote : IQuote {
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

    public new String ToString() {
      return QuoteType.BID + ": " + Bid.ToString(Constants.FORMAT_PRICE) + " / " +
        QuoteType.ASK + ": " + Ask.ToString(Constants.FORMAT_PRICE) +
        ((Last > 0 && Volume > 0) ?
          " Last: " + Last.ToString(Constants.FORMAT_PRICE) + " Volume: " + Volume :
          "");
    }
  }

  public interface ILevel2Quote {
    decimal Price { get; }
    int Quantity { get; }
  }

  public class Level2Quote : ILevel2Quote {
    public decimal Price { get; }
    public int Quantity { get; }

    public Level2Quote(decimal price, int quantity) {
      Price = price;
      Quantity = quantity;
    }

    public new String ToString() {
      return Quantity + " @ " + Price.ToString(Constants.FORMAT_PRICE);
    }
  }

  public interface ILevel2 {
    List<ILevel2Quote> Bid { get; }
    List<ILevel2Quote> Ask { get; }
  }

  public class Level2 : ILevel2 {
    public List<ILevel2Quote> Bid { get; }
    public List<ILevel2Quote> Ask { get; }

    public Level2(List<ILevel2Quote> bid, List<ILevel2Quote> ask) {
      Bid = bid;
      Ask = ask;
    }

    public new String ToString() {
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

  public interface IError {
    public String PropertyName { get; }
    public String Code { get; }
    public String Description { get; }
  }

  public class Error : IError {
    public String PropertyName { get; }
    public String Code { get; }
    public String Description { get; }

    public Error(String propertyName, String code, String description) {
      PropertyName = propertyName;
      Code = code;
      Description = description;
    }
  }

  public interface IResponse<T> {
    public int Code { get; }
    public T Data { get; }
    public bool HasErrors { get; }
    public List<IError> Errors { get; }
  }
}
