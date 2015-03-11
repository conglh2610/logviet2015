using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oas.Infrastructure.Criteria
{
    public class DriverCriteria : Criteria
    {


        public Guid? Id { get; set; }


        public string Name { get; set; }


        public string Address { get; set; }


        public string Phone { get; set; }


        public string DriverLevel { get; set; }


        public string DriverLicense { get; set; }
    }
}
