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

        public ActionResult Enter()
        {
            return View("Enter");
        }

        public ActionResult Login()
        {

            return View();
        }

        public ActionResult my404()
        {
            return View();
        }
       
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

        public ActionResult GuestAbout()
        {
            return View();
        }

        public ActionResult GuestContact()
        {
            return View();
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

                return View("Login");
            }
            else
                return View("Register", new Customers());
        }

        public ActionResult Login()
        {

            return View();
        }
        /*
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
                    string emp_logEmail = Request.Form["EmployeeEmail"];
                    string emp_logPassword = Request.Form["EmployeePassword"];
                    if (emp_logEmail.Equals(emp.EmployeeEmail))
                    {
                        if (emp_logPassword.Equals(emp.EmployeePassword))
                        {
                            Session["UserID"] = emp.EmployeeNumber;
                            Session["type"] = type;
                            return View("IndexEmployees");
                        }
                        TempData["Fail"] = "Incorrect Detail InputI";
                        return RedirectToAction("Login", "Home");
                    } 
                }
            }
            Customers temp = new Customers(); 
            string logEmail = Request.Form["EmployeeEmail"];
            string logPassword = Request.Form["EmployeePassword"];
            temp.PasswordHash = logPassword;
            temp.MD5Hash();
            custDal = new CustomersDal();
            List<Customers> custList = custDal.Customers.ToList();
            foreach (Customers cust in custList)
            {
                if (cust.Email == logEmail)
                {
                    cust.MD5Hash();
                    if (cust.PasswordHash.Equals(temp.PasswordHash))
                    {
                       
                        temp = cust;
                        Session["UserID"] = temp.CustomerNumber;
                        Session["type"] = type;
                        return View("IndexCustomers");
                    }
                    TempData["Fail"] = "Incorrect Detail Input";
                    return RedirectToAction("Login", "Home");
                }
            }
            TempData["Fail"] = "Incorrect Detail Input";
            return RedirectToAction("Login", "Home");
        }
        */

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