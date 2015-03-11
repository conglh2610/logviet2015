using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IRoleService
    {
        #region Role

        IQueryable<Role> GetRoles();

        IQueryable<Role> SearchRole(RoleCriteria criteria, ref int totalRecords);

        Role GetRole(Guid rolesId);

        OperationStatus AddRole(Role roles);

        OperationStatus UpdateRole(Role roles);

        OperationStatus DeleteRole(Guid rolesId);

        #endregion


    }
}
