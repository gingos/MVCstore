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
        Customers cust = new Customers();

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

       
        public ActionResult SubmitRegister()
        {
            
           
            if (ModelState.IsValid) {
                custDal = new CustomersDal();
                Customers cust = new Customers();

                cust.FirstName = Request.Form["FirstName"].ToString();
                cust.LastName = Request.Form["LastName"].ToString();
                cust.Email = Request.Form["Email"].ToString();
                cust.PasswordHash = Request.Form["PasswordHash"].ToString();
                cust.PhoneNumber = Request.Form["PhoneNumber"].ToString();

                cust.MD5Hash();

                custDal.customers.Add(cust);
                custDal.SaveChanges();              
                return RedirectToAction("SubmitLogin", cust);
            }
            else
                return View("Register", cust);
        }

        public ActionResult SubmitLogin(Customers reg)
        {
            if (ModelState.IsValid)
            {
                
                
                return View("Index", reg);
            }
            else
                return View("Register", reg);
        }


    }
}