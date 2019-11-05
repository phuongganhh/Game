using API.Models.Auth;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace API.Controllers
{
    public class AuthController : BaseController
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> SignIn(SignInAction ActionCmd)
        {
            return JsonExpando(await ActionCmd.Execute(CurrentObjectContext));
        }

        
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> SignUp(SignUpAction ActionCmd)
        {
            return JsonExpando(await ActionCmd.Execute(CurrentObjectContext));
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Validate(ValidateAction ActionCmd)
        {
            var result = await ActionCmd.Execute(CurrentObjectContext);
            if(result.code == 200)
            {
                return new ContentResult
                {
                    ContentType = "text/html",
                    ContentEncoding = Encoding.UTF8,
                    Content = $"<script>alert('{result.message}');window.location.href = '{Settings.Instance.FontEnd}';</script>"
                };
            }
            return Redirect(Settings.Instance.FontEnd);
        }


    }
}