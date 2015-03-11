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
    public class CarModelService : ICarModelService
    {
        #region fields
        private readonly IRepository<CarModel> carmodelsRepository;
        #endregion

        #region constructors
        public CarModelService(IRepository<CarModel> carmodelsRepository)
        {
            this.carmodelsRepository = carmodelsRepository;
        }
        #endregion

        #region public methods

        public IQueryable<CarModel> SearchCarModel(CarModelCriteria criteria, ref int totalRecords)
        {
            var query = carmodelsRepository
                       .Get
                       .Include(t => t.CarCategory)
                       .Where(t => (criteria.Id == null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id))
                        && (criteria.CarCategoryId == null || criteria.CarCategoryId == Guid.Empty || t.CarCategoryId.Equals(criteria.CarCategoryId))
                        && (string.IsNullOrEmpty(criteria.Name) || (t.Name.Contains(criteria.Name) || criteria.Name.Contains(t.Name)))
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
                case "categoryid":
                    query = isAsc ? query.OrderBy(t => t.CarCategory.Name) : query.OrderByDescending(t => t.CarCategory.Name);
                    break;
                default: break;
            }

            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;
        }

        public IQueryable<CarModel> GetCarModels()
        {
            return carmodelsRepository
                        .Get
                        .Include(t => t.CarCategory)
                        .AsQueryable();
        }

        public CarModel GetCarModel(Guid carmodelsId)
        {
            return carmodelsRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(carmodelsId));
        }

        public OperationStatus AddCarModel(CarModel carmodels)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                carmodelsRepository.Add(carmodels);
                carmodelsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateCarModel(CarModel carmodels)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                carmodelsRepository.Update(carmodels);
                carmodelsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteCarModel(Guid carmodelsId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var carmodels = carmodelsRepository.Get.SingleOrDefault(t => t.Id.Equals(carmodelsId));
                if (carmodels != null)
                {
                    carmodelsRepository.Remove(carmodels);
                    carmodelsRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "CarModel not found";
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
