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
            if(context.CurrentUser == null)
            {
                throw new BusinessException("Vui lòng đăng nhập!", HttpStatusCode.Unauthorized);
            }
            return Success(new
            {
                player_name = context.CurrentUser.player_name
            });
        }
    }
}