using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IClassStudentService
    {
        #region ClassStudent

        IQueryable<ClassStudent> GetClassStudents();

        IQueryable<ClassStudent> SearchClassStudent(ClassStudentCriteria criteria, ref int totalRecords);

        ClassStudent GetClassStudent(Guid classstudentsId);

        OperationStatus AddClassStudent(ClassStudent classstudents);

        OperationStatus UpdateClassStudent(ClassStudent classstudents);

        OperationStatus DeleteClassStudent(Guid classstudentsId);

        #endregion


    }
}
