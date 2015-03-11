using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oas.Infrastructure.Criteria
{
    public class ProgramCriteria : Criteria
    {


public Guid? Id {get;set;}


public Guid? LanguageId {get;set;}


public string Name {get;set;}


public int? TotalMonths {get;set;}


public decimal? Price {get;set;}


public int? LanguageLevel {get;set;}
    }
}
