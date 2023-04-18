using Architecture.DataBase.DatabaseFirst;
using Architecture.BusinessLogic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.DataAccess.Interface;
using Architecture.BusinessLogic.Repositories;
using Architecture.BusinessLogic.Interface;

namespace Architecture.BusinessLogic.UnitOfWork
{
    public class UnitOfWorkBL : IUnitOfWorkBL
    {
        private readonly AdminContext _context;

        public ILogEntryBL LogEntryBL { get; }

        public IUsersBL UserBL { get; }

        public IEmailSenderBL EmailSenderBL { get; }

        public UnitOfWorkBL(AdminContext context, ILogEntryBL LogEntryBL, IUsersBL UserBL)
        {
            this._context = context;
            this.LogEntryBL = LogEntryBL;
            this.UserBL = UserBL;
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
