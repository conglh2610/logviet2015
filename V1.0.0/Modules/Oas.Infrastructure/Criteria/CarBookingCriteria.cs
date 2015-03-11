using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oas.Infrastructure.Criteria
{
    public class CarBookingCriteria : Criteria
    {


public Guid? Id {get;set;}


public Guid? CarItemId {get;set;}


public string UserId {get;set;}


public Guid? DriverId {get;set;}


public bool? IsNeededDriver {get;set;}


public DateTime? BookFromDate {get;set;}


public DateTime? BookToDate {get;set;}


public int? BookType {get;set;}


public int? TotalDay {get;set;}


public string Schduling {get;set;}


public int? BookStatus {get;set;}
    }
}
