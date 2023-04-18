using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.CronJob.Service.SMS.Interface
{
    public interface ISendSMS
    {
        public Task SendGreetingSMS(CancellationToken cancellationToken);
    }
}
