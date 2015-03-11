using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IClassTeacherService
    {
        #region ClassTeacher

        IQueryable<ClassTeacher> GetClassTeachers();

        IQueryable<ClassTeacher> SearchClassTeacher(ClassTeacherCriteria criteria, ref int totalRecords);

        ClassTeacher GetClassTeacher(Guid classteachersId);

        OperationStatus AddClassTeacher(ClassTeacher classteachers);

        OperationStatus UpdateClassTeacher(ClassTeacher classteachers);

        OperationStatus DeleteClassTeacher(Guid classteachersId);

        #endregion


    }
}
