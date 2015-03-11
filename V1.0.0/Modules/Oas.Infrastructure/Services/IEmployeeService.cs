using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IEmployeeService
    {
        #region Employee

        IQueryable<Employee> GetEmployees();

        IQueryable<Employee> SearchEmployee(EmployeeCriteria criteria, ref int totalRecords);

        Employee GetEmployee(Guid employeesId);

        OperationStatus AddEmployee(Employee employees);

        OperationStatus UpdateEmployee(Employee employees);

        OperationStatus DeleteEmployee(Guid employeesId);

        #endregion


    }
}
