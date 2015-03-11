using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oas.Infrastructure.Criteria
{
    public class MessageHistoryCriteria : Criteria
    {


public Guid? Id {get;set;}


public string Message {get;set;}


public string FromUserId {get;set;}


public string ToUserId {get;set;}


public int? Status {get;set;}


public DateTime? CreateDate {get;set;}


public string User_Id {get;set;}
    }
}
