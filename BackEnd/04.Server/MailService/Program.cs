using Common;
using Common.Database;
using Common.Services;
using PA.Framework;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailService
{
    class Program
    {
        static void Main(string[] args)
        {
            BasicServiceStarter.Run<Mail>("MailService");
        }
    }
    public class Mail : IService
    {
        public void DefaultStart()
        {
        }

        public void Dispose()
        {
        }
        private void UpdateMail(Entity.Mail mail)
        {
            var result = QueryFactory.Instance.From("mail").Where("mail.id", mail.id).Update(new { sent = 1 }).FetchAsync();
            result.Wait();
        }
        private void TimerCallback(object o)
        {
            var mail = QueryFactory.Instance.From("mail").Where("mail.sent", 0).FetchData<Entity.Mail>();
            foreach (var item in mail)
            {
                MailHelper.Instance.Send(item.email, "Xác thực tài khoản", item.message.Decode());
                this.UpdateMail(item);
            }
        }
        public void Start(string[] args)
        {
            Timer timer = new Timer(this.TimerCallback, null, 0, 10000);
            Console.ReadLine();
        }
    }
}
