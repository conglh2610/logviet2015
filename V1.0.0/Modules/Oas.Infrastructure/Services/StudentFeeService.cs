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
    public class StudentFeeService : IStudentFeeService
    {
        #region fields
        private readonly IRepository<StudentFee> studentfeesRepository;
        #endregion

		#region constructors
        public StudentFeeService(IRepository<StudentFee> studentfeesRepository)
        {
            this.studentfeesRepository = studentfeesRepository;
        }
		#endregion


        #region public methods

        public IQueryable<StudentFee> SearchStudentFee(StudentFeeCriteria criteria, ref int totalRecords)
        {
            var query = studentfeesRepository
                       .Get
.Where(t=>(criteria.Id==null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id) )
&&(criteria.ClassStudentId==null || criteria.ClassStudentId == Guid.Empty || t.ClassStudentId.Equals(criteria.ClassStudentId) )
&&(criteria.TransferedStudentId==null || criteria.TransferedStudentId == Guid.Empty || t.TransferedStudentId.Equals(criteria.TransferedStudentId) )
&&(criteria.TotalPay==null || t.TotalPay.Equals(criteria.TotalPay) )
&&(criteria.RemainMoney==null || t.RemainMoney.Equals(criteria.RemainMoney) )
&&(criteria.CreateDate==null || ( t.CreateDate.Equals(criteria.CreateDate) || criteria.CreateDate.Equals(t.CreateDate) ))
&&(criteria.FeePaymentStatus==null || t.FeePaymentStatus.Equals(criteria.FeePaymentStatus) )
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

        public IQueryable<StudentFee> GetStudentFees()
        {
            return studentfeesRepository
                        .Get
						   #region sorting
						   
						   #endregion
                        .AsQueryable();


        }

        public StudentFee GetStudentFee(Guid studentfeesId)
        {
            return studentfeesRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(studentfeesId));
        }

        public OperationStatus AddStudentFee(StudentFee studentfees)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                studentfeesRepository.Add(studentfees);
                studentfeesRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateStudentFee(StudentFee studentfees)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                studentfeesRepository.Update(studentfees);
                studentfeesRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteStudentFee(Guid studentfeesId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var studentfees = studentfeesRepository.Get.SingleOrDefault(t => t.Id.Equals(studentfeesId));
                if (studentfees != null)
                {
                    studentfeesRepository.Remove(studentfees);
                    studentfeesRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "StudentFee not found";
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
