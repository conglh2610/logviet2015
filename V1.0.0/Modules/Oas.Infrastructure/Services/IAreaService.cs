using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IAreaService
    {
        #region Area

        IQueryable<Area> GetAreas();

        IQueryable<Area> SearchArea(AreaCriteria criteria, ref int totalRecords);

        Area GetArea(Guid areasId);

        OperationStatus AddArea(Area areas);

        OperationStatus UpdateArea(Area areas);

        OperationStatus DeleteArea(Guid areasId);

        #endregion


    }
}
