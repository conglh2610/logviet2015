using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oas.Infrastructure.Criteria
{
    public class BusinessLikeCriteria : Criteria
    {


public Guid? Id {get;set;}


public string UserId {get;set;}


public Guid? BusinessId {get;set;}


public bool? Like {get;set;}
    }
}
