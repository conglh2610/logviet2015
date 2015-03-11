using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class UserApplication
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public Guid ApplicationId { get; set; }

        public decimal Price { get; set; }

        public Status Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreateBy { get; set; }

        public DateTime? ExpireDate { get; set; }

        public virtual ICollection<Application> Applications { get; set; }

        
    }



}
