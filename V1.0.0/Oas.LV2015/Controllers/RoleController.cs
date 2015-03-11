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
    public class RoleController : ApiController
    {
        #region fields
        private readonly IRoleService rolesService = null;
        #endregion
		
		#region constructors
        
		public RoleController(IRoleService rolesService)
        {
            this.rolesService = rolesService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetRoleById(Guid id)
        {
            var roles = rolesService.GetRole(id);
            return Request.CreateResponse(HttpStatusCode.OK, roles);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchRole(RoleCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = rolesService.SearchRole(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateRole(Role roles)
        {
            var opStatus = rolesService.UpdateRole(roles);
            if (opStatus.Status)
            {
                return Request.CreateResponse<Role>(HttpStatusCode.Accepted, roles);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddRole(Role roles)
        {
            roles.Id = Guid.NewGuid().ToString();
            var opStatus = rolesService.AddRole(roles);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<Role>(HttpStatusCode.Created, roles);
                string uri = Url.Link("DefaultApi", new { id = roles.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteRole(Guid id)
        {
            var opStatus = rolesService.DeleteRole(id);

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
