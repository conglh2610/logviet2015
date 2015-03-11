using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public enum PaymentMethod
    {
        Paypal,
        TWOCHECKOUT,
        WIRETRANSFER
    }

    public enum PaymentPeriod
    {
        Monthly,
        Yearly
    }

    public enum MessageStatus { 
        Unread,
        Read,
        Removed,
        Edited
    }
}
