using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface ITeacherService
    {
        #region Teacher

        IQueryable<Teacher> GetTeachers();

        IQueryable<Teacher> SearchTeacher(TeacherCriteria criteria, ref int totalRecords);

        Teacher GetTeacher(Guid teachersId);

        OperationStatus AddTeacher(Teacher teachers);

        OperationStatus UpdateTeacher(Teacher teachers);

        OperationStatus DeleteTeacher(Guid teachersId);

        #endregion


    }
}
