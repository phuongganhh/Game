using Common;
using MUP.API.Common;
using MUP.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MUP.API.Controllers
{
    public class AuthController : ApiController
    {
        [Route("Login")]
        [HttpPost]
        public async Task<IHttpActionResult> Login(string username,string password)
        {
            var service = new UserService();
            var users = await service.GetUser(username, password);
            if(!users.Any() || users.Count() > 1)
            {
                return BadRequest("Tài khoản hoặc mật khẩu không đúng!");
            }
            var user = users.FirstOrDefault();
            user.Token = Generator.Token;
            await service.UpdateUser(user);
            return Ok(new { user.Token });
        }
    }
}
