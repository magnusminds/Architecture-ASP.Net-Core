using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.CronJob.Service.WhatsApp.Interface
{
    public interface ISendWhatsApp
    {
        public Task SendWhatsAppMessage(CancellationToken cancellationToken);
    }
}
