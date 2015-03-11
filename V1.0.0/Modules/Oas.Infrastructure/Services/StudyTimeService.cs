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
    public class StudyTimeService : IStudyTimeService
    {
        #region fields
        private readonly IRepository<StudyTime> studytimesRepository;
        #endregion

		#region constructors
        public StudyTimeService(IRepository<StudyTime> studytimesRepository)
        {
            this.studytimesRepository = studytimesRepository;
        }
		#endregion


        #region public methods

        public IQueryable<StudyTime> SearchStudyTime(StudyTimeCriteria criteria, ref int totalRecords)
        {
            var query = studytimesRepository
                       .Get
.Where(t=>(criteria.Id==null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id) )
&&(string.IsNullOrEmpty(criteria.Name)|| ( t.Name.Contains(criteria.Name) || criteria.Name.Contains(t.Name) ))
&&(criteria.StartHour==null || t.StartHour.Equals(criteria.StartHour) )
&&(criteria.EndHour==null || t.EndHour.Equals(criteria.EndHour) )
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

        public IQueryable<StudyTime> GetStudyTimes()
        {
            return studytimesRepository
                        .Get
						   #region sorting
						   
						   #endregion
                        .AsQueryable();


        }

        public StudyTime GetStudyTime(Guid studytimesId)
        {
            return studytimesRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(studytimesId));
        }

        public OperationStatus AddStudyTime(StudyTime studytimes)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                studytimesRepository.Add(studytimes);
                studytimesRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateStudyTime(StudyTime studytimes)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                studytimesRepository.Update(studytimes);
                studytimesRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteStudyTime(Guid studytimesId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var studytimes = studytimesRepository.Get.SingleOrDefault(t => t.Id.Equals(studytimesId));
                if (studytimes != null)
                {
                    studytimesRepository.Remove(studytimes);
                    studytimesRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "StudyTime not found";
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
