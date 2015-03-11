using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oas.Infrastructure.Criteria
{
    public class BookingNoteCriteria : Criteria
    {


public Guid? Id {get;set;}


public Guid? CarBookingId {get;set;}


public string Note {get;set;}


public string CreatedBy {get;set;}


public DateTime? CreatedDate {get;set;}
    }
}
