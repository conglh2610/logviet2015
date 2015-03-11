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
    public class CarAccidentNoteService : ICarAccidentNoteService
    {
        #region fields
        private readonly IRepository<CarAccidentNote> caraccidentnotesRepository;
        #endregion

		#region constructors
        public CarAccidentNoteService(IRepository<CarAccidentNote> caraccidentnotesRepository)
        {
            this.caraccidentnotesRepository = caraccidentnotesRepository;
        }
		#endregion


        #region public methods

        public IQueryable<CarAccidentNote> SearchCarAccidentNote(CarAccidentNoteCriteria criteria, ref int totalRecords)
        {
            var query = caraccidentnotesRepository
                       .Get
.Where(t=>(criteria.Id==null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id) )
&&(criteria.CarAccident==null || criteria.CarAccident == Guid.Empty || t.CarAccident.Equals(criteria.CarAccident) )
&&(string.IsNullOrEmpty(criteria.Title)|| ( t.Title.Contains(criteria.Title) || criteria.Title.Contains(t.Title) ))
&&(string.IsNullOrEmpty(criteria.Description)|| ( t.Description.Contains(criteria.Description) || criteria.Description.Contains(t.Description) ))
)
                       .AsQueryable();

            totalRecords = query.Count();

            criteria.SortColumn = string.IsNullOrEmpty(criteria.SortColumn) ? string.Empty : criteria.SortColumn.ToLower();
            bool isAsc = criteria.SortDirection.ToLower().Equals("false");

           #region sorting
switch (criteria.SortColumn){
case "title" :
query = isAsc ? query.OrderBy(t => t.Title) : query.OrderByDescending(t => t.Title);
break;
case "description" :
query = isAsc ? query.OrderBy(t => t.Description) : query.OrderByDescending(t => t.Description);
break;
default: break;}
		   #endregion
            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;
        }

        public IQueryable<CarAccidentNote> GetCarAccidentNotes()
        {
            return caraccidentnotesRepository
                        .Get
						   #region sorting
						   
						   #endregion
                        .AsQueryable();


        }

        public CarAccidentNote GetCarAccidentNote(Guid caraccidentnotesId)
        {
            return caraccidentnotesRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(caraccidentnotesId));
        }

        public OperationStatus AddCarAccidentNote(CarAccidentNote caraccidentnotes)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                caraccidentnotesRepository.Add(caraccidentnotes);
                caraccidentnotesRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateCarAccidentNote(CarAccidentNote caraccidentnotes)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                caraccidentnotesRepository.Update(caraccidentnotes);
                caraccidentnotesRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteCarAccidentNote(Guid caraccidentnotesId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var caraccidentnotes = caraccidentnotesRepository.Get.SingleOrDefault(t => t.Id.Equals(caraccidentnotesId));
                if (caraccidentnotes != null)
                {
                    caraccidentnotesRepository.Remove(caraccidentnotes);
                    caraccidentnotesRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "CarAccidentNote not found";
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
