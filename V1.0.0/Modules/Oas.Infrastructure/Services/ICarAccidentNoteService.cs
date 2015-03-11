using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface ICarAccidentNoteService
    {
        #region CarAccidentNote

        IQueryable<CarAccidentNote> GetCarAccidentNotes();

        IQueryable<CarAccidentNote> SearchCarAccidentNote(CarAccidentNoteCriteria criteria, ref int totalRecords);

        CarAccidentNote GetCarAccidentNote(Guid caraccidentnotesId);

        OperationStatus AddCarAccidentNote(CarAccidentNote caraccidentnotes);

        OperationStatus UpdateCarAccidentNote(CarAccidentNote caraccidentnotes);

        OperationStatus DeleteCarAccidentNote(Guid caraccidentnotesId);

        #endregion


    }
}
