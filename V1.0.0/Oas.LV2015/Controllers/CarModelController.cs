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
    public class CarModelController : ApiController
    {
        #region fields
        private readonly ICarModelService carmodelsService = null;
        #endregion
		
		#region constructors
        
		public CarModelController(ICarModelService carmodelsService)
        {
            this.carmodelsService = carmodelsService;
        }
		
		#endregion

		#region public methods

        [HttpGet]
        public HttpResponseMessage getCarModels()
        {
            var result = carmodelsService.GetCarModels();
            return Request.CreateResponse(HttpStatusCode.OK, result.ToList());
        }

        [HttpPost]
        public HttpResponseMessage getCarModelsByCriteria(CarModelCriteria criteria)
        {
            int totalRecords = 0;
            var result = carmodelsService.SearchCarModel(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, result.ToList());
        }

        [HttpGet]
        public HttpResponseMessage getCarModel(Guid id)
        {
            var result = carmodelsService.GetCarModel(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        public HttpResponseMessage AddCarModel(CarModel carModel)
        {
            carModel.Id = Guid.NewGuid();
            var opStatus = carmodelsService.AddCarModel(carModel);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<CarModel>(HttpStatusCode.Created, carModel);
                string uri = Url.Link("DefaultApi", new { id = carModel.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

        [HttpDelete]
        public HttpResponseMessage DeleteCarModel(Guid id)
        {
            var opStatus = carmodelsService.DeleteCarModel(id);

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
        public HttpResponseMessage UpdateCarModel([FromBody]CarModel carModel)
        {
            carModel.CarCategoryId = carModel.CarCategory.Id;
            var opStatus = carmodelsService.UpdateCarModel(carModel);
            if (opStatus.Status)
            {
                return Request.CreateResponse<CarModel>(HttpStatusCode.Accepted, carModel);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }


        [HttpGet]
        public HttpResponseMessage checkUniqueCarModel(string carModelName)
        {
            var carModels = carmodelsService.GetCarModels();
            var carModel = carModels.Where(x => x.Name.Equals(carModelName, StringComparison.OrdinalIgnoreCase));
            return Request.CreateResponse(HttpStatusCode.OK, carModel);
        }

		#endregion
    }
}
