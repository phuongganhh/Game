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
        LoggerManager.Logger.Trace($"{filterContext.HttpContext.Request.Url.OriginalString} {filterContext.HttpContext.Request.UserHostAddress}");
        base.OnActionExecuting(filterContext);
    }
}