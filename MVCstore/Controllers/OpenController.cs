using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCstore.Controllers
{
    public class OpenController : Controller
    {
        // GET: Open
        public ActionResult Enter()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View("Home/Register");
        }

        public ActionResult Login()
        {
            return View("Login", "Home");
        }
    }
}