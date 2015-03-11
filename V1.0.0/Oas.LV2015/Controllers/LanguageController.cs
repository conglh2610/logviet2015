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
    public class LanguageController : ApiController
    {
        #region fields
        private readonly ILanguageService languagesService = null;
        #endregion
		
		#region constructors
        
		public LanguageController(ILanguageService languagesService)
        {
            this.languagesService = languagesService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetLanguageById(Guid id)
        {
            var languages = languagesService.GetLanguage(id);
            return Request.CreateResponse(HttpStatusCode.OK, languages);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchLanguage(LanguageCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = languagesService.SearchLanguage(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateLanguage(Language languages)
        {
            var opStatus = languagesService.UpdateLanguage(languages);
            if (opStatus.Status)
            {
                return Request.CreateResponse<Language>(HttpStatusCode.Accepted, languages);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddLanguage(Language languages)
        {
            languages.Id = Guid.NewGuid();
            var opStatus = languagesService.AddLanguage(languages);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<Language>(HttpStatusCode.Created, languages);
                string uri = Url.Link("DefaultApi", new { id = languages.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteLanguage(Guid id)
        {
            var opStatus = languagesService.DeleteLanguage(id);

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
