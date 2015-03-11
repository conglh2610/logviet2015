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
    public class BusinessService : IBusinessService
    {
        #region fields
        private readonly IRepository<Business> businessesRepository;
        #endregion

		#region constructors
        public BusinessService(IRepository<Business> businessesRepository)
        {
            this.businessesRepository = businessesRepository;
        }
		#endregion


        #region public methods

        public IQueryable<Business> SearchBusiness(BusinessCriteria criteria, ref int totalRecords)
        {
            var query = businessesRepository
                       .Get
.Where(t=>(criteria.Id==null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id) )
&&(string.IsNullOrEmpty(criteria.UserId)|| ( t.UserId.Contains(criteria.UserId) || criteria.UserId.Contains(t.UserId) ))
&&(criteria.BusinessCategoryId==null || criteria.BusinessCategoryId == Guid.Empty || t.BusinessCategoryId.Equals(criteria.BusinessCategoryId) )
&&(criteria.UpdatedDate==null || t.UpdatedDate.Equals(criteria.UpdatedDate) )
&&(string.IsNullOrEmpty(criteria.Zipcode)|| ( t.Zipcode.Contains(criteria.Zipcode) || criteria.Zipcode.Contains(t.Zipcode) ))
&&(string.IsNullOrEmpty(criteria.Name)|| ( t.Name.Contains(criteria.Name) || criteria.Name.Contains(t.Name) ))
&&(string.IsNullOrEmpty(criteria.Address)|| ( t.Address.Contains(criteria.Address) || criteria.Address.Contains(t.Address) ))
&&(string.IsNullOrEmpty(criteria.Phone)|| ( t.Phone.Contains(criteria.Phone) || criteria.Phone.Contains(t.Phone) ))
&&(string.IsNullOrEmpty(criteria.Email)|| ( t.Email.Contains(criteria.Email) || criteria.Email.Contains(t.Email) ))
&&(string.IsNullOrEmpty(criteria.Information)|| ( t.Information.Contains(criteria.Information) || criteria.Information.Contains(t.Information) ))
&&(string.IsNullOrEmpty(criteria.SortDescription)|| ( t.SortDescription.Contains(criteria.SortDescription) || criteria.SortDescription.Contains(t.SortDescription) ))
&&(string.IsNullOrEmpty(criteria.Facebook)|| ( t.Facebook.Contains(criteria.Facebook) || criteria.Facebook.Contains(t.Facebook) ))
&&(string.IsNullOrEmpty(criteria.Twitter)|| ( t.Twitter.Contains(criteria.Twitter) || criteria.Twitter.Contains(t.Twitter) ))
&&(criteria.Status==null || t.Status.Equals(criteria.Status) )
&&(criteria.Latitude==null || t.Latitude.Equals(criteria.Latitude) )
&&(criteria.Longtitude==null || t.Longtitude.Equals(criteria.Longtitude) )
&&(criteria.Active==null || t.Active.Equals(criteria.Active) )
&&(criteria.CreateDate==null || ( t.CreateDate.Equals(criteria.CreateDate) || criteria.CreateDate.Equals(t.CreateDate) ))
&&(string.IsNullOrEmpty(criteria.CreateBy)|| ( t.CreateBy.Contains(criteria.CreateBy) || criteria.CreateBy.Contains(t.CreateBy) ))
&&(criteria.TotalViewed==null || t.TotalViewed.Equals(criteria.TotalViewed) )
)
                       .AsQueryable();

            totalRecords = query.Count();

            criteria.SortColumn = string.IsNullOrEmpty(criteria.SortColumn) ? string.Empty : criteria.SortColumn.ToLower();
            bool isAsc = criteria.SortDirection.ToLower().Equals("false");

           #region sorting
switch (criteria.SortColumn){
case "userid" :
query = isAsc ? query.OrderBy(t => t.UserId) : query.OrderByDescending(t => t.UserId);
break;
case "zipcode" :
query = isAsc ? query.OrderBy(t => t.Zipcode) : query.OrderByDescending(t => t.Zipcode);
break;
case "name" :
query = isAsc ? query.OrderBy(t => t.Name) : query.OrderByDescending(t => t.Name);
break;
case "address" :
query = isAsc ? query.OrderBy(t => t.Address) : query.OrderByDescending(t => t.Address);
break;
case "phone" :
query = isAsc ? query.OrderBy(t => t.Phone) : query.OrderByDescending(t => t.Phone);
break;
case "email" :
query = isAsc ? query.OrderBy(t => t.Email) : query.OrderByDescending(t => t.Email);
break;
case "information" :
query = isAsc ? query.OrderBy(t => t.Information) : query.OrderByDescending(t => t.Information);
break;
case "sortdescription" :
query = isAsc ? query.OrderBy(t => t.SortDescription) : query.OrderByDescending(t => t.SortDescription);
break;
case "facebook" :
query = isAsc ? query.OrderBy(t => t.Facebook) : query.OrderByDescending(t => t.Facebook);
break;
case "twitter" :
query = isAsc ? query.OrderBy(t => t.Twitter) : query.OrderByDescending(t => t.Twitter);
break;
case "createdate" :
query = isAsc ? query.OrderBy(t => t.CreateDate) : query.OrderByDescending(t => t.CreateDate);
break;
case "createby" :
query = isAsc ? query.OrderBy(t => t.CreateBy) : query.OrderByDescending(t => t.CreateBy);
break;
default: break;}
		   #endregion
            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;
        }

        public IQueryable<Business> GetBusinesss()
        {
            return businessesRepository
                        .Get
						   #region sorting
						   
						   #endregion
                        .AsQueryable();


        }

        public Business GetBusiness(Guid businessesId)
        {
            return businessesRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(businessesId));
        }

        public OperationStatus AddBusiness(Business businesses)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                businessesRepository.Add(businesses);
                businessesRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateBusiness(Business businesses)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                businessesRepository.Update(businesses);
                businessesRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteBusiness(Guid businessesId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var businesses = businessesRepository.Get.SingleOrDefault(t => t.Id.Equals(businessesId));
                if (businesses != null)
                {
                    businessesRepository.Remove(businesses);
                    businessesRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "Business not found";
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
