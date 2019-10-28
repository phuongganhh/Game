using Common;
using Common.Database;
using Dapper;
using Entity;
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
                .Fetch<User>()
                ;
        }
        private Task UpdateAccount(ObjectContext context,User u)
        {
            var sql =  context.Query
                .From("jz_acc.account")
                .Where("jz_acc.account.id", u.id)
                .Update(new {
                    online = 0,
                    reg_date = DateTime.Now
                });
            return context.Connection.ExecuteAsync(sql.RawSql, sql.NamedBindings);
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