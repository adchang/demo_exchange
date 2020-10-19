using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoExchange.Api;
using DemoExchange.Interface;
using static Utils.Preconditions;
using static Utils.Time;

namespace DemoExchange.Models {
  /// <summary>
  /// For persistence of an <c>Account</c>.
  /// </summary>
  [Table("ExchangeAccount")]
  public class AccountEntity {
    [Key]
    public Guid AccountId { get; set; }

    [Required]
    public long CreatedTimestamp { get; set; }

    [Required]
    public long LastUpdatedTimestamp { get; set; }

    [Required]
    public AccountStatus Status { get; set; }

    [Required]
    public String FirstName { get; set; }

    public String MiddleName { get; set; }

    [Required]
    public String LastName { get; set; }

    public List<AddressEntity> Addresses { get; set; }

    public virtual AccountEntity ShallowCopy() {
      return (AccountEntity)this.MemberwiseClone();
    }

    public override String ToString() {
      return "{AccountId: " + AccountId + ", " +
        "CreatedTimestamp: " + CreatedTimestamp + ", " +
        "LastUpdatedTimestamp: " + LastUpdatedTimestamp + ", " +
        "Status: " + Status + ", " +
        "FirstName: " + FirstName + ", " +
        "MiddleName: " + MiddleName + ", " +
        "LastName: " + LastName + ", " +
        "}";
    }

    public override bool Equals(object other) {
      if (other == null) { // Don't check for GetType
        return false;
      }

      return this.ToString().Equals(other.ToString());
    }

    public override int GetHashCode() {
      return HashCode.Combine(ToString());
    }
  }

  public class AccountBL : AccountEntity, IAccountModel, IIsValid {
    public new String AccountId {
      get { return base.AccountId.ToString(); }
    }
    public new long CreatedTimestamp {
      get { return base.CreatedTimestamp; }
    }
    public DateTime CreatedDateTime {
      get { return FromTicks(base.CreatedTimestamp); }
    }
    public new long LastUpdatedTimestamp {
      get { return base.LastUpdatedTimestamp; }
      private set { base.LastUpdatedTimestamp = value; }
    }
    public DateTime LastUpdatedDateTime {
      get { return FromTicks(base.LastUpdatedTimestamp); }
    }
    public new AccountStatus Status {
      get { return base.Status; }
      private set { base.Status = value; }
    }
    public bool IsPendingVerification {
      get { return AccountStatus.AccountPendingVerification.Equals(Status); }
    }
    public bool IsPendingApproval {
      get { return AccountStatus.AccountPendingApproval.Equals(Status); }
    }
    public bool IsActive {
      get { return AccountStatus.AccountActive.Equals(Status); }
    }
    public bool IsSuspended {
      get { return AccountStatus.AccountSuspended.Equals(Status); }
    }
    public bool IsCancelled {
      get { return AccountStatus.AccountCancelled.Equals(Status); }
    }
    public bool IsDeleted {
      get { return AccountStatus.AccountDeleted.Equals(Status); }
    }
    public new String FirstName {
      get { return base.FirstName; }
      private set { base.FirstName = value; }
    }
    public new String MiddleName {
      get { return base.MiddleName; }
      private set { base.MiddleName = value; }
    }
    public new String LastName {
      get { return base.LastName; }
      private set { base.LastName = value; }
    }
    public String FullName {
      get { return FirstName + " " + LastName; }
    }
    public new List<IAddressModel> Addresses { get; } // TODO: Do a backing addresses?

    private AccountBL(String firstName, String middleName, String lastName) {
      // TODO: preconditions
      base.AccountId = Guid.NewGuid();
      base.CreatedTimestamp = Now;
      base.LastUpdatedTimestamp = base.CreatedTimestamp;
      base.Status = AccountStatus.AccountPendingVerification;
      base.FirstName = firstName;
      base.MiddleName = middleName;
      base.LastName = lastName;
    }

    public AccountBL(AccountEntity entity) {
      base.AccountId = entity.AccountId;
      base.CreatedTimestamp = entity.CreatedTimestamp;
      base.LastUpdatedTimestamp = entity.LastUpdatedTimestamp;
      base.Status = entity.Status;
      base.FirstName = entity.FirstName;
      base.MiddleName = entity.MiddleName;
      base.LastName = entity.LastName;
    }

    public AccountBL(AccountRequest request) : this(request.FirstName, request.MiddleName,
      request.LastName) { }

    public bool IsValid {
      get { return true; } // TODO: 
    }

    public Account ToMessage() => new
    Account {
      AccountId = this.AccountId,
      CreatedTimestamp = this.CreatedTimestamp,
      LastUpdatedTimestamp = this.LastUpdatedTimestamp,
      Status = this.Status,
      FirstName = this.FirstName,
      MiddleName = this.MiddleName,
      LastName = this.LastName
    };
  }

  public class Accounts {
    public class Predicates {
      public static Predicate<IAccountModel> ById(String accountId) {
        return account => account.AccountId.Equals(accountId);
      }

      private Predicates() {
        // Prevent instantiation
      }
    }

    private Accounts() {
      // Prevent instantiation
    }
  }

  /// <summary>
  /// For persistence of an <c>Address</c>.
  /// </summary>
  [Table("ExchangeAddress")]
  public class AddressEntity {
    [Key]
    public Guid AddressId { get; set; }

    [Required]
    public Guid AccountId { get; set; }

    public AccountEntity Account { get; set; }

    [Required]
    public long CreatedTimestamp { get; set; }

    [Required]
    public long LastUpdatedTimestamp { get; set; }

    [Required]
    public AddressType Type { get; set; }

    [Required]
    public String Line1 { get; set; }

    public String Line2 { get; set; }

    public String Subdistrict { get; set; }

    public String District { get; set; }

    [Required]
    public String City { get; set; }

    [Required]
    public String Province { get; set; }

    [Required]
    public String PostalCode { get; set; }

    [Required]
    public String Country { get; set; }

    public virtual AccountEntity ShallowCopy() {
      return (AccountEntity)this.MemberwiseClone();
    }

    public override String ToString() {
      return "{AddressId: " + AddressId + ", " +
        "AccountId: " + AccountId + ", " +
        "CreatedTimestamp: " + CreatedTimestamp + ", " +
        "LastUpdatedTimestamp: " + LastUpdatedTimestamp + ", " +
        "Type: " + Type + ", " +
        "Line1: " + Line1 + ", " +
        "Line2: " + Line2 + ", " +
        "Subdistrict: " + Subdistrict + ", " +
        "District: " + District + ", " +
        "City: " + City + ", " +
        "Province: " + Province + ", " +
        "PostalCode: " + PostalCode + ", " +
        "Country: " + Country + ", " +
        "}";
    }

    public override bool Equals(object other) {
      if (other == null) { // Don't check for GetType
        return false;
      }

      return this.ToString().Equals(other.ToString());
    }

    public override int GetHashCode() {
      return HashCode.Combine(ToString());
    }
  }

  public class AddressBL : AddressEntity, IAddressModel, IIsValid {
    public new String AddressId {
      get { return base.AddressId.ToString(); }
    }
    public new String AccountId {
      get { return base.AccountId.ToString(); }
    }
    public new IAccountModel Account {
      get { return new AccountBL(base.Account); } // TODO: Do a backing?
    }
    public new long CreatedTimestamp {
      get { return base.CreatedTimestamp; }
    }
    public DateTime CreatedDateTime {
      get { return FromTicks(base.CreatedTimestamp); }
    }
    public new long LastUpdatedTimestamp {
      get { return base.LastUpdatedTimestamp; }
      private set { base.LastUpdatedTimestamp = value; }
    }
    public DateTime LastUpdatedDateTime {
      get { return FromTicks(base.LastUpdatedTimestamp); }
    }
    public new AddressType Type {
      get { return base.Type; }
      private set { base.Type = value; }
    }
    public bool IsHome {
      get { return AddressType.AddressHome.Equals(Type); }
    }
    public bool IsWork {
      get { return AddressType.AddressWork.Equals(Type); }
    }
    public bool IsMailing {
      get { return AddressType.AddressMailing.Equals(Type); }
    }
    public new String Line1 {
      get { return base.Line1; }
      private set { base.Line1 = value; }
    }
    public new String Line2 {
      get { return base.Line2; }
      private set { base.Line2 = value; }
    }
    public new String Subdistrict {
      get { return base.Subdistrict; }
      private set { base.Subdistrict = value; }
    }
    public new String District {
      get { return base.District; }
      private set { base.District = value; }
    }
    public new String City {
      get { return base.City; }
      private set { base.City = value; }
    }
    public new String Province {
      get { return base.Province; }
      private set { base.Province = value; }
    }
    public new String PostalCode {
      get { return base.PostalCode; }
      private set { base.PostalCode = value; }
    }
    public new String Country {
      get { return base.Country; }
      private set { base.Country = value; }
    }

    private AddressBL(String accountId, AddressType type, String line1, String line2,
      String subdistrict, String district, String city, String province, String postalCode,
      String country) {
      // TODO: preconditions
      base.AddressId = Guid.NewGuid();
      base.AccountId = Guid.Parse(accountId);
      base.CreatedTimestamp = Now;
      base.LastUpdatedTimestamp = base.CreatedTimestamp;
      base.Type = type;
      base.Line1 = line1;
      base.Line2 = line2;
      base.Subdistrict = subdistrict;
      base.District = district;
      base.City = city;
      base.Province = province;
      base.PostalCode = postalCode;
      base.Country = country;
    }

    public AddressBL(AddressEntity entity) {
      base.AddressId = entity.AddressId;
      base.AccountId = entity.AccountId;
      base.CreatedTimestamp = entity.CreatedTimestamp;
      base.LastUpdatedTimestamp = entity.LastUpdatedTimestamp;
      base.Type = entity.Type;
      base.Line1 = entity.Line1;
      base.Line2 = entity.Line2;
      base.Subdistrict = entity.Subdistrict;
      base.District = entity.District;
      base.City = entity.City;
      base.Province = entity.Province;
      base.PostalCode = entity.PostalCode;
      base.Country = entity.Country;
    }

    public AddressBL(AddressRequest request) : this(request.AccountId, request.Type, request.Line1,
      request.Line2, request.Subdistrict, request.District, request.City, request.Province,
      request.PostalCode, request.Country) { }

    public bool IsValid {
      get { return true; } // TODO: 
    }

    public Address ToMessage() => new
    Address {
      AddressId = this.AddressId,
      AccountId = this.AccountId,
      CreatedTimestamp = this.CreatedTimestamp,
      LastUpdatedTimestamp = this.LastUpdatedTimestamp,
      Type = this.Type,
      Line1 = this.Line1,
      Line2 = this.Line2,
      Subdistrict = this.Subdistrict,
      District = this.District,
      City = this.City,
      Province = this.Province,
      PostalCode = this.PostalCode,
      Country = this.Country
    };
  }
}
