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
    public class StudentFeeController : ApiController
    {
        #region fields
        private readonly IStudentFeeService studentfeesService = null;
        #endregion
		
		#region constructors
        
		public StudentFeeController(IStudentFeeService studentfeesService)
        {
            this.studentfeesService = studentfeesService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetStudentFeeById(Guid id)
        {
            var studentfees = studentfeesService.GetStudentFee(id);
            return Request.CreateResponse(HttpStatusCode.OK, studentfees);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchStudentFee(StudentFeeCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = studentfeesService.SearchStudentFee(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateStudentFee(StudentFee studentfees)
        {
            var opStatus = studentfeesService.UpdateStudentFee(studentfees);
            if (opStatus.Status)
            {
                return Request.CreateResponse<StudentFee>(HttpStatusCode.Accepted, studentfees);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddStudentFee(StudentFee studentfees)
        {
            studentfees.Id = Guid.NewGuid();
            var opStatus = studentfeesService.AddStudentFee(studentfees);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<StudentFee>(HttpStatusCode.Created, studentfees);
                string uri = Url.Link("DefaultApi", new { id = studentfees.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteStudentFee(Guid id)
        {
            var opStatus = studentfeesService.DeleteStudentFee(id);

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
