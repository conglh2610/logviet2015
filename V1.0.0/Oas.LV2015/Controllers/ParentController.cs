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
    public class ParentController : ApiController
    {
        #region fields
        private readonly IParentService parentsService = null;
        #endregion
		
		#region constructors
        
		public ParentController(IParentService parentsService)
        {
            this.parentsService = parentsService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetParentById(Guid id)
        {
            var parents = parentsService.GetParent(id);
            return Request.CreateResponse(HttpStatusCode.OK, parents);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchParent(ParentCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = parentsService.SearchParent(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateParent(Parent parents)
        {
            var opStatus = parentsService.UpdateParent(parents);
            if (opStatus.Status)
            {
                return Request.CreateResponse<Parent>(HttpStatusCode.Accepted, parents);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddParent(Parent parents)
        {
            parents.Id = Guid.NewGuid();
            var opStatus = parentsService.AddParent(parents);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<Parent>(HttpStatusCode.Created, parents);
                string uri = Url.Link("DefaultApi", new { id = parents.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteParent(Guid id)
        {
            var opStatus = parentsService.DeleteParent(id);

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
