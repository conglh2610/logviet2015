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
    public class CarCategoryService : ICarCategoryService
    {
        #region fields
        private readonly IRepository<CarCategory> carcategoriesRepository;
        #endregion

        #region constructors
        public CarCategoryService(IRepository<CarCategory> carcategoriesRepository)
        {
            this.carcategoriesRepository = carcategoriesRepository;
        }
        #endregion

        #region public methods

        public IQueryable<CarCategory> SearchCarCategory(CarCategoryCriteria criteria, ref int totalRecords)
        {
            var query = carcategoriesRepository
                       .Get
                        .Where(t => (criteria.Id == null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id))
                        && (string.IsNullOrEmpty(criteria.Name) || (t.Name.Contains(criteria.Name) || criteria.Name.Contains(t.Name)))
                        )
                       .AsQueryable();

            totalRecords = query.Count();

            criteria.SortColumn = string.IsNullOrEmpty(criteria.SortColumn) ? string.Empty : criteria.SortColumn.ToLower();
            bool isAsc = criteria.SortDirection.ToLower().Equals("false");

            switch (criteria.SortColumn)
            {
                case "name":
                    query = isAsc ? query.OrderBy(t => t.Name) : query.OrderByDescending(t => t.Name);
                    break;
                default: break;
            }
            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;
        }

        public IQueryable<CarCategory> GetCarCategorys()
        {
            return carcategoriesRepository
                        .Get
                        .AsQueryable();
        }

        public CarCategory GetCarCategory(Guid carcategoriesId)
        {
            return carcategoriesRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(carcategoriesId));
        }

        public OperationStatus AddCarCategory(CarCategory carcategories)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                carcategoriesRepository.Add(carcategories);
                carcategoriesRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateCarCategory(CarCategory carcategories)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                carcategoriesRepository.Update(carcategories);
                carcategoriesRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteCarCategory(Guid carcategoriesId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var carcategories = carcategoriesRepository.Get.SingleOrDefault(t => t.Id.Equals(carcategoriesId));
                if (carcategories != null)
                {
                    carcategoriesRepository.Remove(carcategories);
                    carcategoriesRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "CarCategory not found";
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
