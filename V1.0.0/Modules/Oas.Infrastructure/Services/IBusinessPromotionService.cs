using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IBusinessPromotionService
    {
        #region BusinessPromotion

        IQueryable<BusinessPromotion> GetBusinessPromotions();

        IQueryable<BusinessPromotion> SearchBusinessPromotion(BusinessPromotionCriteria criteria, ref int totalRecords);

        BusinessPromotion GetBusinessPromotion(Guid businesspromotionsId);

        OperationStatus AddBusinessPromotion(BusinessPromotion businesspromotions);

        OperationStatus UpdateBusinessPromotion(BusinessPromotion businesspromotions);

        OperationStatus DeleteBusinessPromotion(Guid businesspromotionsId);

        #endregion


    }
}
