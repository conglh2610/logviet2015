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
    public class CarItemController : ApiController
    {
        #region fields
        private readonly ICarItemService caritemsService = null;
        #endregion
		
		#region constructors
        
		public CarItemController(ICarItemService caritemsService)
        {
            this.caritemsService = caritemsService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetCarItem(Guid id)
        {
            var caritems = caritemsService.GetCarItem(id);
            return Request.CreateResponse(HttpStatusCode.OK, caritems);
        }
		
        [HttpPost]
        public HttpResponseMessage GetCarItemsByCriteria(CarItemCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = caritemsService.SearchCarItem(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }

        [HttpGet]
        public HttpResponseMessage GetCarItems()
        {
            var rooms = caritemsService.GetCarItems();
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());
        }	
		
		[HttpPut]
        public HttpResponseMessage UpdateCarItem(CarItem caritems)
        {
            var opStatus = caritemsService.UpdateCarItem(caritems);
            if (opStatus.Status)
            {
                return Request.CreateResponse<CarItem>(HttpStatusCode.Accepted, caritems);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddCarItem(CarItem carItem)
        {
            carItem.Id = Guid.NewGuid();
            var opStatus = caritemsService.AddCarItem(carItem);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<CarItem>(HttpStatusCode.Created, carItem);
                string uri = Url.Link("DefaultApi", new { id = carItem.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteCarItem(Guid id)
        {
            var opStatus = caritemsService.DeleteCarItem(id);

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
