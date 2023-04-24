using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Core.Services.Email
{
    public interface IEmailHelper
    {
        Task SendEmail(string subject, string htmlContent, List<string> to, List<string> cc = null, List<string> bcc = null);

        Task SendEmailWithAttachment(string subject, string htmlContent, List<byte[]> attachmentsArray, string attachmentFileName, List<string> to, List<string> cc = null, List<string> bcc = null);
    }
}
