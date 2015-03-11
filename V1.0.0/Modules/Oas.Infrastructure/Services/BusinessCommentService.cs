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
    public class BusinessCommentService : IBusinessCommentService
    {
        #region fields
        private readonly IRepository<BusinessComment> businesscommentsRepository;
        #endregion

		#region constructors
        public BusinessCommentService(IRepository<BusinessComment> businesscommentsRepository)
        {
            this.businesscommentsRepository = businesscommentsRepository;
        }
		#endregion


        #region public methods

        public IQueryable<BusinessComment> SearchBusinessComment(BusinessCommentCriteria criteria, ref int totalRecords)
        {
            var query = businesscommentsRepository
                       .Get
.Where(t=>(criteria.Id==null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id) )
&&(string.IsNullOrEmpty(criteria.UserId)|| ( t.UserId.Contains(criteria.UserId) || criteria.UserId.Contains(t.UserId) ))
&&(criteria.BusinessId==null || criteria.BusinessId == Guid.Empty || t.BusinessId.Equals(criteria.BusinessId) )
&&(criteria.BusinessRate==null || t.BusinessRate.Equals(criteria.BusinessRate) )
&&(string.IsNullOrEmpty(criteria.Comment)|| ( t.Comment.Contains(criteria.Comment) || criteria.Comment.Contains(t.Comment) ))
&&(string.IsNullOrEmpty(criteria.CreateDate)|| ( t.CreateDate.Contains(criteria.CreateDate) || criteria.CreateDate.Contains(t.CreateDate) ))
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
case "comment" :
query = isAsc ? query.OrderBy(t => t.Comment) : query.OrderByDescending(t => t.Comment);
break;
case "createdate" :
query = isAsc ? query.OrderBy(t => t.CreateDate) : query.OrderByDescending(t => t.CreateDate);
break;
default: break;}
		   #endregion
            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;
        }

        public IQueryable<BusinessComment> GetBusinessComments()
        {
            return businesscommentsRepository
                        .Get
						   #region sorting
						   
						   #endregion
                        .AsQueryable();


        }

        public BusinessComment GetBusinessComment(Guid businesscommentsId)
        {
            return businesscommentsRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(businesscommentsId));
        }

        public OperationStatus AddBusinessComment(BusinessComment businesscomments)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                businesscommentsRepository.Add(businesscomments);
                businesscommentsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateBusinessComment(BusinessComment businesscomments)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                businesscommentsRepository.Update(businesscomments);
                businesscommentsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteBusinessComment(Guid businesscommentsId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var businesscomments = businesscommentsRepository.Get.SingleOrDefault(t => t.Id.Equals(businesscommentsId));
                if (businesscomments != null)
                {
                    businesscommentsRepository.Remove(businesscomments);
                    businesscommentsRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "BusinessComment not found";
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
