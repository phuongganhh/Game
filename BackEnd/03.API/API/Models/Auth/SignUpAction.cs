using Common;
using Common.Database;
using Common.StringHepler;
using Dapper;
using Dapper.FastCrud;
using Entity;
using Models;
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
            return context.Connection.FindAsync<User>(s => s.Where($"Username = @username").WithParameters(new { username }));
        }
        private Task<IEnumerable<jzAccount>> GetUser(ObjectContext context,string username)
        {
            return context.Query.From("jz_acc.account").Where("jz_acc.account.name", username).FetchAsync<jzAccount>();
        }
        private Task<IEnumerable<User>> GetEmail(ObjectContext context)
        {
            return context.Connection.FindAsync<User>(s => s.Where($"Email = @email").WithParameters(new { this.email }));
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
        private async Task InsertAccount(ObjectContext context)
        {
            var sql = context.Query.From("jz_acc.account").Insert(new
            {
                name = this.username.TrimSpace().ToLower(),
                password = this.password,
                online = 1,
                VIP = 1
            });
            var result = await sql.ExecuteNotResult(context.ConnectionAccount);
            if (result <= 0)
            {
                LoggerManager.Logger.Error($"900: Không thể đăng ký tài khoản! - ${sql.ToString()}");
                this.Failed(900);
            }
        }
        private void Failed(int error_code)
        {
            throw new BusinessException("Đã xả ra lỗi: " + error_code,HttpStatusCode.InternalServerError);
        }
        private Task InsertUser(ObjectContext context,User user)
        {
            return context.Connection.InsertAsync(user);
        }
        private Task InsertMailQueue(ObjectContext context,Mail mail)
        {
            return context.Connection.InsertAsync(mail);
        }
        protected override async Task<Result> ExecuteCore(ObjectContext context)
        {
            var acc = await this.GetAccount(context,this.username);
            if(acc.Any())
            {
                throw new BusinessException("Tài khoản này đã có người sử dụng");
            }
            var email = await this.GetEmail(context);
            if(email.Any())
            {
                throw new BusinessException("Email này đã có người sử dụng");
            }
            await this.InsertAccount(context);
            var accountInsert = await this.GetUser(context,this.username);
            var u = accountInsert.FirstOrDefault();
            if (u == null)
            {
                throw new Exception("Không tìm thấy tài khoản! Mã lỗi: 900");
            }
            var userCurrent = new User
            {
                Username = this.username,
                Id = u.id,
                Money = 0,
                Email = this.email,
                Password = this.password,
                Spin = 0,
                Token = context.Token,
                FunctionGroupId = 1
            };
            await this.InsertUser(context, userCurrent);
            await this.InsertMailQueue(context, new Mail
            {
                CreatedDate = DateTime.Now,
                Sent = false,
                SentDate = DateTime.Now,
                Email = this.email,
                Message = $"Hi {this.username}!{Environment.NewLine}Truy cập <b>http://103.27.237.153:82/auth/validate?token={userCurrent.Token}<b> để xác thực tài khoản"
            });
            return await Success("Vui lòng kiểm tra hòm mail hoặc SPAM để xác thực tài khoản!");
        }
    }
}