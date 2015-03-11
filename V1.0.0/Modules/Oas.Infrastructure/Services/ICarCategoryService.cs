using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface ICarCategoryService
    {
        #region CarCategory

        IQueryable<CarCategory> GetCarCategorys();

        IQueryable<CarCategory> SearchCarCategory(CarCategoryCriteria criteria, ref int totalRecords);

        CarCategory GetCarCategory(Guid carcategoriesId);

        OperationStatus AddCarCategory(CarCategory carcategories);

        OperationStatus UpdateCarCategory(CarCategory carcategories);

        OperationStatus DeleteCarCategory(Guid carcategoriesId);

        #endregion


    }
}
