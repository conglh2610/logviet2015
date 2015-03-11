using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface ICarBookingService
    {
        #region CarBooking

        IQueryable<CarBooking> GetCarBookings();

        IQueryable<CarBooking> SearchCarBooking(CarBookingCriteria criteria, ref int totalRecords);

        CarBooking GetCarBooking(Guid carbookingsId);

        OperationStatus AddCarBooking(CarBooking carbookings);

        OperationStatus UpdateCarBooking(CarBooking carbookings);

        OperationStatus DeleteCarBooking(Guid carbookingsId);

        #endregion


    }
}
