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
    public class AdvertismentService : IAdvertismentService
    {
        #region fields
        private readonly IRepository<Advertisment> advertismentsRepository;
        #endregion

		#region constructors
        public AdvertismentService(IRepository<Advertisment> advertismentsRepository)
        {
            this.advertismentsRepository = advertismentsRepository;
        }
		#endregion


        #region public methods

        public IQueryable<Advertisment> SearchAdvertisment(AdvertismentCriteria criteria, ref int totalRecords)
        {
            var query = advertismentsRepository
                       .Get
.Where(t=>(criteria.Id==null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id) )
&&(string.IsNullOrEmpty(criteria.CustomerName)|| ( t.CustomerName.Contains(criteria.CustomerName) || criteria.CustomerName.Contains(t.CustomerName) ))
&&(criteria.IsActive==null || t.IsActive.Equals(criteria.IsActive) )
&&(string.IsNullOrEmpty(criteria.ImageUrl)|| ( t.ImageUrl.Contains(criteria.ImageUrl) || criteria.ImageUrl.Contains(t.ImageUrl) ))
&&(string.IsNullOrEmpty(criteria.Url)|| ( t.Url.Contains(criteria.Url) || criteria.Url.Contains(t.Url) ))
&&(string.IsNullOrEmpty(criteria.Description)|| ( t.Description.Contains(criteria.Description) || criteria.Description.Contains(t.Description) ))
&&(criteria.ClickCost==null || t.ClickCost.Equals(criteria.ClickCost) )
&&(criteria.Position==null || t.Position.Equals(criteria.Position) )
&&(criteria.TotalClicked==null || t.TotalClicked.Equals(criteria.TotalClicked) )
)
                       .AsQueryable();

            totalRecords = query.Count();

            criteria.SortColumn = string.IsNullOrEmpty(criteria.SortColumn) ? string.Empty : criteria.SortColumn.ToLower();
            bool isAsc = criteria.SortDirection.ToLower().Equals("false");

           #region sorting
switch (criteria.SortColumn){
case "customername" :
query = isAsc ? query.OrderBy(t => t.CustomerName) : query.OrderByDescending(t => t.CustomerName);
break;
case "imageurl" :
query = isAsc ? query.OrderBy(t => t.ImageUrl) : query.OrderByDescending(t => t.ImageUrl);
break;
case "url" :
query = isAsc ? query.OrderBy(t => t.Url) : query.OrderByDescending(t => t.Url);
break;
case "description" :
query = isAsc ? query.OrderBy(t => t.Description) : query.OrderByDescending(t => t.Description);
break;
default: break;}
		   #endregion
            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;
        }

        public IQueryable<Advertisment> GetAdvertisments()
        {
            return advertismentsRepository
                        .Get
						   #region sorting
						   
						   #endregion
                        .AsQueryable();


        }

        public Advertisment GetAdvertisment(Guid advertismentsId)
        {
            return advertismentsRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(advertismentsId));
        }

        public OperationStatus AddAdvertisment(Advertisment advertisments)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                advertismentsRepository.Add(advertisments);
                advertismentsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateAdvertisment(Advertisment advertisments)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                advertismentsRepository.Update(advertisments);
                advertismentsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteAdvertisment(Guid advertismentsId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var advertisments = advertismentsRepository.Get.SingleOrDefault(t => t.Id.Equals(advertismentsId));
                if (advertisments != null)
                {
                    advertismentsRepository.Remove(advertisments);
                    advertismentsRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "Advertisment not found";
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
