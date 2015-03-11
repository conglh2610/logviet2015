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
    public class ProgramController : ApiController
    {
        #region fields
        private readonly IProgramService programsService = null;
        #endregion
		
		#region constructors
        
		public ProgramController(IProgramService programsService)
        {
            this.programsService = programsService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetProgramById(Guid id)
        {
            var programs = programsService.GetProgram(id);
            return Request.CreateResponse(HttpStatusCode.OK, programs);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchProgram(ProgramCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = programsService.SearchProgram(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateProgram(Program programs)
        {
            var opStatus = programsService.UpdateProgram(programs);
            if (opStatus.Status)
            {
                return Request.CreateResponse<Program>(HttpStatusCode.Accepted, programs);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddProgram(Program programs)
        {
            programs.Id = Guid.NewGuid();
            var opStatus = programsService.AddProgram(programs);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<Program>(HttpStatusCode.Created, programs);
                string uri = Url.Link("DefaultApi", new { id = programs.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteProgram(Guid id)
        {
            var opStatus = programsService.DeleteProgram(id);

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
