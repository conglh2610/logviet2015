using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IBusinessLikeService
    {
        #region BusinessLike

        IQueryable<BusinessLike> GetBusinessLikes();

        IQueryable<BusinessLike> SearchBusinessLike(BusinessLikeCriteria criteria, ref int totalRecords);

        BusinessLike GetBusinessLike(Guid businesslikesId);

        OperationStatus AddBusinessLike(BusinessLike businesslikes);

        OperationStatus UpdateBusinessLike(BusinessLike businesslikes);

        OperationStatus DeleteBusinessLike(Guid businesslikesId);

        #endregion


    }
}
