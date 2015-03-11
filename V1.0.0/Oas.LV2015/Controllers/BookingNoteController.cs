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
    public class BookingNoteController : ApiController
    {
        #region fields
        private readonly IBookingNoteService bookingnotesService = null;
        #endregion
		
		#region constructors
        
		public BookingNoteController(IBookingNoteService bookingnotesService)
        {
            this.bookingnotesService = bookingnotesService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetBookingNoteById(Guid id)
        {
            var bookingnotes = bookingnotesService.GetBookingNote(id);
            return Request.CreateResponse(HttpStatusCode.OK, bookingnotes);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchBookingNote(BookingNoteCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = bookingnotesService.SearchBookingNote(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateBookingNote(BookingNote bookingnotes)
        {
            var opStatus = bookingnotesService.UpdateBookingNote(bookingnotes);
            if (opStatus.Status)
            {
                return Request.CreateResponse<BookingNote>(HttpStatusCode.Accepted, bookingnotes);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddBookingNote(BookingNote bookingnotes)
        {
            bookingnotes.Id = Guid.NewGuid();
            var opStatus = bookingnotesService.AddBookingNote(bookingnotes);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<BookingNote>(HttpStatusCode.Created, bookingnotes);
                string uri = Url.Link("DefaultApi", new { id = bookingnotes.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteBookingNote(Guid id)
        {
            var opStatus = bookingnotesService.DeleteBookingNote(id);

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
