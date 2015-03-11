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
    public class BusinessPromotionService : IBusinessPromotionService
    {
        #region fields
        private readonly IRepository<BusinessPromotion> businesspromotionsRepository;
        #endregion

		#region constructors
        public BusinessPromotionService(IRepository<BusinessPromotion> businesspromotionsRepository)
        {
            this.businesspromotionsRepository = businesspromotionsRepository;
        }
		#endregion


        #region public methods

        public IQueryable<BusinessPromotion> SearchBusinessPromotion(BusinessPromotionCriteria criteria, ref int totalRecords)
        {
            var query = businesspromotionsRepository
                       .Get
.Where(t=>(criteria.Id==null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id) )
&&(criteria.BusinessId==null || criteria.BusinessId == Guid.Empty || t.BusinessId.Equals(criteria.BusinessId) )
&&(string.IsNullOrEmpty(criteria.Description)|| ( t.Description.Contains(criteria.Description) || criteria.Description.Contains(t.Description) ))
&&(criteria.Discount==null || t.Discount.Equals(criteria.Discount) )
&&(criteria.StartDate==null || ( t.StartDate.Equals(criteria.StartDate) || criteria.StartDate.Equals(t.StartDate) ))
&&(criteria.EndDate==null || ( t.EndDate.Equals(criteria.EndDate) || criteria.EndDate.Equals(t.EndDate) ))
&&(criteria.Limitation==null || t.Limitation.Equals(criteria.Limitation) )
&&(criteria.Viewed==null || t.Viewed.Equals(criteria.Viewed) )
&&(criteria.Status==null || t.Status.Equals(criteria.Status) )
&&(string.IsNullOrEmpty(criteria.Title)|| ( t.Title.Contains(criteria.Title) || criteria.Title.Contains(t.Title) ))
)
                       .AsQueryable();

            totalRecords = query.Count();

            criteria.SortColumn = string.IsNullOrEmpty(criteria.SortColumn) ? string.Empty : criteria.SortColumn.ToLower();
            bool isAsc = criteria.SortDirection.ToLower().Equals("false");

           #region sorting
switch (criteria.SortColumn){
case "description" :
query = isAsc ? query.OrderBy(t => t.Description) : query.OrderByDescending(t => t.Description);
break;
case "startdate" :
query = isAsc ? query.OrderBy(t => t.StartDate) : query.OrderByDescending(t => t.StartDate);
break;
case "enddate" :
query = isAsc ? query.OrderBy(t => t.EndDate) : query.OrderByDescending(t => t.EndDate);
break;
case "title" :
query = isAsc ? query.OrderBy(t => t.Title) : query.OrderByDescending(t => t.Title);
break;
default: break;}
		   #endregion
            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;
        }

        public IQueryable<BusinessPromotion> GetBusinessPromotions()
        {
            return businesspromotionsRepository
                        .Get
						   #region sorting
						   
						   #endregion
                        .AsQueryable();


        }

        public BusinessPromotion GetBusinessPromotion(Guid businesspromotionsId)
        {
            return businesspromotionsRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(businesspromotionsId));
        }

        public OperationStatus AddBusinessPromotion(BusinessPromotion businesspromotions)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                businesspromotionsRepository.Add(businesspromotions);
                businesspromotionsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateBusinessPromotion(BusinessPromotion businesspromotions)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                businesspromotionsRepository.Update(businesspromotions);
                businesspromotionsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteBusinessPromotion(Guid businesspromotionsId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var businesspromotions = businesspromotionsRepository.Get.SingleOrDefault(t => t.Id.Equals(businesspromotionsId));
                if (businesspromotions != null)
                {
                    businesspromotionsRepository.Remove(businesspromotions);
                    businesspromotionsRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "BusinessPromotion not found";
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
