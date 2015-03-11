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
    public class TeacherService : ITeacherService
    {
        #region fields
        private readonly IRepository<Teacher> teachersRepository;
        #endregion

		#region constructors
        public TeacherService(IRepository<Teacher> teachersRepository)
        {
            this.teachersRepository = teachersRepository;
        }
		#endregion


        #region public methods

        public IQueryable<Teacher> SearchTeacher(TeacherCriteria criteria, ref int totalRecords)
        {
            var query = teachersRepository
                       .Get
.Where(t=>(criteria.Id==null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id) )
&&(string.IsNullOrEmpty(criteria.FirstName)|| ( t.FirstName.Contains(criteria.FirstName) || criteria.FirstName.Contains(t.FirstName) ))
&&(string.IsNullOrEmpty(criteria.LastName)|| ( t.LastName.Contains(criteria.LastName) || criteria.LastName.Contains(t.LastName) ))
&&(criteria.DateOfBirth==null || ( t.DateOfBirth.Equals(criteria.DateOfBirth) || criteria.DateOfBirth.Equals(t.DateOfBirth) ))
&&(string.IsNullOrEmpty(criteria.Address)|| ( t.Address.Contains(criteria.Address) || criteria.Address.Contains(t.Address) ))
&&(string.IsNullOrEmpty(criteria.PhoneNumber)|| ( t.PhoneNumber.Contains(criteria.PhoneNumber) || criteria.PhoneNumber.Contains(t.PhoneNumber) ))
&&(string.IsNullOrEmpty(criteria.Email)|| ( t.Email.Contains(criteria.Email) || criteria.Email.Contains(t.Email) ))
&&(criteria.Gender==null || t.Gender.Equals(criteria.Gender) )
&&(criteria.SalaryByHour==null || t.SalaryByHour.Equals(criteria.SalaryByHour) )
&&(criteria.CountryId==null || criteria.CountryId == Guid.Empty || t.CountryId.Equals(criteria.CountryId) )
)
                       .AsQueryable();

            totalRecords = query.Count();

            criteria.SortColumn = string.IsNullOrEmpty(criteria.SortColumn) ? string.Empty : criteria.SortColumn.ToLower();
            bool isAsc = criteria.SortDirection.ToLower().Equals("false");

           #region sorting
switch (criteria.SortColumn){
case "firstname" :
query = isAsc ? query.OrderBy(t => t.FirstName) : query.OrderByDescending(t => t.FirstName);
break;
case "lastname" :
query = isAsc ? query.OrderBy(t => t.LastName) : query.OrderByDescending(t => t.LastName);
break;
case "dateofbirth" :
query = isAsc ? query.OrderBy(t => t.DateOfBirth) : query.OrderByDescending(t => t.DateOfBirth);
break;
case "address" :
query = isAsc ? query.OrderBy(t => t.Address) : query.OrderByDescending(t => t.Address);
break;
case "phonenumber" :
query = isAsc ? query.OrderBy(t => t.PhoneNumber) : query.OrderByDescending(t => t.PhoneNumber);
break;
case "email" :
query = isAsc ? query.OrderBy(t => t.Email) : query.OrderByDescending(t => t.Email);
break;
default: break;}
		   #endregion
            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;
        }

        public IQueryable<Teacher> GetTeachers()
        {
            return teachersRepository
                        .Get
						   #region sorting
						   
						   #endregion
                        .AsQueryable();


        }

        public Teacher GetTeacher(Guid teachersId)
        {
            return teachersRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(teachersId));
        }

        public OperationStatus AddTeacher(Teacher teachers)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                teachersRepository.Add(teachers);
                teachersRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateTeacher(Teacher teachers)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                teachersRepository.Update(teachers);
                teachersRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteTeacher(Guid teachersId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var teachers = teachersRepository.Get.SingleOrDefault(t => t.Id.Equals(teachersId));
                if (teachers != null)
                {
                    teachersRepository.Remove(teachers);
                    teachersRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "Teacher not found";
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
