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
    public class StudentController : ApiController
    {
        #region fields
        private readonly IStudentService studentsService = null;
        #endregion
		
		#region constructors
        
		public StudentController(IStudentService studentsService)
        {
            this.studentsService = studentsService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetStudentById(Guid id)
        {
            var students = studentsService.GetStudent(id);
            return Request.CreateResponse(HttpStatusCode.OK, students);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchStudent(StudentCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = studentsService.SearchStudent(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateStudent(Student students)
        {
            var opStatus = studentsService.UpdateStudent(students);
            if (opStatus.Status)
            {
                return Request.CreateResponse<Student>(HttpStatusCode.Accepted, students);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddStudent(Student students)
        {
            students.Id = Guid.NewGuid();
            var opStatus = studentsService.AddStudent(students);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<Student>(HttpStatusCode.Created, students);
                string uri = Url.Link("DefaultApi", new { id = students.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteStudent(Guid id)
        {
            var opStatus = studentsService.DeleteStudent(id);

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
