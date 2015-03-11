using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oas.Infrastructure.Criteria
{
    public class ClassTeacherCriteria : Criteria
    {


public Guid? Id {get;set;}


public Guid? ClassId {get;set;}


public Guid? TeacherId {get;set;}


public int? Status {get;set;}


public decimal? SalaryByHour {get;set;}
    }
}
