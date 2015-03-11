using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oas.Infrastructure.Criteria
{
    public class UserCriteria : Criteria
    {


public string Id {get;set;}


public string Email {get;set;}


public bool? EmailConfirmed {get;set;}


public string PasswordHash {get;set;}


public string SecurityStamp {get;set;}


public string PhoneNumber {get;set;}


public bool? PhoneNumberConfirmed {get;set;}


public bool? TwoFactorEnabled {get;set;}


public DateTime? LockoutEndDateUtc {get;set;}


public bool? LockoutEnabled {get;set;}


public int? AccessFailedCount {get;set;}


public string UserName {get;set;}


public string FirstName {get;set;}


public string LastName {get;set;}


public string Address {get;set;}


public string Phone {get;set;}


public string ProfileImage {get;set;}


public int? AccountType {get;set;}


public bool? Suspend {get;set;}


public string Tips {get;set;}


public int? Gender {get;set;}


public string ContactTitle {get;set;}


public Guid? MembershipPackageId {get;set;}


public DateTime? StartDate {get;set;}


public DateTime? ExpireDate {get;set;}


public int? Status {get;set;}


public decimal? PackagePrice {get;set;}


public int? PaymentMethod {get;set;}


public int? PaymentPeriod {get;set;}


public bool? IsOnline {get;set;}


public string Discriminator {get;set;}
    }
}
