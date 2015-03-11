using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oas.Infrastructure.Criteria
{
    public class CarAccidentNoteCriteria : Criteria
    {


public Guid? Id {get;set;}


public Guid? CarAccident {get;set;}


public string Title {get;set;}


public string Description {get;set;}
    }
}
