using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oas.Infrastructure.Domain;
using System.Data.Entity;
using Oas.Infrastructure.Criteria;
namespace Oas.Infrastructure.Services
{
    public class CarRentingService : ICarRentingService
    {
        #region fields
        private readonly IRepository<Car> carRepository;
        private readonly IRepository<CarModel> carModelRepository;
        private readonly IRepository<CarCategory> carCategoryRepository;
        #endregion

        public CarRentingService(IRepository<Car> carRepository, IRepository<CarModel> carModelRepository, IRepository<CarCategory> carCategoryRepository)
        {
            this.carRepository = carRepository;
            this.carModelRepository = carModelRepository;
            this.carCategoryRepository = carCategoryRepository;
        }

        #region Car
        public IQueryable<Domain.Car> GetCars()
        {
            return carRepository
                    .Get
                    .Include(t => t.CarModel)
                    .OrderBy(c => c.Name)
                    .AsQueryable();
        }

        public IQueryable<Domain.Car> SearchCar(CarCriteria criteria, ref int totalRecords)
        {
            var query = carRepository.Get.Include(t => t.CarModel)
                                             .AsQueryable();

            totalRecords = query.Count();

            criteria.SortColumn = string.IsNullOrEmpty(criteria.SortColumn) ? string.Empty : criteria.SortColumn.ToLower();

            bool isAsc = criteria.SortDirection.Equals("True");

            switch (criteria.SortColumn)
            {
                case "name":
                    query = isAsc ? query.OrderBy(t => t.Name) : query.OrderByDescending(t => t.Name);
                    break;
                case "carmodelid":
                    query = isAsc ? query.OrderBy(t => t.CarModel.Name) : query.OrderByDescending(t => t.CarModel.Name);
                    break;
                default:
                    break;
            }

            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;
        }



        public Car GetCar(Guid carId)
        {
            return carRepository
                    .Get
                    .Include(t => t.CarModel)
                    .SingleOrDefault(t => t.Id.Equals(carId));
        }
        public OperationStatus UpdateCar(Domain.Car car)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                carRepository.Update(car);
                carRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }
        public OperationStatus DeleteCar(Guid carId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var car = carRepository.Get.SingleOrDefault(t => t.Id.Equals(carId));
                if (car != null)
                {
                    carRepository.Remove(car);
                    carRepository.Commit();
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
        public OperationStatus AddCar(Car car)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                carRepository.Add(car);
                carRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        #endregion

        #region Car Model

        public IQueryable<Domain.CarModel> SearchCarModel(CarModelCriteria criteria, ref int totalRecords)
        {
            var query = carModelRepository.Get.Include(t=>t.CarCategory)
                                  .AsQueryable();

            totalRecords = query.Count();

            criteria.SortColumn = string.IsNullOrEmpty(criteria.SortColumn) ? string.Empty : criteria.SortColumn.ToLower();

            bool isAsc = criteria.SortDirection.Equals("True");

            switch (criteria.SortColumn)
            {
                case "name":
                    query = isAsc ? query.OrderBy(t => t.Name) : query.OrderByDescending(t => t.Name);
                    break;
                case "categoryid":
                    query = isAsc ? query.OrderBy(t => t.CarCategory.Name) : query.OrderByDescending(t => t.CarCategory.Name);
                    break;
                default:
                    break;
            }

            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;
        }

        public IQueryable<Domain.CarModel> GetCarModels()
        {
            return carModelRepository
                    .Get
                    .Include(t => t.CarCategory)
                    .OrderBy(c => c.Name)
                    .AsQueryable();
        }
        public CarModel GetCarModel(Guid carModelId)
        {
            return carModelRepository
                    .Get
                    .Include(t => t.CarCategory)
                    .SingleOrDefault(t => t.Id.Equals(carModelId));
        }
        public OperationStatus UpdateCarModel(Domain.CarModel carModel)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                carModelRepository.Update(carModel);
                carRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }
        public OperationStatus DeleteCarModel(Guid carModelId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var car = carModelRepository.Get.SingleOrDefault(t => t.Id.Equals(carModelId));
                if (car != null)
                {
                    carModelRepository.Remove(car);
                    carModelRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "Car model not found";
                }
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }
        public OperationStatus AddCarModel(CarModel carModel)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                carModelRepository.Add(carModel);
                carModelRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        #endregion

        #region Car Category
        public IQueryable<Domain.CarCategory> GetCarCategories()
        {
            return carCategoryRepository
                    .Get
                    .Include(t => t.CarModels)
                    .OrderBy(c => c.Name)
                    .AsQueryable();
        }

        public IQueryable<Domain.CarCategory> SearchCarCategory(CarCategoryCriteria criteria, ref int totalRecords)
        {
            Func<CarCategory, bool> exp = null;

            exp = t => ((string.IsNullOrEmpty(criteria.Name) || t.Name.Contains(criteria.Name) || criteria.Name.Contains(t.Name))
                            && ((criteria.Id == null || criteria.Id == Guid.Empty) || t.Id.Equals(criteria.Id)));


            var query = carCategoryRepository.Get
                         .Where(exp)
                         .AsQueryable();

            totalRecords = query.Count();

            criteria.SortColumn = string.IsNullOrEmpty(criteria.SortColumn) ? string.Empty : criteria.SortColumn.ToLower();

            bool isAsc = criteria.SortDirection.Equals("False");

            switch (criteria.SortColumn)
            {
                case "name":
                    query = isAsc ? query.OrderBy(t => t.Name) : query.OrderByDescending(t => t.Name);
                    break;
                default:
                    break;
            }

            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;


        }

        public CarCategory GetCarCategory(Guid carCategoryId)
        {
            return carCategoryRepository
                    .Get
                    .Include(t => t.CarModels)
                    .SingleOrDefault(t => t.Id.Equals(carCategoryId));
        }
        public OperationStatus UpdateCarCategory(Domain.CarCategory carCategory)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                carCategoryRepository.Update(carCategory);
                carCategoryRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }
        public OperationStatus DeleteCarCategory(Guid carCategoryId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var car = carCategoryRepository.Get.SingleOrDefault(t => t.Id.Equals(carCategoryId));
                if (car != null)
                {
                    carCategoryRepository.Remove(car);
                    carCategoryRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "Car Category not found";
                }
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }
        public OperationStatus AddCarCategory(CarCategory carCategory)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                carCategoryRepository.Add(carCategory);
                carCategoryRepository.Commit();
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
