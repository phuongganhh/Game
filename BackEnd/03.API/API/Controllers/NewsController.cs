using API.Models;
using Common;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace API.Controllers
{
    public class NewsController : BaseController
    {
        [AllowAnonymous]
        public async Task<ActionResult> GetNews(GetNewsAction ActionCmd)
        {
            return JsonExpando(await ActionCmd.Execute(CurrentObjectContext));
        }
        [AllowAnonymous]
        public async Task<ActionResult> GetDetail(GetNewsDetailAction ActionCmd)
        {
            return JsonExpando(await ActionCmd.Execute(CurrentObjectContext));
        }


    }
}