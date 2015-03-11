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
    public class ProgramService : IProgramService
    {
        #region fields
        private readonly IRepository<Program> programsRepository;
        #endregion

		#region constructors
        public ProgramService(IRepository<Program> programsRepository)
        {
            this.programsRepository = programsRepository;
        }
		#endregion


        #region public methods

        public IQueryable<Program> SearchProgram(ProgramCriteria criteria, ref int totalRecords)
        {
            var query = programsRepository
                       .Get
.Where(t=>(criteria.Id==null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id) )
&&(criteria.LanguageId==null || criteria.LanguageId == Guid.Empty || t.LanguageId.Equals(criteria.LanguageId) )
&&(string.IsNullOrEmpty(criteria.Name)|| ( t.Name.Contains(criteria.Name) || criteria.Name.Contains(t.Name) ))
&&(criteria.TotalMonths==null || t.TotalMonths.Equals(criteria.TotalMonths) )
&&(criteria.Price==null || t.Price.Equals(criteria.Price) )
&&(criteria.LanguageLevel==null || t.LanguageLevel.Equals(criteria.LanguageLevel) )
)
                       .AsQueryable();

            totalRecords = query.Count();

            criteria.SortColumn = string.IsNullOrEmpty(criteria.SortColumn) ? string.Empty : criteria.SortColumn.ToLower();
            bool isAsc = criteria.SortDirection.ToLower().Equals("false");

           #region sorting
switch (criteria.SortColumn){
case "name" :
query = isAsc ? query.OrderBy(t => t.Name) : query.OrderByDescending(t => t.Name);
break;
default: break;}
		   #endregion
            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;
        }

        public IQueryable<Program> GetPrograms()
        {
            return programsRepository
                        .Get
						   #region sorting
						   
						   #endregion
                        .AsQueryable();


        }

        public Program GetProgram(Guid programsId)
        {
            return programsRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(programsId));
        }

        public OperationStatus AddProgram(Program programs)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                programsRepository.Add(programs);
                programsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateProgram(Program programs)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                programsRepository.Update(programs);
                programsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteProgram(Guid programsId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var programs = programsRepository.Get.SingleOrDefault(t => t.Id.Equals(programsId));
                if (programs != null)
                {
                    programsRepository.Remove(programs);
                    programsRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "Program not found";
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
