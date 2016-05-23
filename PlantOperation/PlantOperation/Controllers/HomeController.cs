using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlantOperation.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
    }
}