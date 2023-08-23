using Architecture.CronJob.Service.Email.Interface;

namespace Architecture.CronJob.Configuration
{
    public class SchedulerForOrderInquiry : CronJobService
    {
        private readonly ISendEmailForOrderInquiry _sendEmailForOrderInquiry;

        public SchedulerForOrderInquiry(ISendEmailForOrderInquiry sendEmailForOrderInquiry, IScheduleConfig<SchedulerForOrderInquiry> config) : base(config.CronExpression, config.TimeZoneInfo, config.emailSenderBL)
        {
            _sendEmailForOrderInquiry = sendEmailForOrderInquiry;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return base.StartAsync(cancellationToken);
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            try
            {
                await _sendEmailForOrderInquiry.SendOrderInquiryEmail(3, cancellationToken);
                await _sendEmailForOrderInquiry.SendOrderInquiryEmail(5, cancellationToken);
                await _sendEmailForOrderInquiry.SendOrderInquiryEmail(10, cancellationToken);
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