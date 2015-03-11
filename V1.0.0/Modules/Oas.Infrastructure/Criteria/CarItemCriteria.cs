using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oas.Infrastructure.Criteria
{
    public class CarItemCriteria : Criteria
    {
        public Guid? Id { get; set; }
        public Guid? CarId { get; set; }
        public Guid? CarModelId { get; set; }
        public Guid? CarCategoryId { get; set; }
        public string CarName { get;set; }
        public Guid? BusinessId { get; set; }
        public decimal? UnitPrice { get; set; }
        public string Description { get; set; }
        public string CarNumber { get; set; }
        public int? Total { get; set; }
    }
}
