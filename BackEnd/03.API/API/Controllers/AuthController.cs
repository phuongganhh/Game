using API.Models.Auth;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
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
            await ActionCmd.Execute(CurrentObjectContext);
            return Redirect(Settings.Instance.FontEnd);
        }


    }
}