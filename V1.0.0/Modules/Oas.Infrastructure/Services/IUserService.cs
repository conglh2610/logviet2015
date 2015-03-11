using Oas.Infrastructure.Criteria;
using Oas.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oas.Infrastructure.Services
{
    public interface IUserService
    {
        #region User

        IQueryable<User> GetUsers();

        IQueryable<User> SearchUser(UserCriteria criteria, ref int totalRecords);

        User GetUser(Guid usersId);

        OperationStatus AddUser(User users);

        OperationStatus UpdateUser(User users);

        OperationStatus DeleteUser(Guid usersId);

        #endregion


    }
}
