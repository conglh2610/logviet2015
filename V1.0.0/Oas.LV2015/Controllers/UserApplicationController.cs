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
    public class UserApplicationController : ApiController
    {
        #region fields
        private readonly IUserApplicationService userapplicationsService = null;
        #endregion
		
		#region constructors
        
		public UserApplicationController(IUserApplicationService userapplicationsService)
        {
            this.userapplicationsService = userapplicationsService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetUserApplicationById(Guid id)
        {
            var userapplications = userapplicationsService.GetUserApplication(id);
            return Request.CreateResponse(HttpStatusCode.OK, userapplications);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchUserApplication(UserApplicationCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = userapplicationsService.SearchUserApplication(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateUserApplication(UserApplication userapplications)
        {
            var opStatus = userapplicationsService.UpdateUserApplication(userapplications);
            if (opStatus.Status)
            {
                return Request.CreateResponse<UserApplication>(HttpStatusCode.Accepted, userapplications);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddUserApplication(UserApplication userapplications)
        {
            userapplications.Id = Guid.NewGuid();
            var opStatus = userapplicationsService.AddUserApplication(userapplications);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<UserApplication>(HttpStatusCode.Created, userapplications);
                string uri = Url.Link("DefaultApi", new { id = userapplications.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteUserApplication(Guid id)
        {
            var opStatus = userapplicationsService.DeleteUserApplication(id);

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
