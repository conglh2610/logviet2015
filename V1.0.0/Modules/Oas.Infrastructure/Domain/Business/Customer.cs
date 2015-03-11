using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class Customer
    {
        public Guid Id { get; set; }
        public Guid BusinessId { get; set; }
        public string UserId { get; set; }

        public int Point { get; set; }

        public Rating Rating { get; set; }
    }


    public enum Rating
    {
        None,
        Cu,
        Sliver,
        Gold,
        Diamond
    }
}
