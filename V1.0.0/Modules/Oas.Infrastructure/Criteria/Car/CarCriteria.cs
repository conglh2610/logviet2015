using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Criteria
{
    public class CarCriteria : Criteria
    {
    }

    public class CarCategoryCriteria : Criteria
    {        
        public string Name { get; set; }
    }

    public class CarModelCriteria : Criteria
    {
        public string Name { get; set; }
    }
}
