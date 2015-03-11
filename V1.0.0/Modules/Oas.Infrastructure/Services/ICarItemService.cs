using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface ICarItemService
    {
        #region CarItem

        IQueryable<CarItem> GetCarItems();

        IQueryable<CarItem> SearchCarItem(CarItemCriteria criteria, ref int totalRecords);

        CarItem GetCarItem(Guid caritemsId);

        OperationStatus AddCarItem(CarItem caritems);

        OperationStatus UpdateCarItem(CarItem caritems);

        OperationStatus DeleteCarItem(Guid caritemsId);

        #endregion


    }
}
