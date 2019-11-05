using Common;
using Common.Database;
using Dapper.FastCrud;
using Entity;
using Models;
using SqlKata.Execution;
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
            return context.Connection.FindAsync<User>(s => s.Where($"Username = @username AND Password = @password").WithParameters(new { this.username, this.password }));
        }
        private Task<IEnumerable<jzAccount>> GetJZAccounts(ObjectContext context,string name)
        {
            return context.Query
                .From("jz_acc.account")
                .LeftJoin("jz.cq_user","jz.cq_user.account_id","jz_acc.account.id")
                .Select(
                "jz_acc.account.id",
                "jz_acc.account.name",
                "jz_acc.account.password",
                "jz_acc.account.online",
                "jz_acc.account.VIP",
                "jz.cq_user.name as player_name"
                )
                .Where("jz_acc.account.name", name)
                .Result<jzAccount>(context.ConnectionAccount);
        }
        private Task UpdateUser(ObjectContext context,User user)
        {
            return context.Connection.UpdateAsync(user);
        }
        private Task<IEnumerable<jzAccount>> GetJzAccounts(ObjectContext context,long id)
        {
            return context.Query.From("jz.cq_user").Where("jz.cq_user.id", id).Result<jzAccount>(context.ConnectionJZ);
        }
        private Task InsertUser(ObjectContext context,User u)
        {
            return context.Connection.InsertAsync(u);
        }
        protected override async Task<Result<dynamic>> ExecuteCore(ObjectContext context)
        {
            var user = await this.GetJZAccounts(context,this.username);
            if (!user.Any() || user.Count() > 1)
                throw new BusinessException("Tài khoản hoặc mật khẩu không đúng!", HttpStatusCode.NotFound);
            var u = user.FirstOrDefault();
            if(u.online == 1)
            {
                throw new BusinessException("Tài khoản này chưa được kích hoạt!", HttpStatusCode.NotAcceptable);
            }
            var auth = await this.GetUser(context);
            var account = auth.FirstOrDefault();
            if(account == null)
            {
                account = new User
                {
                    Id = u.id,
                    Username = u.name,
                    Spin = 0,
                    Token = context.Token,
                    FunctionGroupId = 1,
                    Password = u.password,
                    Money = 0,
                    CharacterName = u.player_name
                };
                await this.InsertUser(context, account);
            }
            else
            {
                account.Token = context.Token;
                account.CharacterName = u.player_name;
                await this.UpdateUser(context, account);
            }
            return await Success(new { token = account.Token, player_name = account.CharacterName ?? "Vô danh!" });
        }
    }
}