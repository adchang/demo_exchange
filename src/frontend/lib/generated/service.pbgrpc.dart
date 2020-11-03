///
//  Generated code. Do not modify.
//  source: service.proto
//
// @dart = 2.3
// ignore_for_file: camel_case_types,non_constant_identifier_names,library_prefixes,unused_import,unused_shown_name,return_of_invalid_type

import 'dart:async' as $async;

import 'dart:core' as $core;

import 'package:grpc/service_api.dart' as $grpc;
import 'messages.pb.dart' as $0;
export 'service.pb.dart';

class ErxServiceClient extends $grpc.Client {
  static final _$listAccounts = $grpc.ClientMethod<$0.Empty, $0.AccountList>(
      '/erx.api.ErxService/ListAccounts',
      ($0.Empty value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.AccountList.fromBuffer(value));
  static final _$createAccount =
      $grpc.ClientMethod<$0.AccountRequest, $0.AccountResponse>(
          '/erx.api.ErxService/CreateAccount',
          ($0.AccountRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) =>
              $0.AccountResponse.fromBuffer(value));
  static final _$createAddress =
      $grpc.ClientMethod<$0.AddressRequest, $0.AddressResponse>(
          '/erx.api.ErxService/CreateAddress',
          ($0.AddressRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) =>
              $0.AddressResponse.fromBuffer(value));
  static final _$isMarketOpen = $grpc.ClientMethod<$0.Empty, $0.BoolMessage>(
      '/erx.api.ErxService/IsMarketOpen',
      ($0.Empty value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.BoolMessage.fromBuffer(value));
  static final _$getOrder = $grpc.ClientMethod<$0.StringMessage, $0.Order>(
      '/erx.api.ErxService/GetOrder',
      ($0.StringMessage value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Order.fromBuffer(value));
  static final _$submitOrder =
      $grpc.ClientMethod<$0.OrderRequest, $0.OrderResponse>(
          '/erx.api.ErxService/SubmitOrder',
          ($0.OrderRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $0.OrderResponse.fromBuffer(value));
  static final _$cancelOrder =
      $grpc.ClientMethod<$0.StringMessage, $0.OrderResponse>(
          '/erx.api.ErxService/CancelOrder',
          ($0.StringMessage value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $0.OrderResponse.fromBuffer(value));
  static final _$getLevel2 = $grpc.ClientMethod<$0.StringMessage, $0.Level2>(
      '/erx.api.ErxService/GetLevel2',
      ($0.StringMessage value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Level2.fromBuffer(value));
  static final _$getLevel2Streams =
      $grpc.ClientMethod<$0.StringMessage, $0.Level2>(
          '/erx.api.ErxService/GetLevel2Streams',
          ($0.StringMessage value) => value.writeToBuffer(),
          ($core.List<$core.int> value) => $0.Level2.fromBuffer(value));
  static final _$getQuote = $grpc.ClientMethod<$0.StringMessage, $0.Quote>(
      '/erx.api.ErxService/GetQuote',
      ($0.StringMessage value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.Quote.fromBuffer(value));
  static final _$getHistoricalPriceStreams =
      $grpc.ClientMethod<$0.HistoricalPriceRequest, $0.HistoricalPrice>(
          '/erx.api.ErxService/GetHistoricalPriceStreams',
          ($0.HistoricalPriceRequest value) => value.writeToBuffer(),
          ($core.List<$core.int> value) =>
              $0.HistoricalPrice.fromBuffer(value));

  ErxServiceClient($grpc.ClientChannel channel, {$grpc.CallOptions options})
      : super(channel, options: options);

  $grpc.ResponseFuture<$0.AccountList> listAccounts($0.Empty request,
      {$grpc.CallOptions options}) {
    final call = $createCall(
        _$listAccounts, $async.Stream.fromIterable([request]),
        options: options);
    return $grpc.ResponseFuture(call);
  }

  $grpc.ResponseFuture<$0.AccountResponse> createAccount(
      $0.AccountRequest request,
      {$grpc.CallOptions options}) {
    final call = $createCall(
        _$createAccount, $async.Stream.fromIterable([request]),
        options: options);
    return $grpc.ResponseFuture(call);
  }

  $grpc.ResponseFuture<$0.AddressResponse> createAddress(
      $0.AddressRequest request,
      {$grpc.CallOptions options}) {
    final call = $createCall(
        _$createAddress, $async.Stream.fromIterable([request]),
        options: options);
    return $grpc.ResponseFuture(call);
  }

  $grpc.ResponseFuture<$0.BoolMessage> isMarketOpen($0.Empty request,
      {$grpc.CallOptions options}) {
    final call = $createCall(
        _$isMarketOpen, $async.Stream.fromIterable([request]),
        options: options);
    return $grpc.ResponseFuture(call);
  }

  $grpc.ResponseFuture<$0.Order> getOrder($0.StringMessage request,
      {$grpc.CallOptions options}) {
    final call = $createCall(_$getOrder, $async.Stream.fromIterable([request]),
        options: options);
    return $grpc.ResponseFuture(call);
  }

  $grpc.ResponseFuture<$0.OrderResponse> submitOrder($0.OrderRequest request,
      {$grpc.CallOptions options}) {
    final call = $createCall(
        _$submitOrder, $async.Stream.fromIterable([request]),
        options: options);
    return $grpc.ResponseFuture(call);
  }

  $grpc.ResponseFuture<$0.OrderResponse> cancelOrder($0.StringMessage request,
      {$grpc.CallOptions options}) {
    final call = $createCall(
        _$cancelOrder, $async.Stream.fromIterable([request]),
        options: options);
    return $grpc.ResponseFuture(call);
  }

  $grpc.ResponseFuture<$0.Level2> getLevel2($0.StringMessage request,
      {$grpc.CallOptions options}) {
    final call = $createCall(_$getLevel2, $async.Stream.fromIterable([request]),
        options: options);
    return $grpc.ResponseFuture(call);
  }

  $grpc.ResponseStream<$0.Level2> getLevel2Streams($0.StringMessage request,
      {$grpc.CallOptions options}) {
    final call = $createCall(
        _$getLevel2Streams, $async.Stream.fromIterable([request]),
        options: options);
    return $grpc.ResponseStream(call);
  }

  $grpc.ResponseFuture<$0.Quote> getQuote($0.StringMessage request,
      {$grpc.CallOptions options}) {
    final call = $createCall(_$getQuote, $async.Stream.fromIterable([request]),
        options: options);
    return $grpc.ResponseFuture(call);
  }

  $grpc.ResponseStream<$0.HistoricalPrice> getHistoricalPriceStreams(
      $0.HistoricalPriceRequest request,
      {$grpc.CallOptions options}) {
    final call = $createCall(
        _$getHistoricalPriceStreams, $async.Stream.fromIterable([request]),
        options: options);
    return $grpc.ResponseStream(call);
  }
}

abstract class ErxServiceBase extends $grpc.Service {
  $core.String get $name => 'erx.api.ErxService';

  ErxServiceBase() {
    $addMethod($grpc.ServiceMethod<$0.Empty, $0.AccountList>(
        'ListAccounts',
        listAccounts_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.Empty.fromBuffer(value),
        ($0.AccountList value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.AccountRequest, $0.AccountResponse>(
        'CreateAccount',
        createAccount_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.AccountRequest.fromBuffer(value),
        ($0.AccountResponse value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.AddressRequest, $0.AddressResponse>(
        'CreateAddress',
        createAddress_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.AddressRequest.fromBuffer(value),
        ($0.AddressResponse value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.Empty, $0.BoolMessage>(
        'IsMarketOpen',
        isMarketOpen_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.Empty.fromBuffer(value),
        ($0.BoolMessage value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.StringMessage, $0.Order>(
        'GetOrder',
        getOrder_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.StringMessage.fromBuffer(value),
        ($0.Order value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.OrderRequest, $0.OrderResponse>(
        'SubmitOrder',
        submitOrder_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.OrderRequest.fromBuffer(value),
        ($0.OrderResponse value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.StringMessage, $0.OrderResponse>(
        'CancelOrder',
        cancelOrder_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.StringMessage.fromBuffer(value),
        ($0.OrderResponse value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.StringMessage, $0.Level2>(
        'GetLevel2',
        getLevel2_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.StringMessage.fromBuffer(value),
        ($0.Level2 value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.StringMessage, $0.Level2>(
        'GetLevel2Streams',
        getLevel2Streams_Pre,
        false,
        true,
        ($core.List<$core.int> value) => $0.StringMessage.fromBuffer(value),
        ($0.Level2 value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.StringMessage, $0.Quote>(
        'GetQuote',
        getQuote_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.StringMessage.fromBuffer(value),
        ($0.Quote value) => value.writeToBuffer()));
    $addMethod(
        $grpc.ServiceMethod<$0.HistoricalPriceRequest, $0.HistoricalPrice>(
            'GetHistoricalPriceStreams',
            getHistoricalPriceStreams_Pre,
            false,
            true,
            ($core.List<$core.int> value) =>
                $0.HistoricalPriceRequest.fromBuffer(value),
            ($0.HistoricalPrice value) => value.writeToBuffer()));
  }

  $async.Future<$0.AccountList> listAccounts_Pre(
      $grpc.ServiceCall call, $async.Future<$0.Empty> request) async {
    return listAccounts(call, await request);
  }

  $async.Future<$0.AccountResponse> createAccount_Pre(
      $grpc.ServiceCall call, $async.Future<$0.AccountRequest> request) async {
    return createAccount(call, await request);
  }

  $async.Future<$0.AddressResponse> createAddress_Pre(
      $grpc.ServiceCall call, $async.Future<$0.AddressRequest> request) async {
    return createAddress(call, await request);
  }

  $async.Future<$0.BoolMessage> isMarketOpen_Pre(
      $grpc.ServiceCall call, $async.Future<$0.Empty> request) async {
    return isMarketOpen(call, await request);
  }

  $async.Future<$0.Order> getOrder_Pre(
      $grpc.ServiceCall call, $async.Future<$0.StringMessage> request) async {
    return getOrder(call, await request);
  }

  $async.Future<$0.OrderResponse> submitOrder_Pre(
      $grpc.ServiceCall call, $async.Future<$0.OrderRequest> request) async {
    return submitOrder(call, await request);
  }

  $async.Future<$0.OrderResponse> cancelOrder_Pre(
      $grpc.ServiceCall call, $async.Future<$0.StringMessage> request) async {
    return cancelOrder(call, await request);
  }

  $async.Future<$0.Level2> getLevel2_Pre(
      $grpc.ServiceCall call, $async.Future<$0.StringMessage> request) async {
    return getLevel2(call, await request);
  }

  $async.Stream<$0.Level2> getLevel2Streams_Pre(
      $grpc.ServiceCall call, $async.Future<$0.StringMessage> request) async* {
    yield* getLevel2Streams(call, await request);
  }

  $async.Future<$0.Quote> getQuote_Pre(
      $grpc.ServiceCall call, $async.Future<$0.StringMessage> request) async {
    return getQuote(call, await request);
  }

  $async.Stream<$0.HistoricalPrice> getHistoricalPriceStreams_Pre(
      $grpc.ServiceCall call,
      $async.Future<$0.HistoricalPriceRequest> request) async* {
    yield* getHistoricalPriceStreams(call, await request);
  }

  $async.Future<$0.AccountList> listAccounts(
      $grpc.ServiceCall call, $0.Empty request);
  $async.Future<$0.AccountResponse> createAccount(
      $grpc.ServiceCall call, $0.AccountRequest request);
  $async.Future<$0.AddressResponse> createAddress(
      $grpc.ServiceCall call, $0.AddressRequest request);
  $async.Future<$0.BoolMessage> isMarketOpen(
      $grpc.ServiceCall call, $0.Empty request);
  $async.Future<$0.Order> getOrder(
      $grpc.ServiceCall call, $0.StringMessage request);
  $async.Future<$0.OrderResponse> submitOrder(
      $grpc.ServiceCall call, $0.OrderRequest request);
  $async.Future<$0.OrderResponse> cancelOrder(
      $grpc.ServiceCall call, $0.StringMessage request);
  $async.Future<$0.Level2> getLevel2(
      $grpc.ServiceCall call, $0.StringMessage request);
  $async.Stream<$0.Level2> getLevel2Streams(
      $grpc.ServiceCall call, $0.StringMessage request);
  $async.Future<$0.Quote> getQuote(
      $grpc.ServiceCall call, $0.StringMessage request);
  $async.Stream<$0.HistoricalPrice> getHistoricalPriceStreams(
      $grpc.ServiceCall call, $0.HistoricalPriceRequest request);
}
