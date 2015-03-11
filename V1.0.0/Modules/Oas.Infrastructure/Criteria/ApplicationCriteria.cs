using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oas.Infrastructure.Criteria
{
    public class ApplicationCriteria : Criteria
    {


public Guid? Id {get;set;}


public string Name {get;set;}


public string Description {get;set;}


public decimal? UnitPrice {get;set;}


public Guid? UserApplication_Id {get;set;}
    }
}
