namespace Architecture.CronJob.Service.Email.Interface
{
    public interface ISendGreetingEmailForNotification
    {
        public Task SendGreetingEmail(CancellationToken cancellationToken);
    }
}