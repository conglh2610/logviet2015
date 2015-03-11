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
    public class ApplicationService : IApplicationService
    {
        #region fields
        private readonly IRepository<Application> applicationsRepository;
        #endregion

		#region constructors
        public ApplicationService(IRepository<Application> applicationsRepository)
        {
            this.applicationsRepository = applicationsRepository;
        }
		#endregion


        #region public methods

        public IQueryable<Application> SearchApplication(ApplicationCriteria criteria, ref int totalRecords)
        {
            var query = applicationsRepository
                       .Get
.Where(t=>(criteria.Id==null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id) )
&&(string.IsNullOrEmpty(criteria.Name)|| ( t.Name.Contains(criteria.Name) || criteria.Name.Contains(t.Name) ))
&&(string.IsNullOrEmpty(criteria.Description)|| ( t.Description.Contains(criteria.Description) || criteria.Description.Contains(t.Description) ))
&&(criteria.UnitPrice==null || t.UnitPrice.Equals(criteria.UnitPrice) )
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
case "description" :
query = isAsc ? query.OrderBy(t => t.Description) : query.OrderByDescending(t => t.Description);
break;
default: break;}
		   #endregion
            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;
        }

        public IQueryable<Application> GetApplications()
        {
            return applicationsRepository
                        .Get
						   #region sorting
						   
						   #endregion
                        .AsQueryable();


        }

        public Application GetApplication(Guid applicationsId)
        {
            return applicationsRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(applicationsId));
        }

        public OperationStatus AddApplication(Application applications)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                applicationsRepository.Add(applications);
                applicationsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateApplication(Application applications)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                applicationsRepository.Update(applications);
                applicationsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteApplication(Guid applicationsId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var applications = applicationsRepository.Get.SingleOrDefault(t => t.Id.Equals(applicationsId));
                if (applications != null)
                {
                    applicationsRepository.Remove(applications);
                    applicationsRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "Application not found";
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
