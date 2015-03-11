using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IBusinessCommentService
    {
        #region BusinessComment

        IQueryable<BusinessComment> GetBusinessComments();

        IQueryable<BusinessComment> SearchBusinessComment(BusinessCommentCriteria criteria, ref int totalRecords);

        BusinessComment GetBusinessComment(Guid businesscommentsId);

        OperationStatus AddBusinessComment(BusinessComment businesscomments);

        OperationStatus UpdateBusinessComment(BusinessComment businesscomments);

        OperationStatus DeleteBusinessComment(Guid businesscommentsId);

        #endregion


    }
}
