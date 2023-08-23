using Architecture.CronJob.Service.Email.Interface;

namespace Architecture.CronJob.Configuration
{
    public class SchedulerForSendPriceUpdates : CronJobService
    {
        private readonly ISendEmailForPriceUpdates _sendEmailForPriceUpdates;

        public SchedulerForSendPriceUpdates(ISendEmailForPriceUpdates sendEmailForPriceUpdates, IScheduleConfig<SchedulerForSendPriceUpdates> config) : base(config.CronExpression, config.TimeZoneInfo, config.emailSenderBL)
        {
            _sendEmailForPriceUpdates = sendEmailForPriceUpdates;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return base.StartAsync(cancellationToken);
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            try
            {
                await _sendEmailForPriceUpdates.SendPriceUpdatesEmail(cancellationToken);
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