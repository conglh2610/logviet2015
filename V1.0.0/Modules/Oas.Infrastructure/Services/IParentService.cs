using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IParentService
    {
        #region Parent

        IQueryable<Parent> GetParents();

        IQueryable<Parent> SearchParent(ParentCriteria criteria, ref int totalRecords);

        Parent GetParent(Guid parentsId);

        OperationStatus AddParent(Parent parents);

        OperationStatus UpdateParent(Parent parents);

        OperationStatus DeleteParent(Guid parentsId);

        #endregion


    }
}
