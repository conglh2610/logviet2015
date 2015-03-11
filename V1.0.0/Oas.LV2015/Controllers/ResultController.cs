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
    public class ResultController : ApiController
    {
        #region fields
        private readonly IResultService resultsService = null;
        #endregion
		
		#region constructors
        
		public ResultController(IResultService resultsService)
        {
            this.resultsService = resultsService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetResultById(Guid id)
        {
            var results = resultsService.GetResult(id);
            return Request.CreateResponse(HttpStatusCode.OK, results);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchResult(ResultCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = resultsService.SearchResult(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateResult(Result results)
        {
            var opStatus = resultsService.UpdateResult(results);
            if (opStatus.Status)
            {
                return Request.CreateResponse<Result>(HttpStatusCode.Accepted, results);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddResult(Result results)
        {
            results.Id = Guid.NewGuid();
            var opStatus = resultsService.AddResult(results);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<Result>(HttpStatusCode.Created, results);
                string uri = Url.Link("DefaultApi", new { id = results.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteResult(Guid id)
        {
            var opStatus = resultsService.DeleteResult(id);

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
