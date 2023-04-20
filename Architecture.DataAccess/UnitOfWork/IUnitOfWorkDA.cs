using Architecture.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.DataAccess.UnitOfWork
{
    public interface IUnitOfWorkDA
    {
        IUserDA UserDA { get; }

        IRoleDA RoleDA { get; }

        IRolePermissionDA RolePermissionDA { get; }

        IUserRolesDA RolesDA { get; }
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task rollbackTransactionAsync();
    }
}
