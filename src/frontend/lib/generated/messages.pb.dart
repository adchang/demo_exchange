///
//  Generated code. Do not modify.
//  source: messages.proto
//
// @dart = 2.3
// ignore_for_file: camel_case_types,non_constant_identifier_names,library_prefixes,unused_import,unused_shown_name,return_of_invalid_type

import 'dart:core' as $core;

import 'package:fixnum/fixnum.dart' as $fixnum;
import 'package:protobuf/protobuf.dart' as $pb;

import 'messages.pbenum.dart';

export 'messages.pbenum.dart';

class Empty extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('Empty', package: const $pb.PackageName('erx.api'), createEmptyInstance: create)
    ..hasRequiredFields = false
  ;

  Empty._() : super();
  factory Empty() => create();
  factory Empty.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Empty.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  Empty clone() => Empty()..mergeFromMessage(this);
  Empty copyWith(void Function(Empty) updates) => super.copyWith((message) => updates(message as Empty));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static Empty create() => Empty._();
  Empty createEmptyInstance() => create();
  static $pb.PbList<Empty> createRepeated() => $pb.PbList<Empty>();
  @$core.pragma('dart2js:noInline')
  static Empty getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Empty>(create);
  static Empty _defaultInstance;
}

class StringMessage extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('StringMessage', package: const $pb.PackageName('erx.api'), createEmptyInstance: create)
    ..aOS(1, 'value')
    ..hasRequiredFields = false
  ;

  StringMessage._() : super();
  factory StringMessage() => create();
  factory StringMessage.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory StringMessage.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  StringMessage clone() => StringMessage()..mergeFromMessage(this);
  StringMessage copyWith(void Function(StringMessage) updates) => super.copyWith((message) => updates(message as StringMessage));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static StringMessage create() => StringMessage._();
  StringMessage createEmptyInstance() => create();
  static $pb.PbList<StringMessage> createRepeated() => $pb.PbList<StringMessage>();
  @$core.pragma('dart2js:noInline')
  static StringMessage getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<StringMessage>(create);
  static StringMessage _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get value => $_getSZ(0);
  @$pb.TagNumber(1)
  set value($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasValue() => $_has(0);
  @$pb.TagNumber(1)
  void clearValue() => clearField(1);
}

class IntMessage extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('IntMessage', package: const $pb.PackageName('erx.api'), createEmptyInstance: create)
    ..a<$core.int>(1, 'value', $pb.PbFieldType.O3)
    ..hasRequiredFields = false
  ;

  IntMessage._() : super();
  factory IntMessage() => create();
  factory IntMessage.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory IntMessage.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  IntMessage clone() => IntMessage()..mergeFromMessage(this);
  IntMessage copyWith(void Function(IntMessage) updates) => super.copyWith((message) => updates(message as IntMessage));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static IntMessage create() => IntMessage._();
  IntMessage createEmptyInstance() => create();
  static $pb.PbList<IntMessage> createRepeated() => $pb.PbList<IntMessage>();
  @$core.pragma('dart2js:noInline')
  static IntMessage getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<IntMessage>(create);
  static IntMessage _defaultInstance;

  @$pb.TagNumber(1)
  $core.int get value => $_getIZ(0);
  @$pb.TagNumber(1)
  set value($core.int v) { $_setSignedInt32(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasValue() => $_has(0);
  @$pb.TagNumber(1)
  void clearValue() => clearField(1);
}

class LongMessage extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('LongMessage', package: const $pb.PackageName('erx.api'), createEmptyInstance: create)
    ..aInt64(1, 'value')
    ..hasRequiredFields = false
  ;

  LongMessage._() : super();
  factory LongMessage() => create();
  factory LongMessage.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory LongMessage.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  LongMessage clone() => LongMessage()..mergeFromMessage(this);
  LongMessage copyWith(void Function(LongMessage) updates) => super.copyWith((message) => updates(message as LongMessage));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static LongMessage create() => LongMessage._();
  LongMessage createEmptyInstance() => create();
  static $pb.PbList<LongMessage> createRepeated() => $pb.PbList<LongMessage>();
  @$core.pragma('dart2js:noInline')
  static LongMessage getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<LongMessage>(create);
  static LongMessage _defaultInstance;

  @$pb.TagNumber(1)
  $fixnum.Int64 get value => $_getI64(0);
  @$pb.TagNumber(1)
  set value($fixnum.Int64 v) { $_setInt64(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasValue() => $_has(0);
  @$pb.TagNumber(1)
  void clearValue() => clearField(1);
}

class DoubleMessage extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('DoubleMessage', package: const $pb.PackageName('erx.api'), createEmptyInstance: create)
    ..a<$core.double>(1, 'value', $pb.PbFieldType.OD)
    ..hasRequiredFields = false
  ;

  DoubleMessage._() : super();
  factory DoubleMessage() => create();
  factory DoubleMessage.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory DoubleMessage.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  DoubleMessage clone() => DoubleMessage()..mergeFromMessage(this);
  DoubleMessage copyWith(void Function(DoubleMessage) updates) => super.copyWith((message) => updates(message as DoubleMessage));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static DoubleMessage create() => DoubleMessage._();
  DoubleMessage createEmptyInstance() => create();
  static $pb.PbList<DoubleMessage> createRepeated() => $pb.PbList<DoubleMessage>();
  @$core.pragma('dart2js:noInline')
  static DoubleMessage getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<DoubleMessage>(create);
  static DoubleMessage _defaultInstance;

  @$pb.TagNumber(1)
  $core.double get value => $_getN(0);
  @$pb.TagNumber(1)
  set value($core.double v) { $_setDouble(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasValue() => $_has(0);
  @$pb.TagNumber(1)
  void clearValue() => clearField(1);
}

class BoolMessage extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('BoolMessage', package: const $pb.PackageName('erx.api'), createEmptyInstance: create)
    ..aOB(1, 'value')
    ..hasRequiredFields = false
  ;

  BoolMessage._() : super();
  factory BoolMessage() => create();
  factory BoolMessage.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory BoolMessage.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  BoolMessage clone() => BoolMessage()..mergeFromMessage(this);
  BoolMessage copyWith(void Function(BoolMessage) updates) => super.copyWith((message) => updates(message as BoolMessage));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static BoolMessage create() => BoolMessage._();
  BoolMessage createEmptyInstance() => create();
  static $pb.PbList<BoolMessage> createRepeated() => $pb.PbList<BoolMessage>();
  @$core.pragma('dart2js:noInline')
  static BoolMessage getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<BoolMessage>(create);
  static BoolMessage _defaultInstance;

  @$pb.TagNumber(1)
  $core.bool get value => $_getBF(0);
  @$pb.TagNumber(1)
  set value($core.bool v) { $_setBool(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasValue() => $_has(0);
  @$pb.TagNumber(1)
  void clearValue() => clearField(1);
}

class Error extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('Error', package: const $pb.PackageName('erx.api'), createEmptyInstance: create)
    ..aOS(1, 'propertyName')
    ..aOS(2, 'code')
    ..aOS(3, 'description')
    ..hasRequiredFields = false
  ;

  Error._() : super();
  factory Error() => create();
  factory Error.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Error.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  Error clone() => Error()..mergeFromMessage(this);
  Error copyWith(void Function(Error) updates) => super.copyWith((message) => updates(message as Error));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static Error create() => Error._();
  Error createEmptyInstance() => create();
  static $pb.PbList<Error> createRepeated() => $pb.PbList<Error>();
  @$core.pragma('dart2js:noInline')
  static Error getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Error>(create);
  static Error _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get propertyName => $_getSZ(0);
  @$pb.TagNumber(1)
  set propertyName($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasPropertyName() => $_has(0);
  @$pb.TagNumber(1)
  void clearPropertyName() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get code => $_getSZ(1);
  @$pb.TagNumber(2)
  set code($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasCode() => $_has(1);
  @$pb.TagNumber(2)
  void clearCode() => clearField(2);

  @$pb.TagNumber(3)
  $core.String get description => $_getSZ(2);
  @$pb.TagNumber(3)
  set description($core.String v) { $_setString(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasDescription() => $_has(2);
  @$pb.TagNumber(3)
  void clearDescription() => clearField(3);
}

class User extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('User', package: const $pb.PackageName('erx.api'), createEmptyInstance: create)
    ..aOS(1, 'userId')
    ..aInt64(2, 'createdTimestamp')
    ..aInt64(3, 'lastUpdatedTimestamp')
    ..aOS(5, 'firstName')
    ..aOS(6, 'middleName')
    ..aOS(7, 'lastName')
    ..hasRequiredFields = false
  ;

  User._() : super();
  factory User() => create();
  factory User.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory User.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  User clone() => User()..mergeFromMessage(this);
  User copyWith(void Function(User) updates) => super.copyWith((message) => updates(message as User));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static User create() => User._();
  User createEmptyInstance() => create();
  static $pb.PbList<User> createRepeated() => $pb.PbList<User>();
  @$core.pragma('dart2js:noInline')
  static User getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<User>(create);
  static User _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get userId => $_getSZ(0);
  @$pb.TagNumber(1)
  set userId($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasUserId() => $_has(0);
  @$pb.TagNumber(1)
  void clearUserId() => clearField(1);

  @$pb.TagNumber(2)
  $fixnum.Int64 get createdTimestamp => $_getI64(1);
  @$pb.TagNumber(2)
  set createdTimestamp($fixnum.Int64 v) { $_setInt64(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasCreatedTimestamp() => $_has(1);
  @$pb.TagNumber(2)
  void clearCreatedTimestamp() => clearField(2);

  @$pb.TagNumber(3)
  $fixnum.Int64 get lastUpdatedTimestamp => $_getI64(2);
  @$pb.TagNumber(3)
  set lastUpdatedTimestamp($fixnum.Int64 v) { $_setInt64(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasLastUpdatedTimestamp() => $_has(2);
  @$pb.TagNumber(3)
  void clearLastUpdatedTimestamp() => clearField(3);

  @$pb.TagNumber(5)
  $core.String get firstName => $_getSZ(3);
  @$pb.TagNumber(5)
  set firstName($core.String v) { $_setString(3, v); }
  @$pb.TagNumber(5)
  $core.bool hasFirstName() => $_has(3);
  @$pb.TagNumber(5)
  void clearFirstName() => clearField(5);

  @$pb.TagNumber(6)
  $core.String get middleName => $_getSZ(4);
  @$pb.TagNumber(6)
  set middleName($core.String v) { $_setString(4, v); }
  @$pb.TagNumber(6)
  $core.bool hasMiddleName() => $_has(4);
  @$pb.TagNumber(6)
  void clearMiddleName() => clearField(6);

  @$pb.TagNumber(7)
  $core.String get lastName => $_getSZ(5);
  @$pb.TagNumber(7)
  set lastName($core.String v) { $_setString(5, v); }
  @$pb.TagNumber(7)
  $core.bool hasLastName() => $_has(5);
  @$pb.TagNumber(7)
  void clearLastName() => clearField(7);
}

class Account extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('Account', package: const $pb.PackageName('erx.api'), createEmptyInstance: create)
    ..aOS(1, 'accountId')
    ..aInt64(2, 'createdTimestamp')
    ..aInt64(3, 'lastUpdatedTimestamp')
    ..e<AccountStatus>(4, 'status', $pb.PbFieldType.OE, defaultOrMaker: AccountStatus.ACCOUNT_PENDING_VERIFICATION, valueOf: AccountStatus.valueOf, enumValues: AccountStatus.values)
    ..aOS(5, 'firstName')
    ..aOS(6, 'middleName')
    ..aOS(7, 'lastName')
    ..pc<Address>(8, 'addresses', $pb.PbFieldType.PM, subBuilder: Address.create)
    ..hasRequiredFields = false
  ;

  Account._() : super();
  factory Account() => create();
  factory Account.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Account.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  Account clone() => Account()..mergeFromMessage(this);
  Account copyWith(void Function(Account) updates) => super.copyWith((message) => updates(message as Account));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static Account create() => Account._();
  Account createEmptyInstance() => create();
  static $pb.PbList<Account> createRepeated() => $pb.PbList<Account>();
  @$core.pragma('dart2js:noInline')
  static Account getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Account>(create);
  static Account _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get accountId => $_getSZ(0);
  @$pb.TagNumber(1)
  set accountId($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasAccountId() => $_has(0);
  @$pb.TagNumber(1)
  void clearAccountId() => clearField(1);

  @$pb.TagNumber(2)
  $fixnum.Int64 get createdTimestamp => $_getI64(1);
  @$pb.TagNumber(2)
  set createdTimestamp($fixnum.Int64 v) { $_setInt64(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasCreatedTimestamp() => $_has(1);
  @$pb.TagNumber(2)
  void clearCreatedTimestamp() => clearField(2);

  @$pb.TagNumber(3)
  $fixnum.Int64 get lastUpdatedTimestamp => $_getI64(2);
  @$pb.TagNumber(3)
  set lastUpdatedTimestamp($fixnum.Int64 v) { $_setInt64(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasLastUpdatedTimestamp() => $_has(2);
  @$pb.TagNumber(3)
  void clearLastUpdatedTimestamp() => clearField(3);

  @$pb.TagNumber(4)
  AccountStatus get status => $_getN(3);
  @$pb.TagNumber(4)
  set status(AccountStatus v) { setField(4, v); }
  @$pb.TagNumber(4)
  $core.bool hasStatus() => $_has(3);
  @$pb.TagNumber(4)
  void clearStatus() => clearField(4);

  @$pb.TagNumber(5)
  $core.String get firstName => $_getSZ(4);
  @$pb.TagNumber(5)
  set firstName($core.String v) { $_setString(4, v); }
  @$pb.TagNumber(5)
  $core.bool hasFirstName() => $_has(4);
  @$pb.TagNumber(5)
  void clearFirstName() => clearField(5);

  @$pb.TagNumber(6)
  $core.String get middleName => $_getSZ(5);
  @$pb.TagNumber(6)
  set middleName($core.String v) { $_setString(5, v); }
  @$pb.TagNumber(6)
  $core.bool hasMiddleName() => $_has(5);
  @$pb.TagNumber(6)
  void clearMiddleName() => clearField(6);

  @$pb.TagNumber(7)
  $core.String get lastName => $_getSZ(6);
  @$pb.TagNumber(7)
  set lastName($core.String v) { $_setString(6, v); }
  @$pb.TagNumber(7)
  $core.bool hasLastName() => $_has(6);
  @$pb.TagNumber(7)
  void clearLastName() => clearField(7);

  @$pb.TagNumber(8)
  $core.List<Address> get addresses => $_getList(7);
}

class AccountUser extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('AccountUser', package: const $pb.PackageName('erx.api'), createEmptyInstance: create)
    ..aOS(1, 'accountId')
    ..aOS(2, 'userId')
    ..hasRequiredFields = false
  ;

  AccountUser._() : super();
  factory AccountUser() => create();
  factory AccountUser.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory AccountUser.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  AccountUser clone() => AccountUser()..mergeFromMessage(this);
  AccountUser copyWith(void Function(AccountUser) updates) => super.copyWith((message) => updates(message as AccountUser));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static AccountUser create() => AccountUser._();
  AccountUser createEmptyInstance() => create();
  static $pb.PbList<AccountUser> createRepeated() => $pb.PbList<AccountUser>();
  @$core.pragma('dart2js:noInline')
  static AccountUser getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<AccountUser>(create);
  static AccountUser _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get accountId => $_getSZ(0);
  @$pb.TagNumber(1)
  set accountId($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasAccountId() => $_has(0);
  @$pb.TagNumber(1)
  void clearAccountId() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get userId => $_getSZ(1);
  @$pb.TagNumber(2)
  set userId($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasUserId() => $_has(1);
  @$pb.TagNumber(2)
  void clearUserId() => clearField(2);
}

class Address extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('Address', package: const $pb.PackageName('erx.api'), createEmptyInstance: create)
    ..aOS(1, 'addressId')
    ..aOS(2, 'accountId')
    ..aInt64(3, 'createdTimestamp')
    ..aInt64(4, 'lastUpdatedTimestamp')
    ..e<AddressType>(5, 'type', $pb.PbFieldType.OE, defaultOrMaker: AddressType.ADDRESS_HOME, valueOf: AddressType.valueOf, enumValues: AddressType.values)
    ..aOS(6, 'line1', protoName: 'line_1')
    ..aOS(7, 'line2', protoName: 'line_2')
    ..aOS(8, 'subdistrict')
    ..aOS(9, 'district')
    ..aOS(10, 'city')
    ..aOS(11, 'province')
    ..aOS(12, 'postalCode')
    ..aOS(13, 'country')
    ..hasRequiredFields = false
  ;

  Address._() : super();
  factory Address() => create();
  factory Address.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Address.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  Address clone() => Address()..mergeFromMessage(this);
  Address copyWith(void Function(Address) updates) => super.copyWith((message) => updates(message as Address));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static Address create() => Address._();
  Address createEmptyInstance() => create();
  static $pb.PbList<Address> createRepeated() => $pb.PbList<Address>();
  @$core.pragma('dart2js:noInline')
  static Address getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Address>(create);
  static Address _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get addressId => $_getSZ(0);
  @$pb.TagNumber(1)
  set addressId($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasAddressId() => $_has(0);
  @$pb.TagNumber(1)
  void clearAddressId() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get accountId => $_getSZ(1);
  @$pb.TagNumber(2)
  set accountId($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasAccountId() => $_has(1);
  @$pb.TagNumber(2)
  void clearAccountId() => clearField(2);

  @$pb.TagNumber(3)
  $fixnum.Int64 get createdTimestamp => $_getI64(2);
  @$pb.TagNumber(3)
  set createdTimestamp($fixnum.Int64 v) { $_setInt64(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasCreatedTimestamp() => $_has(2);
  @$pb.TagNumber(3)
  void clearCreatedTimestamp() => clearField(3);

  @$pb.TagNumber(4)
  $fixnum.Int64 get lastUpdatedTimestamp => $_getI64(3);
  @$pb.TagNumber(4)
  set lastUpdatedTimestamp($fixnum.Int64 v) { $_setInt64(3, v); }
  @$pb.TagNumber(4)
  $core.bool hasLastUpdatedTimestamp() => $_has(3);
  @$pb.TagNumber(4)
  void clearLastUpdatedTimestamp() => clearField(4);

  @$pb.TagNumber(5)
  AddressType get type => $_getN(4);
  @$pb.TagNumber(5)
  set type(AddressType v) { setField(5, v); }
  @$pb.TagNumber(5)
  $core.bool hasType() => $_has(4);
  @$pb.TagNumber(5)
  void clearType() => clearField(5);

  @$pb.TagNumber(6)
  $core.String get line1 => $_getSZ(5);
  @$pb.TagNumber(6)
  set line1($core.String v) { $_setString(5, v); }
  @$pb.TagNumber(6)
  $core.bool hasLine1() => $_has(5);
  @$pb.TagNumber(6)
  void clearLine1() => clearField(6);

  @$pb.TagNumber(7)
  $core.String get line2 => $_getSZ(6);
  @$pb.TagNumber(7)
  set line2($core.String v) { $_setString(6, v); }
  @$pb.TagNumber(7)
  $core.bool hasLine2() => $_has(6);
  @$pb.TagNumber(7)
  void clearLine2() => clearField(7);

  @$pb.TagNumber(8)
  $core.String get subdistrict => $_getSZ(7);
  @$pb.TagNumber(8)
  set subdistrict($core.String v) { $_setString(7, v); }
  @$pb.TagNumber(8)
  $core.bool hasSubdistrict() => $_has(7);
  @$pb.TagNumber(8)
  void clearSubdistrict() => clearField(8);

  @$pb.TagNumber(9)
  $core.String get district => $_getSZ(8);
  @$pb.TagNumber(9)
  set district($core.String v) { $_setString(8, v); }
  @$pb.TagNumber(9)
  $core.bool hasDistrict() => $_has(8);
  @$pb.TagNumber(9)
  void clearDistrict() => clearField(9);

  @$pb.TagNumber(10)
  $core.String get city => $_getSZ(9);
  @$pb.TagNumber(10)
  set city($core.String v) { $_setString(9, v); }
  @$pb.TagNumber(10)
  $core.bool hasCity() => $_has(9);
  @$pb.TagNumber(10)
  void clearCity() => clearField(10);

  @$pb.TagNumber(11)
  $core.String get province => $_getSZ(10);
  @$pb.TagNumber(11)
  set province($core.String v) { $_setString(10, v); }
  @$pb.TagNumber(11)
  $core.bool hasProvince() => $_has(10);
  @$pb.TagNumber(11)
  void clearProvince() => clearField(11);

  @$pb.TagNumber(12)
  $core.String get postalCode => $_getSZ(11);
  @$pb.TagNumber(12)
  set postalCode($core.String v) { $_setString(11, v); }
  @$pb.TagNumber(12)
  $core.bool hasPostalCode() => $_has(11);
  @$pb.TagNumber(12)
  void clearPostalCode() => clearField(12);

  @$pb.TagNumber(13)
  $core.String get country => $_getSZ(12);
  @$pb.TagNumber(13)
  set country($core.String v) { $_setString(12, v); }
  @$pb.TagNumber(13)
  $core.bool hasCountry() => $_has(12);
  @$pb.TagNumber(13)
  void clearCountry() => clearField(13);
}

class AccountList extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('AccountList', package: const $pb.PackageName('erx.api'), createEmptyInstance: create)
    ..pc<Account>(1, 'accounts', $pb.PbFieldType.PM, subBuilder: Account.create)
    ..hasRequiredFields = false
  ;

  AccountList._() : super();
  factory AccountList() => create();
  factory AccountList.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory AccountList.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  AccountList clone() => AccountList()..mergeFromMessage(this);
  AccountList copyWith(void Function(AccountList) updates) => super.copyWith((message) => updates(message as AccountList));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static AccountList create() => AccountList._();
  AccountList createEmptyInstance() => create();
  static $pb.PbList<AccountList> createRepeated() => $pb.PbList<AccountList>();
  @$core.pragma('dart2js:noInline')
  static AccountList getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<AccountList>(create);
  static AccountList _defaultInstance;

  @$pb.TagNumber(1)
  $core.List<Account> get accounts => $_getList(0);
}

class AccountRequest extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('AccountRequest', package: const $pb.PackageName('erx.api'), createEmptyInstance: create)
    ..aOS(1, 'firstName')
    ..aOS(2, 'middleName')
    ..aOS(3, 'lastName')
    ..hasRequiredFields = false
  ;

  AccountRequest._() : super();
  factory AccountRequest() => create();
  factory AccountRequest.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory AccountRequest.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  AccountRequest clone() => AccountRequest()..mergeFromMessage(this);
  AccountRequest copyWith(void Function(AccountRequest) updates) => super.copyWith((message) => updates(message as AccountRequest));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static AccountRequest create() => AccountRequest._();
  AccountRequest createEmptyInstance() => create();
  static $pb.PbList<AccountRequest> createRepeated() => $pb.PbList<AccountRequest>();
  @$core.pragma('dart2js:noInline')
  static AccountRequest getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<AccountRequest>(create);
  static AccountRequest _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get firstName => $_getSZ(0);
  @$pb.TagNumber(1)
  set firstName($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasFirstName() => $_has(0);
  @$pb.TagNumber(1)
  void clearFirstName() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get middleName => $_getSZ(1);
  @$pb.TagNumber(2)
  set middleName($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasMiddleName() => $_has(1);
  @$pb.TagNumber(2)
  void clearMiddleName() => clearField(2);

  @$pb.TagNumber(3)
  $core.String get lastName => $_getSZ(2);
  @$pb.TagNumber(3)
  set lastName($core.String v) { $_setString(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasLastName() => $_has(2);
  @$pb.TagNumber(3)
  void clearLastName() => clearField(3);
}

class AccountResponse extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('AccountResponse', package: const $pb.PackageName('erx.api'), createEmptyInstance: create)
    ..a<$core.int>(1, 'code', $pb.PbFieldType.O3)
    ..aOM<Account>(2, 'data', subBuilder: Account.create)
    ..pc<Error>(3, 'errors', $pb.PbFieldType.PM, subBuilder: Error.create)
    ..hasRequiredFields = false
  ;

  AccountResponse._() : super();
  factory AccountResponse() => create();
  factory AccountResponse.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory AccountResponse.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  AccountResponse clone() => AccountResponse()..mergeFromMessage(this);
  AccountResponse copyWith(void Function(AccountResponse) updates) => super.copyWith((message) => updates(message as AccountResponse));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static AccountResponse create() => AccountResponse._();
  AccountResponse createEmptyInstance() => create();
  static $pb.PbList<AccountResponse> createRepeated() => $pb.PbList<AccountResponse>();
  @$core.pragma('dart2js:noInline')
  static AccountResponse getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<AccountResponse>(create);
  static AccountResponse _defaultInstance;

  @$pb.TagNumber(1)
  $core.int get code => $_getIZ(0);
  @$pb.TagNumber(1)
  set code($core.int v) { $_setSignedInt32(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasCode() => $_has(0);
  @$pb.TagNumber(1)
  void clearCode() => clearField(1);

  @$pb.TagNumber(2)
  Account get data => $_getN(1);
  @$pb.TagNumber(2)
  set data(Account v) { setField(2, v); }
  @$pb.TagNumber(2)
  $core.bool hasData() => $_has(1);
  @$pb.TagNumber(2)
  void clearData() => clearField(2);
  @$pb.TagNumber(2)
  Account ensureData() => $_ensure(1);

  @$pb.TagNumber(3)
  $core.List<Error> get errors => $_getList(2);
}

class AddressRequest extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('AddressRequest', package: const $pb.PackageName('erx.api'), createEmptyInstance: create)
    ..aOS(1, 'accountId')
    ..e<AddressType>(2, 'type', $pb.PbFieldType.OE, defaultOrMaker: AddressType.ADDRESS_HOME, valueOf: AddressType.valueOf, enumValues: AddressType.values)
    ..aOS(3, 'line1', protoName: 'line_1')
    ..aOS(4, 'line2', protoName: 'line_2')
    ..aOS(5, 'subdistrict')
    ..aOS(6, 'district')
    ..aOS(7, 'city')
    ..aOS(8, 'province')
    ..aOS(9, 'postalCode')
    ..aOS(10, 'country')
    ..hasRequiredFields = false
  ;

  AddressRequest._() : super();
  factory AddressRequest() => create();
  factory AddressRequest.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory AddressRequest.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  AddressRequest clone() => AddressRequest()..mergeFromMessage(this);
  AddressRequest copyWith(void Function(AddressRequest) updates) => super.copyWith((message) => updates(message as AddressRequest));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static AddressRequest create() => AddressRequest._();
  AddressRequest createEmptyInstance() => create();
  static $pb.PbList<AddressRequest> createRepeated() => $pb.PbList<AddressRequest>();
  @$core.pragma('dart2js:noInline')
  static AddressRequest getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<AddressRequest>(create);
  static AddressRequest _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get accountId => $_getSZ(0);
  @$pb.TagNumber(1)
  set accountId($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasAccountId() => $_has(0);
  @$pb.TagNumber(1)
  void clearAccountId() => clearField(1);

  @$pb.TagNumber(2)
  AddressType get type => $_getN(1);
  @$pb.TagNumber(2)
  set type(AddressType v) { setField(2, v); }
  @$pb.TagNumber(2)
  $core.bool hasType() => $_has(1);
  @$pb.TagNumber(2)
  void clearType() => clearField(2);

  @$pb.TagNumber(3)
  $core.String get line1 => $_getSZ(2);
  @$pb.TagNumber(3)
  set line1($core.String v) { $_setString(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasLine1() => $_has(2);
  @$pb.TagNumber(3)
  void clearLine1() => clearField(3);

  @$pb.TagNumber(4)
  $core.String get line2 => $_getSZ(3);
  @$pb.TagNumber(4)
  set line2($core.String v) { $_setString(3, v); }
  @$pb.TagNumber(4)
  $core.bool hasLine2() => $_has(3);
  @$pb.TagNumber(4)
  void clearLine2() => clearField(4);

  @$pb.TagNumber(5)
  $core.String get subdistrict => $_getSZ(4);
  @$pb.TagNumber(5)
  set subdistrict($core.String v) { $_setString(4, v); }
  @$pb.TagNumber(5)
  $core.bool hasSubdistrict() => $_has(4);
  @$pb.TagNumber(5)
  void clearSubdistrict() => clearField(5);

  @$pb.TagNumber(6)
  $core.String get district => $_getSZ(5);
  @$pb.TagNumber(6)
  set district($core.String v) { $_setString(5, v); }
  @$pb.TagNumber(6)
  $core.bool hasDistrict() => $_has(5);
  @$pb.TagNumber(6)
  void clearDistrict() => clearField(6);

  @$pb.TagNumber(7)
  $core.String get city => $_getSZ(6);
  @$pb.TagNumber(7)
  set city($core.String v) { $_setString(6, v); }
  @$pb.TagNumber(7)
  $core.bool hasCity() => $_has(6);
  @$pb.TagNumber(7)
  void clearCity() => clearField(7);

  @$pb.TagNumber(8)
  $core.String get province => $_getSZ(7);
  @$pb.TagNumber(8)
  set province($core.String v) { $_setString(7, v); }
  @$pb.TagNumber(8)
  $core.bool hasProvince() => $_has(7);
  @$pb.TagNumber(8)
  void clearProvince() => clearField(8);

  @$pb.TagNumber(9)
  $core.String get postalCode => $_getSZ(8);
  @$pb.TagNumber(9)
  set postalCode($core.String v) { $_setString(8, v); }
  @$pb.TagNumber(9)
  $core.bool hasPostalCode() => $_has(8);
  @$pb.TagNumber(9)
  void clearPostalCode() => clearField(9);

  @$pb.TagNumber(10)
  $core.String get country => $_getSZ(9);
  @$pb.TagNumber(10)
  set country($core.String v) { $_setString(9, v); }
  @$pb.TagNumber(10)
  $core.bool hasCountry() => $_has(9);
  @$pb.TagNumber(10)
  void clearCountry() => clearField(10);
}

class AddressResponse extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('AddressResponse', package: const $pb.PackageName('erx.api'), createEmptyInstance: create)
    ..a<$core.int>(1, 'code', $pb.PbFieldType.O3)
    ..aOM<Address>(2, 'data', subBuilder: Address.create)
    ..pc<Error>(3, 'errors', $pb.PbFieldType.PM, subBuilder: Error.create)
    ..hasRequiredFields = false
  ;

  AddressResponse._() : super();
  factory AddressResponse() => create();
  factory AddressResponse.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory AddressResponse.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  AddressResponse clone() => AddressResponse()..mergeFromMessage(this);
  AddressResponse copyWith(void Function(AddressResponse) updates) => super.copyWith((message) => updates(message as AddressResponse));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static AddressResponse create() => AddressResponse._();
  AddressResponse createEmptyInstance() => create();
  static $pb.PbList<AddressResponse> createRepeated() => $pb.PbList<AddressResponse>();
  @$core.pragma('dart2js:noInline')
  static AddressResponse getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<AddressResponse>(create);
  static AddressResponse _defaultInstance;

  @$pb.TagNumber(1)
  $core.int get code => $_getIZ(0);
  @$pb.TagNumber(1)
  set code($core.int v) { $_setSignedInt32(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasCode() => $_has(0);
  @$pb.TagNumber(1)
  void clearCode() => clearField(1);

  @$pb.TagNumber(2)
  Address get data => $_getN(1);
  @$pb.TagNumber(2)
  set data(Address v) { setField(2, v); }
  @$pb.TagNumber(2)
  $core.bool hasData() => $_has(1);
  @$pb.TagNumber(2)
  void clearData() => clearField(2);
  @$pb.TagNumber(2)
  Address ensureData() => $_ensure(1);

  @$pb.TagNumber(3)
  $core.List<Error> get errors => $_getList(2);
}

class CanFillOrderRequest extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('CanFillOrderRequest', package: const $pb.PackageName('erx.api'), createEmptyInstance: create)
    ..aOM<Order>(1, 'order', subBuilder: Order.create)
    ..a<$core.int>(2, 'fillQuantity', $pb.PbFieldType.O3)
    ..hasRequiredFields = false
  ;

  CanFillOrderRequest._() : super();
  factory CanFillOrderRequest() => create();
  factory CanFillOrderRequest.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory CanFillOrderRequest.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  CanFillOrderRequest clone() => CanFillOrderRequest()..mergeFromMessage(this);
  CanFillOrderRequest copyWith(void Function(CanFillOrderRequest) updates) => super.copyWith((message) => updates(message as CanFillOrderRequest));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static CanFillOrderRequest create() => CanFillOrderRequest._();
  CanFillOrderRequest createEmptyInstance() => create();
  static $pb.PbList<CanFillOrderRequest> createRepeated() => $pb.PbList<CanFillOrderRequest>();
  @$core.pragma('dart2js:noInline')
  static CanFillOrderRequest getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<CanFillOrderRequest>(create);
  static CanFillOrderRequest _defaultInstance;

  @$pb.TagNumber(1)
  Order get order => $_getN(0);
  @$pb.TagNumber(1)
  set order(Order v) { setField(1, v); }
  @$pb.TagNumber(1)
  $core.bool hasOrder() => $_has(0);
  @$pb.TagNumber(1)
  void clearOrder() => clearField(1);
  @$pb.TagNumber(1)
  Order ensureOrder() => $_ensure(0);

  @$pb.TagNumber(2)
  $core.int get fillQuantity => $_getIZ(1);
  @$pb.TagNumber(2)
  set fillQuantity($core.int v) { $_setSignedInt32(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasFillQuantity() => $_has(1);
  @$pb.TagNumber(2)
  void clearFillQuantity() => clearField(2);
}

class CanFillOrderResponse extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('CanFillOrderResponse', package: const $pb.PackageName('erx.api'), createEmptyInstance: create)
    ..aOB(1, 'value')
    ..hasRequiredFields = false
  ;

  CanFillOrderResponse._() : super();
  factory CanFillOrderResponse() => create();
  factory CanFillOrderResponse.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory CanFillOrderResponse.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  CanFillOrderResponse clone() => CanFillOrderResponse()..mergeFromMessage(this);
  CanFillOrderResponse copyWith(void Function(CanFillOrderResponse) updates) => super.copyWith((message) => updates(message as CanFillOrderResponse));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static CanFillOrderResponse create() => CanFillOrderResponse._();
  CanFillOrderResponse createEmptyInstance() => create();
  static $pb.PbList<CanFillOrderResponse> createRepeated() => $pb.PbList<CanFillOrderResponse>();
  @$core.pragma('dart2js:noInline')
  static CanFillOrderResponse getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<CanFillOrderResponse>(create);
  static CanFillOrderResponse _defaultInstance;

  @$pb.TagNumber(1)
  $core.bool get value => $_getBF(0);
  @$pb.TagNumber(1)
  set value($core.bool v) { $_setBool(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasValue() => $_has(0);
  @$pb.TagNumber(1)
  void clearValue() => clearField(1);
}

class Order extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('Order', package: const $pb.PackageName('erx.api'), createEmptyInstance: create)
    ..aOS(1, 'orderId')
    ..aInt64(2, 'createdTimestamp')
    ..aOS(3, 'accountId')
    ..aOM<Account>(4, 'account', subBuilder: Account.create)
    ..e<OrderStatus>(5, 'status', $pb.PbFieldType.OE, defaultOrMaker: OrderStatus.ORDER_OPEN, valueOf: OrderStatus.valueOf, enumValues: OrderStatus.values)
    ..e<OrderAction>(6, 'action', $pb.PbFieldType.OE, defaultOrMaker: OrderAction.ORDER_BUY, valueOf: OrderAction.valueOf, enumValues: OrderAction.values)
    ..aOS(7, 'ticker')
    ..e<OrderType>(8, 'type', $pb.PbFieldType.OE, defaultOrMaker: OrderType.ORDER_MARKET, valueOf: OrderType.valueOf, enumValues: OrderType.values)
    ..a<$core.int>(9, 'quantity', $pb.PbFieldType.O3)
    ..a<$core.int>(10, 'openQuantity', $pb.PbFieldType.O3)
    ..a<$core.double>(11, 'orderPrice', $pb.PbFieldType.OD)
    ..a<$core.double>(12, 'strikePrice', $pb.PbFieldType.OD)
    ..e<OrderTimeInForce>(13, 'timeInForce', $pb.PbFieldType.OE, defaultOrMaker: OrderTimeInForce.ORDER_DAY, valueOf: OrderTimeInForce.valueOf, enumValues: OrderTimeInForce.values)
    ..aInt64(14, 'toBeCanceledTimestamp')
    ..aInt64(15, 'canceledTimestamp')
    ..hasRequiredFields = false
  ;

  Order._() : super();
  factory Order() => create();
  factory Order.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Order.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  Order clone() => Order()..mergeFromMessage(this);
  Order copyWith(void Function(Order) updates) => super.copyWith((message) => updates(message as Order));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static Order create() => Order._();
  Order createEmptyInstance() => create();
  static $pb.PbList<Order> createRepeated() => $pb.PbList<Order>();
  @$core.pragma('dart2js:noInline')
  static Order getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Order>(create);
  static Order _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get orderId => $_getSZ(0);
  @$pb.TagNumber(1)
  set orderId($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasOrderId() => $_has(0);
  @$pb.TagNumber(1)
  void clearOrderId() => clearField(1);

  @$pb.TagNumber(2)
  $fixnum.Int64 get createdTimestamp => $_getI64(1);
  @$pb.TagNumber(2)
  set createdTimestamp($fixnum.Int64 v) { $_setInt64(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasCreatedTimestamp() => $_has(1);
  @$pb.TagNumber(2)
  void clearCreatedTimestamp() => clearField(2);

  @$pb.TagNumber(3)
  $core.String get accountId => $_getSZ(2);
  @$pb.TagNumber(3)
  set accountId($core.String v) { $_setString(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasAccountId() => $_has(2);
  @$pb.TagNumber(3)
  void clearAccountId() => clearField(3);

  @$pb.TagNumber(4)
  Account get account => $_getN(3);
  @$pb.TagNumber(4)
  set account(Account v) { setField(4, v); }
  @$pb.TagNumber(4)
  $core.bool hasAccount() => $_has(3);
  @$pb.TagNumber(4)
  void clearAccount() => clearField(4);
  @$pb.TagNumber(4)
  Account ensureAccount() => $_ensure(3);

  @$pb.TagNumber(5)
  OrderStatus get status => $_getN(4);
  @$pb.TagNumber(5)
  set status(OrderStatus v) { setField(5, v); }
  @$pb.TagNumber(5)
  $core.bool hasStatus() => $_has(4);
  @$pb.TagNumber(5)
  void clearStatus() => clearField(5);

  @$pb.TagNumber(6)
  OrderAction get action => $_getN(5);
  @$pb.TagNumber(6)
  set action(OrderAction v) { setField(6, v); }
  @$pb.TagNumber(6)
  $core.bool hasAction() => $_has(5);
  @$pb.TagNumber(6)
  void clearAction() => clearField(6);

  @$pb.TagNumber(7)
  $core.String get ticker => $_getSZ(6);
  @$pb.TagNumber(7)
  set ticker($core.String v) { $_setString(6, v); }
  @$pb.TagNumber(7)
  $core.bool hasTicker() => $_has(6);
  @$pb.TagNumber(7)
  void clearTicker() => clearField(7);

  @$pb.TagNumber(8)
  OrderType get type => $_getN(7);
  @$pb.TagNumber(8)
  set type(OrderType v) { setField(8, v); }
  @$pb.TagNumber(8)
  $core.bool hasType() => $_has(7);
  @$pb.TagNumber(8)
  void clearType() => clearField(8);

  @$pb.TagNumber(9)
  $core.int get quantity => $_getIZ(8);
  @$pb.TagNumber(9)
  set quantity($core.int v) { $_setSignedInt32(8, v); }
  @$pb.TagNumber(9)
  $core.bool hasQuantity() => $_has(8);
  @$pb.TagNumber(9)
  void clearQuantity() => clearField(9);

  @$pb.TagNumber(10)
  $core.int get openQuantity => $_getIZ(9);
  @$pb.TagNumber(10)
  set openQuantity($core.int v) { $_setSignedInt32(9, v); }
  @$pb.TagNumber(10)
  $core.bool hasOpenQuantity() => $_has(9);
  @$pb.TagNumber(10)
  void clearOpenQuantity() => clearField(10);

  @$pb.TagNumber(11)
  $core.double get orderPrice => $_getN(10);
  @$pb.TagNumber(11)
  set orderPrice($core.double v) { $_setDouble(10, v); }
  @$pb.TagNumber(11)
  $core.bool hasOrderPrice() => $_has(10);
  @$pb.TagNumber(11)
  void clearOrderPrice() => clearField(11);

  @$pb.TagNumber(12)
  $core.double get strikePrice => $_getN(11);
  @$pb.TagNumber(12)
  set strikePrice($core.double v) { $_setDouble(11, v); }
  @$pb.TagNumber(12)
  $core.bool hasStrikePrice() => $_has(11);
  @$pb.TagNumber(12)
  void clearStrikePrice() => clearField(12);

  @$pb.TagNumber(13)
  OrderTimeInForce get timeInForce => $_getN(12);
  @$pb.TagNumber(13)
  set timeInForce(OrderTimeInForce v) { setField(13, v); }
  @$pb.TagNumber(13)
  $core.bool hasTimeInForce() => $_has(12);
  @$pb.TagNumber(13)
  void clearTimeInForce() => clearField(13);

  @$pb.TagNumber(14)
  $fixnum.Int64 get toBeCanceledTimestamp => $_getI64(13);
  @$pb.TagNumber(14)
  set toBeCanceledTimestamp($fixnum.Int64 v) { $_setInt64(13, v); }
  @$pb.TagNumber(14)
  $core.bool hasToBeCanceledTimestamp() => $_has(13);
  @$pb.TagNumber(14)
  void clearToBeCanceledTimestamp() => clearField(14);

  @$pb.TagNumber(15)
  $fixnum.Int64 get canceledTimestamp => $_getI64(14);
  @$pb.TagNumber(15)
  set canceledTimestamp($fixnum.Int64 v) { $_setInt64(14, v); }
  @$pb.TagNumber(15)
  $core.bool hasCanceledTimestamp() => $_has(14);
  @$pb.TagNumber(15)
  void clearCanceledTimestamp() => clearField(15);
}

class TransactionProcessed extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('TransactionProcessed', package: const $pb.PackageName('erx.api'), createEmptyInstance: create)
    ..aOS(1, 'transactionId')
    ..aInt64(2, 'createdTimestamp')
    ..aOS(3, 'ticker')
    ..a<$core.double>(4, 'last', $pb.PbFieldType.OD)
    ..a<$core.int>(5, 'volume', $pb.PbFieldType.O3)
    ..aOM<Level2>(6, 'level2', subBuilder: Level2.create)
    ..hasRequiredFields = false
  ;

  TransactionProcessed._() : super();
  factory TransactionProcessed() => create();
  factory TransactionProcessed.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory TransactionProcessed.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  TransactionProcessed clone() => TransactionProcessed()..mergeFromMessage(this);
  TransactionProcessed copyWith(void Function(TransactionProcessed) updates) => super.copyWith((message) => updates(message as TransactionProcessed));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static TransactionProcessed create() => TransactionProcessed._();
  TransactionProcessed createEmptyInstance() => create();
  static $pb.PbList<TransactionProcessed> createRepeated() => $pb.PbList<TransactionProcessed>();
  @$core.pragma('dart2js:noInline')
  static TransactionProcessed getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<TransactionProcessed>(create);
  static TransactionProcessed _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get transactionId => $_getSZ(0);
  @$pb.TagNumber(1)
  set transactionId($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasTransactionId() => $_has(0);
  @$pb.TagNumber(1)
  void clearTransactionId() => clearField(1);

  @$pb.TagNumber(2)
  $fixnum.Int64 get createdTimestamp => $_getI64(1);
  @$pb.TagNumber(2)
  set createdTimestamp($fixnum.Int64 v) { $_setInt64(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasCreatedTimestamp() => $_has(1);
  @$pb.TagNumber(2)
  void clearCreatedTimestamp() => clearField(2);

  @$pb.TagNumber(3)
  $core.String get ticker => $_getSZ(2);
  @$pb.TagNumber(3)
  set ticker($core.String v) { $_setString(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasTicker() => $_has(2);
  @$pb.TagNumber(3)
  void clearTicker() => clearField(3);

  @$pb.TagNumber(4)
  $core.double get last => $_getN(3);
  @$pb.TagNumber(4)
  set last($core.double v) { $_setDouble(3, v); }
  @$pb.TagNumber(4)
  $core.bool hasLast() => $_has(3);
  @$pb.TagNumber(4)
  void clearLast() => clearField(4);

  @$pb.TagNumber(5)
  $core.int get volume => $_getIZ(4);
  @$pb.TagNumber(5)
  set volume($core.int v) { $_setSignedInt32(4, v); }
  @$pb.TagNumber(5)
  $core.bool hasVolume() => $_has(4);
  @$pb.TagNumber(5)
  void clearVolume() => clearField(5);

  @$pb.TagNumber(6)
  Level2 get level2 => $_getN(5);
  @$pb.TagNumber(6)
  set level2(Level2 v) { setField(6, v); }
  @$pb.TagNumber(6)
  $core.bool hasLevel2() => $_has(5);
  @$pb.TagNumber(6)
  void clearLevel2() => clearField(6);
  @$pb.TagNumber(6)
  Level2 ensureLevel2() => $_ensure(5);
}

class MarketOrderRequest extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('MarketOrderRequest', package: const $pb.PackageName('erx.api'), createEmptyInstance: create)
    ..aOS(1, 'accountId')
    ..e<OrderAction>(2, 'action', $pb.PbFieldType.OE, defaultOrMaker: OrderAction.ORDER_BUY, valueOf: OrderAction.valueOf, enumValues: OrderAction.values)
    ..aOS(3, 'ticker')
    ..a<$core.int>(4, 'quantity', $pb.PbFieldType.O3)
    ..hasRequiredFields = false
  ;

  MarketOrderRequest._() : super();
  factory MarketOrderRequest() => create();
  factory MarketOrderRequest.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory MarketOrderRequest.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  MarketOrderRequest clone() => MarketOrderRequest()..mergeFromMessage(this);
  MarketOrderRequest copyWith(void Function(MarketOrderRequest) updates) => super.copyWith((message) => updates(message as MarketOrderRequest));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static MarketOrderRequest create() => MarketOrderRequest._();
  MarketOrderRequest createEmptyInstance() => create();
  static $pb.PbList<MarketOrderRequest> createRepeated() => $pb.PbList<MarketOrderRequest>();
  @$core.pragma('dart2js:noInline')
  static MarketOrderRequest getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<MarketOrderRequest>(create);
  static MarketOrderRequest _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get accountId => $_getSZ(0);
  @$pb.TagNumber(1)
  set accountId($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasAccountId() => $_has(0);
  @$pb.TagNumber(1)
  void clearAccountId() => clearField(1);

  @$pb.TagNumber(2)
  OrderAction get action => $_getN(1);
  @$pb.TagNumber(2)
  set action(OrderAction v) { setField(2, v); }
  @$pb.TagNumber(2)
  $core.bool hasAction() => $_has(1);
  @$pb.TagNumber(2)
  void clearAction() => clearField(2);

  @$pb.TagNumber(3)
  $core.String get ticker => $_getSZ(2);
  @$pb.TagNumber(3)
  set ticker($core.String v) { $_setString(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasTicker() => $_has(2);
  @$pb.TagNumber(3)
  void clearTicker() => clearField(3);

  @$pb.TagNumber(4)
  $core.int get quantity => $_getIZ(3);
  @$pb.TagNumber(4)
  set quantity($core.int v) { $_setSignedInt32(3, v); }
  @$pb.TagNumber(4)
  $core.bool hasQuantity() => $_has(3);
  @$pb.TagNumber(4)
  void clearQuantity() => clearField(4);
}

class OrderRequest extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('OrderRequest', package: const $pb.PackageName('erx.api'), createEmptyInstance: create)
    ..aOS(1, 'accountId')
    ..e<OrderAction>(2, 'action', $pb.PbFieldType.OE, defaultOrMaker: OrderAction.ORDER_BUY, valueOf: OrderAction.valueOf, enumValues: OrderAction.values)
    ..aOS(3, 'ticker')
    ..e<OrderType>(4, 'type', $pb.PbFieldType.OE, defaultOrMaker: OrderType.ORDER_MARKET, valueOf: OrderType.valueOf, enumValues: OrderType.values)
    ..a<$core.int>(5, 'quantity', $pb.PbFieldType.O3)
    ..a<$core.double>(6, 'orderPrice', $pb.PbFieldType.OD)
    ..e<OrderTimeInForce>(7, 'timeInForce', $pb.PbFieldType.OE, defaultOrMaker: OrderTimeInForce.ORDER_DAY, valueOf: OrderTimeInForce.valueOf, enumValues: OrderTimeInForce.values)
    ..hasRequiredFields = false
  ;

  OrderRequest._() : super();
  factory OrderRequest() => create();
  factory OrderRequest.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory OrderRequest.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  OrderRequest clone() => OrderRequest()..mergeFromMessage(this);
  OrderRequest copyWith(void Function(OrderRequest) updates) => super.copyWith((message) => updates(message as OrderRequest));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static OrderRequest create() => OrderRequest._();
  OrderRequest createEmptyInstance() => create();
  static $pb.PbList<OrderRequest> createRepeated() => $pb.PbList<OrderRequest>();
  @$core.pragma('dart2js:noInline')
  static OrderRequest getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<OrderRequest>(create);
  static OrderRequest _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get accountId => $_getSZ(0);
  @$pb.TagNumber(1)
  set accountId($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasAccountId() => $_has(0);
  @$pb.TagNumber(1)
  void clearAccountId() => clearField(1);

  @$pb.TagNumber(2)
  OrderAction get action => $_getN(1);
  @$pb.TagNumber(2)
  set action(OrderAction v) { setField(2, v); }
  @$pb.TagNumber(2)
  $core.bool hasAction() => $_has(1);
  @$pb.TagNumber(2)
  void clearAction() => clearField(2);

  @$pb.TagNumber(3)
  $core.String get ticker => $_getSZ(2);
  @$pb.TagNumber(3)
  set ticker($core.String v) { $_setString(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasTicker() => $_has(2);
  @$pb.TagNumber(3)
  void clearTicker() => clearField(3);

  @$pb.TagNumber(4)
  OrderType get type => $_getN(3);
  @$pb.TagNumber(4)
  set type(OrderType v) { setField(4, v); }
  @$pb.TagNumber(4)
  $core.bool hasType() => $_has(3);
  @$pb.TagNumber(4)
  void clearType() => clearField(4);

  @$pb.TagNumber(5)
  $core.int get quantity => $_getIZ(4);
  @$pb.TagNumber(5)
  set quantity($core.int v) { $_setSignedInt32(4, v); }
  @$pb.TagNumber(5)
  $core.bool hasQuantity() => $_has(4);
  @$pb.TagNumber(5)
  void clearQuantity() => clearField(5);

  @$pb.TagNumber(6)
  $core.double get orderPrice => $_getN(5);
  @$pb.TagNumber(6)
  set orderPrice($core.double v) { $_setDouble(5, v); }
  @$pb.TagNumber(6)
  $core.bool hasOrderPrice() => $_has(5);
  @$pb.TagNumber(6)
  void clearOrderPrice() => clearField(6);

  @$pb.TagNumber(7)
  OrderTimeInForce get timeInForce => $_getN(6);
  @$pb.TagNumber(7)
  set timeInForce(OrderTimeInForce v) { setField(7, v); }
  @$pb.TagNumber(7)
  $core.bool hasTimeInForce() => $_has(6);
  @$pb.TagNumber(7)
  void clearTimeInForce() => clearField(7);
}

class OrderResponse extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('OrderResponse', package: const $pb.PackageName('erx.api'), createEmptyInstance: create)
    ..a<$core.int>(1, 'code', $pb.PbFieldType.O3)
    ..aOM<Order>(2, 'data', subBuilder: Order.create)
    ..pc<Error>(3, 'errors', $pb.PbFieldType.PM, subBuilder: Error.create)
    ..hasRequiredFields = false
  ;

  OrderResponse._() : super();
  factory OrderResponse() => create();
  factory OrderResponse.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory OrderResponse.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  OrderResponse clone() => OrderResponse()..mergeFromMessage(this);
  OrderResponse copyWith(void Function(OrderResponse) updates) => super.copyWith((message) => updates(message as OrderResponse));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static OrderResponse create() => OrderResponse._();
  OrderResponse createEmptyInstance() => create();
  static $pb.PbList<OrderResponse> createRepeated() => $pb.PbList<OrderResponse>();
  @$core.pragma('dart2js:noInline')
  static OrderResponse getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<OrderResponse>(create);
  static OrderResponse _defaultInstance;

  @$pb.TagNumber(1)
  $core.int get code => $_getIZ(0);
  @$pb.TagNumber(1)
  set code($core.int v) { $_setSignedInt32(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasCode() => $_has(0);
  @$pb.TagNumber(1)
  void clearCode() => clearField(1);

  @$pb.TagNumber(2)
  Order get data => $_getN(1);
  @$pb.TagNumber(2)
  set data(Order v) { setField(2, v); }
  @$pb.TagNumber(2)
  $core.bool hasData() => $_has(1);
  @$pb.TagNumber(2)
  void clearData() => clearField(2);
  @$pb.TagNumber(2)
  Order ensureData() => $_ensure(1);

  @$pb.TagNumber(3)
  $core.List<Error> get errors => $_getList(2);
}

class Quote extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('Quote', package: const $pb.PackageName('erx.api'), createEmptyInstance: create)
    ..a<$core.double>(1, 'bid', $pb.PbFieldType.OD)
    ..a<$core.double>(2, 'ask', $pb.PbFieldType.OD)
    ..a<$core.double>(3, 'last', $pb.PbFieldType.OD)
    ..a<$core.int>(4, 'volume', $pb.PbFieldType.O3)
    ..hasRequiredFields = false
  ;

  Quote._() : super();
  factory Quote() => create();
  factory Quote.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Quote.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  Quote clone() => Quote()..mergeFromMessage(this);
  Quote copyWith(void Function(Quote) updates) => super.copyWith((message) => updates(message as Quote));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static Quote create() => Quote._();
  Quote createEmptyInstance() => create();
  static $pb.PbList<Quote> createRepeated() => $pb.PbList<Quote>();
  @$core.pragma('dart2js:noInline')
  static Quote getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Quote>(create);
  static Quote _defaultInstance;

  @$pb.TagNumber(1)
  $core.double get bid => $_getN(0);
  @$pb.TagNumber(1)
  set bid($core.double v) { $_setDouble(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasBid() => $_has(0);
  @$pb.TagNumber(1)
  void clearBid() => clearField(1);

  @$pb.TagNumber(2)
  $core.double get ask => $_getN(1);
  @$pb.TagNumber(2)
  set ask($core.double v) { $_setDouble(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasAsk() => $_has(1);
  @$pb.TagNumber(2)
  void clearAsk() => clearField(2);

  @$pb.TagNumber(3)
  $core.double get last => $_getN(2);
  @$pb.TagNumber(3)
  set last($core.double v) { $_setDouble(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasLast() => $_has(2);
  @$pb.TagNumber(3)
  void clearLast() => clearField(3);

  @$pb.TagNumber(4)
  $core.int get volume => $_getIZ(3);
  @$pb.TagNumber(4)
  set volume($core.int v) { $_setSignedInt32(3, v); }
  @$pb.TagNumber(4)
  $core.bool hasVolume() => $_has(3);
  @$pb.TagNumber(4)
  void clearVolume() => clearField(4);
}

class HistoricalPriceRequest extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('HistoricalPriceRequest', package: const $pb.PackageName('erx.api'), createEmptyInstance: create)
    ..aOS(1, 'ticker')
    ..e<PriceType>(2, 'type', $pb.PbFieldType.OE, defaultOrMaker: PriceType.PRICE_TYPE_YEAR, valueOf: PriceType.valueOf, enumValues: PriceType.values)
    ..aInt64(3, 'startTime')
    ..aInt64(4, 'endTime')
    ..hasRequiredFields = false
  ;

  HistoricalPriceRequest._() : super();
  factory HistoricalPriceRequest() => create();
  factory HistoricalPriceRequest.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory HistoricalPriceRequest.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  HistoricalPriceRequest clone() => HistoricalPriceRequest()..mergeFromMessage(this);
  HistoricalPriceRequest copyWith(void Function(HistoricalPriceRequest) updates) => super.copyWith((message) => updates(message as HistoricalPriceRequest));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static HistoricalPriceRequest create() => HistoricalPriceRequest._();
  HistoricalPriceRequest createEmptyInstance() => create();
  static $pb.PbList<HistoricalPriceRequest> createRepeated() => $pb.PbList<HistoricalPriceRequest>();
  @$core.pragma('dart2js:noInline')
  static HistoricalPriceRequest getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<HistoricalPriceRequest>(create);
  static HistoricalPriceRequest _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get ticker => $_getSZ(0);
  @$pb.TagNumber(1)
  set ticker($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasTicker() => $_has(0);
  @$pb.TagNumber(1)
  void clearTicker() => clearField(1);

  @$pb.TagNumber(2)
  PriceType get type => $_getN(1);
  @$pb.TagNumber(2)
  set type(PriceType v) { setField(2, v); }
  @$pb.TagNumber(2)
  $core.bool hasType() => $_has(1);
  @$pb.TagNumber(2)
  void clearType() => clearField(2);

  @$pb.TagNumber(3)
  $fixnum.Int64 get startTime => $_getI64(2);
  @$pb.TagNumber(3)
  set startTime($fixnum.Int64 v) { $_setInt64(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasStartTime() => $_has(2);
  @$pb.TagNumber(3)
  void clearStartTime() => clearField(3);

  @$pb.TagNumber(4)
  $fixnum.Int64 get endTime => $_getI64(3);
  @$pb.TagNumber(4)
  set endTime($fixnum.Int64 v) { $_setInt64(3, v); }
  @$pb.TagNumber(4)
  $core.bool hasEndTime() => $_has(3);
  @$pb.TagNumber(4)
  void clearEndTime() => clearField(4);
}

class HistoricalPrice extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('HistoricalPrice', package: const $pb.PackageName('erx.api'), createEmptyInstance: create)
    ..aOS(1, 'ticker')
    ..aInt64(2, 'timestamp')
    ..e<PriceType>(3, 'type', $pb.PbFieldType.OE, defaultOrMaker: PriceType.PRICE_TYPE_YEAR, valueOf: PriceType.valueOf, enumValues: PriceType.values)
    ..a<$core.double>(4, 'open', $pb.PbFieldType.OD)
    ..a<$core.double>(5, 'close', $pb.PbFieldType.OD)
    ..a<$core.double>(6, 'high', $pb.PbFieldType.OD)
    ..a<$core.double>(7, 'low', $pb.PbFieldType.OD)
    ..a<$core.int>(8, 'volume', $pb.PbFieldType.O3)
    ..hasRequiredFields = false
  ;

  HistoricalPrice._() : super();
  factory HistoricalPrice() => create();
  factory HistoricalPrice.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory HistoricalPrice.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  HistoricalPrice clone() => HistoricalPrice()..mergeFromMessage(this);
  HistoricalPrice copyWith(void Function(HistoricalPrice) updates) => super.copyWith((message) => updates(message as HistoricalPrice));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static HistoricalPrice create() => HistoricalPrice._();
  HistoricalPrice createEmptyInstance() => create();
  static $pb.PbList<HistoricalPrice> createRepeated() => $pb.PbList<HistoricalPrice>();
  @$core.pragma('dart2js:noInline')
  static HistoricalPrice getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<HistoricalPrice>(create);
  static HistoricalPrice _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get ticker => $_getSZ(0);
  @$pb.TagNumber(1)
  set ticker($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasTicker() => $_has(0);
  @$pb.TagNumber(1)
  void clearTicker() => clearField(1);

  @$pb.TagNumber(2)
  $fixnum.Int64 get timestamp => $_getI64(1);
  @$pb.TagNumber(2)
  set timestamp($fixnum.Int64 v) { $_setInt64(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasTimestamp() => $_has(1);
  @$pb.TagNumber(2)
  void clearTimestamp() => clearField(2);

  @$pb.TagNumber(3)
  PriceType get type => $_getN(2);
  @$pb.TagNumber(3)
  set type(PriceType v) { setField(3, v); }
  @$pb.TagNumber(3)
  $core.bool hasType() => $_has(2);
  @$pb.TagNumber(3)
  void clearType() => clearField(3);

  @$pb.TagNumber(4)
  $core.double get open => $_getN(3);
  @$pb.TagNumber(4)
  set open($core.double v) { $_setDouble(3, v); }
  @$pb.TagNumber(4)
  $core.bool hasOpen() => $_has(3);
  @$pb.TagNumber(4)
  void clearOpen() => clearField(4);

  @$pb.TagNumber(5)
  $core.double get close => $_getN(4);
  @$pb.TagNumber(5)
  set close($core.double v) { $_setDouble(4, v); }
  @$pb.TagNumber(5)
  $core.bool hasClose() => $_has(4);
  @$pb.TagNumber(5)
  void clearClose() => clearField(5);

  @$pb.TagNumber(6)
  $core.double get high => $_getN(5);
  @$pb.TagNumber(6)
  set high($core.double v) { $_setDouble(5, v); }
  @$pb.TagNumber(6)
  $core.bool hasHigh() => $_has(5);
  @$pb.TagNumber(6)
  void clearHigh() => clearField(6);

  @$pb.TagNumber(7)
  $core.double get low => $_getN(6);
  @$pb.TagNumber(7)
  set low($core.double v) { $_setDouble(6, v); }
  @$pb.TagNumber(7)
  $core.bool hasLow() => $_has(6);
  @$pb.TagNumber(7)
  void clearLow() => clearField(7);

  @$pb.TagNumber(8)
  $core.int get volume => $_getIZ(7);
  @$pb.TagNumber(8)
  set volume($core.int v) { $_setSignedInt32(7, v); }
  @$pb.TagNumber(8)
  $core.bool hasVolume() => $_has(7);
  @$pb.TagNumber(8)
  void clearVolume() => clearField(8);
}

class Level2Quote extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('Level2Quote', package: const $pb.PackageName('erx.api'), createEmptyInstance: create)
    ..a<$core.double>(1, 'price', $pb.PbFieldType.OD)
    ..a<$core.int>(2, 'quantity', $pb.PbFieldType.O3)
    ..hasRequiredFields = false
  ;

  Level2Quote._() : super();
  factory Level2Quote() => create();
  factory Level2Quote.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Level2Quote.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  Level2Quote clone() => Level2Quote()..mergeFromMessage(this);
  Level2Quote copyWith(void Function(Level2Quote) updates) => super.copyWith((message) => updates(message as Level2Quote));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static Level2Quote create() => Level2Quote._();
  Level2Quote createEmptyInstance() => create();
  static $pb.PbList<Level2Quote> createRepeated() => $pb.PbList<Level2Quote>();
  @$core.pragma('dart2js:noInline')
  static Level2Quote getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Level2Quote>(create);
  static Level2Quote _defaultInstance;

  @$pb.TagNumber(1)
  $core.double get price => $_getN(0);
  @$pb.TagNumber(1)
  set price($core.double v) { $_setDouble(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasPrice() => $_has(0);
  @$pb.TagNumber(1)
  void clearPrice() => clearField(1);

  @$pb.TagNumber(2)
  $core.int get quantity => $_getIZ(1);
  @$pb.TagNumber(2)
  set quantity($core.int v) { $_setSignedInt32(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasQuantity() => $_has(1);
  @$pb.TagNumber(2)
  void clearQuantity() => clearField(2);
}

class Level2 extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('Level2', package: const $pb.PackageName('erx.api'), createEmptyInstance: create)
    ..pc<Level2Quote>(1, 'bids', $pb.PbFieldType.PM, subBuilder: Level2Quote.create)
    ..pc<Level2Quote>(2, 'asks', $pb.PbFieldType.PM, subBuilder: Level2Quote.create)
    ..hasRequiredFields = false
  ;

  Level2._() : super();
  factory Level2() => create();
  factory Level2.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Level2.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  Level2 clone() => Level2()..mergeFromMessage(this);
  Level2 copyWith(void Function(Level2) updates) => super.copyWith((message) => updates(message as Level2));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static Level2 create() => Level2._();
  Level2 createEmptyInstance() => create();
  static $pb.PbList<Level2> createRepeated() => $pb.PbList<Level2>();
  @$core.pragma('dart2js:noInline')
  static Level2 getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Level2>(create);
  static Level2 _defaultInstance;

  @$pb.TagNumber(1)
  $core.List<Level2Quote> get bids => $_getList(0);

  @$pb.TagNumber(2)
  $core.List<Level2Quote> get asks => $_getList(1);
}

class AddTickerResponse extends $pb.GeneratedMessage {
  static final $pb.BuilderInfo _i = $pb.BuilderInfo('AddTickerResponse', package: const $pb.PackageName('erx.api'), createEmptyInstance: create)
    ..a<$core.int>(1, 'buyOrderCount', $pb.PbFieldType.O3)
    ..a<$core.int>(2, 'sellOrderCount', $pb.PbFieldType.O3)
    ..hasRequiredFields = false
  ;

  AddTickerResponse._() : super();
  factory AddTickerResponse() => create();
  factory AddTickerResponse.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory AddTickerResponse.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);
  AddTickerResponse clone() => AddTickerResponse()..mergeFromMessage(this);
  AddTickerResponse copyWith(void Function(AddTickerResponse) updates) => super.copyWith((message) => updates(message as AddTickerResponse));
  $pb.BuilderInfo get info_ => _i;
  @$core.pragma('dart2js:noInline')
  static AddTickerResponse create() => AddTickerResponse._();
  AddTickerResponse createEmptyInstance() => create();
  static $pb.PbList<AddTickerResponse> createRepeated() => $pb.PbList<AddTickerResponse>();
  @$core.pragma('dart2js:noInline')
  static AddTickerResponse getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<AddTickerResponse>(create);
  static AddTickerResponse _defaultInstance;

  @$pb.TagNumber(1)
  $core.int get buyOrderCount => $_getIZ(0);
  @$pb.TagNumber(1)
  set buyOrderCount($core.int v) { $_setSignedInt32(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasBuyOrderCount() => $_has(0);
  @$pb.TagNumber(1)
  void clearBuyOrderCount() => clearField(1);

  @$pb.TagNumber(2)
  $core.int get sellOrderCount => $_getIZ(1);
  @$pb.TagNumber(2)
  set sellOrderCount($core.int v) { $_setSignedInt32(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasSellOrderCount() => $_has(1);
  @$pb.TagNumber(2)
  void clearSellOrderCount() => clearField(2);
}

