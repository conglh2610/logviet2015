using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IDriverService
    {
        #region Driver

        IQueryable<Driver> GetDrivers();

        IQueryable<Driver> SearchDriver(DriverCriteria criteria, ref int totalRecords);

        Driver GetDriver(Guid driversId);

        OperationStatus AddDriver(Driver drivers);

        OperationStatus UpdateDriver(Driver drivers);

        OperationStatus DeleteDriver(Guid driversId);

        #endregion


    }
}
