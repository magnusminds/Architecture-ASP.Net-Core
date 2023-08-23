using Architecture.BusinessLogic.Interface;
using Architecture.Core.Services.Email;
using Architecture.Core.UserDefinedException;
using MagnusMinds.Utility.EmailService;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Architecture.BusinessLogic.Repositories
{
    public class EmailSenderBL : IEmailSenderBL
    {
        public IEmailHelper _emailHelper;
        private readonly IConfiguration _configuration;

        public async Task SendEmail(long TenantId, string emailSubject, string emailBody, List<string> emailList, List<string> notificationEmail, CancellationToken cancellationToken)
        {
            if (emailList.Count > 0)
            {
                // var tenantSMTPDetailData = await _unitOfWorkDA.TenantSMTPDetailDA.GetAll(cancellationToken);
                //var tenantSMTPDetail = tenantSMTPDetailData.FirstOrDefault(x => x.TenantID == TenantId);

                EmailSender emailSender = new EmailSender(new EmailConfiguration()
                {
                    From = string.Empty,
                    Password = string.Empty,
                    Port = 0,
                    SmtpServer = string.Empty,
                    UserName = string.Empty,
                    UseSSL = true
                });
                EmailHelper _email = new EmailHelper(emailSender);
                await _email.SendEmail(subject: emailSubject, htmlContent: emailBody, to: emailList, cc: notificationEmail);

                await _emailHelper.SendEmail(subject: emailSubject, htmlContent: emailBody, to: emailList);

                await _emailHelper.SendEmail(subject: emailSubject, htmlContent: emailBody, to: emailList);
            }
            else
            {
                throw new SystemInternalException("Please provide email list");
            }
        }

        public async Task SendEmail(string subject, string htmlContent, List<string> to, List<string> cc = null, List<string> bcc = null)
        {
            var sendExceptionEmail = Convert.ToString(_configuration["AppSettings:SendExceptionEmail"]);
            var exceptionEmailToList = _configuration.GetSection("AppSettings:ExceptionEmailToList").Get<List<string>>();
            if (sendExceptionEmail == "1" && exceptionEmailToList.Any())
            {
                await _emailHelper.SendEmail(subject, htmlContent, exceptionEmailToList, cc, bcc);
            }
        }

        public async Task SendEmailWithAttachments(long TenantId, string emailSubject, string emailBody, List<Byte[]> attachmentFiles, string attachmentFileName, List<string> emailList, List<string> notificationEmail, CancellationToken cancellationToken)
        {
            if (emailList.Count > 0)
            {
                //var tenantSMTPDetailData = await _unitOfWorkDA.TenantSMTPDetailDA.GetAll(cancellationToken);
                // var tenantSMTPDetail = tenantSMTPDetailData.FirstOrDefault(x => x.TenantID == TenantId);
                // if (tenantSMTPDetail != null)
                // {
                //if (tenantSMTPDetail != null && !string.IsNullOrEmpty(tenantSMTPDetail.FromEmail) && !string.IsNullOrEmpty(tenantSMTPDetail.SMTPServer) && !string.IsNullOrEmpty(tenantSMTPDetail.Username) && !string.IsNullOrEmpty(tenantSMTPDetail.Password) && tenantSMTPDetail.SMTPPort > 0)
                //{
                EmailSender emailSender = new EmailSender(new EmailConfiguration()
                {
                    From = string.Empty,
                    Password = string.Empty,
                    Port = 0,
                    SmtpServer = string.Empty,
                    UserName = string.Empty,
                    UseSSL = true
                });
                EmailHelper _email = new EmailHelper(emailSender);
                await _email.SendEmailWithAttachment(subject: emailSubject, htmlContent: emailBody, attachmentsArray: attachmentFiles, attachmentFileName: attachmentFileName, to: emailList, cc: notificationEmail);

                await _emailHelper.SendEmailWithAttachment(subject: emailSubject, attachmentsArray: attachmentFiles, attachmentFileName: attachmentFileName, htmlContent: emailBody, to: emailList);

                // }
                //else
                //{
                await _emailHelper.SendEmailWithAttachment(subject: emailSubject, attachmentsArray: attachmentFiles, attachmentFileName: attachmentFileName, htmlContent: emailBody, to: emailList);
                // }
            }
            else
            {
                throw new SystemInternalException("Please provide email list");
            }
        }
    }
}