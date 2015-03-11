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
    public class EmailTemplateController : ApiController
    {
        #region fields
        private readonly IEmailTemplateService emailtemplatesService = null;
        #endregion
		
		#region constructors
        
		public EmailTemplateController(IEmailTemplateService emailtemplatesService)
        {
            this.emailtemplatesService = emailtemplatesService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetEmailTemplateById(Guid id)
        {
            var emailtemplates = emailtemplatesService.GetEmailTemplate(id);
            return Request.CreateResponse(HttpStatusCode.OK, emailtemplates);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchEmailTemplate(EmailTemplateCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = emailtemplatesService.SearchEmailTemplate(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateEmailTemplate(EmailTemplate emailtemplates)
        {
            var opStatus = emailtemplatesService.UpdateEmailTemplate(emailtemplates);
            if (opStatus.Status)
            {
                return Request.CreateResponse<EmailTemplate>(HttpStatusCode.Accepted, emailtemplates);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddEmailTemplate(EmailTemplate emailtemplates)
        {
            emailtemplates.Id = Guid.NewGuid();
            var opStatus = emailtemplatesService.AddEmailTemplate(emailtemplates);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<EmailTemplate>(HttpStatusCode.Created, emailtemplates);
                string uri = Url.Link("DefaultApi", new { id = emailtemplates.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteEmailTemplate(Guid id)
        {
            var opStatus = emailtemplatesService.DeleteEmailTemplate(id);

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
