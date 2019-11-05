using Common.Database;
using Dapper;
using Dapper.FastCrud;
using Entity;
using Models;
using SqlKata.Execution;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Common.Attribute
{
    public class CustomAuthorizeAttribute : ActionFilterAttribute
    {
        private static readonly Type allowAnonymousAttrType = typeof(AllowAnonymousAttribute);

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (filterContext.ActionDescriptor.IsDefined(allowAnonymousAttrType, true) ||
                filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(allowAnonymousAttrType, true))
                {
                    return;
                }
                var token = filterContext.RequestContext.HttpContext.Request.Params["token"];
                if (token == null)
                {
                    filterContext.Result = new JsonResult()
                    {
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                        Data = new Result
                        {
                            message = "Vui lòng đăng nhập!",
                            code = (int)HttpStatusCode.Unauthorized
                        }
                    };
                    return;
                }
                var objectContext = new ObjectContext();
                var u = objectContext.Connection
                    .Find<User>(s => s.Where($"Token = @token").WithParameters(new { token }));
                if (!u.Any() || u.Count() > 1)
                {
                    filterContext.Result = new JsonResult()
                    {
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                        Data = new Result
                        {
                            message = "Vui lòng đăng nhập!",
                            code = (int)HttpStatusCode.Unauthorized
                        }
                    };
                    return;
                }
                else
                {
                    var controller = filterContext.Controller as BaseController;
                    controller.CurrentObjectContext.CurrentUser = u.FirstOrDefault();
                    base.OnActionExecuting(filterContext);
                }
            }
            catch (Exception ex)
            {
                filterContext.Result = new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new Result
                    {
                        message = ex.Message,
                        code = (int)HttpStatusCode.InternalServerError
                    }
                };
                return;
            }
        }
    }
}
