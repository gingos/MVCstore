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

        /// <summary>
        /// Site's Main Page, Allows Login or Register
        /// </summary>
        /// <returns></returns>
        public ActionResult Enter()
        {
            return View("Enter");
        }

        /// <summary>
        /// Directs To Login View
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {

            return View();
        }

        /// <summary>
        /// if user customer crosses to employee or vice versa, he will get 404
        /// </summary>
        /// <returns></returns>
        public ActionResult my404()
        {
            return View();
        }
       
        /// <summary>
        /// Main page for Employees
        /// </summary>
        /// <returns></returns>
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
        
        /// <summary>
        /// Main page for Customers
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// About page for Guests
        /// </summary>
        /// <returns></returns>
        public ActionResult GuestAbout()
        {
            return View();
        }

        /// <summary>
        /// Contact Us page for guests
        /// </summary>
        /// <returns></returns>
        public ActionResult GuestContact()
        {
            return View();
        }

        
        /// <summary>
        /// Register new Customers
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {

          return View(new Customers());
        }

        /// <summary>
        /// Action Handles new customer Registration
        /// </summary>
        /// <returns></returns>
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
                    custDal.Customers.Add(cust);
                   custDal.SaveChanges();  

                return View("Login");
            }
            else
                return View("Register", new Customers());
        }

        /// <summary>
        /// Action handles login: accesses DB and verifies credentials
        /// </summary>
        /// <returns></returns>
        public ActionResult SubmitLogin()
        {

            string logEmail = Request.Form["logEmail"];
            string logPassword = Request.Form["logPassword"];
            if (Request.Form["typeSwitch"] != null){
                //search for match in Employees DAL

                EmployeeDAL empDal = new EmployeeDAL();
                List<Employee> empList = empDal.employees.Where(e => e.EmployeeEmail.Equals(logEmail)).ToList();
                if (empList.Count == 0)
                {
                    // no such email in database
                    TempData["fail"] = "Incorrect Detail Input";
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    //found employee email match, try password match
                    Employee e = empList[0];
                    if (logPassword.Equals(e.EmployeePassword))
                    {
                        Session["UserID"] = empList[0].EmployeeNumber;
                        Session["type"] = "Employee";
                        return View("IndexEmployees");
                    }
                    else
                    {
                        //password doesn't match email
                        TempData["fail"] = "Incorrect Detail Input";
                        return RedirectToAction("Login", "Home");
                    }
                }
            }
            else
            {
                ////search for match in Customers DAL
                Customers temp = new Customers();
                temp.Email = logEmail;
                temp.PasswordHash = logPassword;
                temp.MD5Hash();
                custDal = new CustomersDal();
                List<Customers> custList = custDal.Customers.Where(c => c.Email.Equals(temp.Email)).ToList();
                if (custList.Count == 0)
                {
                    TempData["fail"] = "Incorrect Detail Input";
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    //found customer email match, try password match
                    Customers c = custList[0];
                    if (c.PasswordHash.Equals(temp.PasswordHash))
                    {
                        Session["UserID"] = c.CustomerNumber;
                        Session["type"] = "Customer";
                        return View("IndexCustomers");
                    }
                    else
                    {
                        // password doesn't match email
                        TempData["fail"] = "Incorrect Detail Input";
                        return RedirectToAction("Login", "Home");
                    }
                }
            }
        }

    }
}