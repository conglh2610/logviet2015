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
    public class MembershipPackageController : ApiController
    {
        #region fields
        private readonly IMembershipPackageService membershippackagesService = null;
        #endregion
		
		#region constructors
        
		public MembershipPackageController(IMembershipPackageService membershippackagesService)
        {
            this.membershippackagesService = membershippackagesService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetMembershipPackageById(Guid id)
        {
            var membershippackages = membershippackagesService.GetMembershipPackage(id);
            return Request.CreateResponse(HttpStatusCode.OK, membershippackages);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchMembershipPackage(MembershipPackageCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = membershippackagesService.SearchMembershipPackage(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateMembershipPackage(MembershipPackage membershippackages)
        {
            var opStatus = membershippackagesService.UpdateMembershipPackage(membershippackages);
            if (opStatus.Status)
            {
                return Request.CreateResponse<MembershipPackage>(HttpStatusCode.Accepted, membershippackages);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddMembershipPackage(MembershipPackage membershippackages)
        {
            membershippackages.Id = Guid.NewGuid();
            var opStatus = membershippackagesService.AddMembershipPackage(membershippackages);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<MembershipPackage>(HttpStatusCode.Created, membershippackages);
                string uri = Url.Link("DefaultApi", new { id = membershippackages.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteMembershipPackage(Guid id)
        {
            var opStatus = membershippackagesService.DeleteMembershipPackage(id);

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
