using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IAdvertismentService
    {
        #region Advertisment

        IQueryable<Advertisment> GetAdvertisments();

        IQueryable<Advertisment> SearchAdvertisment(AdvertismentCriteria criteria, ref int totalRecords);

        Advertisment GetAdvertisment(Guid advertismentsId);

        OperationStatus AddAdvertisment(Advertisment advertisments);

        OperationStatus UpdateAdvertisment(Advertisment advertisments);

        OperationStatus DeleteAdvertisment(Guid advertismentsId);

        #endregion


    }
}
