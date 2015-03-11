using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IBusinessCategoryService
    {
        #region BusinessCategory

        IQueryable<BusinessCategory> GetBusinessCategorys();

        IQueryable<BusinessCategory> SearchBusinessCategory(BusinessCategoryCriteria criteria, ref int totalRecords);

        BusinessCategory GetBusinessCategory(Guid businesscategoriesId);

        OperationStatus AddBusinessCategory(BusinessCategory businesscategories);

        OperationStatus UpdateBusinessCategory(BusinessCategory businesscategories);

        OperationStatus DeleteBusinessCategory(Guid businesscategoriesId);

        #endregion


    }
}
