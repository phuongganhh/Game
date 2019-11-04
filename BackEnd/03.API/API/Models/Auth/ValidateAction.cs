using Common;
using Common.Database;
using Dapper;
using Entity;
using Newtonsoft.Json;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace API.Models.Auth
{
    public class ValidateAction : CommandBase
    {
        public string token { get; set; }
        protected override Task ValidateCore(ObjectContext context)
        {
            if(this.token == null)
            {
                throw new BusinessException("Not found");
            }
            return Task.CompletedTask;
        }
        private Task<IEnumerable<User>> GetUser(ObjectContext context)
        {
            return context.Query
                .From("user")
                .Where("user.token", this.token)
                .Select("pa.user.token","pa.user.id")
                .FetchAsync<User>()
                ;
        }
        private async Task UpdateAccount(ObjectContext context,User u)
        {
            var result = await context.Query
                .From("jz_acc.account")
                .Where("jz_acc.account.id", u.id)
                .Update(new
                {
                    online = 0,
                    reg_date = DateTime.Now
                }).ExecuteAsync();
            if (!result)
            {
                LoggerManager.Logger.Error("903: Không thể update tài khoản - " + JsonConvert.SerializeObject(u));
                throw new BusinessException("Đã xảy ra lỗi: 903",System.Net.HttpStatusCode.InternalServerError);
            }
        }
        protected override async Task<Result> ExecuteCore(ObjectContext context)
        {
            var user = await this.GetUser(context);
            if(!user.Any() || user.Count() > 1)
            {
                throw new BusinessException("Bad request!");
            }
            await this.UpdateAccount(context, user.FirstOrDefault());
            return await this.Success("Kích hoạt tài khoản thành công!");

        }
    }
}