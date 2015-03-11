using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IMembershipPackageService
    {
        #region MembershipPackage

        IQueryable<MembershipPackage> GetMembershipPackages();

        IQueryable<MembershipPackage> SearchMembershipPackage(MembershipPackageCriteria criteria, ref int totalRecords);

        MembershipPackage GetMembershipPackage(Guid membershippackagesId);

        OperationStatus AddMembershipPackage(MembershipPackage membershippackages);

        OperationStatus UpdateMembershipPackage(MembershipPackage membershippackages);

        OperationStatus DeleteMembershipPackage(Guid membershippackagesId);

        #endregion


    }
}
