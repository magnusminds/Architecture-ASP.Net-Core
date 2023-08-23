namespace Architecture.CronJob.Service.Email.Interface
{
    public interface ISendEmailForOrderInquiry
    {
        public Task SendOrderInquiryEmail(int days, CancellationToken cancellationToken);
    }
}