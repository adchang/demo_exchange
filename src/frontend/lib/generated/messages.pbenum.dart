///
//  Generated code. Do not modify.
//  source: messages.proto
//
// @dart = 2.3
// ignore_for_file: camel_case_types,non_constant_identifier_names,library_prefixes,unused_import,unused_shown_name,return_of_invalid_type

// ignore_for_file: UNDEFINED_SHOWN_NAME,UNUSED_SHOWN_NAME
import 'dart:core' as $core;
import 'package:protobuf/protobuf.dart' as $pb;

class AccountStatus extends $pb.ProtobufEnum {
  static const AccountStatus ACCOUNT_PENDING_VERIFICATION = AccountStatus._(0, 'ACCOUNT_PENDING_VERIFICATION');
  static const AccountStatus ACCOUNT_PENDING_APPROVAL = AccountStatus._(1, 'ACCOUNT_PENDING_APPROVAL');
  static const AccountStatus ACCOUNT_ACTIVE = AccountStatus._(2, 'ACCOUNT_ACTIVE');
  static const AccountStatus ACCOUNT_SUSPENDED = AccountStatus._(3, 'ACCOUNT_SUSPENDED');
  static const AccountStatus ACCOUNT_CANCELLED = AccountStatus._(4, 'ACCOUNT_CANCELLED');
  static const AccountStatus ACCOUNT_DELETED = AccountStatus._(5, 'ACCOUNT_DELETED');

  static const $core.List<AccountStatus> values = <AccountStatus> [
    ACCOUNT_PENDING_VERIFICATION,
    ACCOUNT_PENDING_APPROVAL,
    ACCOUNT_ACTIVE,
    ACCOUNT_SUSPENDED,
    ACCOUNT_CANCELLED,
    ACCOUNT_DELETED,
  ];

  static final $core.Map<$core.int, AccountStatus> _byValue = $pb.ProtobufEnum.initByValue(values);
  static AccountStatus valueOf($core.int value) => _byValue[value];

  const AccountStatus._($core.int v, $core.String n) : super(v, n);
}

class AddressType extends $pb.ProtobufEnum {
  static const AddressType ADDRESS_HOME = AddressType._(0, 'ADDRESS_HOME');
  static const AddressType ADDRESS_WORK = AddressType._(1, 'ADDRESS_WORK');
  static const AddressType ADDRESS_MAILING = AddressType._(2, 'ADDRESS_MAILING');

  static const $core.List<AddressType> values = <AddressType> [
    ADDRESS_HOME,
    ADDRESS_WORK,
    ADDRESS_MAILING,
  ];

  static final $core.Map<$core.int, AddressType> _byValue = $pb.ProtobufEnum.initByValue(values);
  static AddressType valueOf($core.int value) => _byValue[value];

  const AddressType._($core.int v, $core.String n) : super(v, n);
}

class OrderStatus extends $pb.ProtobufEnum {
  static const OrderStatus ORDER_OPEN = OrderStatus._(0, 'ORDER_OPEN');
  static const OrderStatus ORDER_COMPLETED = OrderStatus._(1, 'ORDER_COMPLETED');
  static const OrderStatus ORDER_UPDATED = OrderStatus._(2, 'ORDER_UPDATED');
  static const OrderStatus ORDER_CANCELLED = OrderStatus._(3, 'ORDER_CANCELLED');
  static const OrderStatus ORDER_DELETED = OrderStatus._(4, 'ORDER_DELETED');

  static const $core.List<OrderStatus> values = <OrderStatus> [
    ORDER_OPEN,
    ORDER_COMPLETED,
    ORDER_UPDATED,
    ORDER_CANCELLED,
    ORDER_DELETED,
  ];

  static final $core.Map<$core.int, OrderStatus> _byValue = $pb.ProtobufEnum.initByValue(values);
  static OrderStatus valueOf($core.int value) => _byValue[value];

  const OrderStatus._($core.int v, $core.String n) : super(v, n);
}

class OrderAction extends $pb.ProtobufEnum {
  static const OrderAction ORDER_BUY = OrderAction._(0, 'ORDER_BUY');
  static const OrderAction ORDER_SELL = OrderAction._(1, 'ORDER_SELL');

  static const $core.List<OrderAction> values = <OrderAction> [
    ORDER_BUY,
    ORDER_SELL,
  ];

  static final $core.Map<$core.int, OrderAction> _byValue = $pb.ProtobufEnum.initByValue(values);
  static OrderAction valueOf($core.int value) => _byValue[value];

  const OrderAction._($core.int v, $core.String n) : super(v, n);
}

class OrderType extends $pb.ProtobufEnum {
  static const OrderType ORDER_MARKET = OrderType._(0, 'ORDER_MARKET');
  static const OrderType ORDER_LIMIT = OrderType._(1, 'ORDER_LIMIT');
  static const OrderType ORDER_STOP_MARKET = OrderType._(2, 'ORDER_STOP_MARKET');
  static const OrderType ORDER_STOP_LIMIT = OrderType._(3, 'ORDER_STOP_LIMIT');
  static const OrderType ORDER_TRAILING_STOP_MARKET = OrderType._(4, 'ORDER_TRAILING_STOP_MARKET');
  static const OrderType ORDER_TRAILING_STOP_LIMIT = OrderType._(5, 'ORDER_TRAILING_STOP_LIMIT');
  static const OrderType ORDER_FILL_OR_KILL = OrderType._(6, 'ORDER_FILL_OR_KILL');
  static const OrderType ORDER_IMMEDIATE_OR_CANCEL = OrderType._(7, 'ORDER_IMMEDIATE_OR_CANCEL');

  static const $core.List<OrderType> values = <OrderType> [
    ORDER_MARKET,
    ORDER_LIMIT,
    ORDER_STOP_MARKET,
    ORDER_STOP_LIMIT,
    ORDER_TRAILING_STOP_MARKET,
    ORDER_TRAILING_STOP_LIMIT,
    ORDER_FILL_OR_KILL,
    ORDER_IMMEDIATE_OR_CANCEL,
  ];

  static final $core.Map<$core.int, OrderType> _byValue = $pb.ProtobufEnum.initByValue(values);
  static OrderType valueOf($core.int value) => _byValue[value];

  const OrderType._($core.int v, $core.String n) : super(v, n);
}

class OrderTimeInForce extends $pb.ProtobufEnum {
  static const OrderTimeInForce ORDER_DAY = OrderTimeInForce._(0, 'ORDER_DAY');
  static const OrderTimeInForce ORDER_GOOD_TIL_CANCELED = OrderTimeInForce._(1, 'ORDER_GOOD_TIL_CANCELED');
  static const OrderTimeInForce ORDER_MARKET_CLOSE = OrderTimeInForce._(2, 'ORDER_MARKET_CLOSE');

  static const $core.List<OrderTimeInForce> values = <OrderTimeInForce> [
    ORDER_DAY,
    ORDER_GOOD_TIL_CANCELED,
    ORDER_MARKET_CLOSE,
  ];

  static final $core.Map<$core.int, OrderTimeInForce> _byValue = $pb.ProtobufEnum.initByValue(values);
  static OrderTimeInForce valueOf($core.int value) => _byValue[value];

  const OrderTimeInForce._($core.int v, $core.String n) : super(v, n);
}

class QuoteType extends $pb.ProtobufEnum {
  static const QuoteType QUOTE_BID = QuoteType._(0, 'QUOTE_BID');
  static const QuoteType QUOTE_ASK = QuoteType._(1, 'QUOTE_ASK');

  static const $core.List<QuoteType> values = <QuoteType> [
    QUOTE_BID,
    QUOTE_ASK,
  ];

  static final $core.Map<$core.int, QuoteType> _byValue = $pb.ProtobufEnum.initByValue(values);
  static QuoteType valueOf($core.int value) => _byValue[value];

  const QuoteType._($core.int v, $core.String n) : super(v, n);
}

