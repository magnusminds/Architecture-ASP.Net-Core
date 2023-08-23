using Architecture.CronJob.Service.SMS.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Architecture.CronJob.Configuration
{
    public class SchedulerForSendSMS : CronJobService
    {
        private readonly ISendSMS _sendSMS;

        public SchedulerForSendSMS(ISendSMS sendSMS, IServiceScopeFactory serviceScopeFactory, IScheduleConfig<SchedulerForSendSMS> config) : base(config.CronExpression, config.TimeZoneInfo, config.emailSenderBL)
        {
            _sendSMS = sendSMS;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return base.StartAsync(cancellationToken);
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            try
            {
                await _sendSMS.SendGreetingSMS(cancellationToken);
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