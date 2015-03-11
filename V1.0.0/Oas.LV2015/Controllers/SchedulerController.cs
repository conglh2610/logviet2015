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
    public class SchedulerController : ApiController
    {
        #region fields
        private readonly ISchedulerService schedulersService = null;
        #endregion
		
		#region constructors
        
		public SchedulerController(ISchedulerService schedulersService)
        {
            this.schedulersService = schedulersService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetSchedulerById(Guid id)
        {
            var schedulers = schedulersService.GetScheduler(id);
            return Request.CreateResponse(HttpStatusCode.OK, schedulers);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchScheduler(SchedulerCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = schedulersService.SearchScheduler(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateScheduler(Scheduler schedulers)
        {
            var opStatus = schedulersService.UpdateScheduler(schedulers);
            if (opStatus.Status)
            {
                return Request.CreateResponse<Scheduler>(HttpStatusCode.Accepted, schedulers);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddScheduler(Scheduler schedulers)
        {
            schedulers.Id = Guid.NewGuid();
            var opStatus = schedulersService.AddScheduler(schedulers);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<Scheduler>(HttpStatusCode.Created, schedulers);
                string uri = Url.Link("DefaultApi", new { id = schedulers.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteScheduler(Guid id)
        {
            var opStatus = schedulersService.DeleteScheduler(id);

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
