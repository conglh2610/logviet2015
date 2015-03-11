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
    public class CarBookingService : ICarBookingService
    {
        #region fields
        private readonly IRepository<CarBooking> carbookingsRepository;
        #endregion

		#region constructors
        public CarBookingService(IRepository<CarBooking> carbookingsRepository)
        {
            this.carbookingsRepository = carbookingsRepository;
        }
		#endregion


        #region public methods

        public IQueryable<CarBooking> SearchCarBooking(CarBookingCriteria criteria, ref int totalRecords)
        {
            var query = carbookingsRepository
                       .Get
.Where(t=>(criteria.Id==null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id) )
&&(criteria.CarItemId==null || criteria.CarItemId == Guid.Empty || t.CarItemId.Equals(criteria.CarItemId) )
&&(string.IsNullOrEmpty(criteria.UserId)|| ( t.UserId.Contains(criteria.UserId) || criteria.UserId.Contains(t.UserId) ))
&&(criteria.DriverId==null || criteria.DriverId == Guid.Empty || t.DriverId.Equals(criteria.DriverId) )
&&(criteria.IsNeededDriver==null || t.IsNeededDriver.Equals(criteria.IsNeededDriver) )
&&(criteria.BookFromDate==null || ( t.BookFromDate.Equals(criteria.BookFromDate) || criteria.BookFromDate.Equals(t.BookFromDate) ))
&&(criteria.BookToDate==null || ( t.BookToDate.Equals(criteria.BookToDate) || criteria.BookToDate.Equals(t.BookToDate) ))
&&(criteria.BookType==null || t.BookType.Equals(criteria.BookType) )
&&(criteria.TotalDay==null || t.TotalDay.Equals(criteria.TotalDay) )
&&(string.IsNullOrEmpty(criteria.Schduling)|| ( t.Schduling.Contains(criteria.Schduling) || criteria.Schduling.Contains(t.Schduling) ))
&&(criteria.BookStatus==null || t.BookStatus.Equals(criteria.BookStatus) )
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
case "bookfromdate" :
query = isAsc ? query.OrderBy(t => t.BookFromDate) : query.OrderByDescending(t => t.BookFromDate);
break;
case "booktodate" :
query = isAsc ? query.OrderBy(t => t.BookToDate) : query.OrderByDescending(t => t.BookToDate);
break;
case "schduling" :
query = isAsc ? query.OrderBy(t => t.Schduling) : query.OrderByDescending(t => t.Schduling);
break;
default: break;}
		   #endregion
            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;
        }

        public IQueryable<CarBooking> GetCarBookings()
        {
            return carbookingsRepository
                        .Get
						   #region sorting
						   
						   #endregion
                        .AsQueryable();


        }

        public CarBooking GetCarBooking(Guid carbookingsId)
        {
            return carbookingsRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(carbookingsId));
        }

        public OperationStatus AddCarBooking(CarBooking carbookings)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                carbookingsRepository.Add(carbookings);
                carbookingsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateCarBooking(CarBooking carbookings)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                carbookingsRepository.Update(carbookings);
                carbookingsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteCarBooking(Guid carbookingsId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var carbookings = carbookingsRepository.Get.SingleOrDefault(t => t.Id.Equals(carbookingsId));
                if (carbookings != null)
                {
                    carbookingsRepository.Remove(carbookings);
                    carbookingsRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "CarBooking not found";
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
