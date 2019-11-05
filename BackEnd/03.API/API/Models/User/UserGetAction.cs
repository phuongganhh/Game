using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace API.Models
{
    public class UserGetAction : CommandBase<dynamic>
    {
        protected override Task<Result<dynamic>> ExecuteCore(ObjectContext context)
        {
            return Success(new
            {
                player_name = context.CurrentUser.CharacterName ?? context.CurrentUser.Username
            });
        }
    }
}