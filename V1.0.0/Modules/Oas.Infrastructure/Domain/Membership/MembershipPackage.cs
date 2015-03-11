using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class MembershipPackage
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
         
        public decimal Price { get; set; }

        public decimal OldPrice { get; set; }

        public int Duration { get; set; }
       
    }
}
