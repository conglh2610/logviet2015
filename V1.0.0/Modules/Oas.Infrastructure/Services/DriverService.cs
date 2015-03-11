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
    public class DriverService : IDriverService
    {
        #region fields
        private readonly IRepository<Driver> driversRepository;
        #endregion

		#region constructors
        public DriverService(IRepository<Driver> driversRepository)
        {
            this.driversRepository = driversRepository;
        }
		#endregion


        #region public methods

        public IQueryable<Driver> SearchDriver(DriverCriteria criteria, ref int totalRecords)
        {
            var query = driversRepository
                       .Get
.Where(t=>(criteria.Id==null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id) )
&&(string.IsNullOrEmpty(criteria.Name)|| ( t.Name.Contains(criteria.Name) || criteria.Name.Contains(t.Name) ))
&&(string.IsNullOrEmpty(criteria.Address)|| ( t.Address.Contains(criteria.Address) || criteria.Address.Contains(t.Address) ))
&&(string.IsNullOrEmpty(criteria.Phone)|| ( t.Phone.Contains(criteria.Phone) || criteria.Phone.Contains(t.Phone) ))
&&(string.IsNullOrEmpty(criteria.DriverLevel)|| ( t.DriverLevel.Contains(criteria.DriverLevel) || criteria.DriverLevel.Contains(t.DriverLevel) ))
&&(string.IsNullOrEmpty(criteria.DriverLicense)|| ( t.DriverLicense.Contains(criteria.DriverLicense) || criteria.DriverLicense.Contains(t.DriverLicense) ))
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
case "address" :
query = isAsc ? query.OrderBy(t => t.Address) : query.OrderByDescending(t => t.Address);
break;
case "phone" :
query = isAsc ? query.OrderBy(t => t.Phone) : query.OrderByDescending(t => t.Phone);
break;
case "driverlevel" :
query = isAsc ? query.OrderBy(t => t.DriverLevel) : query.OrderByDescending(t => t.DriverLevel);
break;
case "driverlicense" :
query = isAsc ? query.OrderBy(t => t.DriverLicense) : query.OrderByDescending(t => t.DriverLicense);
break;
default: break;}
		   #endregion
            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;
        }

        public IQueryable<Driver> GetDrivers()
        {
            return driversRepository
                        .Get
						   #region sorting
						   
						   #endregion
                        .AsQueryable();


        }

        public Driver GetDriver(Guid driversId)
        {
            return driversRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(driversId));
        }

        public OperationStatus AddDriver(Driver drivers)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                driversRepository.Add(drivers);
                driversRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateDriver(Driver drivers)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                driversRepository.Update(drivers);
                driversRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteDriver(Guid driversId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var drivers = driversRepository.Get.SingleOrDefault(t => t.Id.Equals(driversId));
                if (drivers != null)
                {
                    driversRepository.Remove(drivers);
                    driversRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "Driver not found";
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
