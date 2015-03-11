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
    public class PackageItemService : IPackageItemService
    {
        #region fields
        private readonly IRepository<PackageItem> packageitemsRepository;
        #endregion

		#region constructors
        public PackageItemService(IRepository<PackageItem> packageitemsRepository)
        {
            this.packageitemsRepository = packageitemsRepository;
        }
		#endregion


        #region public methods

        public IQueryable<PackageItem> SearchPackageItem(PackageItemCriteria criteria, ref int totalRecords)
        {
            var query = packageitemsRepository
                       .Get
.Where(t=>(criteria.Id==null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id) )
&&(string.IsNullOrEmpty(criteria.Name)|| ( t.Name.Contains(criteria.Name) || criteria.Name.Contains(t.Name) ))
&&(string.IsNullOrEmpty(criteria.Description)|| ( t.Description.Contains(criteria.Description) || criteria.Description.Contains(t.Description) ))
&&(criteria.MembershipPackageId==null || criteria.MembershipPackageId == Guid.Empty || t.MembershipPackageId.Equals(criteria.MembershipPackageId) )
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

        public IQueryable<PackageItem> GetPackageItems()
        {
            return packageitemsRepository
                        .Get
						   #region sorting
						   
						   #endregion
                        .AsQueryable();


        }

        public PackageItem GetPackageItem(Guid packageitemsId)
        {
            return packageitemsRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(packageitemsId));
        }

        public OperationStatus AddPackageItem(PackageItem packageitems)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                packageitemsRepository.Add(packageitems);
                packageitemsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdatePackageItem(PackageItem packageitems)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                packageitemsRepository.Update(packageitems);
                packageitemsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeletePackageItem(Guid packageitemsId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var packageitems = packageitemsRepository.Get.SingleOrDefault(t => t.Id.Equals(packageitemsId));
                if (packageitems != null)
                {
                    packageitemsRepository.Remove(packageitems);
                    packageitemsRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "PackageItem not found";
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
