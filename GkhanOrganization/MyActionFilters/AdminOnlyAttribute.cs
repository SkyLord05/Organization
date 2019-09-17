using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GkhanOrganization.MyActionFilters
{
    public class AdminOnlyAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Session["UserLogin"]!=null)
            {
                if (((DataAccessLayer.Entities.User)filterContext.RequestContext.HttpContext.Session["UserLogin"]).UserType == 1)
                {
                    filterContext.RequestContext.HttpContext.Response.Redirect("/Home/Index", true);
                }
            }
            else
            {
                filterContext.RequestContext.HttpContext.Response.Redirect("/Home/Login", true);
            }
        }

    }
}