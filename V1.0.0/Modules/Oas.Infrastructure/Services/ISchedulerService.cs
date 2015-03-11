using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface ISchedulerService
    {
        #region Scheduler

        IQueryable<Scheduler> GetSchedulers();

        IQueryable<Scheduler> SearchScheduler(SchedulerCriteria criteria, ref int totalRecords);

        Scheduler GetScheduler(Guid schedulersId);

        OperationStatus AddScheduler(Scheduler schedulers);

        OperationStatus UpdateScheduler(Scheduler schedulers);

        OperationStatus DeleteScheduler(Guid schedulersId);

        #endregion


    }
}
