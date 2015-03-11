using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Services;
using Oas.Infrastructure.Domain;

namespace Oas.LV2015.Controllers
{
    public class MessageHistoryController : ApiController
    {
        #region fields
        private readonly IMessageHistoryService messagehistoriesService = null;
        #endregion
		
		#region constructors
        
		public MessageHistoryController(IMessageHistoryService messagehistoriesService)
        {
            this.messagehistoriesService = messagehistoriesService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetMessageHistoryById(Guid id)
        {
            var messagehistories = messagehistoriesService.GetMessageHistory(id);
            return Request.CreateResponse(HttpStatusCode.OK, messagehistories);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchMessageHistory(MessageHistoryCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = messagehistoriesService.SearchMessageHistory(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateMessageHistory(MessageHistory messagehistories)
        {
            var opStatus = messagehistoriesService.UpdateMessageHistory(messagehistories);
            if (opStatus.Status)
            {
                return Request.CreateResponse<MessageHistory>(HttpStatusCode.Accepted, messagehistories);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddMessageHistory(MessageHistory messagehistories)
        {
            messagehistories.Id = Guid.NewGuid();
            var opStatus = messagehistoriesService.AddMessageHistory(messagehistories);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<MessageHistory>(HttpStatusCode.Created, messagehistories);
                string uri = Url.Link("DefaultApi", new { id = messagehistories.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteMessageHistory(Guid id)
        {
            var opStatus = messagehistoriesService.DeleteMessageHistory(id);

            if (opStatus.Status)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
            }
        }
		
		#endregion
    }
}
