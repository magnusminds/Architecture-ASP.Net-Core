using Architecture.BusinessLogic.Interface;
using Architecture.CronJob.Service.Email.Interface;
using Architecture.Dto;
using Microsoft.Extensions.Logging;
using System.Text.Json;


namespace Architecture.CronJob.Service.Email.Repository
{
    public class SendGreetingEmailForNotification : ISendGreetingEmailForNotification
    {
        private readonly IEmailSenderBL _emailSenderBL;
        private readonly CurrentUser _currentUser;
        private readonly ILogger<SendGreetingEmailForNotification> _logger;


        public async Task SendGreetingEmail(CancellationToken cancellationToken)
        {
            var result = JsonSerializer.Serialize(new { message = Newtonsoft.Json.JsonConvert.SerializeObject("Cronjob has started for Email") });
            _logger.LogWarning(result);

            try
            {
            }
            catch (Exception ex)
            {
                result = JsonSerializer.Serialize(new { message = Newtonsoft.Json.JsonConvert.SerializeObject("Cronjob has an exception for Email - Message : " + ex.Message) });
                _logger.LogError(result);
                throw ex;
            }

            result = JsonSerializer.Serialize(new { message = Newtonsoft.Json.JsonConvert.SerializeObject("Cronjob has ended for Email") });
            _logger.LogWarning(result);
        }
    }
}
