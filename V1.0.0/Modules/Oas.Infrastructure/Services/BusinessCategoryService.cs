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
    public class BusinessCategoryService : IBusinessCategoryService
    {
        #region fields
        private readonly IRepository<BusinessCategory> businesscategoriesRepository;
        #endregion

		#region constructors
        public BusinessCategoryService(IRepository<BusinessCategory> businesscategoriesRepository)
        {
            this.businesscategoriesRepository = businesscategoriesRepository;
        }
		#endregion


        #region public methods

        public IQueryable<BusinessCategory> SearchBusinessCategory(BusinessCategoryCriteria criteria, ref int totalRecords)
        {
            var query = businesscategoriesRepository
                       .Get
.Where(t=>(criteria.Id==null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id) )
&&(criteria.ParentId==null || criteria.ParentId == Guid.Empty || t.ParentId.Equals(criteria.ParentId) )
&&(string.IsNullOrEmpty(criteria.Name)|| ( t.Name.Contains(criteria.Name) || criteria.Name.Contains(t.Name) ))
&&(string.IsNullOrEmpty(criteria.GooglePlaceIconUrl)|| ( t.GooglePlaceIconUrl.Contains(criteria.GooglePlaceIconUrl) || criteria.GooglePlaceIconUrl.Contains(t.GooglePlaceIconUrl) ))
&&(criteria.CategoryId==null || t.CategoryId.Equals(criteria.CategoryId) )
)
                       .AsQueryable();

            totalRecords = query.Count();

            criteria.SortColumn = string.IsNullOrEmpty(criteria.SortColumn) ? string.Empty : criteria.SortColumn.ToLower();
            bool isAsc = criteria.SortDirection.ToLower().Equals("false");

           #region sorting
switch (criteria.SortColumn){
case "name" :
query = isAsc ? query.OrderBy(t => t.Name) : query.OrderByDescending(t => t.Name);
break;
case "googleplaceiconurl" :
query = isAsc ? query.OrderBy(t => t.GooglePlaceIconUrl) : query.OrderByDescending(t => t.GooglePlaceIconUrl);
break;
default: break;}
		   #endregion
            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;
        }

        public IQueryable<BusinessCategory> GetBusinessCategorys()
        {
            return businesscategoriesRepository
                        .Get
						   #region sorting
						   
						   #endregion
                        .AsQueryable();


        }

        public BusinessCategory GetBusinessCategory(Guid businesscategoriesId)
        {
            return businesscategoriesRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(businesscategoriesId));
        }

        public OperationStatus AddBusinessCategory(BusinessCategory businesscategories)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                businesscategoriesRepository.Add(businesscategories);
                businesscategoriesRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateBusinessCategory(BusinessCategory businesscategories)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                businesscategoriesRepository.Update(businesscategories);
                businesscategoriesRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteBusinessCategory(Guid businesscategoriesId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var businesscategories = businesscategoriesRepository.Get.SingleOrDefault(t => t.Id.Equals(businesscategoriesId));
                if (businesscategories != null)
                {
                    businesscategoriesRepository.Remove(businesscategories);
                    businesscategoriesRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "BusinessCategory not found";
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
