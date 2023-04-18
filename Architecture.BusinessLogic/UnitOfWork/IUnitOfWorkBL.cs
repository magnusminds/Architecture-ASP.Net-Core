using Architecture.BusinessLogic.Interface;
using Architecture.BusinessLogic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.BusinessLogic.UnitOfWork
{
    public interface IUnitOfWorkBL
    {
        public ILogEntryBL LogEntryBL { get; }

        public IUsersBL UserBL { get; }

        public IEmailSenderBL EmailSenderBL { get; }
    }
}
