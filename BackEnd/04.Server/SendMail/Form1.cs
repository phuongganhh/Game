using Common;
using Common.Database;
using Models;
using SqlKata.Execution;
using System;
using System.Data;
using System.Linq;
using System.Timers;
using System.Windows.Forms;

namespace SendMail
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private System.Timers.Timer timer = new System.Timers.Timer();
        private void Form1_Load(object sender, EventArgs e)
        {
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 20000;
            timer.Enabled = true;
            timer.Start();
        }
        private static ObjectContext context = new ObjectContext();
        private void UpdateMail(Mail mail)
        {
            var result = context.Query.From("mail").Where("mail.id", mail.Id).Update(new { sent = 1 }).PostData();
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                var mail = context.Query.From("mail").Where("mail.sent", 0).FetchData<Mail>();
                foreach (var item in mail)
                {
                    LoggerManager.Instance.Info("SEND MAIL " + item.Email);
                    MailHelper.Instance.Send(item.Email, "Xác thực tài khoản", item.Message);
                    this.UpdateMail(item);
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Instance.Error(ex);
            }
        }
    }
}
