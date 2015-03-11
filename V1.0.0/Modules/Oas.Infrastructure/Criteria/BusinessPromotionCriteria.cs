using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oas.Infrastructure.Criteria
{
    public class BusinessPromotionCriteria : Criteria
    {


public Guid? Id {get;set;}


public Guid? BusinessId {get;set;}


public string Description {get;set;}


public int? Discount {get;set;}


public DateTime? StartDate {get;set;}


public DateTime? EndDate {get;set;}


public int? Limitation {get;set;}


public int? Viewed {get;set;}


public int? Status {get;set;}


public string Title {get;set;}
    }
}
