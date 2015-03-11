using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oas.Infrastructure.Criteria
{
    public class RoleCriteria : Criteria
    {


public string Id {get;set;}


public string Name {get;set;}


public string Description {get;set;}


public bool? ManageUser {get;set;}


public bool? ManageBusiness {get;set;}


public bool? ManageMembershipPackage {get;set;}


public string Discriminator {get;set;}
    }
}
