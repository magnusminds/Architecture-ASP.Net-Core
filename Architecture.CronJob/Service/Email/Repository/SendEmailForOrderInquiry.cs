using Architecture.CronJob.Service.Email.Interface;
using Architecture.Dto;
using Microsoft.Extensions.Logging;
using System.Text.Json;


namespace Architecture.CronJob.Service.Email.Repository
{
    public class SendEmailForOrderInquiry : ISendEmailForOrderInquiry
    {
        private readonly CurrentUser _currentUser;
        private readonly ILogger<SendEmailForOrderInquiry> _logger;

        public async Task SendOrderInquiryEmail(int days, CancellationToken cancellationToken)
        {
            var result = JsonSerializer.Serialize(new { message = Newtonsoft.Json.JsonConvert.SerializeObject("Cronjob has started for SendEmailForOrderInquiry") });
            _logger.LogWarning(result);

            try
            {

            }
            catch (Exception ex)
            {
                result = JsonSerializer.Serialize(new { message = Newtonsoft.Json.JsonConvert.SerializeObject("Cronjob has an exception for SendEmailForOrderInquiry - Message : " + ex.Message) });
                _logger.LogError(result);
                throw ex;
            }
            result = JsonSerializer.Serialize(new { message = Newtonsoft.Json.JsonConvert.SerializeObject("Cronjob has ended for SendEmailForOrderInquiry") });
            _logger.LogWarning(result);
        }

        #region private methods

        private async Task<string> GetShortedURL(long OrderId, string originalURL, CancellationToken cancellationToken)
        {
            string shortedUrl;

            shortedUrl = string.Empty;


            return shortedUrl;
        }

        

        #endregion private methods
    }
}
