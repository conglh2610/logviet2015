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
    public class EmployeeController : ApiController
    {
        #region fields
        private readonly IEmployeeService employeesService = null;
        #endregion
		
		#region constructors
        
		public EmployeeController(IEmployeeService employeesService)
        {
            this.employeesService = employeesService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetEmployeeById(Guid id)
        {
            var employees = employeesService.GetEmployee(id);
            return Request.CreateResponse(HttpStatusCode.OK, employees);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchEmployee(EmployeeCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = employeesService.SearchEmployee(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateEmployee(Employee employees)
        {
            var opStatus = employeesService.UpdateEmployee(employees);
            if (opStatus.Status)
            {
                return Request.CreateResponse<Employee>(HttpStatusCode.Accepted, employees);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddEmployee(Employee employees)
        {
            employees.Id = Guid.NewGuid();
            var opStatus = employeesService.AddEmployee(employees);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<Employee>(HttpStatusCode.Created, employees);
                string uri = Url.Link("DefaultApi", new { id = employees.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteEmployee(Guid id)
        {
            var opStatus = employeesService.DeleteEmployee(id);

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
