using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oas.Infrastructure.Criteria
{
    public class UserApplicationCriteria : Criteria
    {


public Guid? Id {get;set;}


public string UserId {get;set;}


public Guid? ApplicationId {get;set;}


public decimal? Price {get;set;}


public int? Status {get;set;}


public DateTime? CreatedDate {get;set;}


public string CreateBy {get;set;}


public DateTime? ExpireDate {get;set;}
    }
}
