using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Oas.Infrastructure.Criteria;

namespace Oas.Infrastructure.Services
{
    public class ImageService : IImageService
    {
        #region fields
        private readonly IRepository<Image> imagesRepository;
        #endregion

		#region constructors
        public ImageService(IRepository<Image> imagesRepository)
        {
            this.imagesRepository = imagesRepository;
        }
		#endregion


        #region public methods

        public IQueryable<Image> SearchImage(ImageCriteria criteria, ref int totalRecords)
        {
            var query = imagesRepository
                       .Get
.Where(t=>(criteria.Id==null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id) )
&&(criteria.BusinessId==null || criteria.BusinessId == Guid.Empty || t.BusinessId.Equals(criteria.BusinessId) )
&&(criteria.CarId==null || criteria.CarId == Guid.Empty || t.CarId.Equals(criteria.CarId) )
&&(criteria.CarItemId==null || criteria.CarItemId == Guid.Empty || t.CarItemId.Equals(criteria.CarItemId) )
&&(string.IsNullOrEmpty(criteria.Caption)|| ( t.Caption.Contains(criteria.Caption) || criteria.Caption.Contains(t.Caption) ))
&&(string.IsNullOrEmpty(criteria.Url)|| ( t.Url.Contains(criteria.Url) || criteria.Url.Contains(t.Url) ))
&&(criteria.IsProfileImage==null || t.IsProfileImage.Equals(criteria.IsProfileImage) )
&&(criteria.BookingNoteId==null || criteria.BookingNoteId == Guid.Empty || t.BookingNoteId.Equals(criteria.BookingNoteId) )
&&(criteria.CarAccidentNoteId==null || criteria.CarAccidentNoteId == Guid.Empty || t.CarAccidentNoteId.Equals(criteria.CarAccidentNoteId) )
)
                       .AsQueryable();

            totalRecords = query.Count();

            criteria.SortColumn = string.IsNullOrEmpty(criteria.SortColumn) ? string.Empty : criteria.SortColumn.ToLower();
            bool isAsc = criteria.SortDirection.ToLower().Equals("false");

           #region sorting
switch (criteria.SortColumn){
case "caption" :
query = isAsc ? query.OrderBy(t => t.Caption) : query.OrderByDescending(t => t.Caption);
break;
case "url" :
query = isAsc ? query.OrderBy(t => t.Url) : query.OrderByDescending(t => t.Url);
break;
default: break;}
		   #endregion
            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;
        }

        public IQueryable<Image> GetImages()
        {
            return imagesRepository
                        .Get
						   #region sorting
						   
						   #endregion
                        .AsQueryable();


        }

        public Image GetImage(Guid imagesId)
        {
            return imagesRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(imagesId));
        }

        public OperationStatus AddImage(Image images)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                imagesRepository.Add(images);
                imagesRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateImage(Image images)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                imagesRepository.Update(images);
                imagesRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteImage(Guid imagesId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var images = imagesRepository.Get.SingleOrDefault(t => t.Id.Equals(imagesId));
                if (images != null)
                {
                    imagesRepository.Remove(images);
                    imagesRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "Image not found";
                }
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        #endregion

    }
}
