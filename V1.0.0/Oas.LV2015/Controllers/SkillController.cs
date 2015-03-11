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
    public class SkillController : ApiController
    {
        #region fields
        private readonly ISkillService skillsService = null;
        #endregion
		
		#region constructors
        
		public SkillController(ISkillService skillsService)
        {
            this.skillsService = skillsService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetSkillById(Guid id)
        {
            var skills = skillsService.GetSkill(id);
            return Request.CreateResponse(HttpStatusCode.OK, skills);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchSkill(SkillCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = skillsService.SearchSkill(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateSkill(Skill skills)
        {
            var opStatus = skillsService.UpdateSkill(skills);
            if (opStatus.Status)
            {
                return Request.CreateResponse<Skill>(HttpStatusCode.Accepted, skills);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddSkill(Skill skills)
        {
            skills.Id = Guid.NewGuid();
            var opStatus = skillsService.AddSkill(skills);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<Skill>(HttpStatusCode.Created, skills);
                string uri = Url.Link("DefaultApi", new { id = skills.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteSkill(Guid id)
        {
            var opStatus = skillsService.DeleteSkill(id);

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
