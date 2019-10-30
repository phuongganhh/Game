
using FluentEmail;
using LF.Framework;
using LF.Framework.MailManager;
using System.Net.Mail;

namespace Common
{
    public class MailHelper
    {
        public MailHelper()
        {

        }

        private static MailHelper _instance { get; set; }
        public static MailHelper Instance
        {
            get
            {
                return _instance ?? (_instance = new MailHelper());
            }
        }

        public void Send(string mailAddress,string title,string mess)
        {
            MailOptions<SingleMailDto> options = new MailOptions<SingleMailDto>();
            options.Data = new SingleMailDto();
            options.Data.SenderEmail = "lebaonhi1998@gmail.com";
            options.Data.Password = "Thuan2015";
            options.Data.SenderName = "Admin Zero Online";
            options.Data.ReceiverEmail = mailAddress;
            options.Data.ReceiverName = "Nguoi choi zer online";
            options.Data.Subject = title;
            options.Data.Body = mess;
            using (BaseSmtpClient client = SmtpClientCreator.GMail(options.Data.SenderEmail, options.Data.Password))
            {
                var mail = new Email(client, options.Data.SenderEmail, options.Data.SenderName);
                mail
                    .To(options.Data.ReceiverEmail, options.Data.ReceiverName)
                    .Subject(options.Data.Subject)
                    .Body(options.Data.Body)
                    .BodyAsHtml()
                    .Send()
                    ;
            }
        }
    }
}
