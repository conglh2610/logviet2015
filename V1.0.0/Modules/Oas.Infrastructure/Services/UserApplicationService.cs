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
    public class UserApplicationService : IUserApplicationService
    {
        #region fields
        private readonly IRepository<UserApplication> userapplicationsRepository;
        #endregion

		#region constructors
        public UserApplicationService(IRepository<UserApplication> userapplicationsRepository)
        {
            this.userapplicationsRepository = userapplicationsRepository;
        }
		#endregion


        #region public methods

        public IQueryable<UserApplication> SearchUserApplication(UserApplicationCriteria criteria, ref int totalRecords)
        {
            var query = userapplicationsRepository
                       .Get
.Where(t=>(criteria.Id==null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id) )
&&(string.IsNullOrEmpty(criteria.UserId)|| ( t.UserId.Contains(criteria.UserId) || criteria.UserId.Contains(t.UserId) ))
&&(criteria.ApplicationId==null || criteria.ApplicationId == Guid.Empty || t.ApplicationId.Equals(criteria.ApplicationId) )
&&(criteria.Price==null || t.Price.Equals(criteria.Price) )
&&(criteria.Status==null || t.Status.Equals(criteria.Status) )
&&(criteria.CreatedDate==null || ( t.CreatedDate.Equals(criteria.CreatedDate) || criteria.CreatedDate.Equals(t.CreatedDate) ))
&&(string.IsNullOrEmpty(criteria.CreateBy)|| ( t.CreateBy.Contains(criteria.CreateBy) || criteria.CreateBy.Contains(t.CreateBy) ))
&&(criteria.ExpireDate==null || ( t.ExpireDate.Equals(criteria.ExpireDate) || criteria.ExpireDate.Equals(t.ExpireDate) ))
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
case "createddate" :
query = isAsc ? query.OrderBy(t => t.CreatedDate) : query.OrderByDescending(t => t.CreatedDate);
break;
case "createby" :
query = isAsc ? query.OrderBy(t => t.CreateBy) : query.OrderByDescending(t => t.CreateBy);
break;
case "expiredate" :
query = isAsc ? query.OrderBy(t => t.ExpireDate) : query.OrderByDescending(t => t.ExpireDate);
break;
default: break;}
		   #endregion
            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;
        }

        public IQueryable<UserApplication> GetUserApplications()
        {
            return userapplicationsRepository
                        .Get
						   #region sorting
						   
						   #endregion
                        .AsQueryable();


        }

        public UserApplication GetUserApplication(Guid userapplicationsId)
        {
            return userapplicationsRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(userapplicationsId));
        }

        public OperationStatus AddUserApplication(UserApplication userapplications)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                userapplicationsRepository.Add(userapplications);
                userapplicationsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateUserApplication(UserApplication userapplications)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                userapplicationsRepository.Update(userapplications);
                userapplicationsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteUserApplication(Guid userapplicationsId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var userapplications = userapplicationsRepository.Get.SingleOrDefault(t => t.Id.Equals(userapplicationsId));
                if (userapplications != null)
                {
                    userapplicationsRepository.Remove(userapplications);
                    userapplicationsRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "UserApplication not found";
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
