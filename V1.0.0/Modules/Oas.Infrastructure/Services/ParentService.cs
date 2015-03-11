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
    public class ParentService : IParentService
    {
        #region fields
        private readonly IRepository<Parent> parentsRepository;
        #endregion

		#region constructors
        public ParentService(IRepository<Parent> parentsRepository)
        {
            this.parentsRepository = parentsRepository;
        }
		#endregion


        #region public methods

        public IQueryable<Parent> SearchParent(ParentCriteria criteria, ref int totalRecords)
        {
            var query = parentsRepository
                       .Get
.Where(t=>(criteria.Id==null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id) )
&&(criteria.ParentType==null || t.ParentType.Equals(criteria.ParentType) )
&&(string.IsNullOrEmpty(criteria.FirstName)|| ( t.FirstName.Contains(criteria.FirstName) || criteria.FirstName.Contains(t.FirstName) ))
&&(string.IsNullOrEmpty(criteria.LastName)|| ( t.LastName.Contains(criteria.LastName) || criteria.LastName.Contains(t.LastName) ))
&&(criteria.DateOfBirth==null || ( t.DateOfBirth.Equals(criteria.DateOfBirth) || criteria.DateOfBirth.Equals(t.DateOfBirth) ))
&&(string.IsNullOrEmpty(criteria.Address)|| ( t.Address.Contains(criteria.Address) || criteria.Address.Contains(t.Address) ))
&&(string.IsNullOrEmpty(criteria.PhoneNumber)|| ( t.PhoneNumber.Contains(criteria.PhoneNumber) || criteria.PhoneNumber.Contains(t.PhoneNumber) ))
&&(string.IsNullOrEmpty(criteria.Email)|| ( t.Email.Contains(criteria.Email) || criteria.Email.Contains(t.Email) ))
&&(criteria.Gender==null || t.Gender.Equals(criteria.Gender) )
&&(string.IsNullOrEmpty(criteria.FaceBook)|| ( t.FaceBook.Contains(criteria.FaceBook) || criteria.FaceBook.Contains(t.FaceBook) ))
&&(string.IsNullOrEmpty(criteria.Twitter)|| ( t.Twitter.Contains(criteria.Twitter) || criteria.Twitter.Contains(t.Twitter) ))
&&(string.IsNullOrEmpty(criteria.googleplus)|| ( t.googleplus.Contains(criteria.googleplus) || criteria.googleplus.Contains(t.googleplus) ))
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
case "facebook" :
query = isAsc ? query.OrderBy(t => t.FaceBook) : query.OrderByDescending(t => t.FaceBook);
break;
case "twitter" :
query = isAsc ? query.OrderBy(t => t.Twitter) : query.OrderByDescending(t => t.Twitter);
break;
case "googleplus" :
query = isAsc ? query.OrderBy(t => t.googleplus) : query.OrderByDescending(t => t.googleplus);
break;
default: break;}
		   #endregion
            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;
        }

        public IQueryable<Parent> GetParents()
        {
            return parentsRepository
                        .Get
						   #region sorting
						   
						   #endregion
                        .AsQueryable();


        }

        public Parent GetParent(Guid parentsId)
        {
            return parentsRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(parentsId));
        }

        public OperationStatus AddParent(Parent parents)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                parentsRepository.Add(parents);
                parentsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateParent(Parent parents)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                parentsRepository.Update(parents);
                parentsRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteParent(Guid parentsId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var parents = parentsRepository.Get.SingleOrDefault(t => t.Id.Equals(parentsId));
                if (parents != null)
                {
                    parentsRepository.Remove(parents);
                    parentsRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "Parent not found";
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
