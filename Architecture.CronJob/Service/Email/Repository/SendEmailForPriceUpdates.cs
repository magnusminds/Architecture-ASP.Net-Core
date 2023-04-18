using Architecture.Core.Services.Email;
using Architecture.CronJob.Service.Email.Interface;
using Architecture.Dto;
using Architecture.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Architecture.CronJob.Service.Email.Repository
{
    public class SendEmailForPriceUpdates : ISendEmailForPriceUpdates
    {
        private readonly IEmailHelper _emailHelper;
        private readonly CurrentUser _currentUser;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<SendEmailForPriceUpdates> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public SendEmailForPriceUpdates(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task SendPriceUpdatesEmail(CancellationToken cancellationToken)
        {
            var result = JsonSerializer.Serialize(new { message = Newtonsoft.Json.JsonConvert.SerializeObject("Cronjob has started for SendEmailForPriceUpdates") });
            _logger.LogWarning(result);
            try
            {
                result = JsonSerializer.Serialize(new { message = Newtonsoft.Json.JsonConvert.SerializeObject("Email notification is not active.") });
                _logger.LogWarning(result);
            }
            catch(Exception ex)
            {
                result = JsonSerializer.Serialize(new { message = Newtonsoft.Json.JsonConvert.SerializeObject("Cronjob has an exception for SendEmailForPriceUpdates - Message : " + ex.Message) });
                _logger.LogError(result);
                throw ex;
            }
            result = JsonSerializer.Serialize(new { message = Newtonsoft.Json.JsonConvert.SerializeObject("Cronjob has ended for SendEmailForPriceUpdates") });
            _logger.LogWarning(result);
        }


    }
}
