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
    public class UserService : IUserService
    {
        #region fields
        private readonly IRepository<User> usersRepository;
        #endregion

		#region constructors
        public UserService(IRepository<User> usersRepository)
        {
            this.usersRepository = usersRepository;
        }
		#endregion


        #region public methods

        public IQueryable<User> SearchUser(UserCriteria criteria, ref int totalRecords)
        {
            var query = usersRepository
                       .Get
.Where(t=>(string.IsNullOrEmpty(criteria.Id)|| ( t.Id.Contains(criteria.Id) || criteria.Id.Contains(t.Id) ))
&&(string.IsNullOrEmpty(criteria.Email)|| ( t.Email.Contains(criteria.Email) || criteria.Email.Contains(t.Email) ))
&&(criteria.EmailConfirmed==null || t.EmailConfirmed.Equals(criteria.EmailConfirmed) )
&&(string.IsNullOrEmpty(criteria.PasswordHash)|| ( t.PasswordHash.Contains(criteria.PasswordHash) || criteria.PasswordHash.Contains(t.PasswordHash) ))
&&(string.IsNullOrEmpty(criteria.SecurityStamp)|| ( t.SecurityStamp.Contains(criteria.SecurityStamp) || criteria.SecurityStamp.Contains(t.SecurityStamp) ))
&&(string.IsNullOrEmpty(criteria.PhoneNumber)|| ( t.PhoneNumber.Contains(criteria.PhoneNumber) || criteria.PhoneNumber.Contains(t.PhoneNumber) ))
&&(criteria.PhoneNumberConfirmed==null || t.PhoneNumberConfirmed.Equals(criteria.PhoneNumberConfirmed) )
&&(criteria.TwoFactorEnabled==null || t.TwoFactorEnabled.Equals(criteria.TwoFactorEnabled) )
&&(criteria.LockoutEndDateUtc==null || ( t.LockoutEndDateUtc.Equals(criteria.LockoutEndDateUtc) || criteria.LockoutEndDateUtc.Equals(t.LockoutEndDateUtc) ))
&&(criteria.LockoutEnabled==null || t.LockoutEnabled.Equals(criteria.LockoutEnabled) )
&&(criteria.AccessFailedCount==null || t.AccessFailedCount.Equals(criteria.AccessFailedCount) )
&&(string.IsNullOrEmpty(criteria.UserName)|| ( t.UserName.Contains(criteria.UserName) || criteria.UserName.Contains(t.UserName) ))
&&(string.IsNullOrEmpty(criteria.FirstName)|| ( t.FirstName.Contains(criteria.FirstName) || criteria.FirstName.Contains(t.FirstName) ))
&&(string.IsNullOrEmpty(criteria.LastName)|| ( t.LastName.Contains(criteria.LastName) || criteria.LastName.Contains(t.LastName) ))
&&(string.IsNullOrEmpty(criteria.Address)|| ( t.Address.Contains(criteria.Address) || criteria.Address.Contains(t.Address) ))
&&(string.IsNullOrEmpty(criteria.Phone)|| ( t.Phone.Contains(criteria.Phone) || criteria.Phone.Contains(t.Phone) ))
&&(string.IsNullOrEmpty(criteria.ProfileImage)|| ( t.ProfileImage.Contains(criteria.ProfileImage) || criteria.ProfileImage.Contains(t.ProfileImage) ))
&&(criteria.AccountType==null || t.AccountType.Equals(criteria.AccountType) )
&&(criteria.Suspend==null || t.Suspend.Equals(criteria.Suspend) )
&&(string.IsNullOrEmpty(criteria.Tips)|| ( t.Tips.Contains(criteria.Tips) || criteria.Tips.Contains(t.Tips) ))
&&(criteria.Gender==null || t.Gender.Equals(criteria.Gender) )
&&(string.IsNullOrEmpty(criteria.ContactTitle)|| ( t.ContactTitle.Contains(criteria.ContactTitle) || criteria.ContactTitle.Contains(t.ContactTitle) ))
&&(criteria.MembershipPackageId==null || criteria.MembershipPackageId == Guid.Empty || t.MembershipPackageId.Equals(criteria.MembershipPackageId) )
&&(criteria.StartDate==null || ( t.StartDate.Equals(criteria.StartDate) || criteria.StartDate.Equals(t.StartDate) ))
&&(criteria.ExpireDate==null || ( t.ExpireDate.Equals(criteria.ExpireDate) || criteria.ExpireDate.Equals(t.ExpireDate) ))
&&(criteria.Status==null || t.Status.Equals(criteria.Status) )
&&(criteria.PackagePrice==null || t.PackagePrice.Equals(criteria.PackagePrice) )
&&(criteria.PaymentMethod==null || t.PaymentMethod.Equals(criteria.PaymentMethod) )
&&(criteria.PaymentPeriod==null || t.PaymentPeriod.Equals(criteria.PaymentPeriod) )
&&(criteria.IsOnline==null || t.IsOnline.Equals(criteria.IsOnline) )
)
                       .AsQueryable();

            totalRecords = query.Count();

            criteria.SortColumn = string.IsNullOrEmpty(criteria.SortColumn) ? string.Empty : criteria.SortColumn.ToLower();
            bool isAsc = criteria.SortDirection.ToLower().Equals("false");

           #region sorting
switch (criteria.SortColumn){
case "id" :
query = isAsc ? query.OrderBy(t => t.Id) : query.OrderByDescending(t => t.Id);
break;
case "email" :
query = isAsc ? query.OrderBy(t => t.Email) : query.OrderByDescending(t => t.Email);
break;
case "passwordhash" :
query = isAsc ? query.OrderBy(t => t.PasswordHash) : query.OrderByDescending(t => t.PasswordHash);
break;
case "securitystamp" :
query = isAsc ? query.OrderBy(t => t.SecurityStamp) : query.OrderByDescending(t => t.SecurityStamp);
break;
case "phonenumber" :
query = isAsc ? query.OrderBy(t => t.PhoneNumber) : query.OrderByDescending(t => t.PhoneNumber);
break;
case "lockoutenddateutc" :
query = isAsc ? query.OrderBy(t => t.LockoutEndDateUtc) : query.OrderByDescending(t => t.LockoutEndDateUtc);
break;
case "username" :
query = isAsc ? query.OrderBy(t => t.UserName) : query.OrderByDescending(t => t.UserName);
break;
case "firstname" :
query = isAsc ? query.OrderBy(t => t.FirstName) : query.OrderByDescending(t => t.FirstName);
break;
case "lastname" :
query = isAsc ? query.OrderBy(t => t.LastName) : query.OrderByDescending(t => t.LastName);
break;
case "address" :
query = isAsc ? query.OrderBy(t => t.Address) : query.OrderByDescending(t => t.Address);
break;
case "phone" :
query = isAsc ? query.OrderBy(t => t.Phone) : query.OrderByDescending(t => t.Phone);
break;
case "profileimage" :
query = isAsc ? query.OrderBy(t => t.ProfileImage) : query.OrderByDescending(t => t.ProfileImage);
break;
case "tips" :
query = isAsc ? query.OrderBy(t => t.Tips) : query.OrderByDescending(t => t.Tips);
break;
case "contacttitle" :
query = isAsc ? query.OrderBy(t => t.ContactTitle) : query.OrderByDescending(t => t.ContactTitle);
break;
case "startdate" :
query = isAsc ? query.OrderBy(t => t.StartDate) : query.OrderByDescending(t => t.StartDate);
break;
case "expiredate" :
query = isAsc ? query.OrderBy(t => t.ExpireDate) : query.OrderByDescending(t => t.ExpireDate);
break;
default: break;}
		   #endregion
            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;
        }

        public IQueryable<User> GetUsers()
        {
            return usersRepository
                        .Get
						   #region sorting
						   
						   #endregion
                        .AsQueryable();


        }

        public User GetUser(Guid usersId)
        {
            return usersRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(usersId));
        }

        public OperationStatus AddUser(User users)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                usersRepository.Add(users);
                usersRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateUser(User users)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                usersRepository.Update(users);
                usersRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteUser(Guid usersId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var users = usersRepository.Get.SingleOrDefault(t => t.Id.Equals(usersId));
                if (users != null)
                {
                    usersRepository.Remove(users);
                    usersRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "User not found";
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
