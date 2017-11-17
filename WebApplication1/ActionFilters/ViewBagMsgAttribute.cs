using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.ActionFilters
{
    public class ViewBagMsgAttribute : ActionFilterAttribute 
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.Message = "Your application desciption page(attribute)";
            base.OnActionExecuting(filterContext);
        }
    }
}