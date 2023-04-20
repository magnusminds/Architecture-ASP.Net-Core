
namespace Architecture.CronJob.Service.WhatsApp.Interface
{
    public interface ISendWhatsApp
    {
        public Task SendWhatsAppMessage(CancellationToken cancellationToken);
    }
}
