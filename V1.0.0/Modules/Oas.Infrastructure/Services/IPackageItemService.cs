using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IPackageItemService
    {
        #region PackageItem

        IQueryable<PackageItem> GetPackageItems();

        IQueryable<PackageItem> SearchPackageItem(PackageItemCriteria criteria, ref int totalRecords);

        PackageItem GetPackageItem(Guid packageitemsId);

        OperationStatus AddPackageItem(PackageItem packageitems);

        OperationStatus UpdatePackageItem(PackageItem packageitems);

        OperationStatus DeletePackageItem(Guid packageitemsId);

        #endregion


    }
}
