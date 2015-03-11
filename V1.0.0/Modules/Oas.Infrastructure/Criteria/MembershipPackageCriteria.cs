using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oas.Infrastructure.Criteria
{
    public class MembershipPackageCriteria : Criteria
    {


public Guid? Id {get;set;}


public string Name {get;set;}


public string Description {get;set;}


public decimal? Price {get;set;}


public decimal? OldPrice {get;set;}


public int? Duration {get;set;}
    }
}
