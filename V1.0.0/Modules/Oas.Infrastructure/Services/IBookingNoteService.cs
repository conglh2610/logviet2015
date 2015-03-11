using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IBookingNoteService
    {
        #region BookingNote

        IQueryable<BookingNote> GetBookingNotes();

        IQueryable<BookingNote> SearchBookingNote(BookingNoteCriteria criteria, ref int totalRecords);

        BookingNote GetBookingNote(Guid bookingnotesId);

        OperationStatus AddBookingNote(BookingNote bookingnotes);

        OperationStatus UpdateBookingNote(BookingNote bookingnotes);

        OperationStatus DeleteBookingNote(Guid bookingnotesId);

        #endregion


    }
}
