using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oas.Infrastructure.Criteria
{
    public class BusinesseCriteria : Criteria
    {


public Guid? Id {get;set;}


public string UserId {get;set;}


public Guid? BusinessCategoryId {get;set;}


public bool? UpdatedDate {get;set;}


public string Zipcode {get;set;}


public string Name {get;set;}


public string Address {get;set;}


public string Phone {get;set;}


public string Email {get;set;}


public string Information {get;set;}


public string SortDescription {get;set;}


public string Facebook {get;set;}


public string Twitter {get;set;}


public int? Status {get;set;}


public float? Latitude {get;set;}


public float? Longtitude {get;set;}


public bool? Active {get;set;}


public DateTime? CreateDate {get;set;}


public string CreateBy {get;set;}


public long? TotalViewed {get;set;}
    }
}
