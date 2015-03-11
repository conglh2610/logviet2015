using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IResultService
    {
        #region Result

        IQueryable<Result> GetResults();

        IQueryable<Result> SearchResult(ResultCriteria criteria, ref int totalRecords);

        Result GetResult(Guid resultsId);

        OperationStatus AddResult(Result results);

        OperationStatus UpdateResult(Result results);

        OperationStatus DeleteResult(Guid resultsId);

        #endregion


    }
}
