using MagnusMinds.Utility.EmailService;
using MimeKit;
using System.Net.Mime;


namespace Architecture.Core.Services.Email
{
    public class EmailHelper : IEmailHelper
    {
        private readonly IEmailSender _emailSender;

        public EmailHelper(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task SendEmail(string subject, string htmlContent, List<string> to, List<string> cc = null, List<string> bcc = null)
        {
            ValidateEmailRequest(subject, htmlContent, to);
            var mimeMessage = new MimeMessage();
            FillMimeMessage(to, cc, bcc, mimeMessage);
            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlContent };
            Task.Run(async () => { await _emailSender.SendEmailAsync(mimeMessage); });
        }

        public async Task SendEmailWithAttachment(string subject, string htmlContent, List<byte[]> attachmentsArray, string attachmentFileName, List<string> to, List<string> cc = null, List<string> bcc = null)
        {
            ValidateEmailRequest(subject, htmlContent, to);
            var mimeMessage = new MimeMessage();
            FillMimeMessage(to, cc, bcc, mimeMessage);
            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlContent };
            var builder = new BodyBuilder();
            if (attachmentsArray != null)
                foreach (var attachment in attachmentsArray)
                    builder.Attachments.Add(attachmentFileName, attachment, MimeKit.ContentType.Parse(MediaTypeNames.Application.Pdf));
            Task.Run(async () => { await _emailSender.SendEmailAsync(mimeMessage, builder.Attachments); });
        }

        private static void FillMimeMessage(List<string> to, List<string> cc, List<string> bcc, MimeMessage mimeMessage)
        {
            foreach (var item in to)
                mimeMessage.To.Add(new MailboxAddress("TANYO", item));
            if (cc != null)
                foreach (var item in cc)
                    mimeMessage.Cc.Add(new MailboxAddress("TANYO", item));
            if (bcc != null)
                foreach (var item in bcc)
                    mimeMessage.Bcc.Add(new MailboxAddress("TANYO", item));
        }

        private static void ValidateEmailRequest(string subject, string htmlContent, List<string> to)
        {
            if (string.IsNullOrEmpty(subject))
            {
                throw new Exception("Subject is required for sending an email");
            }

            if (string.IsNullOrEmpty(htmlContent))
            {
                throw new Exception("Body is required for sending an email");
            }

            if (to == null || !to.Any())
            {
                throw new Exception("To's List is required for sending an email");
            }
        }
    }
}
