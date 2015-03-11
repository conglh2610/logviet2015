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
    public class ClassStudentService : IClassStudentService
    {
        #region fields
        private readonly IRepository<ClassStudent> classstudentsRepository;
        #endregion

		#region constructors
        public ClassStudentService(IRepository<ClassStudent> classstudentsRepository)
        {
            this.classstudentsRepository = classstudentsRepository;
        }
		#endregion


        #region public methods

        public IQueryable<ClassStudent> SearchClassStudent(ClassStudentCriteria criteria, ref int totalRecords)
        {
            var query = classstudentsRepository
                       .Get
.Where(t=>(criteria.Id==null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id) )
&&(criteria.ClassId==null || criteria.ClassId == Guid.Empty || t.ClassId.Equals(criteria.ClassId) )
&&(criteria.StudentId==null || criteria.StudentId == Guid.Empty || t.StudentId.Equals(criteria.StudentId) )
&&(criteria.Status==null || t.Status.Equals(criteria.Status) )
&&(criteria.Discount==null || t.Discount.Equals(criteria.Discount) )
&&(criteria.FinalResult==null || t.FinalResult.Equals(criteria.FinalResult) )
)
                       .AsQueryable();

            totalRecords = query.Count();

            criteria.SortColumn = string.IsNullOrEmpty(criteria.SortColumn) ? string.Empty : criteria.SortColumn.ToLower();
            bool isAsc = criteria.SortDirection.ToLower().Equals("false");

           #region sorting
switch (criteria.SortColumn){
default: break;}
		   #endregion
            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;
        }

        public IQueryable<ClassStudent> GetClassStudents()
        {
            return classstudentsRepository
                        .Get
						   #region sorting
						   
						   #endregion
                        .AsQueryable();


        }

        public ClassStudent GetClassStudent(Guid classstudentsId)
        {
            return classstudentsRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(classstudentsId));
        }

        public OperationStatus AddClassStudent(ClassStudent classstudents)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                classstudentsRepository.Add(classstudents);
                classstudentsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateClassStudent(ClassStudent classstudents)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                classstudentsRepository.Update(classstudents);
                classstudentsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteClassStudent(Guid classstudentsId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var classstudents = classstudentsRepository.Get.SingleOrDefault(t => t.Id.Equals(classstudentsId));
                if (classstudents != null)
                {
                    classstudentsRepository.Remove(classstudents);
                    classstudentsRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "ClassStudent not found";
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
