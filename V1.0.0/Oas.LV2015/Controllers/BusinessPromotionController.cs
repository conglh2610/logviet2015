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
    public class BusinessPromotionController : ApiController
    {
        #region fields
        private readonly IBusinessPromotionService businesspromotionsService = null;
        #endregion
		
		#region constructors
        
		public BusinessPromotionController(IBusinessPromotionService businesspromotionsService)
        {
            this.businesspromotionsService = businesspromotionsService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetBusinessPromotionById(Guid id)
        {
            var businesspromotions = businesspromotionsService.GetBusinessPromotion(id);
            return Request.CreateResponse(HttpStatusCode.OK, businesspromotions);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchBusinessPromotion(BusinessPromotionCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = businesspromotionsService.SearchBusinessPromotion(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateBusinessPromotion(BusinessPromotion businesspromotions)
        {
            var opStatus = businesspromotionsService.UpdateBusinessPromotion(businesspromotions);
            if (opStatus.Status)
            {
                return Request.CreateResponse<BusinessPromotion>(HttpStatusCode.Accepted, businesspromotions);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddBusinessPromotion(BusinessPromotion businesspromotions)
        {
            businesspromotions.Id = Guid.NewGuid();
            var opStatus = businesspromotionsService.AddBusinessPromotion(businesspromotions);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<BusinessPromotion>(HttpStatusCode.Created, businesspromotions);
                string uri = Url.Link("DefaultApi", new { id = businesspromotions.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteBusinessPromotion(Guid id)
        {
            var opStatus = businesspromotionsService.DeleteBusinessPromotion(id);

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
