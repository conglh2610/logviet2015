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
    public class CarAccidentNoteController : ApiController
    {
        #region fields
        private readonly ICarAccidentNoteService caraccidentnotesService = null;
        #endregion
		
		#region constructors
        
		public CarAccidentNoteController(ICarAccidentNoteService caraccidentnotesService)
        {
            this.caraccidentnotesService = caraccidentnotesService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetCarAccidentNoteById(Guid id)
        {
            var caraccidentnotes = caraccidentnotesService.GetCarAccidentNote(id);
            return Request.CreateResponse(HttpStatusCode.OK, caraccidentnotes);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchCarAccidentNote(CarAccidentNoteCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = caraccidentnotesService.SearchCarAccidentNote(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateCarAccidentNote(CarAccidentNote caraccidentnotes)
        {
            var opStatus = caraccidentnotesService.UpdateCarAccidentNote(caraccidentnotes);
            if (opStatus.Status)
            {
                return Request.CreateResponse<CarAccidentNote>(HttpStatusCode.Accepted, caraccidentnotes);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddCarAccidentNote(CarAccidentNote caraccidentnotes)
        {
            caraccidentnotes.Id = Guid.NewGuid();
            var opStatus = caraccidentnotesService.AddCarAccidentNote(caraccidentnotes);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<CarAccidentNote>(HttpStatusCode.Created, caraccidentnotes);
                string uri = Url.Link("DefaultApi", new { id = caraccidentnotes.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteCarAccidentNote(Guid id)
        {
            var opStatus = caraccidentnotesService.DeleteCarAccidentNote(id);

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
