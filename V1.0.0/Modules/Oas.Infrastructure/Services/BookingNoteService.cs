using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Oas.Infrastructure.Criteria;

namespace Oas.Infrastructure.Services
{
    public class BookingNoteService : IBookingNoteService
    {
        #region fields
        private readonly IRepository<BookingNote> bookingnotesRepository;
        #endregion

		#region constructors
        public BookingNoteService(IRepository<BookingNote> bookingnotesRepository)
        {
            this.bookingnotesRepository = bookingnotesRepository;
        }
		#endregion


        #region public methods

        public IQueryable<BookingNote> SearchBookingNote(BookingNoteCriteria criteria, ref int totalRecords)
        {
            var query = bookingnotesRepository
                       .Get
.Where(t=>(criteria.Id==null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id) )
&&(criteria.CarBookingId==null || criteria.CarBookingId == Guid.Empty || t.CarBookingId.Equals(criteria.CarBookingId) )
&&(string.IsNullOrEmpty(criteria.Note)|| ( t.Note.Contains(criteria.Note) || criteria.Note.Contains(t.Note) ))
&&(string.IsNullOrEmpty(criteria.CreatedBy)|| ( t.CreatedBy.Contains(criteria.CreatedBy) || criteria.CreatedBy.Contains(t.CreatedBy) ))
&&(criteria.CreatedDate==null || ( t.CreatedDate.Equals(criteria.CreatedDate) || criteria.CreatedDate.Equals(t.CreatedDate) ))
)
                       .AsQueryable();

            totalRecords = query.Count();

            criteria.SortColumn = string.IsNullOrEmpty(criteria.SortColumn) ? string.Empty : criteria.SortColumn.ToLower();
            bool isAsc = criteria.SortDirection.ToLower().Equals("false");

           #region sorting
switch (criteria.SortColumn){
case "note" :
query = isAsc ? query.OrderBy(t => t.Note) : query.OrderByDescending(t => t.Note);
break;
case "createdby" :
query = isAsc ? query.OrderBy(t => t.CreatedBy) : query.OrderByDescending(t => t.CreatedBy);
break;
case "createddate" :
query = isAsc ? query.OrderBy(t => t.CreatedDate) : query.OrderByDescending(t => t.CreatedDate);
break;
default: break;}
		   #endregion
            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;
        }

        public IQueryable<BookingNote> GetBookingNotes()
        {
            return bookingnotesRepository
                        .Get
						   #region sorting
						   
						   #endregion
                        .AsQueryable();


        }

        public BookingNote GetBookingNote(Guid bookingnotesId)
        {
            return bookingnotesRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(bookingnotesId));
        }

        public OperationStatus AddBookingNote(BookingNote bookingnotes)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                bookingnotesRepository.Add(bookingnotes);
                bookingnotesRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateBookingNote(BookingNote bookingnotes)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                bookingnotesRepository.Update(bookingnotes);
                bookingnotesRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteBookingNote(Guid bookingnotesId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var bookingnotes = bookingnotesRepository.Get.SingleOrDefault(t => t.Id.Equals(bookingnotesId));
                if (bookingnotes != null)
                {
                    bookingnotesRepository.Remove(bookingnotes);
                    bookingnotesRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "BookingNote not found";
                }
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        #endregion

    }
}
