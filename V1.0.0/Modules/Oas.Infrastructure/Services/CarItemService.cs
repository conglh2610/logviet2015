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
    public class CarItemService : ICarItemService
    {
        #region fields
        private readonly IRepository<CarItem> caritemsRepository;
        #endregion

        #region constructors
        public CarItemService(IRepository<CarItem> caritemsRepository)
        {
            this.caritemsRepository = caritemsRepository;
        }
        #endregion

        #region public methods

        public IQueryable<CarItem> SearchCarItem(CarItemCriteria criteria, ref int totalRecords)
        {
            var query = caritemsRepository
                       .Get
                       .Include(t => t.Car).Include(t => t.Car.CarModel).Include(t => t.Car.CarModel.CarCategory)
                       .Where(t => (criteria.Id == null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id))
                        && (criteria.CarId == null || criteria.CarId == Guid.Empty || t.CarId.ToString().Contains(criteria.CarId.ToString()))
                        && (criteria.CarModelId == null || criteria.CarModelId == Guid.Empty || t.Car.CarModelId.ToString().Contains(criteria.CarModelId.ToString()))
                        && (criteria.CarCategoryId == null || criteria.CarCategoryId == Guid.Empty || t.Car.CarModel.CarCategoryId.ToString().Contains(criteria.CarCategoryId.ToString()))
                        && (criteria.BusinessId == null || criteria.BusinessId == Guid.Empty || t.BusinessId.Equals(criteria.BusinessId))
                        && (criteria.UnitPrice == null || t.UnitPrice.Equals(criteria.UnitPrice))
                        && (string.IsNullOrEmpty(criteria.Description) || (t.Description.Contains(criteria.Description) || criteria.Description.Contains(t.Description)))
                        && (string.IsNullOrEmpty(criteria.CarName) || (t.Car.Name.Contains(criteria.CarName) || criteria.CarName.Contains(t.Car.Name)))
                        && (string.IsNullOrEmpty(criteria.CarNumber) || (t.CarNumber.Contains(criteria.CarNumber) || criteria.CarNumber.Contains(t.CarNumber)))
                        && (criteria.Total == null || t.Total.Equals(criteria.Total))
                        )
                       .AsQueryable();

            totalRecords = query.Count();

            criteria.SortColumn = string.IsNullOrEmpty(criteria.SortColumn) ? string.Empty : criteria.SortColumn.ToLower();

            bool isAsc = criteria.SortDirection.ToLower().Equals("true");

            switch (criteria.SortColumn)
            {
                case "description":
                    query = isAsc ? query.OrderBy(t => t.Description) : query.OrderByDescending(t => t.Description);
                    break;
                case "carnumber":
                    query = isAsc ? query.OrderBy(t => t.CarNumber) : query.OrderByDescending(t => t.CarNumber);
                    break;
                case "categoryid":
                    query = isAsc ? query.OrderBy(t => t.Car.CarModel.CarCategory.Name) : query.OrderByDescending(t => t.Car.CarModel.CarCategory.Name);
                    break;
                case "modelid":
                    query = isAsc ? query.OrderBy(t => t.Car.CarModel.Name) : query.OrderByDescending(t => t.Car.CarModel.Name);
                    break;
                case "carid":
                    query = isAsc ? query.OrderBy(t => t.Car.Name) : query.OrderByDescending(t => t.Car.Name);
                    break;
                default:
                    query = isAsc ? query.OrderBy(t => t.Car.Name) : query.OrderByDescending(t => t.Car.Name);
                    break;
            }

            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;
        }

        public IQueryable<CarItem> GetCarItems()
        {
            return caritemsRepository
                        .Get
                        .Include(t => t.Car).Include(t => t.Car.CarModel).Include(t => t.Car.CarModel.CarCategory)
                        .AsQueryable();
        }

        public CarItem GetCarItem(Guid caritemsId)
        {
            return caritemsRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(caritemsId));
        }

        public OperationStatus AddCarItem(CarItem caritems)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                caritemsRepository.Add(caritems);
                caritemsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateCarItem(CarItem caritems)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                caritemsRepository.Update(caritems);
                caritemsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteCarItem(Guid caritemsId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var caritems = caritemsRepository.Get.SingleOrDefault(t => t.Id.Equals(caritemsId));
                if (caritems != null)
                {
                    caritemsRepository.Remove(caritems);
                    caritemsRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "CarItem not found";
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
