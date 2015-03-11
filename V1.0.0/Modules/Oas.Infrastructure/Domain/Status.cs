using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public enum Status
    {
        Pending,
        Trial,
        Approved,
        Rejected,
        Expired
    }

    public enum Gender
    { 
        Male, 
        Female
    }
}
