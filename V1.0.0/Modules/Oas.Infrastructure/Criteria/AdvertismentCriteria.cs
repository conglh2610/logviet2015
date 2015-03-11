using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oas.Infrastructure.Criteria
{
    public class AdvertismentCriteria : Criteria
    {


public Guid? Id {get;set;}


public string CustomerName {get;set;}


public bool? IsActive {get;set;}


public string ImageUrl {get;set;}


public string Url {get;set;}


public string Description {get;set;}


public decimal? ClickCost {get;set;}


public int? Position {get;set;}


public long? TotalClicked {get;set;}
    }
}
