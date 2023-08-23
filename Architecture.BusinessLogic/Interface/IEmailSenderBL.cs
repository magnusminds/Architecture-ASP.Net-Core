using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Architecture.BusinessLogic.Interface
{
    public interface IEmailSenderBL
    {
        Task SendEmail(long TenantId, string emailSubject, string emailBody, List<string> emailList, List<string> notificationEmail, CancellationToken cancellationToken);

        Task SendEmail(string subject, string htmlContent, List<string> to, List<string> cc = null, List<string> bcc = null);

        Task SendEmailWithAttachments(long TenantId, string emailSubject, string emailBody, List<Byte[]> attachmentFiles, string attachmentFileName, List<string> emailList, List<string> notificationEmail, CancellationToken cancellationToken);
    }
}