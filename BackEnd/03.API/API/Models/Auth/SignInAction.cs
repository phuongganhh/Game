using Common;
using Common.Database;
using Entity;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Models.Auth
{
    public class SignInAction : CommandBase<dynamic>
    {
        public string username { get; set; }
        public string password { get; set; }
        private Task<IEnumerable<User>> GetUser(ObjectContext context)
        {
            var sql = context.Query
                .From("jz_acc.account")
                .Select(
                    "jz_acc.account.id",
                    "jz_acc.account.name",
                    "jz.cq_user.name as player_name",
                    "jz.cq_user.id as player_id"
                )
                .LeftJoin("jz.cq_user","jz.cq_user.account_id","jz_acc.account.id")
                .Where("jz_acc.account.name", this.username)
                .Where("jz_acc.account.password", this.password)
                ;
            return sql.Fetch<User>();
        }
        private Task<IEnumerable<User>> GetUserAuth(ObjectContext context,long acc_id)
        {
            return context.Query.From("user").Where("user.id", acc_id).Fetch<User>();
        }
        private Task InsertUser(ObjectContext context,ref User user)
        {
            return context.Query.From("user").Insert(new
            {
                token = user.token,
                user.id,
                user.player_id,
                function_group_id = 0,
                spin_count = 0,
                money = 0
            }).FetchAsync();
        }
        private Task UpdateUser(ObjectContext context,ref User user)
        {
            return context.Query.From("pa.user").Where("pa.user.id",user.id).Update(new
            {
                token = user.token
            }).FetchAsync();
        }
        protected override async Task<Result<dynamic>> ExecuteCore(ObjectContext context)
        {
            var user = await this.GetUser(context);
            if (!user.Any() || user.Count() > 1)
                throw new BusinessException("Tài khoản hoặc mật khẩu không đúng!", HttpStatusCode.NotFound);
            var u = user.FirstOrDefault();
            var auth = await this.GetUserAuth(context, u.id);
            u.token = context.Token;
            if (!auth.Any())
            {
                await this.InsertUser(context,ref u);
            }
            else
            {
                await this.UpdateUser(context, ref u);
            }
            return await Success(new { u.token, player_name = u.player_name ?? "Chưa tạo nhân vật!" });
        }
    }
}