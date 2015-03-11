using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oas.Infrastructure.Criteria
{
    public class BusinessCommentCriteria : Criteria
    {


public Guid? Id {get;set;}


public string UserId {get;set;}


public Guid? BusinessId {get;set;}


public int? BusinessRate {get;set;}


public string Comment {get;set;}


public string CreateDate {get;set;}
    }
}
