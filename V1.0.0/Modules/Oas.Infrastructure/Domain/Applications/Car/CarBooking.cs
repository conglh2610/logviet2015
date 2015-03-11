using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Domain
{
    public class CarBooking
    {
        [Key]
        public Guid Id { get; set; }

        public Guid CarItemId { get; set; }

        public string UserId { get; set; }

        public Guid DriverId { get; set; }

        public bool IsNeededDriver { get; set; }

        public DateTime BookFromDate { get; set; }

        public DateTime BookToDate { get; set; }

        public BookType BookType { get; set; }

        public int TotalDay { get; set; }

        public string Schduling { get; set; }

        public BookStatus BookStatus { get; set; }

        public Driver Driver { get; set; }

        public virtual ICollection<BookingNote> BookingNotes { get; set; }

        public Guid CarCustomerId { get; set; }

        [ForeignKey("CarCustomerId")]
        public CarCustomer CarCustomer { get; set; }
    }

    public enum BookType
    {
        OneOff,
        Repeating
    }

    public enum BookStatus
    {
        /// <summary>
        /// This is default status when user makes a booking.
        /// </summary>
        Pending,

        /// <summary>
        /// This is reviewed by business owner or its employee
        /// This includes a note or message back to user to notify what need to do next.
        /// </summary>
        Confirmed,

        /// <summary>
        /// Its mean that business owner will hold the booking until due date.
        /// User maybe make a payment for this holding depend on policy of a business owner
        /// </summary>
        Holding,

        /// <summary>
        /// Approve a booking and the payment is completed.
        /// </summary>
        Approved,




        /// <summary>
        /// Reject a booking
        /// </summary>
        Rejected,


        /// <summary>
        /// The car has been in an accident
        /// </summary>
        Crashed,

        /// <summary>
        /// The car has been in an accident and resolved 
        /// this required a note to describe what going on.
        /// </summary>
        CrashedResolved,


        /// <summary>
        /// Finish a booking
        /// </summary>
        Completed,



    }

}
