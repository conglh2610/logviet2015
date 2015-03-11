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
    public class CarCategoryController : ApiController
    {
        #region fields
        private readonly ICarCategoryService carcategoriesService = null;
        #endregion
		
		#region constructors
        
		public CarCategoryController(ICarCategoryService carcategoriesService)
        {
            this.carcategoriesService = carcategoriesService;
        }
		
		#endregion

		#region public methods

        [HttpPost]
        public HttpResponseMessage getCarCategoriesByCriteria(CarCategoryCriteria criteria)
        {
            int totalRecords = 0;
            var result = carcategoriesService.SearchCarCategory(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, result.ToList());
        }

        [HttpPost]
        public HttpResponseMessage getCarCategories()
        {
            var result = carcategoriesService.GetCarCategorys();
            return Request.CreateResponse(HttpStatusCode.OK, result.ToList());
        }

        [HttpGet]
        public HttpResponseMessage getCarCategory(Guid id)
        {
            var result = carcategoriesService.GetCarCategory(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        public HttpResponseMessage AddCarCategory(CarCategory carCategory)
        {
            carCategory.Id = Guid.NewGuid();
            var opStatus = carcategoriesService.AddCarCategory(carCategory);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<CarCategory>(HttpStatusCode.Created, carCategory);
                string uri = Url.Link("DefaultApi", new { id = carCategory.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

        [HttpDelete]
        public HttpResponseMessage DeleteCarCategory(Guid id)
        {
            var opStatus = carcategoriesService.DeleteCarCategory(id);

            if (opStatus.Status)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateCarCategory([FromBody]CarCategory carCategory)
        {
            var opStatus = carcategoriesService.UpdateCarCategory(carCategory);
            if (opStatus.Status)
            {
                return Request.CreateResponse<CarCategory>(HttpStatusCode.Accepted, carCategory);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }

		#endregion
    }
}
