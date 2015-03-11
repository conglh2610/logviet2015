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
    public class StudyTimeController : ApiController
    {
        #region fields
        private readonly IStudyTimeService studytimesService = null;
        #endregion
		
		#region constructors
        
		public StudyTimeController(IStudyTimeService studytimesService)
        {
            this.studytimesService = studytimesService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetStudyTimeById(Guid id)
        {
            var studytimes = studytimesService.GetStudyTime(id);
            return Request.CreateResponse(HttpStatusCode.OK, studytimes);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchStudyTime(StudyTimeCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = studytimesService.SearchStudyTime(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateStudyTime(StudyTime studytimes)
        {
            var opStatus = studytimesService.UpdateStudyTime(studytimes);
            if (opStatus.Status)
            {
                return Request.CreateResponse<StudyTime>(HttpStatusCode.Accepted, studytimes);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddStudyTime(StudyTime studytimes)
        {
            studytimes.Id = Guid.NewGuid();
            var opStatus = studytimesService.AddStudyTime(studytimes);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<StudyTime>(HttpStatusCode.Created, studytimes);
                string uri = Url.Link("DefaultApi", new { id = studytimes.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteStudyTime(Guid id)
        {
            var opStatus = studytimesService.DeleteStudyTime(id);

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
