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
    public class UserController : ApiController
    {
        #region fields
        private readonly IUserService usersService = null;
        #endregion
		
		#region constructors
        
		public UserController(IUserService usersService)
        {
            this.usersService = usersService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetUserById(Guid id)
        {
            var users = usersService.GetUser(id);
            return Request.CreateResponse(HttpStatusCode.OK, users);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchUser(UserCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = usersService.SearchUser(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateUser(User users)
        {
            var opStatus = usersService.UpdateUser(users);
            if (opStatus.Status)
            {
                return Request.CreateResponse<User>(HttpStatusCode.Accepted, users);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddUser(User users)
        {
            users.Id = Guid.NewGuid().ToString();
            var opStatus = usersService.AddUser(users);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<User>(HttpStatusCode.Created, users);
                string uri = Url.Link("DefaultApi", new { id = users.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteUser(Guid id)
        {
            var opStatus = usersService.DeleteUser(id);

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
