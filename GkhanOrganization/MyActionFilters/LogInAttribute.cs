using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GkhanOrganization.MyActionFilters
{
    public class LogInAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Session["UserLogin"]==null)
            {
                filterContext.RequestContext.HttpContext.Response.Redirect("/Home/Login", true);
            }

        }
    }
}