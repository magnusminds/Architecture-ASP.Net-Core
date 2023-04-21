using Architecture.DataAccess.Interface;

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
