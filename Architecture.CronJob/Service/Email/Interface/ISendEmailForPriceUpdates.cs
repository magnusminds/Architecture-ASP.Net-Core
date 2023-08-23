namespace Architecture.CronJob.Service.Email.Interface
{
    public interface ISendEmailForPriceUpdates
    {
        public Task SendPriceUpdatesEmail(CancellationToken cancellationToken);
    }
}