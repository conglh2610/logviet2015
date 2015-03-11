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
    public class BusinessCommentController : ApiController
    {
        #region fields
        private readonly IBusinessCommentService businesscommentsService = null;
        #endregion
		
		#region constructors
        
		public BusinessCommentController(IBusinessCommentService businesscommentsService)
        {
            this.businesscommentsService = businesscommentsService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetBusinessCommentById(Guid id)
        {
            var businesscomments = businesscommentsService.GetBusinessComment(id);
            return Request.CreateResponse(HttpStatusCode.OK, businesscomments);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchBusinessComment(BusinessCommentCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = businesscommentsService.SearchBusinessComment(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateBusinessComment(BusinessComment businesscomments)
        {
            var opStatus = businesscommentsService.UpdateBusinessComment(businesscomments);
            if (opStatus.Status)
            {
                return Request.CreateResponse<BusinessComment>(HttpStatusCode.Accepted, businesscomments);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddBusinessComment(BusinessComment businesscomments)
        {
            businesscomments.Id = Guid.NewGuid();
            var opStatus = businesscommentsService.AddBusinessComment(businesscomments);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<BusinessComment>(HttpStatusCode.Created, businesscomments);
                string uri = Url.Link("DefaultApi", new { id = businesscomments.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteBusinessComment(Guid id)
        {
            var opStatus = businesscommentsService.DeleteBusinessComment(id);

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
