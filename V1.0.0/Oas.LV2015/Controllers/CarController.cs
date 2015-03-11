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
    public class CarController : ApiController
    {
        #region fields
        private readonly ICarService carsService = null;
        #endregion
		
		#region constructors
        
		public CarController(ICarService carsService)
        {
            this.carsService = carsService;
        }
		
		#endregion

		#region public methods

        [HttpPost]
        public HttpResponseMessage getCarsByCriteria(CarCriteria criteria)
        {
            int totalRecords = 0;
            var result = carsService.SearchCar(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, result.ToList());
        }

        [HttpGet]
        public HttpResponseMessage getCars()
        {
            var result = carsService.GetCars();
            return Request.CreateResponse(HttpStatusCode.OK, result.ToList());
        }

        [HttpGet]
        public HttpResponseMessage GetCar(Guid id)
        {
            var car = carsService.GetCar(id);
            return Request.CreateResponse(HttpStatusCode.OK, car);
        }

        [HttpPost]
        public HttpResponseMessage AddCar([FromBody]Car car)
        {
            car.Id = Guid.NewGuid();
            var opStatus = carsService.AddCar(car);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<Car>(HttpStatusCode.Created, car);
                string uri = Url.Link("DefaultApi", new { id = car.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

        [HttpPut]
        public HttpResponseMessage UpdateCar([FromBody]Car car)
        {
            car.CarModelId = car.CarModel.Id;
            var opStatus = carsService.UpdateCar(car);
            if (opStatus.Status)
            {
                return Request.CreateResponse<Car>(HttpStatusCode.Accepted, car);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }

        [HttpDelete]
        public HttpResponseMessage DeleteCar(Guid id)
        {
            var opStatus = carsService.DeleteCar(id);

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
