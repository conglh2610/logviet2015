using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IClassService
    {
        #region Class

        IQueryable<Class> GetClasss();

        IQueryable<Class> SearchClass(ClassCriteria criteria, ref int totalRecords);

        Class GetClass(Guid classesId);

        OperationStatus AddClass(Class classes);

        OperationStatus UpdateClass(Class classes);

        OperationStatus DeleteClass(Guid classesId);

        #endregion


    }
}
