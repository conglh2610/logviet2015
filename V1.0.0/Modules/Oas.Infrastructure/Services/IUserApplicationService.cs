using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IUserApplicationService
    {
        #region UserApplication

        IQueryable<UserApplication> GetUserApplications();

        IQueryable<UserApplication> SearchUserApplication(UserApplicationCriteria criteria, ref int totalRecords);

        UserApplication GetUserApplication(Guid userapplicationsId);

        OperationStatus AddUserApplication(UserApplication userapplications);

        OperationStatus UpdateUserApplication(UserApplication userapplications);

        OperationStatus DeleteUserApplication(Guid userapplicationsId);

        #endregion


    }
}
