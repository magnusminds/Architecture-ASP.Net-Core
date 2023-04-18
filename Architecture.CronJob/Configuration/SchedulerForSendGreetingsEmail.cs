using Architecture.CronJob.Service.Email.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.CronJob.Configuration
{
    public class SchedulerForSendGreetingsEmail : CronJobService
    {
        private readonly ISendGreetingEmailForNotification _sendGreetingEmailForNotification;

        public SchedulerForSendGreetingsEmail(ISendGreetingEmailForNotification sendGreetingEmailForNotification, IScheduleConfig<SchedulerForSendGreetingsEmail> config, IServiceScopeFactory serviceScopeFactory) : base(config.CronExpression, config.TimeZoneInfo, config.emailSenderBL)
        {
            _sendGreetingEmailForNotification = sendGreetingEmailForNotification;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return base.StartAsync(cancellationToken);
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            try
            {
                await _sendGreetingEmailForNotification.SendGreetingEmail(cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }
    }
}
