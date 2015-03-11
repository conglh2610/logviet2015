using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IStudentService
    {
        #region Student

        IQueryable<Student> GetStudents();

        IQueryable<Student> SearchStudent(StudentCriteria criteria, ref int totalRecords);

        Student GetStudent(Guid studentsId);

        OperationStatus AddStudent(Student students);

        OperationStatus UpdateStudent(Student students);

        OperationStatus DeleteStudent(Guid studentsId);

        #endregion

        List<StudentClassHistoryViewModel> ViewStudentClassHistory(Guid studentsId, StudentCriteria criteria, ref int totalRecords);


    }
}
