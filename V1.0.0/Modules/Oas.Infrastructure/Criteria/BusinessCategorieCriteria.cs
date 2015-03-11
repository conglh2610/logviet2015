using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oas.Infrastructure.Criteria
{
    public class BusinessCategorieCriteria : Criteria
    {


public Guid? Id {get;set;}


public Guid? ParentId {get;set;}


public string Name {get;set;}


public string GooglePlaceIconUrl {get;set;}


public int? CategoryId {get;set;}
    }
}
