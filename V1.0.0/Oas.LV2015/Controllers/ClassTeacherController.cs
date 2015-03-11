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
    public class ClassTeacherController : ApiController
    {
        #region fields
        private readonly IClassTeacherService classteachersService = null;
        #endregion
		
		#region constructors
        
		public ClassTeacherController(IClassTeacherService classteachersService)
        {
            this.classteachersService = classteachersService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetClassTeacherById(Guid id)
        {
            var classteachers = classteachersService.GetClassTeacher(id);
            return Request.CreateResponse(HttpStatusCode.OK, classteachers);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchClassTeacher(ClassTeacherCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = classteachersService.SearchClassTeacher(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateClassTeacher(ClassTeacher classteachers)
        {
            var opStatus = classteachersService.UpdateClassTeacher(classteachers);
            if (opStatus.Status)
            {
                return Request.CreateResponse<ClassTeacher>(HttpStatusCode.Accepted, classteachers);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddClassTeacher(ClassTeacher classteachers)
        {
            classteachers.Id = Guid.NewGuid();
            var opStatus = classteachersService.AddClassTeacher(classteachers);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<ClassTeacher>(HttpStatusCode.Created, classteachers);
                string uri = Url.Link("DefaultApi", new { id = classteachers.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteClassTeacher(Guid id)
        {
            var opStatus = classteachersService.DeleteClassTeacher(id);

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
