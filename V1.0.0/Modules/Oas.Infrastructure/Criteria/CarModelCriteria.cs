using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oas.Infrastructure.Criteria
{
    public class CarModelCriteria : Criteria
    {


public Guid? Id {get;set;}


public Guid? CarCategoryId {get;set;}


public string Name {get;set;}
    }
}
