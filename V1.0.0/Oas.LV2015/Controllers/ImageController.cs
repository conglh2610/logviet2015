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
    public class ImageController : ApiController
    {
        #region fields
        private readonly IImageService imagesService = null;
        #endregion
		
		#region constructors
        
		public ImageController(IImageService imagesService)
        {
            this.imagesService = imagesService;
        }
		
		#endregion

		#region public methods
		
		[HttpGet]
        public HttpResponseMessage GetImageById(Guid id)
        {
            var images = imagesService.GetImage(id);
            return Request.CreateResponse(HttpStatusCode.OK, images);
        }
		
        [HttpPost]
        public HttpResponseMessage SearchImage(ImageCriteria criteria)
        {
            int totalRecords = 0;
            var rooms = imagesService.SearchImage(criteria, ref totalRecords);
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, rooms.ToList());

        }		
		
		[HttpPut]
        public HttpResponseMessage UpdateImage(Image images)
        {
            var opStatus = imagesService.UpdateImage(images);
            if (opStatus.Status)
            {
                return Request.CreateResponse<Image>(HttpStatusCode.Accepted, images);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }
		
		[HttpPost]
        public HttpResponseMessage AddImage(Image images)
        {
            images.Id = Guid.NewGuid();
            var opStatus = imagesService.AddImage(images);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse<Image>(HttpStatusCode.Created, images);
                string uri = Url.Link("DefaultApi", new { id = images.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

		public HttpResponseMessage DeleteImage(Guid id)
        {
            var opStatus = imagesService.DeleteImage(id);

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
