using FluentEmail;
using System;

namespace LF.Framework.MailManager
{
    public static class MailManager
    {
        public static void UseSystemMail(Action<Email> handler)
        {
            using (BaseSmtpClient client = SmtpClientCreator.GMail("binhds816@gmail.com", "thanhbinh1995"))
            {
                handler(new Email(client, "binhds816@gmail.com", "LF System"));
            }
        }

        public static void SendSingleMail(MailOptions<SingleMailDto> options)
        {
            using (BaseSmtpClient client = SmtpClientCreator.GMail(options.Data.SenderEmail, options.Data.Password))
            {
                var mail = new Email(client, options.Data.SenderEmail, options.Data.SenderName);
                mail
                    .To(options.Data.ReceiverEmail, options.Data.ReceiverName)
                    .Subject(options.Data.Subject).Body(options.Data.Body)
                    .Send()
                    ;
            }
        }
        public static void SendListMail(MailOptions<ListMailDto> options)
        {
            for (int i = 0; i < options.Data.ReceiverEmails.Count; i++)
            {
                MailManager.SendSingleMail(new MailOptions<SingleMailDto>
                {
                    Data = new SingleMailDto
                    {
                        SenderEmail = options.Data.SenderEmail,
                        SenderName = options.Data.SenderName,
                        Subject = options.Data.Subject,
                        Body = options.Data.Body,
                        ReceiverEmail = options.Data.ReceiverEmails[i],
                        ReceiverName = options.Data.ReceiverName[i],
                    }
                });
            }
        }
    }
}
