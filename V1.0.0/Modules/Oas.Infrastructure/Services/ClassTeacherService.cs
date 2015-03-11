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
    public class ClassTeacherService : IClassTeacherService
    {
        #region fields
        private readonly IRepository<ClassTeacher> classteachersRepository;
        #endregion

		#region constructors
        public ClassTeacherService(IRepository<ClassTeacher> classteachersRepository)
        {
            this.classteachersRepository = classteachersRepository;
        }
		#endregion


        #region public methods

        public IQueryable<ClassTeacher> SearchClassTeacher(ClassTeacherCriteria criteria, ref int totalRecords)
        {
            var query = classteachersRepository
                       .Get
.Where(t=>(criteria.Id==null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id) )
&&(criteria.ClassId==null || criteria.ClassId == Guid.Empty || t.ClassId.Equals(criteria.ClassId) )
&&(criteria.TeacherId==null || criteria.TeacherId == Guid.Empty || t.TeacherId.Equals(criteria.TeacherId) )
&&(criteria.Status==null || t.Status.Equals(criteria.Status) )
&&(criteria.SalaryByHour==null || t.SalaryByHour.Equals(criteria.SalaryByHour) )
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

        public IQueryable<ClassTeacher> GetClassTeachers()
        {
            return classteachersRepository
                        .Get
						   #region sorting
						   
						   #endregion
                        .AsQueryable();


        }

        public ClassTeacher GetClassTeacher(Guid classteachersId)
        {
            return classteachersRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(classteachersId));
        }

        public OperationStatus AddClassTeacher(ClassTeacher classteachers)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                classteachersRepository.Add(classteachers);
                classteachersRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateClassTeacher(ClassTeacher classteachers)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                classteachersRepository.Update(classteachers);
                classteachersRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteClassTeacher(Guid classteachersId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var classteachers = classteachersRepository.Get.SingleOrDefault(t => t.Id.Equals(classteachersId));
                if (classteachers != null)
                {
                    classteachersRepository.Remove(classteachers);
                    classteachersRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "ClassTeacher not found";
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
