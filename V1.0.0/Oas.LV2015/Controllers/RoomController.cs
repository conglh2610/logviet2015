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
    public class RoomController : ApiController
    {
        #region fields
        private readonly IRoomService roomsService = null;
        #endregion
		
		#region constructors
        
		public RoomController(IRoomService roomsService)
        {
            this.roomsService = roomsService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetRoomById(Guid id)
        {
            var rooms = roomsService.GetRoom(id);
            return Request.CreateResponse(HttpStatusCode.OK, rooms);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchRoom(RoomCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = roomsService.SearchRoom(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateRoom(Room rooms)
        {
            var opStatus = roomsService.UpdateRoom(rooms);
            if (opStatus.Status)
            {
                return Request.CreateResponse<Room>(HttpStatusCode.Accepted, rooms);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddRoom(Room rooms)
        {
            rooms.Id = Guid.NewGuid();
            var opStatus = roomsService.AddRoom(rooms);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<Room>(HttpStatusCode.Created, rooms);
                string uri = Url.Link("DefaultApi", new { id = rooms.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteRoom(Guid id)
        {
            var opStatus = roomsService.DeleteRoom(id);

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
