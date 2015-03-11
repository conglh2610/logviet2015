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
    public class SettingController : ApiController
    {
        #region fields
        private readonly ISettingService settingsService = null;
        #endregion
		
		#region constructors
        
		public SettingController(ISettingService settingsService)
        {
            this.settingsService = settingsService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetSettingById(Guid id)
        {
            var settings = settingsService.GetSetting(id);
            return Request.CreateResponse(HttpStatusCode.OK, settings);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchSetting(SettingCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = settingsService.SearchSetting(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateSetting(Setting settings)
        {
            var opStatus = settingsService.UpdateSetting(settings);
            if (opStatus.Status)
            {
                return Request.CreateResponse<Setting>(HttpStatusCode.Accepted, settings);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddSetting(Setting settings)
        {
            settings.Id = Guid.NewGuid();
            var opStatus = settingsService.AddSetting(settings);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<Setting>(HttpStatusCode.Created, settings);
                string uri = Url.Link("DefaultApi", new { id = settings.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteSetting(Guid id)
        {
            var opStatus = settingsService.DeleteSetting(id);

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
