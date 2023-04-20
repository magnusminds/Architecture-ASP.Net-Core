using Architecture.CronJob.Service.WhatsApp.Interface;
using Architecture.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using static System.Formats.Asn1.AsnWriter;

namespace Architecture.CronJob.Service.WhatsApp.Repository
{
    public class SendWhatsApp : ISendWhatsApp
    {
        private readonly ILogger<SendWhatsApp> _logger;
        private readonly CurrentUser _currentUser;
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public SendWhatsApp(IServiceScopeFactory serviceScopeFactory, IConfiguration configuration)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _configuration = configuration;
        }

        public async Task SendWhatsAppMessage(CancellationToken cancellationToken)
        {
            var result = JsonSerializer.Serialize(new { message = Newtonsoft.Json.JsonConvert.SerializeObject("Cronjob has started for WhatsApp") });
            _logger.LogWarning(result);

            try
            { }
            catch (Exception ex)
            {
                result = JsonSerializer.Serialize(new { message = Newtonsoft.Json.JsonConvert.SerializeObject("Cronjob has an exception for WhatsApp - Message : " + ex.Message) });
                _logger.LogError(result);
                throw ex;
            }

            result = JsonSerializer.Serialize(new { message = Newtonsoft.Json.JsonConvert.SerializeObject("Cronjob has ended for WhatsApp") });
            _logger.LogWarning(result);
        }

    }
}
