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
    public class BusinessLikeController : ApiController
    {
        #region fields
        private readonly IBusinessLikeService businesslikesService = null;
        #endregion
		
		#region constructors
        
		public BusinessLikeController(IBusinessLikeService businesslikesService)
        {
            this.businesslikesService = businesslikesService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetBusinessLikeById(Guid id)
        {
            var businesslikes = businesslikesService.GetBusinessLike(id);
            return Request.CreateResponse(HttpStatusCode.OK, businesslikes);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchBusinessLike(BusinessLikeCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = businesslikesService.SearchBusinessLike(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateBusinessLike(BusinessLike businesslikes)
        {
            var opStatus = businesslikesService.UpdateBusinessLike(businesslikes);
            if (opStatus.Status)
            {
                return Request.CreateResponse<BusinessLike>(HttpStatusCode.Accepted, businesslikes);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddBusinessLike(BusinessLike businesslikes)
        {
            businesslikes.Id = Guid.NewGuid();
            var opStatus = businesslikesService.AddBusinessLike(businesslikes);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<BusinessLike>(HttpStatusCode.Created, businesslikes);
                string uri = Url.Link("DefaultApi", new { id = businesslikes.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteBusinessLike(Guid id)
        {
            var opStatus = businesslikesService.DeleteBusinessLike(id);

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
