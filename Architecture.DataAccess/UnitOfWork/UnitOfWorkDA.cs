using Architecture.DataAccess.Interface;
using Architecture.DataBase.DatabaseFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.DataAccess.UnitOfWork
{
    public class UnitOfWorkDA
    {
        private readonly AdminContext _context;

        public IUserDA UserDA { get; }
        public IRoleDA RoleDA { get; }

        public IUserRolesDA RolesDA { get; }

        public UnitOfWorkDA(AdminContext context, IUserDA userDA, IRoleDA roleDA,IUserRolesDA rolesDA)
        {
            {
                this._context = context;
                this.UserDA = userDA;
                this.RoleDA = roleDA;
                this.RolesDA = rolesDA;
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
        #endregion


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
