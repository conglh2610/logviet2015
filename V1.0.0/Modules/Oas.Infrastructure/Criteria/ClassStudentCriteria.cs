using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oas.Infrastructure.Criteria
{
    public class ClassStudentCriteria : Criteria
    {


public Guid? Id {get;set;}


public Guid? ClassId {get;set;}


public Guid? StudentId {get;set;}


public int? Status {get;set;}


public int? Discount {get;set;}


public int? FinalResult {get;set;}
    }
}
