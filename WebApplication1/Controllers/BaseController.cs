using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public abstract class BaseController : Controller
    {
        protected ContentResult ErrorBack(string errorMessage)
        {
            return Content(string.Format("<script language='javascript' type='text/javascript'>alert('{0}');window.history.go(-1);</script>", errorMessage));
            //return Content(string.Format("alert('{0}');window.history.back();", errorMessage));
            //return JavaScript(string.Format("alert('{0}')", errorMessage));
        }

        //protected override void HandleUnknownAction(string actionName)
        //{
        //    this.Redirect("/").ExecuteResult(this.ControllerContext);
        //}

        /*
        protected ActionResult ExportExcel(XLWorkbook book)
        {
                return book.Deliver("generatedFile.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }*/
    }
}