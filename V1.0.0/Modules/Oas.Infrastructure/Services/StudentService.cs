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
    public class StudentService : IStudentService
    {
        #region fields
        private readonly IRepository<Student> studentsRepository;
        private readonly IRepository<ClassStudent> classStudentRepository;
        #endregion

        #region constructors
        public StudentService(IRepository<Student> studentsRepository, IRepository<ClassStudent> classStudentRepository)
        {
            this.studentsRepository = studentsRepository;
            this.classStudentRepository = classStudentRepository;
        }
        #endregion


        #region public methods

        public IQueryable<Student> SearchStudent(StudentCriteria criteria, ref int totalRecords)
        {
            var query = studentsRepository
                       .Get
                        .Where(t => (criteria.Id == null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id))
                        && (string.IsNullOrEmpty(criteria.FirstName) || (t.FirstName.Contains(criteria.FirstName) || criteria.FirstName.Contains(t.FirstName)))
                        && (string.IsNullOrEmpty(criteria.LastName) || (t.LastName.Contains(criteria.LastName) || criteria.LastName.Contains(t.LastName)))
                        && (string.IsNullOrEmpty(criteria.Address) || (t.Address.Contains(criteria.Address) || criteria.Address.Contains(t.Address)))
                        && (string.IsNullOrEmpty(criteria.PhoneNumber) || (t.PhoneNumber.Contains(criteria.PhoneNumber) || criteria.PhoneNumber.Contains(t.PhoneNumber)))
                        && (string.IsNullOrEmpty(criteria.Email) || (t.Email.Contains(criteria.Email) || criteria.Email.Contains(t.Email)))
                        && (criteria.Gender == null || t.Gender.Equals(criteria.Gender))
                        && (string.IsNullOrEmpty(criteria.FaceBook) || (t.FaceBook.Contains(criteria.FaceBook) || criteria.FaceBook.Contains(t.FaceBook)))
                        && (string.IsNullOrEmpty(criteria.Twitter) || (t.Twitter.Contains(criteria.Twitter) || criteria.Twitter.Contains(t.Twitter)))
                        && (string.IsNullOrEmpty(criteria.googleplus) || (t.googleplus.Contains(criteria.googleplus) || criteria.googleplus.Contains(t.googleplus)))
                        )
                       .AsQueryable();

            totalRecords = query.Count();

            criteria.SortColumn = string.IsNullOrEmpty(criteria.SortColumn) ? string.Empty : criteria.SortColumn.ToLower();
            bool isAsc = criteria.SortDirection.ToLower().Equals("false");

            #region sorting
            switch (criteria.SortColumn)
            {
                case "firstname":
                case "name":
                    query = isAsc ? query.OrderBy(t => t.FirstName) : query.OrderByDescending(t => t.FirstName);
                    break;
                case "lastname":
                    query = isAsc ? query.OrderBy(t => t.LastName) : query.OrderByDescending(t => t.LastName);
                    break;
                case "dateofbirth":
                    query = isAsc ? query.OrderBy(t => t.DateOfBirth) : query.OrderByDescending(t => t.DateOfBirth);
                    break;
                case "address":
                    query = isAsc ? query.OrderBy(t => t.Address) : query.OrderByDescending(t => t.Address);
                    break;
                case "phonenumber":
                    query = isAsc ? query.OrderBy(t => t.PhoneNumber) : query.OrderByDescending(t => t.PhoneNumber);
                    break;
                case "email":
                    query = isAsc ? query.OrderBy(t => t.Email) : query.OrderByDescending(t => t.Email);
                    break;
                case "facebook":
                    query = isAsc ? query.OrderBy(t => t.FaceBook) : query.OrderByDescending(t => t.FaceBook);
                    break;
                case "twitter":
                    query = isAsc ? query.OrderBy(t => t.Twitter) : query.OrderByDescending(t => t.Twitter);
                    break;
                case "googleplus":
                    query = isAsc ? query.OrderBy(t => t.googleplus) : query.OrderByDescending(t => t.googleplus);
                    break;
                default:
                    query = isAsc ? query.OrderBy(t => t.FirstName) : query.OrderByDescending(t => t.FirstName);
                    break;

            }
            #endregion
            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;
        }

        public IQueryable<Student> GetStudents()
        {
            return studentsRepository
                        .Get
            #region sorting

            #endregion
.AsQueryable();


        }

        public Student GetStudent(Guid studentsId)
        {
            return studentsRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(studentsId));
        }

        public OperationStatus AddStudent(Student students)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                studentsRepository.Add(students);
                studentsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateStudent(Student students)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                studentsRepository.Update(students);
                studentsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteStudent(Guid studentsId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var students = studentsRepository.Get.SingleOrDefault(t => t.Id.Equals(studentsId));
                if (students != null)
                {
                    studentsRepository.Remove(students);
                    studentsRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "Student not found";
                }
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public List<StudentClassHistoryViewModel> ViewStudentClassHistory(Guid studentId, StudentCriteria criteria, ref int totalRecords)
        {
            var query = classStudentRepository
                .Get
                .Where(x => x.StudentId == studentId);


            var result = (from q in query
                          select new StudentClassHistoryViewModel
                          {
                              LanguageName = q.Class.Program.Language.Name,
                              ClassName = q.Class.Name,
                              EndDate = q.Class.EndDate,
                              ProgramName = q.Class.Program.Name,
                              Result = (int)q.FinalResult,
                              StartDate = q.Class.StartDate,
                              Status = (int)q.Class.Status
                          }).ToList();

            totalRecords = result.Count;

            return result;
        }

        #endregion

    }
}
