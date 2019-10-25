using API.Models;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace API.Controllers
{
    public class UserController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> Get(UserGetAction ActionCmd)
        {
            return JsonExpando(await ActionCmd.Execute(CurrentObjectContext));
        }
    }
}