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
    public class MessageHistoryService : IMessageHistoryService
    {
        #region fields
        private readonly IRepository<MessageHistory> messagehistoriesRepository;
        #endregion

		#region constructors
        public MessageHistoryService(IRepository<MessageHistory> messagehistoriesRepository)
        {
            this.messagehistoriesRepository = messagehistoriesRepository;
        }
		#endregion


        #region public methods

        public IQueryable<MessageHistory> SearchMessageHistory(MessageHistoryCriteria criteria, ref int totalRecords)
        {
            var query = messagehistoriesRepository
                       .Get
.Where(t=>(criteria.Id==null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id) )
&&(string.IsNullOrEmpty(criteria.Message)|| ( t.Message.Contains(criteria.Message) || criteria.Message.Contains(t.Message) ))
&&(string.IsNullOrEmpty(criteria.FromUserId)|| ( t.FromUserId.Contains(criteria.FromUserId) || criteria.FromUserId.Contains(t.FromUserId) ))
&&(string.IsNullOrEmpty(criteria.ToUserId)|| ( t.ToUserId.Contains(criteria.ToUserId) || criteria.ToUserId.Contains(t.ToUserId) ))
&&(criteria.Status==null || t.Status.Equals(criteria.Status) )
&&(criteria.CreateDate==null || ( t.CreateDate.Equals(criteria.CreateDate) || criteria.CreateDate.Equals(t.CreateDate) ))
)
                       .AsQueryable();

            totalRecords = query.Count();

            criteria.SortColumn = string.IsNullOrEmpty(criteria.SortColumn) ? string.Empty : criteria.SortColumn.ToLower();
            bool isAsc = criteria.SortDirection.ToLower().Equals("false");

           #region sorting
switch (criteria.SortColumn){
case "message" :
query = isAsc ? query.OrderBy(t => t.Message) : query.OrderByDescending(t => t.Message);
break;
case "fromuserid" :
query = isAsc ? query.OrderBy(t => t.FromUserId) : query.OrderByDescending(t => t.FromUserId);
break;
case "touserid" :
query = isAsc ? query.OrderBy(t => t.ToUserId) : query.OrderByDescending(t => t.ToUserId);
break;
case "createdate" :
query = isAsc ? query.OrderBy(t => t.CreateDate) : query.OrderByDescending(t => t.CreateDate);
break;
default: break;}
		   #endregion
            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;
        }

        public IQueryable<MessageHistory> GetMessageHistorys()
        {
            return messagehistoriesRepository
                        .Get
						   #region sorting
						   
						   #endregion
                        .AsQueryable();


        }

        public MessageHistory GetMessageHistory(Guid messagehistoriesId)
        {
            return messagehistoriesRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(messagehistoriesId));
        }

        public OperationStatus AddMessageHistory(MessageHistory messagehistories)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                messagehistoriesRepository.Add(messagehistories);
                messagehistoriesRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateMessageHistory(MessageHistory messagehistories)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                messagehistoriesRepository.Update(messagehistories);
                messagehistoriesRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteMessageHistory(Guid messagehistoriesId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var messagehistories = messagehistoriesRepository.Get.SingleOrDefault(t => t.Id.Equals(messagehistoriesId));
                if (messagehistories != null)
                {
                    messagehistoriesRepository.Remove(messagehistories);
                    messagehistoriesRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "MessageHistory not found";
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
