using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IBusinessService
    {
        #region Business

        IQueryable<Business> GetBusinesss();

        IQueryable<Business> SearchBusiness(BusinessCriteria criteria, ref int totalRecords);

        Business GetBusiness(Guid businessesId);

        OperationStatus AddBusiness(Business businesses);

        OperationStatus UpdateBusiness(Business businesses);

        OperationStatus DeleteBusiness(Guid businessesId);

        #endregion


    }
}
