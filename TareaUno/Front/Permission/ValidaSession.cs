using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Front.Permission
{
    public class ValidaSession : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["UserSession"] == null)
            {

                filterContext.Result = new RedirectResult("~/Home/Login");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}