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
    public class AreaController : ApiController
    {
        #region fields
        private readonly IAreaService areasService = null;
        #endregion
		
		#region constructors
        
		public AreaController(IAreaService areasService)
        {
            this.areasService = areasService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetAreaById(Guid id)
        {
            var areas = areasService.GetArea(id);
            return Request.CreateResponse(HttpStatusCode.OK, areas);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchArea(AreaCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = areasService.SearchArea(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateArea(Area areas)
        {
            var opStatus = areasService.UpdateArea(areas);
            if (opStatus.Status)
            {
                return Request.CreateResponse<Area>(HttpStatusCode.Accepted, areas);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddArea(Area areas)
        {
            areas.Id = Guid.NewGuid();
            var opStatus = areasService.AddArea(areas);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<Area>(HttpStatusCode.Created, areas);
                string uri = Url.Link("DefaultApi", new { id = areas.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteArea(Guid id)
        {
            var opStatus = areasService.DeleteArea(id);

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
