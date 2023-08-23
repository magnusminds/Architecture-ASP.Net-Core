using Architecture.DataAccess.Interface;

namespace Architecture.DataAccess.Repositories
{
    public class LogEntryDA : ILogEntryDA
    {
        //private readonly ISqlRepository<> _logEntry;

        //public LogEntryDA(ISqlRepository<null> logEntry)
        //{
        //    _logEntry = logEntry;
        //}

        //public IEnumerable<LogEntry> GetLogEntries()
        //{
        //    return _logEntry.Table.AsEnumerable();
        //}
        //public IPagedList<LogEntry> GetLogEntryPaging(int pageIndex = 0, int pageSize = int.MaxValue)
        //{
        //    return new PagedList<LogEntry>(_logEntry.Table, pageIndex, pageSize);
        //}

        //public LogEntry GetLogEntry(long Id)
        //{
        //    if (Id == 0)
        //    {
        //        return null;
        //    }
        //    return _logEntry.GetById(Id);
        //}

        //public LogEntry AddLogEntry(LogEntry newUser)
        //{
        //    _logEntry.Insert(newUser);
        //    return newUser;
        //}

        //public LogEntry UpdateLogEntry(LogEntry newUser)
        //{
        //    _logEntry.Update(newUser);
        //    return newUser;
        //}
    }
}