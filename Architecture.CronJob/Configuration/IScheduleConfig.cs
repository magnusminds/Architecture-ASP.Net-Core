using Architecture.BusinessLogic.Interface;

namespace Architecture.CronJob.Configuration
{
    public interface IScheduleConfig<T>
    {
        string CronExpression { get; set; }
        TimeZoneInfo TimeZoneInfo { get; set; }
        IEmailSenderBL emailSenderBL { get; set; }
    }

    public class ScheduleConfig<T> : IScheduleConfig<T>
    {
        public string CronExpression { get; set; }
        public TimeZoneInfo TimeZoneInfo { get; set; }
        public IEmailSenderBL emailSenderBL { get; set; }
    }
}