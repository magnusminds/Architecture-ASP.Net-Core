using Architecture.Core.DataTable;
using Architecture.DataBase.DatabaseFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.DataAccess.Interface
{
    public interface ILogEntryDA
    {
        IEnumerable<LogEntry> GetLogEntries();
        LogEntry GetLogEntry(long Id);
        IPagedList<LogEntry> GetLogEntryPaging(int pageIndex = 0, int pageSize = int.MaxValue);
        LogEntry AddLogEntry(LogEntry newLogEntry);
        LogEntry UpdateLogEntry(LogEntry newLogEntry);
    }
}
