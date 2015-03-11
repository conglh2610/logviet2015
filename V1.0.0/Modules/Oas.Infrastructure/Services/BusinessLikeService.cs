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
    public class BusinessLikeService : IBusinessLikeService
    {
        #region fields
        private readonly IRepository<BusinessLike> businesslikesRepository;
        #endregion

		#region constructors
        public BusinessLikeService(IRepository<BusinessLike> businesslikesRepository)
        {
            this.businesslikesRepository = businesslikesRepository;
        }
		#endregion


        #region public methods

        public IQueryable<BusinessLike> SearchBusinessLike(BusinessLikeCriteria criteria, ref int totalRecords)
        {
            var query = businesslikesRepository
                       .Get
.Where(t=>(criteria.Id==null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id) )
&&(string.IsNullOrEmpty(criteria.UserId)|| ( t.UserId.Contains(criteria.UserId) || criteria.UserId.Contains(t.UserId) ))
&&(criteria.BusinessId==null || criteria.BusinessId == Guid.Empty || t.BusinessId.Equals(criteria.BusinessId) )
&&(criteria.Like==null || t.Like.Equals(criteria.Like) )
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
default: break;}
		   #endregion
            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;
        }

        public IQueryable<BusinessLike> GetBusinessLikes()
        {
            return businesslikesRepository
                        .Get
						   #region sorting
						   
						   #endregion
                        .AsQueryable();


        }

        public BusinessLike GetBusinessLike(Guid businesslikesId)
        {
            return businesslikesRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(businesslikesId));
        }

        public OperationStatus AddBusinessLike(BusinessLike businesslikes)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                businesslikesRepository.Add(businesslikes);
                businesslikesRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateBusinessLike(BusinessLike businesslikes)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                businesslikesRepository.Update(businesslikes);
                businesslikesRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteBusinessLike(Guid businesslikesId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var businesslikes = businesslikesRepository.Get.SingleOrDefault(t => t.Id.Equals(businesslikesId));
                if (businesslikes != null)
                {
                    businesslikesRepository.Remove(businesslikes);
                    businesslikesRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "BusinessLike not found";
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
