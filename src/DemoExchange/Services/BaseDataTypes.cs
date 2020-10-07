using System;
using System.Collections.Generic;
using DemoExchange.Interface;

namespace DemoExchange {
  public class BaseResponse<T> : IResponse<T> {
    public int Code { get; set; }
    public T Data { get; set; }
    public bool HasErrors {
      get { return Errors != null && Errors.Count > 0; }
    }
    public List<IError> Errors { get; set; }

    public BaseResponse() { }

    public BaseResponse(T data) : this(200, data) { }

    public BaseResponse(int code, T data) {
      Code = code;
      Data = data;
    }

    public BaseResponse(int code, T data, IError error) : this(code, data) {
      Errors = new List<IError> {
        error
      };
    }

    public BaseResponse(int code, T data, List<IError> errors) : this(code, data) {
      Errors = errors;
    }
  }
}
