using Architecture.CronJob.Service.SMS.Interface;
using Architecture.Dto;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Architecture.CronJob.Service.SMS.Repository
{
    public class SendSMS : ISendSMS
    {
        private readonly ILogger<SendSMS> _logger;
        private readonly CurrentUser _currentUser;

        public async Task SendGreetingSMS(CancellationToken cancellationToken)
        {
            var result = JsonSerializer.Serialize(new { message = Newtonsoft.Json.JsonConvert.SerializeObject("Cronjob has started for SMS") });
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
