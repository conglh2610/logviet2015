using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class BookingNote
    {
        [Key]
        public Guid Id { get; set; }

        public Guid CarBookingId { get; set; }

        public string Note { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual CarBooking CarBooking { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
