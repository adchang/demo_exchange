using System;
using System.Collections.Generic;
using DemoExchange.Api;
using DemoExchange.Interface;

namespace DemoExchange {
  public class ResponseBase<T, V> : IResponse<T, V> {
    public int Code { get; set; }
    public T Data { get; set; }
    public bool HasErrors {
      get { return Errors != null && Errors.Count > 0; }
    }
    public List<Error> Errors { get; set; }

    public ResponseBase() { }

    public ResponseBase(T data) : this(Constants.Response.OK, data) { }

    public ResponseBase(int code, T data) {
      Code = code;
      Data = data;
    }

    public ResponseBase(int code, T data, Error error) : this(code, data) {
      Errors = new List<Error> {
        error
      };
    }

    public ResponseBase(int code, T data, List<Error> errors) : this(code, data) {
      Errors = errors;
    }

    public virtual V ToMessage() {
      return default;
    }
  }
}
