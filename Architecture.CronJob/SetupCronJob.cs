using Microsoft.Extensions.DependencyInjection;

namespace Architecture.CronJob
{
    public static class SetupCronJob
    {
        public static void SetupCronJobs(this IServiceCollection services)
        {
            // Build the intermediate service provider
            var sp = services.BuildServiceProvider();

            ////Send email to Customer for Greetings/Orders every minute
            //services.AddCronJob<SchedulerForSendGreetingsEmail>(c =>
            //{
            //    c.TimeZoneInfo = TimeZoneInfo.Local;
            //    c.CronExpression = @"*/1 * * * *";
            //    c.emailSenderBL = sp.GetRequiredService<IEmailSenderBL>();
            //});
            //services.AddSingleton<ISendGreetingEmailForNotification, SendGreetingEmailForNotification>();

            ////Send SMS to Customer for Greetings/Orders every minute
            //services.AddCronJob<SchedulerForSendSMS>(x =>
            //{
            //    x.TimeZoneInfo = TimeZoneInfo.Local;
            //    x.CronExpression = @"*/1 * * * *";
            //    x.emailSenderBL = sp.GetRequiredService<IEmailSenderBL>();
            //});
            //services.AddSingleton<ISendSMS, SendSMS>();

            ////Send WhatsApp Message
            //services.AddCronJob<SchedulerForSendWhatsApp>(c =>
            //{
            //    c.TimeZoneInfo = TimeZoneInfo.Local;
            //    c.CronExpression = @"*/1 * * * *";
            //    c.emailSenderBL = sp.GetRequiredService<IEmailSenderBL>();
            //});
            //services.AddSingleton<ISendWhatsApp, SendWhatsApp>();

            ////Send email for updated material prices and product prices
            //services.AddCronJob<SchedulerForSendPriceUpdates>(c =>
            //{
            //    c.TimeZoneInfo = TimeZoneInfo.Local;
            //    c.CronExpression = @"0 7 * * *";
            //    c.emailSenderBL = sp.GetRequiredService<IEmailSenderBL>();
            //});

            //services.AddSingleton<ISendEmailForPriceUpdates, SendEmailForPriceUpdates>();

            ////Send reminder email of order which has inquiry status from last 3, 5, 10 days
            //services.AddCronJob<SchedulerForOrderInquiry>(c =>
            //{
            //    c.TimeZoneInfo = TimeZoneInfo.Local;
            //    c.CronExpression = @"0 8 * * *";
            //    c.emailSenderBL = sp.GetRequiredService<IEmailSenderBL>();
            //});

            //services.AddSingleton<ISendEmailForOrderInquiry, SendEmailForOrderInquiry>();
        }
    }
}