using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Oas.Infrastructure.Domain
{
    public class Role : IdentityRole
    {
        public string Description { get; set; }
        public virtual ICollection<IdentityUserRole> UserRoles { get; set; }
        public bool ManageUser { get; set; }
        public bool ManageBusiness { get; set; }
        public bool ManageMembershipPackage { get; set; }
    }


}
