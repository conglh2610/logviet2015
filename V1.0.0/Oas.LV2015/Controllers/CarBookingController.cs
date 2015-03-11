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
    public class CarBookingController : ApiController
    {
        #region fields
        private readonly ICarBookingService carbookingsService = null;
        #endregion
		
		#region constructors
        
		public CarBookingController(ICarBookingService carbookingsService)
        {
            this.carbookingsService = carbookingsService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetCarBookingById(Guid id)
        {
            var carbookings = carbookingsService.GetCarBooking(id);
            return Request.CreateResponse(HttpStatusCode.OK, carbookings);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchCarBooking(CarBookingCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = carbookingsService.SearchCarBooking(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateCarBooking(CarBooking carbookings)
        {
            var opStatus = carbookingsService.UpdateCarBooking(carbookings);
            if (opStatus.Status)
            {
                return Request.CreateResponse<CarBooking>(HttpStatusCode.Accepted, carbookings);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddCarBooking(CarBooking carbookings)
        {
            carbookings.Id = Guid.NewGuid();
            var opStatus = carbookingsService.AddCarBooking(carbookings);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<CarBooking>(HttpStatusCode.Created, carbookings);
                string uri = Url.Link("DefaultApi", new { id = carbookings.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteCarBooking(Guid id)
        {
            var opStatus = carbookingsService.DeleteCarBooking(id);

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
