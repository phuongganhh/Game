using Common;
using Common.Database;
using Common.Services;
using PA.Framework;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace MailService
{
    class Program
    {
        static void Main(string[] args)
        {
            BasicServiceStarter.Run<Mail>();
            Console.ReadKey();
        }
    }
    public class Mail : IService
    {
        public void DefaultStart()
        {
        }
        private static ObjectContext context = new ObjectContext();
        public void Dispose()
        {
        }
        private void UpdateMail(Entity.Mail mail)
        {
            var result = context.Query.From("mail").Where("mail.id", mail.id).Update(new { sent = 1 }).PostData();
        }
        private static Timer timer { get; set; } = new Timer();
        private void TimerCallback(object o)
        {
            
            
        }
        public void Start(string[] args)
        {
            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;
            timer.Interval = 20000;
            timer.Start();
            Console.ReadLine();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                var mail = context.Query.From("mail").Where("mail.sent", 0).FetchData<Entity.Mail>();
                foreach (var item in mail)
                {
                    MailHelper.Instance.Send(item.email, "Xác thực tài khoản", item.message.Decode());
                    this.UpdateMail(item);
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Logger.Error(ex.Message);
            }
        }
    }
}
