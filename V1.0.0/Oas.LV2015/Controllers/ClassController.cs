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
    public class ClassController : ApiController
    {
        #region fields
        private readonly IClassService classesService = null;
        #endregion
		
		#region constructors
        
		public ClassController(IClassService classesService)
        {
            this.classesService = classesService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetClassById(Guid id)
        {
            var classes = classesService.GetClass(id);
            return Request.CreateResponse(HttpStatusCode.OK, classes);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchClass(ClassCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = classesService.SearchClass(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateClass(Class classes)
        {
            var opStatus = classesService.UpdateClass(classes);
            if (opStatus.Status)
            {
                return Request.CreateResponse<Class>(HttpStatusCode.Accepted, classes);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddClass(Class classes)
        {
            classes.Id = Guid.NewGuid();
            var opStatus = classesService.AddClass(classes);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<Class>(HttpStatusCode.Created, classes);
                string uri = Url.Link("DefaultApi", new { id = classes.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteClass(Guid id)
        {
            var opStatus = classesService.DeleteClass(id);

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
