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
    public class ResultService : IResultService
    {
        #region fields
        private readonly IRepository<Result> resultsRepository;
        #endregion

		#region constructors
        public ResultService(IRepository<Result> resultsRepository)
        {
            this.resultsRepository = resultsRepository;
        }
		#endregion


        #region public methods

        public IQueryable<Result> SearchResult(ResultCriteria criteria, ref int totalRecords)
        {
            var query = resultsRepository
                       .Get
.Where(t=>(criteria.Id==null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id) )
&&(criteria.ClassStudentId==null || criteria.ClassStudentId == Guid.Empty || t.ClassStudentId.Equals(criteria.ClassStudentId) )
&&(criteria.ClassId==null || criteria.ClassId == Guid.Empty || t.ClassId.Equals(criteria.ClassId) )
&&(criteria.SkillId==null || criteria.SkillId == Guid.Empty || t.SkillId.Equals(criteria.SkillId) )
&&(criteria.Score==null || t.Score.Equals(criteria.Score) )
&&(criteria.CreateDate==null || ( t.CreateDate.Equals(criteria.CreateDate) || criteria.CreateDate.Equals(t.CreateDate) ))
)
                       .AsQueryable();

            totalRecords = query.Count();

            criteria.SortColumn = string.IsNullOrEmpty(criteria.SortColumn) ? string.Empty : criteria.SortColumn.ToLower();
            bool isAsc = criteria.SortDirection.ToLower().Equals("false");

           #region sorting
switch (criteria.SortColumn){
case "createdate" :
query = isAsc ? query.OrderBy(t => t.CreateDate) : query.OrderByDescending(t => t.CreateDate);
break;
default: break;}
		   #endregion
            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;
        }

        public IQueryable<Result> GetResults()
        {
            return resultsRepository
                        .Get
						   #region sorting
						   
						   #endregion
                        .AsQueryable();


        }

        public Result GetResult(Guid resultsId)
        {
            return resultsRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(resultsId));
        }

        public OperationStatus AddResult(Result results)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                resultsRepository.Add(results);
                resultsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateResult(Result results)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                resultsRepository.Update(results);
                resultsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteResult(Guid resultsId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var results = resultsRepository.Get.SingleOrDefault(t => t.Id.Equals(resultsId));
                if (results != null)
                {
                    resultsRepository.Remove(results);
                    resultsRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "Result not found";
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
