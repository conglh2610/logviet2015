using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IImageService
    {
        #region Image

        IQueryable<Image> GetImages();

        IQueryable<Image> SearchImage(ImageCriteria criteria, ref int totalRecords);

        Image GetImage(Guid imagesId);

        OperationStatus AddImage(Image images);

        OperationStatus UpdateImage(Image images);

        OperationStatus DeleteImage(Guid imagesId);

        #endregion


    }
}
