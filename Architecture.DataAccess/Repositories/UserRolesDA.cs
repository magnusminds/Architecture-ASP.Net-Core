using Architecture.Core.DataTable;
using Architecture.DataAccess.Generic;
using Architecture.DataAccess.Interface;
using Architecture.DataBase.DatabaseFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.DataAccess.Repositories
{
    public class UserRolesDA : IUserRolesDA
    {
        private readonly IRepositoryDA<UserRole> _usersRole;
        public UserRolesDA(IRepositoryDA<UserRole> usersRole)
        {
            _usersRole = usersRole;

        }

        public IEnumerable<UserRole> GetUserRoles()
        {
            return _usersRole.Table.AsEnumerable();
        }
        public IPagedList<UserRole> GetUsersPaging(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return new PagedList<UserRole>(_usersRole.Table, pageIndex, pageSize);
        }

        public UserRole GetUserRoleById(long userId)
        {
            if (userId == 0)
            {
                return null;
            }
            return _usersRole.GetById(userId);
        }

        public UserRole AddUserRole(UserRole newUser)
        {
            _usersRole.Insert(newUser);
            return newUser;
        }

        public UserRole UpdateUserRole(UserRole newUser)
        {
            _usersRole.Update(newUser);
            return newUser;
        }
    }
}
