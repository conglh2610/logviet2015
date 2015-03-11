using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Oas.Infrastructure.Criteria;

namespace Oas.Infrastructure.Services
{
    public class CarService : ICarService
    {
        #region fields
        private readonly IRepository<Car> carsRepository;
        #endregion

        #region constructors
        public CarService(IRepository<Car> carsRepository)
        {
            this.carsRepository = carsRepository;
        }
        #endregion

        #region public methods

        public IQueryable<Car> SearchCar(CarCriteria criteria, ref int totalRecords)
        {
            var query = carsRepository
                       .Get
                       .Include(t=>t.CarModel).Include(t=>t.CarModel.CarCategory)
                       .Where(t => (criteria.Id == null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id))
                       && (criteria.CarModelId == null || criteria.CarModelId == Guid.Empty || t.CarModelId.Equals(criteria.CarModelId))
                       && (string.IsNullOrEmpty(criteria.Name) || (t.Name.Contains(criteria.Name) || criteria.Name.Contains(t.Name)))
                       && (string.IsNullOrEmpty(criteria.Year) || (t.Year.Contains(criteria.Year) || criteria.Year.Contains(t.Year)))
                       && (string.IsNullOrEmpty(criteria.Description) || (t.Description.Contains(criteria.Description) || criteria.Description.Contains(t.Description)))
                       && (criteria.IsMT == null || t.IsMT.Equals(criteria.IsMT))
                       && (criteria.IsAT == null || t.IsAT.Equals(criteria.IsAT))
                       && (criteria.TotalOfSeating == null || t.TotalOfSeating.Equals(criteria.TotalOfSeating))
                        )
                       .AsQueryable();

            totalRecords = query.Count();

            criteria.SortColumn = string.IsNullOrEmpty(criteria.SortColumn) ? string.Empty : criteria.SortColumn.ToLower();

            bool isAsc = criteria.SortDirection.ToLower().Equals("true");

            switch (criteria.SortColumn)
            {
                case "name":
                    query = isAsc ? query.OrderBy(t => t.Name) : query.OrderByDescending(t => t.Name);
                    break;
                case "year":
                    query = isAsc ? query.OrderBy(t => t.Year) : query.OrderByDescending(t => t.Year);
                    break;
                case "description":
                    query = isAsc ? query.OrderBy(t => t.Description) : query.OrderByDescending(t => t.Description);
                    break;
                case "carmodelid":
                    query = isAsc ? query.OrderBy(t => t.CarModel.Name) : query.OrderByDescending(t => t.CarModel.Name);
                    break;
                default: break;
            }

            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;
        }

        public IQueryable<Car> GetCars()
        {
            return carsRepository
                        .Get.Include(t => t.CarModel)
                        .AsQueryable();
        }

        public Car GetCar(Guid carsId)
        {
            return carsRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(carsId));
        }

        public OperationStatus AddCar(Car cars)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                carsRepository.Add(cars);
                carsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateCar(Car cars)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                carsRepository.Update(cars);
                carsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteCar(Guid carsId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var cars = carsRepository.Get.SingleOrDefault(t => t.Id.Equals(carsId));
                if (cars != null)
                {
                    carsRepository.Remove(cars);
                    carsRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "Car not found";
                }
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        #endregion
    }
}
