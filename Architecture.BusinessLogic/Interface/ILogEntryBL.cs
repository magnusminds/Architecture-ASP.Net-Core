using Architecture.Entities;
using System.Collections.Generic;

namespace Architecture.BusinessLogic.Repositories
{
    public interface ILogEntryBL
    {
        List<LogEntryEntity> GetLogEntryEntities(int pageIndex = 0, int pageSize = int.MaxValue);
        LogEntryEntity CreataLogEntry(LogEntryEntity user);
        LogEntryEntity UpdateLogEntry(LogEntryEntity user);
    }
}
