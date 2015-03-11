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
    public class CountryController : ApiController
    {
        #region fields
        private readonly ICountryService countriesService = null;
        #endregion
		
		#region constructors
        
		public CountryController(ICountryService countriesService)
        {
            this.countriesService = countriesService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetCountryById(Guid id)
        {
            var countries = countriesService.GetCountry(id);
            return Request.CreateResponse(HttpStatusCode.OK, countries);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchCountry(CountryCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = countriesService.SearchCountry(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateCountry(Country countries)
        {
            var opStatus = countriesService.UpdateCountry(countries);
            if (opStatus.Status)
            {
                return Request.CreateResponse<Country>(HttpStatusCode.Accepted, countries);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddCountry(Country countries)
        {
            countries.Id = Guid.NewGuid();
            var opStatus = countriesService.AddCountry(countries);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<Country>(HttpStatusCode.Created, countries);
                string uri = Url.Link("DefaultApi", new { id = countries.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteCountry(Guid id)
        {
            var opStatus = countriesService.DeleteCountry(id);

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
