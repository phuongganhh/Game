using API.Models;
using Common;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace API.Controllers
{
    public class NewsGroupController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> Get(GetNewsGroupAction ActionCmd)
        {
            return JsonExpando(await ActionCmd.Execute(CurrentObjectContext));
        }
    }
}