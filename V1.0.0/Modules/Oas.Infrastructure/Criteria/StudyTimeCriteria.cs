using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oas.Infrastructure.Criteria
{
    public class StudyTimeCriteria : Criteria
    {


public Guid? Id {get;set;}


public string Name {get;set;}


public int? StartHour {get;set;}


public int? EndHour {get;set;}
    }
}
