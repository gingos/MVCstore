using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCstore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Some wise chinese proverb about life.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "How to contact us, in case Google comes knocking.";

            return View();
        }
    }
}