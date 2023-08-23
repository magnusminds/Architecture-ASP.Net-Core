using Architecture.CronJob.Service.WhatsApp.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Architecture.CronJob.Configuration
{
    public class SchedulerForSendWhatsApp : CronJobService
    {
        private readonly ISendWhatsApp _sendWhatsApp;

        private readonly IServiceScopeFactory _serviceScopeFactory;

        public SchedulerForSendWhatsApp(ISendWhatsApp sendWhatsApp, IServiceScopeFactory serviceScopeFactory, IScheduleConfig<SchedulerForSendWhatsApp> config) : base(config.CronExpression, config.TimeZoneInfo, config.emailSenderBL)
        {
            _sendWhatsApp = sendWhatsApp;
            _serviceScopeFactory = serviceScopeFactory;
            IServiceScope scope = _serviceScopeFactory.CreateScope();
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return base.StartAsync(cancellationToken);
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            try
            {
                await _sendWhatsApp.SendWhatsAppMessage(cancellationToken);
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