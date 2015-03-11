using Oas.Infrastructure;
using Oas.Infrastructure.Domain;
using Oas.Infrastructure.Domain;
using Oas.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using Oas.Infrastructure.Criteria;

namespace Oas.LV2015.Controllers
{
    public class StudentsController : ApiController
    {
        #region fields
        private readonly IStudentService studentsService = null;
        #endregion

        public StudentsController(IStudentService studentService)
        {
            this.studentsService = studentService;
        }


        [HttpPost]
        public HttpResponseMessage AddStudent([FromBody]Student student)
        {
            student.Id = Guid.NewGuid();
            var opStatus = studentsService.AddStudent(student);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<Student>(HttpStatusCode.Created, student);
                string uri = Url.Link("DefaultApi", new { id = student.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }


        [HttpGet]
        public HttpResponseMessage GetStudents()
        {
            var students = studentsService.GetStudents();
            var totalRecords = students.Count();
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, students);

        }

        [HttpPost]
        public HttpResponseMessage Search(StudentCriteria criteria)
        {
            int totalRecords = 0;
            var students = studentsService.SearchStudent(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());

            return Request.CreateResponse(HttpStatusCode.OK, students.ToList());

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

        [HttpGet]
        public HttpResponseMessage GetStudentById(Guid id)
        {
            var student = studentsService.GetStudent(id);
            return Request.CreateResponse(HttpStatusCode.OK, student);
        }

        [HttpPut]
        public HttpResponseMessage PutStudent(Guid id, [FromBody]Student student)
        {
            var opStatus = studentsService.UpdateStudent(student);
            if (opStatus.Status)
            {
                return Request.CreateResponse<Student>(HttpStatusCode.Accepted, student);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }

        [HttpPost]
        public HttpResponseMessage ViewStudentClassHistory(Guid studentId, StudentCriteria criteria)
        {
            int totalRecords = 0;
            List<StudentClassHistoryViewModel> result = studentsService.ViewStudentClassHistory(studentId, criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

    }
}
