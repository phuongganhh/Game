using Common;
using Microsoft.Owin;
using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

public class Cor : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
        var request = filterContext.HttpContext.Request.Url;
        LoggerManager.Instance.Trace($"{request.Scheme}://{request.Host}{request.AbsolutePath}", request.Query);
        base.OnActionExecuting(filterContext);
    }
}