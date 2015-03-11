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
    public class RoleService : IRoleService
    {
        #region fields
        private readonly IRepository<Role> rolesRepository;
        #endregion

		#region constructors
        public RoleService(IRepository<Role> rolesRepository)
        {
            this.rolesRepository = rolesRepository;
        }
		#endregion


        #region public methods

        public IQueryable<Role> SearchRole(RoleCriteria criteria, ref int totalRecords)
        {
            var query = rolesRepository
                       .Get
.Where(t=>(string.IsNullOrEmpty(criteria.Id)|| ( t.Id.Contains(criteria.Id) || criteria.Id.Contains(t.Id) ))
&&(string.IsNullOrEmpty(criteria.Name)|| ( t.Name.Contains(criteria.Name) || criteria.Name.Contains(t.Name) ))
&&(string.IsNullOrEmpty(criteria.Description)|| ( t.Description.Contains(criteria.Description) || criteria.Description.Contains(t.Description) ))
&&(criteria.ManageUser==null || t.ManageUser.Equals(criteria.ManageUser) )
&&(criteria.ManageBusiness==null || t.ManageBusiness.Equals(criteria.ManageBusiness) )
&&(criteria.ManageMembershipPackage==null || t.ManageMembershipPackage.Equals(criteria.ManageMembershipPackage) )
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

        public IQueryable<Role> GetRoles()
        {
            return rolesRepository
                        .Get
						   #region sorting
						   
						   #endregion
                        .AsQueryable();


        }

        public Role GetRole(Guid rolesId)
        {
            return rolesRepository
                        .Get
                        .FirstOrDefault(t => t.Id.Equals(rolesId));
        }

        public OperationStatus AddRole(Role roles)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                rolesRepository.Add(roles);
                rolesRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateRole(Role roles)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                rolesRepository.Update(roles);
                rolesRepository.Commit();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteRole(Guid rolesId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var roles = rolesRepository.Get.SingleOrDefault(t => t.Id.Equals(rolesId));
                if (roles != null)
                {
                    rolesRepository.Remove(roles);
                    rolesRepository.Commit();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "Role not found";
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
