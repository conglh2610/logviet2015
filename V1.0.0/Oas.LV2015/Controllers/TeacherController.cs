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
    public class TeacherController : ApiController
    {
        #region fields
        private readonly ITeacherService teachersService = null;
        #endregion
		
		#region constructors
        
		public TeacherController(ITeacherService teachersService)
        {
            this.teachersService = teachersService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetTeacherById(Guid id)
        {
            var teachers = teachersService.GetTeacher(id);
            return Request.CreateResponse(HttpStatusCode.OK, teachers);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchTeacher(TeacherCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = teachersService.SearchTeacher(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateTeacher(Teacher teachers)
        {
            var opStatus = teachersService.UpdateTeacher(teachers);
            if (opStatus.Status)
            {
                return Request.CreateResponse<Teacher>(HttpStatusCode.Accepted, teachers);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddTeacher(Teacher teachers)
        {
            teachers.Id = Guid.NewGuid();
            var opStatus = teachersService.AddTeacher(teachers);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<Teacher>(HttpStatusCode.Created, teachers);
                string uri = Url.Link("DefaultApi", new { id = teachers.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteTeacher(Guid id)
        {
            var opStatus = teachersService.DeleteTeacher(id);

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
