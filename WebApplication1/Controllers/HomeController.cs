using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test(string id)
        {
            return View((object) id);
        }

        public ActionResult Test2(string id)
        {
            return PartialView("Test",(object) id);
        }

        public ActionResult ContentDemo()
        {
            // SaveChange();

            return Content("<script>alert('新增成功');location.href='/';</script>");
        }

        public ActionResult Succeed()
        {
            return View("JSAlertRedirect", (object)"新增成功");
        }

        public FileResult File1()
        {
            return File(Server.MapPath("/Content/BrotherGoOn"), "image/jpeg", "兄弟加油");
        }

        public ActionResult File2(int dl = 0)
        {
            if (dl == 1)
            {
                return File(Server.MapPath("~/Content/BrotherGoOn.jpg"), "image/jpeg", "兄弟加油.jpg");
            }
            else
            {
                return File(Server.MapPath("~/Content/BrotherGoOn.jpg"), "image/jpeg");
            }
        }
    }
}