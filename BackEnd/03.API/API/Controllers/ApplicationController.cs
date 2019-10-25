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
    public class ApplicationController : BaseController
    {
        [AllowAnonymous]
        public async Task<ActionResult> GetMenu(GetMenuAction ActionCmd)
        {
            return JsonExpando(await ActionCmd.Execute(CurrentObjectContext));
        }
    }
}