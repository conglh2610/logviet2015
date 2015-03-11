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
    public class BusinessController : ApiController
    {
        #region fields
        private readonly IBusinessService businessesService = null;
        #endregion
		
		#region constructors
        
		public BusinessController(IBusinessService businessesService)
        {
            this.businessesService = businessesService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetBusinessById(Guid id)
        {
            var businesses = businessesService.GetBusiness(id);
            return Request.CreateResponse(HttpStatusCode.OK, businesses);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchBusiness(BusinessCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = businessesService.SearchBusiness(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateBusiness(Business businesses)
        {
            var opStatus = businessesService.UpdateBusiness(businesses);
            if (opStatus.Status)
            {
                return Request.CreateResponse<Business>(HttpStatusCode.Accepted, businesses);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddBusiness(Business businesses)
        {
            businesses.Id = Guid.NewGuid();
            var opStatus = businessesService.AddBusiness(businesses);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<Business>(HttpStatusCode.Created, businesses);
                string uri = Url.Link("DefaultApi", new { id = businesses.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteBusiness(Guid id)
        {
            var opStatus = businessesService.DeleteBusiness(id);

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
