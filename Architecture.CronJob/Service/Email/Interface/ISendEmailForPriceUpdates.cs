using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.CronJob.Service.Email.Interface
{
    public interface ISendEmailForPriceUpdates
    {
        public Task SendPriceUpdatesEmail(CancellationToken cancellationToken);
    }
}
