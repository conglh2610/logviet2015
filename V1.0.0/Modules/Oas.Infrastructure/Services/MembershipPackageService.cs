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
    public class MembershipPackageService : IMembershipPackageService
    {
        #region fields
        private readonly IRepository<MembershipPackage> membershippackagesRepository;
        #endregion

		#region constructors
        public MembershipPackageService(IRepository<MembershipPackage> membershippackagesRepository)
        {
            this.membershippackagesRepository = membershippackagesRepository;
        }
		#endregion


        #region public methods

        public IQueryable<MembershipPackage> SearchMembershipPackage(MembershipPackageCriteria criteria, ref int totalRecords)
        {
            var query = membershippackagesRepository
                       .Get
.Where(t=>(criteria.Id==null || criteria.Id == Guid.Empty || t.Id.Equals(criteria.Id) )
&&(string.IsNullOrEmpty(criteria.Name)|| ( t.Name.Contains(criteria.Name) || criteria.Name.Contains(t.Name) ))
&&(string.IsNullOrEmpty(criteria.Description)|| ( t.Description.Contains(criteria.Description) || criteria.Description.Contains(t.Description) ))
&&(criteria.Price==null || t.Price.Equals(criteria.Price) )
&&(criteria.OldPrice==null || t.OldPrice.Equals(criteria.OldPrice) )
&&(criteria.Duration==null || t.Duration.Equals(criteria.Duration) )
)
                       .AsQueryable();

            totalRecords = query.Count();

            criteria.SortColumn = string.IsNullOrEmpty(criteria.SortColumn) ? string.Empty : criteria.SortColumn.ToLower();
            bool isAsc = criteria.SortDirection.ToLower().Equals("false");

           #region sorting
switch (criteria.SortColumn){
case "name" :
query = isAsc ? query.OrderBy(t => t.Name) : query.OrderByDescending(t => t.Name);
break;
case "description" :
query = isAsc ? query.OrderBy(t => t.Description) : query.OrderByDescending(t => t.Description);
break;
default: break;}
		   #endregion
            query = query.Skip(criteria.CurrentPage * criteria.ItemPerPage).Take(criteria.ItemPerPage);

            return query;
        }

        public IQueryable<MembershipPackage> GetMembershipPackages()
        {
            return membershippackagesRepository
                        .Get
						   #region sorting
						   
						   #endregion
                        .AsQueryable();


        }

        public MembershipPackage GetMembershipPackage(Guid membershippackagesId)
        {
            return membershippackagesRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(membershippackagesId));
        }

        public OperationStatus AddMembershipPackage(MembershipPackage membershippackages)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                membershippackagesRepository.Add(membershippackages);
                membershippackagesRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateMembershipPackage(MembershipPackage membershippackages)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                membershippackagesRepository.Update(membershippackages);
                membershippackagesRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteMembershipPackage(Guid membershippackagesId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var membershippackages = membershippackagesRepository.Get.SingleOrDefault(t => t.Id.Equals(membershippackagesId));
                if (membershippackages != null)
                {
                    membershippackagesRepository.Remove(membershippackages);
                    membershippackagesRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "MembershipPackage not found";
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
