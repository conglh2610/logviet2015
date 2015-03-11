using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oas.Infrastructure.Criteria
{
    public class ClasseCriteria : Criteria
    {


public Guid? Id {get;set;}


public int? Number {get;set;}


public string Name {get;set;}


public Guid? ProgramId {get;set;}


public Guid? EmployeeId {get;set;}


public DateTime? StartDate {get;set;}


public DateTime? EndDate {get;set;}


public int? Size {get;set;}


public int? Status {get;set;}
    }
}
