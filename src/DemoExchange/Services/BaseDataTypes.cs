using System;
using System.Collections.Generic;
using DemoExchange.Interface;

namespace DemoExchange {
  public class ResponseBase<T> : IResponse<T> {
    public int Code { get; set; }
    public T Data { get; set; }
    public bool HasErrors {
      get { return Errors != null && Errors.Count > 0; }
    }
    public List<IError> Errors { get; set; }

    public ResponseBase() { }

    public ResponseBase(T data) : this(200, data) { }

    public ResponseBase(int code, T data) {
      Code = code;
      Data = data;
    }

    public ResponseBase(int code, T data, IError error) : this(code, data) {
      Errors = new List<IError> {
        error
      };
    }

    public ResponseBase(int code, T data, List<IError> errors) : this(code, data) {
      Errors = errors;
    }
  }
}
