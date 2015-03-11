using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IStudentFeeService
    {
        #region StudentFee

        IQueryable<StudentFee> GetStudentFees();

        IQueryable<StudentFee> SearchStudentFee(StudentFeeCriteria criteria, ref int totalRecords);

        StudentFee GetStudentFee(Guid studentfeesId);

        OperationStatus AddStudentFee(StudentFee studentfees);

        OperationStatus UpdateStudentFee(StudentFee studentfees);

        OperationStatus DeleteStudentFee(Guid studentfeesId);

        #endregion


    }
}
