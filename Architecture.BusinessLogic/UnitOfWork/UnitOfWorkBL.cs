using Architecture.BusinessLogic.Interface;
using Architecture.Entities;
using System;

namespace Architecture.BusinessLogic.UnitOfWork
{
    public class UnitOfWorkBL : IUnitOfWorkBL
    {
        private readonly ApplicationDbContext _context;

        public IUsersBL UserBL { get; }

        public IEmailSenderBL EmailSenderBL { get; }

        public IRolePermissionBL RolePermissionBL { get; }

        public IRoleBL RoleBL { get; }

        public UnitOfWorkBL(ApplicationDbContext context, IUsersBL UserBL, IRolePermissionBL RolePermissionBL, IRoleBL RoleBL, IEmailSenderBL EmailSenderBL)
        {
            this._context = context;
            this.UserBL = UserBL;
            this.RoleBL = RoleBL;
            this.RolePermissionBL = RolePermissionBL;
            this.EmailSenderBL = EmailSenderBL;
        }

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