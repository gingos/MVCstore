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
using System.Web.Security;

namespace MVCstore.Controllers
{
    public class HomeController : Controller
    {
        CustomersDal custDal = null;
        
        public ActionResult my404()
        {
            return View();
        }
        /*
        public ActionResult Index()
        {
            int userID;
            if (Session != null && Session["type"] != null)
            {
                if (Session["userID"] != null)
                    userID = (int)Session["userID"];
            }
            else
            {
                return RedirectToAction("my404", "Home");
            }
            return View();
        }
        */
        public ActionResult IndexEmployees()
        {
            int userID;
            if (Session != null && Session["type"] != null || !((string)Session["type"]).Equals("Employee"))
            {
                if (Session["userID"] != null)
                    userID = (int)Session["userID"];
            }
            else
            {
                return RedirectToAction("my404", "Home");
            }
            return View("IndexEmployees");
        }
        
        public ActionResult IndexCustomers()
        {
            int userID;
            if (Session != null && Session["type"] != null || !((string)Session["type"]).Equals("Customer"))
            {
                if (Session["userID"] != null)
                    userID = (int)Session["userID"];
            }
            else
            {
                return RedirectToAction("my404", "Home");
            }
            return View("IndexCustomers");
        }


        public ActionResult About()
        {
            int userID;
            if (Session != null && Session["type"] != null)
            {
                if (Session["userID"] != null)
                    userID = (int)Session["userID"];
            }
            else
            {
                RedirectToAction("my404", "Home");
            }
            return View();
        }

        public ActionResult Contact()
        {
            

            int userID;
            if (Session != null && Session["type"] != null)
            {
                if (Session["userID"] != null)
                    userID = (int)Session["userID"];
            }
            else
            {
                return RedirectToAction("my404", "Home");
            }

            ViewBag.Message = "How to contact us, in case Google comes knocking.";
            return View();
        }

        public ActionResult Login()
        {
            int userID;
            if (Session != null)
            {
                if (Session["userID"] != null)
                    userID = (int)Session["userID"];
            }
            else
            {
                RedirectToAction("my404", "Home");
            }
            return View();
        }

        public ActionResult Register()
        {

          return View(new Customers());
        }


        public ActionResult SubmitRegister()
        {

          
            if (ModelState.IsValid) {
                Customers cust = new Customers();

                cust.FirstName = Request.Form["FirstName"];
                cust.LastName = Request.Form["LastName"];
                cust.Email = Request.Form["Email"];
                cust.PasswordHash = Request.Form["PasswordHash"];
                cust.PhoneNumber = Request.Form["PhoneNumber"];
                cust.MD5Hash();
                  custDal = new CustomersDal();
                   custDal.customers.Add(cust);
                   custDal.SaveChanges();  

                return RedirectToAction("Register", cust);
            }
            else
                return View("Register", new Customers());
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

        public ActionResult MockLogin()
        {
            Session["userID"] = 2;
            Session["Type"] = "Customer";


            
            //return RedirectToAction("Index");

            int user = (int)Session["userID"];
            string type = (string)Session["Type"];

            if (type.Equals("Employee"))
                return RedirectToAction("IndexEmployees", "Home");
            else if (type.Equals("Customer"))
                return RedirectToAction("IndexCustomers", "Home");
            else
                return RedirectToAction("my404", "Home");
        }


    }
}