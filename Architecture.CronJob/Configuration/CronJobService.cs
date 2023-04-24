using Microsoft.Extensions.Hosting;
using Architecture.BusinessLogic.Interface;
using Cronos;

namespace Architecture.CronJob
{
    public class CronJobService : IHostedService, IDisposable
    {
        private System.Timers.Timer _timer;
        private readonly CronExpression _expression;
        private readonly TimeZoneInfo _timeZoneInfo;
        private readonly IEmailSenderBL _emailSenderBL;

        protected CronJobService(string cronExpression, TimeZoneInfo timeZoneInfo, IEmailSenderBL emailSenderBL)
        {
           _expression = CronExpression.Parse(cronExpression);
            _timeZoneInfo = timeZoneInfo;
            _emailSenderBL = emailSenderBL;
        }

        public void Dispose()
        {
            if (_timer != null)
                _timer.Dispose();
        }


        public virtual async Task StartAsync(CancellationToken cancellationToken)
        {
            await ScheduleJob(cancellationToken);
        }

        protected virtual async Task ScheduleJob(CancellationToken cancellationToken)
        {
            var next = _expression.GetNextOccurrence(DateTimeOffset.Now, _timeZoneInfo);
            if (next.HasValue)
            {
                var delay = next.Value - DateTimeOffset.Now;

                // interval can be int.MaxValue at max
                if (Math.Ceiling(delay.TotalMilliseconds) > int.MaxValue)
                {
                    // set timer to int.MaxValue
                    _timer = new System.Timers.Timer(int.MaxValue);
                    _timer.Elapsed += async (sender, args) =>
                    {
                        try
                        {
                            _timer.Dispose();  // reset and dispose timer
                            _timer = null;

                            if (!cancellationToken.IsCancellationRequested)
                            {
                                await ScheduleJob(cancellationToken);    // reschedule next
                            }
                        }
                        catch (Exception ex)
                        {
                            //Send Mail
                            await SendEmailAsync(ex);
                        }
                    };
                    _timer.Start();
                }
                else
                {
                    _timer = new System.Timers.Timer(delay.TotalMilliseconds);
                    _timer.Elapsed += async (sender, args) =>
                    {
                        try
                        {
                            _timer.Dispose();  // reset and dispose timer
                            _timer = null;

                            if (!cancellationToken.IsCancellationRequested)
                            {
                                await DoWork(cancellationToken);
                            }

                            if (!cancellationToken.IsCancellationRequested)
                            {
                                await ScheduleJob(cancellationToken);    // reschedule next
                            }
                        }
                        catch (Exception ex)
                        {
                            //Send Mail
                            await SendEmailAsync(ex);
                        }
                    };
                    _timer.Start();
                }
            }
            await Task.CompletedTask;
        }

        private async Task SendEmailAsync(Exception e)
        {
            var logEntry = new
            {
                RequestPath = string.Empty,
                TimeStamp = DateTime.Now,
                ActionDescriptor = string.Empty,
                IpAddress = string.Empty,
                Message = e.Message,
                Exception = Convert.ToString(e),
                Source = e.Source,
                StackTrace = e.StackTrace,
                Type = Convert.ToString(e.GetType()),
                BrowserName = string.Empty,
                UserId = string.Empty,
                Name = string.Empty,
                EmailAddress = string.Empty,
                TenantId = string.Empty,
                TenantName = string.Empty,
            };

            // Send log in email
            string htmlContent = GenerateHTMl(logEntry);

            if (logEntry.Message != "The client has disconnected")
            {
                await _emailSenderBL.SendEmail("Error Exception | TANYO-CRONJOB | " + DateTime.Now, htmlContent, null);
            }
        }

        private static string GenerateHTMl(dynamic logEntry)
        {
            string htmlTableStart = "<table style=\"width: 100 %; border: 1px solid #e5e5e5;\" border=\"1\" cellspacing=\"0\" cellpadding=\"6\">";
            string htmlTrStart = "<tr>";
            string htmlTableEnd = "</table>";
            string htmlTrEnd = "</tr>";
            string htmlTdStart = "<td>";
            string htmlTdEnd = "</td>";
            string htmlContent = htmlTableStart + htmlTrStart + htmlTdStart + "RequestPath" + htmlTdEnd + htmlTdStart + logEntry.RequestPath + htmlTdEnd + htmlTrEnd
                                 + htmlTrStart + htmlTdStart + "TimeStamp" + htmlTdEnd + htmlTdStart + logEntry.TimeStamp + htmlTdEnd + htmlTrEnd
                                 + htmlTrStart + htmlTdStart + "ActionDescriptor" + htmlTdEnd + htmlTdStart + logEntry.ActionDescriptor + htmlTdEnd + htmlTrEnd
                                 + htmlTrStart + htmlTdStart + "IpAddress" + htmlTdEnd + htmlTdStart + logEntry.IpAddress + htmlTdEnd + htmlTrEnd
                                 + htmlTrStart + htmlTdStart + "EmailAddress" + htmlTdEnd + htmlTdStart + logEntry.EmailAddress + htmlTdEnd + htmlTrEnd
                                 + htmlTrStart + htmlTdStart + "UserId" + htmlTdEnd + htmlTdStart + logEntry.UserId + htmlTdEnd + htmlTrEnd
                                 + htmlTrStart + htmlTdStart + "TenantID" + htmlTdEnd + htmlTdStart + logEntry.TenantId + htmlTdEnd + htmlTrEnd
                                 + htmlTrStart + htmlTdStart + "TenantName" + htmlTdEnd + htmlTdStart + logEntry.TenantName + htmlTdEnd + htmlTrEnd
                                 + htmlTrStart + htmlTdStart + "Source" + htmlTdEnd + htmlTdStart + logEntry.Source + htmlTdEnd + htmlTrEnd
                                 + htmlTrStart + htmlTdStart + "Type" + htmlTdEnd + htmlTdStart + logEntry.Type + htmlTdEnd + htmlTrEnd
                                 + htmlTrStart + htmlTdStart + "Message" + htmlTdEnd + htmlTdStart + logEntry.Message?.Replace("\r\n", Environment.NewLine) + htmlTdEnd + htmlTrEnd
                                 + htmlTrStart + htmlTdStart + "Exception" + htmlTdEnd + htmlTdStart + logEntry.Exception?.Replace("\r\n", Environment.NewLine) + htmlTdEnd + htmlTrEnd
                                 + htmlTrStart + htmlTdStart + "StackTrace" + htmlTdEnd + htmlTdStart + logEntry.StackTrace?.Replace("\r\n", Environment.NewLine) + htmlTdEnd + htmlTrEnd
                                 + htmlTrStart + htmlTdStart + "BrowserName" + htmlTdEnd + htmlTdStart + logEntry.BrowserName + htmlTdEnd + htmlTrEnd
                                 + htmlTableEnd;
            return htmlContent;
        }

        public virtual async Task DoWork(CancellationToken cancellationToken)
        {
            await Task.Delay(5000, cancellationToken);
        }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Stop();
            await Task.CompletedTask;
        }
    }



}
