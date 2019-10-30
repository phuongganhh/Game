using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LF.Framework.MailManager
{
    public class GMailSmtpClient : BaseSmtpClient
    {
        public GMailSmtpClient(string mailAddress, string password)
        {
            Host = "smtp.gmail.com";
            Port = 587;
            EnableSsl = true;
            DeliveryMethod = SmtpDeliveryMethod.Network;
            UseDefaultCredentials = false;
            Credentials = new NetworkCredential(mailAddress, password);
        }
        public GMailSmtpClient(string tokenId)
        {
            //TODO: Implement authorize via OAuth
        }
    }
}
