using Common;
using Common.Database;
using Common.StringHepler;
using Dapper;
using Entity;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Models.Auth
{
    public class SignUpAction : CommandBase
    {
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        private Task<IEnumerable<User>> GetAccount(ObjectContext context,string username)
        {
            return context.Query.From("jz_acc.account")
                .Select("jz_acc.account.name","jz_acc.account.id")
                .Where("jz_acc.account.name", username)
                .FetchAsync<User>()
                ;
        }
        private Task<IEnumerable<User>> GetEmail(ObjectContext context)
        {
            return context.Query.From("pa.user")
                .Select("pa.user.email")
                .Where("pa.user.email", this.email)
                .FetchAsync<User>()
                ;
        }
        protected override Task ValidateCore(ObjectContext context)
        {
            if(string.IsNullOrEmpty(this.username.TrimSpace()) || string.IsNullOrEmpty(this.password.TrimSpace()) || string.IsNullOrEmpty(this.email.TrimSpace()))
            {
                throw new BusinessException("Vui lòng không bỏ trống");
            }
            if (!this.email.TrimSpace().EndsWith("@gmail.com"))
            {
                throw new BusinessException("Phải là tài khoản Gmail mới được chấp nhận!");
            }
            var emailName = this.email.Split('@').FirstOrDefault();
            if(emailName != null)
            {
                this.email = emailName.Replace(".", "") + "@gmail.com";
            }
            else
            {
                throw new BusinessException("Email không hợp lệ!");
            }
            this.username = this.username.ToLower().TrimSpace();
            if(this.username.TrimSpace().Length < 4 || this.username.TrimSpace().Length > 12)
            {
                throw new BusinessException("Tên tài khoản phải có độ dài từ 4 đến 12 ký tự!");
            }
            return Task.CompletedTask;
        }
        private async Task<bool> InsertAccount(ObjectContext context)
        {
            var result = await context.Query.From("jz_acc.account").Insert(new
            {
                name = this.username.TrimSpace().ToLower(),
                password = this.password,
                online = 1,
                VIP = 1
            }).ExecuteAsync();
            if(!result)
            {
                LoggerManager.Logger.Error($"900: Không thể đăng ký tài khoản! - ${this.username}");
                this.Failed(900);
            }
            return result;
        }
        private void Failed(int error_code)
        {
            throw new BusinessException("Đã xả ra lỗi: " + error_code,HttpStatusCode.InternalServerError);
        }
        private async Task<bool> InsertUser(ObjectContext context,User user)
        {
            user.token = context.Token;
            user.email = this.email;
            var result = await context.Query.From("pa.user").Insert(new
            {
                user.id,
                user.token,
                user.email,
                user.function_group_id,
                user.spin_count,
                user.money
            }).ExecuteAsync();
            if (!result)
            {
                LoggerManager.Logger.Error("901: Không thể đăng ký pa.user - " + this.username);
                this.Failed(901);
            }
            return result;
        }
        private async Task<bool> InsertMailQueue(ObjectContext context,Mail mail)
        {
            var result = await context.Query.From("mail").Insert(mail).ExecuteAsync();
            if (!result)
            {
                LoggerManager.Logger.Error("902: Không thể thêm email - " + this.email);
                this.Failed(902);
            }
            return result;
        }
        protected override async Task<Result> ExecuteCore(ObjectContext context)
        {
            var acc = await this.GetAccount(context,this.username);
            if(acc.Count() > 0)
            {
                throw new BusinessException("Tài khoản này đã có người sử dụng");
            }
            var email = await this.GetEmail(context);
            if(email.Count() > 0)
            {
                throw new BusinessException("Email này đã có người sử dụng");
            }
             await this.InsertAccount(context);
            var accountInsert = await this.GetAccount(context,this.username);
            var u = accountInsert.FirstOrDefault();
            if (u == null)
            {
                throw new Exception("Xảy ra lỗi không xác định! Mã lỗi: 900");
            }
            await this.InsertUser(context, u);
            await this.InsertMailQueue(context, new Mail
            {
                created_date = DateTime.Now,
                sent = 0,
                sent_date = DateTime.Now,
                email = this.email,
                message = $"Hi {this.username}!{Environment.NewLine}Truy cập <b>http://103.27.237.153:82/auth/validate?token={u.token}<b> để xác thực tài khoản".Encode()
            });
            return await Success("Vui lòng kiểm tra hòm mail hoặc SPAM để xác thực tài khoản!");
        }
    }
}