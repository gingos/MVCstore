﻿using MVCstore.DAL;
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

        public ActionResult Enter()
        {
            return View("Enter");
        }

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

      /*  public ActionResult Login()
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
        } */

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
                    custDal.Customers.Add(cust);
                   custDal.SaveChanges();  

                return RedirectToAction("Register", cust);
            }
            else
                return View("Register", new Customers());
        }

        public ActionResult Login()
        {

            return View();
        }

        public ActionResult SubmitLogin()
        {
            string type = "Customer";
            if (Request.Form["Employee"] != null)
            {
                type = "Employee";

                EmployeeDAL empDal = new EmployeeDAL();
                List<Employee> empList = empDal.employees.ToList();
                foreach (Employee emp in empList)
                {
                    string emp_logEmail = Request.Form["logEmail"];
                    string emp_logPassword = Request.Form["logPassword"];
                    if (emp_logEmail.Equals(emp.EmployeeEmail))
                    {
                        if (emp_logPassword.Equals(emp.EmployeePassword))
                        {
                            Session["UserID"] = emp.EmployeeNumber;
                            Session["type"] = type;
                            return View("IndexEmployees");
                        }
                        return View("Login");
                    }
                    return View("Login");
                }
            }
            Customers temp = new Customers(); 
            string logEmail = Request.Form["logEmail"];
            string logPassword = Request.Form["logPassword"];
            temp.PasswordHash = logPassword;
            temp.MD5Hash();
            custDal = new CustomersDal();
            List<Customers> custList = custDal.Customers.ToList();
            foreach (Customers cust in custList)
            {
                if (cust.Email == logEmail)
                {
                    cust.MD5Hash();
                    if (cust.PasswordHash == temp.PasswordHash)
                    {
                       
                        temp = cust;
                        Session["UserID"] = temp.CustomerNumber;
                        Session["type"] = type;
                        return View("IndexCustomers");
                    }
                    return View("Login");
                }
                return View("Login");
            }
            return View("Login");
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