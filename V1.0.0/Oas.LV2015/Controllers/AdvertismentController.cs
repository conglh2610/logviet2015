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
    public class AdvertismentController : ApiController
    {
        #region fields
        private readonly IAdvertismentService advertismentsService = null;
        #endregion
		
		#region constructors
        
		public AdvertismentController(IAdvertismentService advertismentsService)
        {
            this.advertismentsService = advertismentsService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetAdvertismentById(Guid id)
        {
            var advertisments = advertismentsService.GetAdvertisment(id);
            return Request.CreateResponse(HttpStatusCode.OK, advertisments);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchAdvertisment(AdvertismentCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = advertismentsService.SearchAdvertisment(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateAdvertisment(Advertisment advertisments)
        {
            var opStatus = advertismentsService.UpdateAdvertisment(advertisments);
            if (opStatus.Status)
            {
                return Request.CreateResponse<Advertisment>(HttpStatusCode.Accepted, advertisments);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddAdvertisment(Advertisment advertisments)
        {
            advertisments.Id = Guid.NewGuid();
            var opStatus = advertismentsService.AddAdvertisment(advertisments);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<Advertisment>(HttpStatusCode.Created, advertisments);
                string uri = Url.Link("DefaultApi", new { id = advertisments.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteAdvertisment(Guid id)
        {
            var opStatus = advertismentsService.DeleteAdvertisment(id);

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
