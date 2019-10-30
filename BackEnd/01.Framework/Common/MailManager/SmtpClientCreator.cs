using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LF.Framework.MailManager
{
    
    public static class SmtpClientCreator
    {
        public static BaseSmtpClient GMail(string email, string password)
        {
            return new GMailSmtpClient(email, password);
        }
    }
}
