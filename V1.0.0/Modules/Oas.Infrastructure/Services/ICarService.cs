using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface ICarService
    {
        #region Car

        IQueryable<Car> GetCars();

        IQueryable<Car> SearchCar(CarCriteria criteria, ref int totalRecords);

        Car GetCar(Guid carsId);

        OperationStatus AddCar(Car cars);

        OperationStatus UpdateCar(Car cars);

        OperationStatus DeleteCar(Guid carsId);

        #endregion


    }
}
