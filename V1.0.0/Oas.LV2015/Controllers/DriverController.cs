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
    public class DriverController : ApiController
    {
        #region fields
        private readonly IDriverService driversService = null;
        #endregion
		
		#region constructors
        
		public DriverController(IDriverService driversService)
        {
            this.driversService = driversService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetDriverById(Guid id)
        {
            var drivers = driversService.GetDriver(id);
            return Request.CreateResponse(HttpStatusCode.OK, drivers);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchDriver(DriverCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = driversService.SearchDriver(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateDriver(Driver drivers)
        {
            var opStatus = driversService.UpdateDriver(drivers);
            if (opStatus.Status)
            {
                return Request.CreateResponse<Driver>(HttpStatusCode.Accepted, drivers);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddDriver(Driver drivers)
        {
            drivers.Id = Guid.NewGuid();
            var opStatus = driversService.AddDriver(drivers);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<Driver>(HttpStatusCode.Created, drivers);
                string uri = Url.Link("DefaultApi", new { id = drivers.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteDriver(Guid id)
        {
            var opStatus = driversService.DeleteDriver(id);

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
