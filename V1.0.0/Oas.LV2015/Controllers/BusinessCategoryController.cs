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
    public class BusinessCategoryController : ApiController
    {
        #region fields
        private readonly IBusinessCategoryService businesscategoriesService = null;
        #endregion
		
		#region constructors
        
		public BusinessCategoryController(IBusinessCategoryService businesscategoriesService)
        {
            this.businesscategoriesService = businesscategoriesService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetBusinessCategoryById(Guid id)
        {
            var businesscategories = businesscategoriesService.GetBusinessCategory(id);
            return Request.CreateResponse(HttpStatusCode.OK, businesscategories);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchBusinessCategory(BusinessCategoryCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = businesscategoriesService.SearchBusinessCategory(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateBusinessCategory(BusinessCategory businesscategories)
        {
            var opStatus = businesscategoriesService.UpdateBusinessCategory(businesscategories);
            if (opStatus.Status)
            {
                return Request.CreateResponse<BusinessCategory>(HttpStatusCode.Accepted, businesscategories);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddBusinessCategory(BusinessCategory businesscategories)
        {
            businesscategories.Id = Guid.NewGuid();
            var opStatus = businesscategoriesService.AddBusinessCategory(businesscategories);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<BusinessCategory>(HttpStatusCode.Created, businesscategories);
                string uri = Url.Link("DefaultApi", new { id = businesscategories.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteBusinessCategory(Guid id)
        {
            var opStatus = businesscategoriesService.DeleteBusinessCategory(id);

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
