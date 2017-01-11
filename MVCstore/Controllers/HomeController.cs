using MVCstore.DAL;
using MVCstore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVCstore.Controllers
{
    public class HomeController : Controller
    {
        CustomersDal custDal;

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

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            
            return View("Register", new Customers());
        }

       
        public ActionResult SubmitRegister(Customers reg)
        {
            string hash = reg.MD5Hash(reg.PasswordHash);
            reg.PasswordHash = hash;
            custDal = new CustomersDal();

            if (ModelState.IsValid) {
                Customers cust = new Customers
                {
                    FirstName = reg.FirstName,
                    LastName = reg.LastName,
                    Email = reg.Email,
                    PasswordHash = hash,
                    PhoneNumber = reg.PhoneNumber                    
                };

                custDal.customers.Add(cust);
                custDal.SaveChanges();              

                return RedirectToAction("Index", cust);
            }
            else
                return View("Register", reg);
        }

        public ActionResult SubmitLogin(Customers reg)
        {
            if (ModelState.IsValid)
            {
                string hash = reg.MD5Hash(reg.PasswordHash);
                
                return View("Index", reg);
            }
            else
                return View("Register", reg);
        }


    }
}