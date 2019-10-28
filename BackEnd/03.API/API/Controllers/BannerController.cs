using API.Models.Banner;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace API.Controllers
{
    public class BannerController : BaseController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Get(GetBannerAction ActionCmd)
        {
            return JsonExpando(await ActionCmd.Execute(CurrentObjectContext));
        }
    }
}