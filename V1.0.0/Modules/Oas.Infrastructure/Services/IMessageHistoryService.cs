using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IMessageHistoryService
    {
        #region MessageHistory

        IQueryable<MessageHistory> GetMessageHistorys();

        IQueryable<MessageHistory> SearchMessageHistory(MessageHistoryCriteria criteria, ref int totalRecords);

        MessageHistory GetMessageHistory(Guid messagehistoriesId);

        OperationStatus AddMessageHistory(MessageHistory messagehistories);

        OperationStatus UpdateMessageHistory(MessageHistory messagehistories);

        OperationStatus DeleteMessageHistory(Guid messagehistoriesId);

        #endregion


    }
}
