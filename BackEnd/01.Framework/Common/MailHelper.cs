using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Utils;

namespace Common
{
    public class MailHelper
    {
        public MailHelper()
        {

        }

        public static MailHelper _instance { get; set; }
        public static MailHelper Insert
        {
            get
            {
                return _instance ?? (_instance = new MailHelper());
            }
        }

        public void Send(string mailAddress,string title,string mess)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Administrator Zero Online", "wars99.vietnam@gmail.com"));
            message.To.Add(new MailboxAddress("Administrator Zero Online", mailAddress));
            message.Subject = title;
            message.MessageId = MimeUtils.GenerateMessageId("wars99.com");
            message.Body = new TextPart("html")
            {
                Text = mess
            };

            using (var client = new SmtpClient())
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("smtp.gmail.com", 587, false);
                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("wars99.vietnam@gmail.com", "abc@#$123456");
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
