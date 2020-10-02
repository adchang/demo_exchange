using System;

namespace DemoExchange.Interface {
  /// <summary>
  /// Is a model.
  /// </summary>
  public interface IModel {
    public bool IsValid();
  }

  /// <summary>
  /// Model for an order.
  /// </summary>
  public interface IModelOrder : IModel { }

  /// <summary>
  /// Validators for models.
  /// </summary>
  public interface IValidator<IModel> {
    public bool IsValid();
  }
}
