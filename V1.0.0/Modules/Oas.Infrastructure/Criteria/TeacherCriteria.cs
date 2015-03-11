using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oas.Infrastructure.Criteria
{
    public class TeacherCriteria : Criteria
    {


public Guid? Id {get;set;}


public string FirstName {get;set;}


public string LastName {get;set;}


public DateTime? DateOfBirth {get;set;}


public string Address {get;set;}


public string PhoneNumber {get;set;}


public string Email {get;set;}


public int? Gender {get;set;}


public decimal? SalaryByHour {get;set;}


public Guid? CountryId {get;set;}
    }
}
