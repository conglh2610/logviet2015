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
    public class ApplicationController : ApiController
    {
        #region fields
        private readonly IApplicationService applicationsService = null;
        #endregion
		
		#region constructors
        
		public ApplicationController(IApplicationService applicationsService)
        {
            this.applicationsService = applicationsService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetApplicationById(Guid id)
        {
            var applications = applicationsService.GetApplication(id);
            return Request.CreateResponse(HttpStatusCode.OK, applications);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchApplication(ApplicationCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = applicationsService.SearchApplication(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateApplication(Application applications)
        {
            var opStatus = applicationsService.UpdateApplication(applications);
            if (opStatus.Status)
            {
                return Request.CreateResponse<Application>(HttpStatusCode.Accepted, applications);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddApplication(Application applications)
        {
            applications.Id = Guid.NewGuid();
            var opStatus = applicationsService.AddApplication(applications);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<Application>(HttpStatusCode.Created, applications);
                string uri = Url.Link("DefaultApi", new { id = applications.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteApplication(Guid id)
        {
            var opStatus = applicationsService.DeleteApplication(id);

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
