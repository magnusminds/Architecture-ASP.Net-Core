namespace Architecture.CronJob.Service.SMS.Interface
{
    public interface ISendSMS
    {
        public Task SendGreetingSMS(CancellationToken cancellationToken);
    }
}