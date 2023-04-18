using Architecture.Core.DataTable;
using Architecture.DataBase.DatabaseFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.DataAccess.Interface
{
    public interface IUserRolesDA
    {
        IEnumerable<UserRole> GetUserRoles();
        UserRole GetUserRoleById(long Id);
        IPagedList<UserRole> GetUsersPaging(int pageIndex = 0, int pageSize = int.MaxValue);
        UserRole AddUserRole(UserRole userRole);
        UserRole UpdateUserRole(UserRole userRole);
    }
}
