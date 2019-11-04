using Common;
using Common.Database;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private void UpdateMail(Entity.Mail mail)
        {
            var result = context.Query.From("mail").Where("mail.id", mail.id).Update(new { sent = 1 }).PostData();
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                var mail = context.Query.From("mail").Where("mail.sent", 0).FetchData<Entity.Mail>();
                foreach (var item in mail)
                {
                    LoggerManager.Logger.Info("SEND MAIL " + item.email);
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
