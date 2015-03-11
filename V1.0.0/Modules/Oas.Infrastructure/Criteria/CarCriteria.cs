using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oas.Infrastructure.Criteria
{
    public class CarCriteria : Criteria
    {


public Guid? Id {get;set;}


public Guid? CarModelId {get;set;}


public string Name {get;set;}


public string Year {get;set;}


public string Description {get;set;}


public bool? IsMT {get;set;}


public bool? IsAT {get;set;}


public int? TotalOfSeating {get;set;}
    }
}
