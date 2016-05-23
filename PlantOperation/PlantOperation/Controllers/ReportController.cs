using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlantOperation.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "User");
            }
            return View();
        }
        // view report
        public ActionResult ViewReport()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "User");
            }
            ViewBag.StrDate = Request.Form["rdate"].ToString();
            return View("Report");
        }
    }
}