using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class BusinessPromotion
    {
        [Key]
        public Guid Id { get; set; }

        public Guid BusinessId { get; set; }

        public string Description { get; set; }

        public int Discount { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Limitation { get; set; }

        public int Viewed { get; set; }


        public Status Status { get; set; }

        [ForeignKey("BusinessId")]
        public Business Business { get; set; }


        public string Title { get; set; }

    }
}
