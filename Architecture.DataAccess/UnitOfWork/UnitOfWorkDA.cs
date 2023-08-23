using Architecture.DataAccess.Interface;
using Architecture.Entities;

namespace Architecture.DataAccess.UnitOfWork
{
    public class UnitOfWorkDA : IUnitOfWorkDA
    {
        private readonly ApplicationDbContext _context;

        public IUserDA UserDA { get; }
        public IRoleDA RoleDA { get; }

        public IUserRolesDA RolesDA { get; }

        public IRolePermissionDA RolePermissionDA { get; }

        public ILoginTokenDA LoginTokenDA { get; }

        public UnitOfWorkDA(ApplicationDbContext context, IUserDA userDA, IRoleDA roleDA, IUserRolesDA rolesDA, IRolePermissionDA rolePermissionDA, ILoginTokenDA loginTokenDA)
        {
            {
                this._context = context;
                this.UserDA = userDA;
                this.RoleDA = roleDA;
                this.RolesDA = rolesDA;
                this.RolePermissionDA = rolePermissionDA;
                this.LoginTokenDA = loginTokenDA;
            }
        }

        #region TransactionMethod

        public async Task BeginTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task rollbackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        #endregion TransactionMethod

        #region DisposeMethod

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion DisposeMethod
    }
}