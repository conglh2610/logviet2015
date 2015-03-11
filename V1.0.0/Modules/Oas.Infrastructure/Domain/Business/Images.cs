using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class Image
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? BusinessId { get; set; }
        public Guid? CarId { get; set; }
        public Guid? CarItemId { get; set; }
        public Guid? BookingNoteId { get; set; }

        public Guid? CarAccidentNoteId { get; set; }


        public string Caption { get; set; }
        public string Url { get; set; }
        public bool IsProfileImage { get; set; }

        [ForeignKey("BusinessId")]
        public Business Business { get; set; }

        [ForeignKey("CarId")]
        public Car Car { get; set; }

        [ForeignKey("CarItemId")]
        public CarItem CarItem { get; set; }

        [ForeignKey("CarAccidentNoteId")]
        public CarAccidentNote CarAccidentNote { get; set; }

        [ForeignKey("BookingNoteId")]
        public BookingNote BookingNote { get; set; }

    }
}