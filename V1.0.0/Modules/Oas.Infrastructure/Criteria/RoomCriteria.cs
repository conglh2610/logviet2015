using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oas.Infrastructure.Criteria
{
    public class RoomCriteria : Criteria
    {


public Guid? Id {get;set;}


public string Name {get;set;}


public Guid? AreaId {get;set;}
    }
}