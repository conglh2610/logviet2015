using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IApplicationService
    {
        #region Application

        IQueryable<Application> GetApplications();

        IQueryable<Application> SearchApplication(ApplicationCriteria criteria, ref int totalRecords);

        Application GetApplication(Guid applicationsId);

        OperationStatus AddApplication(Application applications);

        OperationStatus UpdateApplication(Application applications);

        OperationStatus DeleteApplication(Guid applicationsId);

        #endregion


    }
}
