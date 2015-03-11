using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface ICarModelService
    {
        #region CarModel

        IQueryable<CarModel> GetCarModels();

        IQueryable<CarModel> SearchCarModel(CarModelCriteria criteria, ref int totalRecords);

        CarModel GetCarModel(Guid carmodelsId);

        OperationStatus AddCarModel(CarModel carmodels);

        OperationStatus UpdateCarModel(CarModel carmodels);

        OperationStatus DeleteCarModel(Guid carmodelsId);

        #endregion


    }
}
