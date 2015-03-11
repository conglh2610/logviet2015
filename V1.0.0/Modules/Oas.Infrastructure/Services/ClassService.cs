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
    public class ClassService : IClassService
    {
        #region fields
        private readonly IRepository<Class> classesRepository;
        #endregion

		#region constructors
        public ClassService(IRepository<Class> classesRepository)
        {
            this.classesRepository = classesRepository;
        }
		#endregion


        #region public methods

        public IQueryable<Class> SearchClass(ClassCriteria criteria, ref int totalRecords)
        {
            var query = classesRepository
                       .Get
.Where(t=>(criteria.Id==null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id) )
&&(criteria.Number==null || t.Number.Equals(criteria.Number) )
&&(string.IsNullOrEmpty(criteria.Name)|| ( t.Name.Contains(criteria.Name) || criteria.Name.Contains(t.Name) ))
&&(criteria.ProgramId==null || criteria.ProgramId == Guid.Empty || t.ProgramId.Equals(criteria.ProgramId) )
&&(criteria.EmployeeId==null || criteria.EmployeeId == Guid.Empty || t.EmployeeId.Equals(criteria.EmployeeId) )
&&(criteria.StartDate==null || ( t.StartDate.Equals(criteria.StartDate) || criteria.StartDate.Equals(t.StartDate) ))
&&(criteria.EndDate==null || ( t.EndDate.Equals(criteria.EndDate) || criteria.EndDate.Equals(t.EndDate) ))
&&(criteria.Size==null || t.Size.Equals(criteria.Size) )
&&(criteria.Status==null || t.Status.Equals(criteria.Status) )
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
case "startdate" :
query = isAsc ? query.OrderBy(t => t.StartDate) : query.OrderByDescending(t => t.StartDate);
break;
case "enddate" :
query = isAsc ? query.OrderBy(t => t.EndDate) : query.OrderByDescending(t => t.EndDate);
break;
default: break;}
		   #endregion
            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;
        }

        public IQueryable<Class> GetClasss()
        {
            return classesRepository
                        .Get
						   #region sorting
						   
						   #endregion
                        .AsQueryable();


        }

        public Class GetClass(Guid classesId)
        {
            return classesRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(classesId));
        }

        public OperationStatus AddClass(Class classes)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                classesRepository.Add(classes);
                classesRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateClass(Class classes)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                classesRepository.Update(classes);
                classesRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteClass(Guid classesId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var classes = classesRepository.Get.SingleOrDefault(t => t.Id.Equals(classesId));
                if (classes != null)
                {
                    classesRepository.Remove(classes);
                    classesRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "Class not found";
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
