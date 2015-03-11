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
    public class SchedulerService : ISchedulerService
    {
        #region fields
        private readonly IRepository<Scheduler> schedulersRepository;
        #endregion

		#region constructors
        public SchedulerService(IRepository<Scheduler> schedulersRepository)
        {
            this.schedulersRepository = schedulersRepository;
        }
		#endregion


        #region public methods

        public IQueryable<Scheduler> SearchScheduler(SchedulerCriteria criteria, ref int totalRecords)
        {
            var query = schedulersRepository
                       .Get
.Where(t=>(criteria.Id==null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id) )
&&(criteria.ClassId==null || criteria.ClassId == Guid.Empty || t.ClassId.Equals(criteria.ClassId) )
&&(criteria.RoomId==null || criteria.RoomId == Guid.Empty || t.RoomId.Equals(criteria.RoomId) )
&&(criteria.StudyDateId==null || criteria.StudyDateId == Guid.Empty || t.StudyDateId.Equals(criteria.StudyDateId) )
&&(criteria.StudyTimeId==null || criteria.StudyTimeId == Guid.Empty || t.StudyTimeId.Equals(criteria.StudyTimeId) )
)
                       .AsQueryable();

            totalRecords = query.Count();

            criteria.SortColumn = string.IsNullOrEmpty(criteria.SortColumn) ? string.Empty : criteria.SortColumn.ToLower();
            bool isAsc = criteria.SortDirection.ToLower().Equals("false");

           #region sorting
switch (criteria.SortColumn){
default: break;}
		   #endregion
            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;
        }

        public IQueryable<Scheduler> GetSchedulers()
        {
            return schedulersRepository
                        .Get
						   #region sorting
						   
						   #endregion
                        .AsQueryable();


        }

        public Scheduler GetScheduler(Guid schedulersId)
        {
            return schedulersRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(schedulersId));
        }

        public OperationStatus AddScheduler(Scheduler schedulers)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                schedulersRepository.Add(schedulers);
                schedulersRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateScheduler(Scheduler schedulers)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                schedulersRepository.Update(schedulers);
                schedulersRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteScheduler(Guid schedulersId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var schedulers = schedulersRepository.Get.SingleOrDefault(t => t.Id.Equals(schedulersId));
                if (schedulers != null)
                {
                    schedulersRepository.Remove(schedulers);
                    schedulersRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "Scheduler not found";
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
