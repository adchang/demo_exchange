///
//  Generated code. Do not modify.
//  source: messages.proto
//
// @dart = 2.3
// ignore_for_file: camel_case_types,non_constant_identifier_names,library_prefixes,unused_import,unused_shown_name,return_of_invalid_type

const AccountStatus$json = const {
  '1': 'AccountStatus',
  '2': const [
    const {'1': 'ACCOUNT_PENDING_VERIFICATION', '2': 0},
    const {'1': 'ACCOUNT_PENDING_APPROVAL', '2': 1},
    const {'1': 'ACCOUNT_ACTIVE', '2': 2},
    const {'1': 'ACCOUNT_SUSPENDED', '2': 3},
    const {'1': 'ACCOUNT_CANCELLED', '2': 4},
    const {'1': 'ACCOUNT_DELETED', '2': 5},
  ],
};

const AddressType$json = const {
  '1': 'AddressType',
  '2': const [
    const {'1': 'ADDRESS_HOME', '2': 0},
    const {'1': 'ADDRESS_WORK', '2': 1},
    const {'1': 'ADDRESS_MAILING', '2': 2},
  ],
};

const OrderStatus$json = const {
  '1': 'OrderStatus',
  '2': const [
    const {'1': 'ORDER_OPEN', '2': 0},
    const {'1': 'ORDER_COMPLETED', '2': 1},
    const {'1': 'ORDER_UPDATED', '2': 2},
    const {'1': 'ORDER_CANCELLED', '2': 3},
    const {'1': 'ORDER_DELETED', '2': 4},
  ],
};

const OrderAction$json = const {
  '1': 'OrderAction',
  '2': const [
    const {'1': 'ORDER_BUY', '2': 0},
    const {'1': 'ORDER_SELL', '2': 1},
  ],
};

const OrderType$json = const {
  '1': 'OrderType',
  '2': const [
    const {'1': 'ORDER_MARKET', '2': 0},
    const {'1': 'ORDER_LIMIT', '2': 1},
    const {'1': 'ORDER_STOP_MARKET', '2': 2},
    const {'1': 'ORDER_STOP_LIMIT', '2': 3},
    const {'1': 'ORDER_TRAILING_STOP_MARKET', '2': 4},
    const {'1': 'ORDER_TRAILING_STOP_LIMIT', '2': 5},
    const {'1': 'ORDER_FILL_OR_KILL', '2': 6},
    const {'1': 'ORDER_IMMEDIATE_OR_CANCEL', '2': 7},
  ],
};

const OrderTimeInForce$json = const {
  '1': 'OrderTimeInForce',
  '2': const [
    const {'1': 'ORDER_DAY', '2': 0},
    const {'1': 'ORDER_GOOD_TIL_CANCELED', '2': 1},
    const {'1': 'ORDER_MARKET_CLOSE', '2': 2},
  ],
};

const QuoteType$json = const {
  '1': 'QuoteType',
  '2': const [
    const {'1': 'QUOTE_BID', '2': 0},
    const {'1': 'QUOTE_ASK', '2': 1},
  ],
};

const Empty$json = const {
  '1': 'Empty',
};

const StringMessage$json = const {
  '1': 'StringMessage',
  '2': const [
    const {'1': 'value', '3': 1, '4': 1, '5': 9, '10': 'value'},
  ],
};

const IntMessage$json = const {
  '1': 'IntMessage',
  '2': const [
    const {'1': 'value', '3': 1, '4': 1, '5': 5, '10': 'value'},
  ],
};

const LongMessage$json = const {
  '1': 'LongMessage',
  '2': const [
    const {'1': 'value', '3': 1, '4': 1, '5': 3, '10': 'value'},
  ],
};

const DoubleMessage$json = const {
  '1': 'DoubleMessage',
  '2': const [
    const {'1': 'value', '3': 1, '4': 1, '5': 1, '10': 'value'},
  ],
};

const BoolMessage$json = const {
  '1': 'BoolMessage',
  '2': const [
    const {'1': 'value', '3': 1, '4': 1, '5': 8, '10': 'value'},
  ],
};

const Error$json = const {
  '1': 'Error',
  '2': const [
    const {'1': 'property_name', '3': 1, '4': 1, '5': 9, '10': 'propertyName'},
    const {'1': 'code', '3': 2, '4': 1, '5': 9, '10': 'code'},
    const {'1': 'description', '3': 3, '4': 1, '5': 9, '10': 'description'},
  ],
};

const User$json = const {
  '1': 'User',
  '2': const [
    const {'1': 'user_id', '3': 1, '4': 1, '5': 9, '10': 'userId'},
    const {'1': 'created_timestamp', '3': 2, '4': 1, '5': 3, '10': 'createdTimestamp'},
    const {'1': 'last_updated_timestamp', '3': 3, '4': 1, '5': 3, '10': 'lastUpdatedTimestamp'},
    const {'1': 'first_name', '3': 5, '4': 1, '5': 9, '10': 'firstName'},
    const {'1': 'middle_name', '3': 6, '4': 1, '5': 9, '10': 'middleName'},
    const {'1': 'last_name', '3': 7, '4': 1, '5': 9, '10': 'lastName'},
  ],
};

const Account$json = const {
  '1': 'Account',
  '2': const [
    const {'1': 'account_id', '3': 1, '4': 1, '5': 9, '10': 'accountId'},
    const {'1': 'created_timestamp', '3': 2, '4': 1, '5': 3, '10': 'createdTimestamp'},
    const {'1': 'last_updated_timestamp', '3': 3, '4': 1, '5': 3, '10': 'lastUpdatedTimestamp'},
    const {'1': 'status', '3': 4, '4': 1, '5': 14, '6': '.erx.api.AccountStatus', '10': 'status'},
    const {'1': 'first_name', '3': 5, '4': 1, '5': 9, '10': 'firstName'},
    const {'1': 'middle_name', '3': 6, '4': 1, '5': 9, '10': 'middleName'},
    const {'1': 'last_name', '3': 7, '4': 1, '5': 9, '10': 'lastName'},
    const {'1': 'addresses', '3': 8, '4': 3, '5': 11, '6': '.erx.api.Address', '10': 'addresses'},
  ],
};

const AccountUser$json = const {
  '1': 'AccountUser',
  '2': const [
    const {'1': 'account_id', '3': 1, '4': 1, '5': 9, '10': 'accountId'},
    const {'1': 'user_id', '3': 2, '4': 1, '5': 9, '10': 'userId'},
  ],
};

const Address$json = const {
  '1': 'Address',
  '2': const [
    const {'1': 'address_id', '3': 1, '4': 1, '5': 9, '10': 'addressId'},
    const {'1': 'account_id', '3': 2, '4': 1, '5': 9, '10': 'accountId'},
    const {'1': 'created_timestamp', '3': 3, '4': 1, '5': 3, '10': 'createdTimestamp'},
    const {'1': 'last_updated_timestamp', '3': 4, '4': 1, '5': 3, '10': 'lastUpdatedTimestamp'},
    const {'1': 'type', '3': 5, '4': 1, '5': 14, '6': '.erx.api.AddressType', '10': 'type'},
    const {'1': 'line_1', '3': 6, '4': 1, '5': 9, '10': 'line1'},
    const {'1': 'line_2', '3': 7, '4': 1, '5': 9, '10': 'line2'},
    const {'1': 'subdistrict', '3': 8, '4': 1, '5': 9, '10': 'subdistrict'},
    const {'1': 'district', '3': 9, '4': 1, '5': 9, '10': 'district'},
    const {'1': 'city', '3': 10, '4': 1, '5': 9, '10': 'city'},
    const {'1': 'province', '3': 11, '4': 1, '5': 9, '10': 'province'},
    const {'1': 'postal_code', '3': 12, '4': 1, '5': 9, '10': 'postalCode'},
    const {'1': 'country', '3': 13, '4': 1, '5': 9, '10': 'country'},
  ],
};

const AccountList$json = const {
  '1': 'AccountList',
  '2': const [
    const {'1': 'accounts', '3': 1, '4': 3, '5': 11, '6': '.erx.api.Account', '10': 'accounts'},
  ],
};

const AccountRequest$json = const {
  '1': 'AccountRequest',
  '2': const [
    const {'1': 'first_name', '3': 1, '4': 1, '5': 9, '10': 'firstName'},
    const {'1': 'middle_name', '3': 2, '4': 1, '5': 9, '10': 'middleName'},
    const {'1': 'last_name', '3': 3, '4': 1, '5': 9, '10': 'lastName'},
  ],
};

const AccountResponse$json = const {
  '1': 'AccountResponse',
  '2': const [
    const {'1': 'code', '3': 1, '4': 1, '5': 5, '10': 'code'},
    const {'1': 'data', '3': 2, '4': 1, '5': 11, '6': '.erx.api.Account', '10': 'data'},
    const {'1': 'errors', '3': 3, '4': 3, '5': 11, '6': '.erx.api.Error', '10': 'errors'},
  ],
};

const AddressRequest$json = const {
  '1': 'AddressRequest',
  '2': const [
    const {'1': 'account_id', '3': 1, '4': 1, '5': 9, '10': 'accountId'},
    const {'1': 'type', '3': 2, '4': 1, '5': 14, '6': '.erx.api.AddressType', '10': 'type'},
    const {'1': 'line_1', '3': 3, '4': 1, '5': 9, '10': 'line1'},
    const {'1': 'line_2', '3': 4, '4': 1, '5': 9, '10': 'line2'},
    const {'1': 'subdistrict', '3': 5, '4': 1, '5': 9, '10': 'subdistrict'},
    const {'1': 'district', '3': 6, '4': 1, '5': 9, '10': 'district'},
    const {'1': 'city', '3': 7, '4': 1, '5': 9, '10': 'city'},
    const {'1': 'province', '3': 8, '4': 1, '5': 9, '10': 'province'},
    const {'1': 'postal_code', '3': 9, '4': 1, '5': 9, '10': 'postalCode'},
    const {'1': 'country', '3': 10, '4': 1, '5': 9, '10': 'country'},
  ],
};

const AddressResponse$json = const {
  '1': 'AddressResponse',
  '2': const [
    const {'1': 'code', '3': 1, '4': 1, '5': 5, '10': 'code'},
    const {'1': 'data', '3': 2, '4': 1, '5': 11, '6': '.erx.api.Address', '10': 'data'},
    const {'1': 'errors', '3': 3, '4': 3, '5': 11, '6': '.erx.api.Error', '10': 'errors'},
  ],
};

const CanFillOrderRequest$json = const {
  '1': 'CanFillOrderRequest',
  '2': const [
    const {'1': 'order', '3': 1, '4': 1, '5': 11, '6': '.erx.api.Order', '10': 'order'},
    const {'1': 'fill_quantity', '3': 2, '4': 1, '5': 5, '10': 'fillQuantity'},
  ],
};

const CanFillOrderResponse$json = const {
  '1': 'CanFillOrderResponse',
  '2': const [
    const {'1': 'value', '3': 1, '4': 1, '5': 8, '10': 'value'},
  ],
};

const Order$json = const {
  '1': 'Order',
  '2': const [
    const {'1': 'order_id', '3': 1, '4': 1, '5': 9, '10': 'orderId'},
    const {'1': 'created_timestamp', '3': 2, '4': 1, '5': 3, '10': 'createdTimestamp'},
    const {'1': 'account_id', '3': 3, '4': 1, '5': 9, '10': 'accountId'},
    const {'1': 'account', '3': 4, '4': 1, '5': 11, '6': '.erx.api.Account', '10': 'account'},
    const {'1': 'status', '3': 5, '4': 1, '5': 14, '6': '.erx.api.OrderStatus', '10': 'status'},
    const {'1': 'action', '3': 6, '4': 1, '5': 14, '6': '.erx.api.OrderAction', '10': 'action'},
    const {'1': 'ticker', '3': 7, '4': 1, '5': 9, '10': 'ticker'},
    const {'1': 'type', '3': 8, '4': 1, '5': 14, '6': '.erx.api.OrderType', '10': 'type'},
    const {'1': 'quantity', '3': 9, '4': 1, '5': 5, '10': 'quantity'},
    const {'1': 'open_quantity', '3': 10, '4': 1, '5': 5, '10': 'openQuantity'},
    const {'1': 'order_price', '3': 11, '4': 1, '5': 1, '10': 'orderPrice'},
    const {'1': 'strike_price', '3': 12, '4': 1, '5': 1, '10': 'strikePrice'},
    const {'1': 'time_in_force', '3': 13, '4': 1, '5': 14, '6': '.erx.api.OrderTimeInForce', '10': 'timeInForce'},
    const {'1': 'to_be_canceled_timestamp', '3': 14, '4': 1, '5': 3, '10': 'toBeCanceledTimestamp'},
    const {'1': 'canceled_timestamp', '3': 15, '4': 1, '5': 3, '10': 'canceledTimestamp'},
  ],
};

const TransactionProcessed$json = const {
  '1': 'TransactionProcessed',
  '2': const [
    const {'1': 'transaction_id', '3': 1, '4': 1, '5': 9, '10': 'transactionId'},
    const {'1': 'created_timestamp', '3': 2, '4': 1, '5': 3, '10': 'createdTimestamp'},
    const {'1': 'ticker', '3': 3, '4': 1, '5': 9, '10': 'ticker'},
    const {'1': 'last', '3': 4, '4': 1, '5': 1, '10': 'last'},
    const {'1': 'volume', '3': 5, '4': 1, '5': 5, '10': 'volume'},
    const {'1': 'level2', '3': 6, '4': 1, '5': 11, '6': '.erx.api.Level2', '10': 'level2'},
  ],
};

const MarketOrderRequest$json = const {
  '1': 'MarketOrderRequest',
  '2': const [
    const {'1': 'account_id', '3': 1, '4': 1, '5': 9, '10': 'accountId'},
    const {'1': 'action', '3': 2, '4': 1, '5': 14, '6': '.erx.api.OrderAction', '10': 'action'},
    const {'1': 'ticker', '3': 3, '4': 1, '5': 9, '10': 'ticker'},
    const {'1': 'quantity', '3': 4, '4': 1, '5': 5, '10': 'quantity'},
  ],
};

const OrderRequest$json = const {
  '1': 'OrderRequest',
  '2': const [
    const {'1': 'account_id', '3': 1, '4': 1, '5': 9, '10': 'accountId'},
    const {'1': 'action', '3': 2, '4': 1, '5': 14, '6': '.erx.api.OrderAction', '10': 'action'},
    const {'1': 'ticker', '3': 3, '4': 1, '5': 9, '10': 'ticker'},
    const {'1': 'type', '3': 4, '4': 1, '5': 14, '6': '.erx.api.OrderType', '10': 'type'},
    const {'1': 'quantity', '3': 5, '4': 1, '5': 5, '10': 'quantity'},
    const {'1': 'order_price', '3': 6, '4': 1, '5': 1, '10': 'orderPrice'},
    const {'1': 'time_in_force', '3': 7, '4': 1, '5': 14, '6': '.erx.api.OrderTimeInForce', '10': 'timeInForce'},
  ],
};

const Quote$json = const {
  '1': 'Quote',
  '2': const [
    const {'1': 'bid', '3': 1, '4': 1, '5': 1, '10': 'bid'},
    const {'1': 'ask', '3': 2, '4': 1, '5': 1, '10': 'ask'},
    const {'1': 'last', '3': 3, '4': 1, '5': 1, '10': 'last'},
    const {'1': 'volume', '3': 4, '4': 1, '5': 5, '10': 'volume'},
  ],
};

const Level2Quote$json = const {
  '1': 'Level2Quote',
  '2': const [
    const {'1': 'price', '3': 1, '4': 1, '5': 1, '10': 'price'},
    const {'1': 'quantity', '3': 2, '4': 1, '5': 5, '10': 'quantity'},
  ],
};

const Level2$json = const {
  '1': 'Level2',
  '2': const [
    const {'1': 'bids', '3': 1, '4': 3, '5': 11, '6': '.erx.api.Level2Quote', '10': 'bids'},
    const {'1': 'asks', '3': 2, '4': 3, '5': 11, '6': '.erx.api.Level2Quote', '10': 'asks'},
  ],
};

const OrderResponse$json = const {
  '1': 'OrderResponse',
  '2': const [
    const {'1': 'code', '3': 1, '4': 1, '5': 5, '10': 'code'},
    const {'1': 'data', '3': 2, '4': 1, '5': 11, '6': '.erx.api.Order', '10': 'data'},
    const {'1': 'errors', '3': 3, '4': 3, '5': 11, '6': '.erx.api.Error', '10': 'errors'},
  ],
};

const AddTickerResponse$json = const {
  '1': 'AddTickerResponse',
  '2': const [
    const {'1': 'buy_order_count', '3': 1, '4': 1, '5': 5, '10': 'buyOrderCount'},
    const {'1': 'sell_order_count', '3': 2, '4': 1, '5': 5, '10': 'sellOrderCount'},
  ],
};

