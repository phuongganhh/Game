using Newtonsoft.Json;
using System.Web.Mvc;

namespace Common
{
    public class BaseController : Controller
    {
        public ObjectContext CurrentObjectContext { get; internal set; }
        protected override void OnActionExecuting(ActionExecutingContext ctx)
        {
            this.CreateObjectContext();
            base.OnActionExecuting(ctx);
        }
        protected virtual void CreateObjectContext()
        {
            CurrentObjectContext = ObjectContext.CreateContext(this);
        }

        protected ActionResult JsonExpando(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return Content(json, "application/json");
        }
    }
}
