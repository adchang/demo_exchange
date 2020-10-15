using System;
using System.Collections.Generic;
using DemoExchange.Api;

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

  public interface IResponse<T, V> {
    public int Code { get; }
    public T Data { get; }
    public bool HasErrors { get; }
    public List<Error> Errors { get; }
    public V ToMessage();
  }
}
