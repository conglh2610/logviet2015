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
    public class PackageItemController : ApiController
    {
        #region fields
        private readonly IPackageItemService packageitemsService = null;
        #endregion
		
		#region constructors
        
		public PackageItemController(IPackageItemService packageitemsService)
        {
            this.packageitemsService = packageitemsService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetPackageItemById(Guid id)
        {
            var packageitems = packageitemsService.GetPackageItem(id);
            return Request.CreateResponse(HttpStatusCode.OK, packageitems);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchPackageItem(PackageItemCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = packageitemsService.SearchPackageItem(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdatePackageItem(PackageItem packageitems)
        {
            var opStatus = packageitemsService.UpdatePackageItem(packageitems);
            if (opStatus.Status)
            {
                return Request.CreateResponse<PackageItem>(HttpStatusCode.Accepted, packageitems);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddPackageItem(PackageItem packageitems)
        {
            packageitems.Id = Guid.NewGuid();
            var opStatus = packageitemsService.AddPackageItem(packageitems);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<PackageItem>(HttpStatusCode.Created, packageitems);
                string uri = Url.Link("DefaultApi", new { id = packageitems.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeletePackageItem(Guid id)
        {
            var opStatus = packageitemsService.DeletePackageItem(id);

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
