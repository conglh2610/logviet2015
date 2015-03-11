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
    public class ClassStudentController : ApiController
    {
        #region fields
        private readonly IClassStudentService classstudentsService = null;
        #endregion
		
		#region constructors
        
		public ClassStudentController(IClassStudentService classstudentsService)
        {
            this.classstudentsService = classstudentsService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetClassStudentById(Guid id)
        {
            var classstudents = classstudentsService.GetClassStudent(id);
            return Request.CreateResponse(HttpStatusCode.OK, classstudents);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchClassStudent(ClassStudentCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = classstudentsService.SearchClassStudent(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateClassStudent(ClassStudent classstudents)
        {
            var opStatus = classstudentsService.UpdateClassStudent(classstudents);
            if (opStatus.Status)
            {
                return Request.CreateResponse<ClassStudent>(HttpStatusCode.Accepted, classstudents);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddClassStudent(ClassStudent classstudents)
        {
            classstudents.Id = Guid.NewGuid();
            var opStatus = classstudentsService.AddClassStudent(classstudents);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<ClassStudent>(HttpStatusCode.Created, classstudents);
                string uri = Url.Link("DefaultApi", new { id = classstudents.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteClassStudent(Guid id)
        {
            var opStatus = classstudentsService.DeleteClassStudent(id);

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
