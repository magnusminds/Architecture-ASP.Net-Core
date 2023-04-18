using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.DataAccess.UnitOfWork
{
    public interface IUnitOfWorkDA
    {

        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task rollbackTransactionAsync();
    }
}
