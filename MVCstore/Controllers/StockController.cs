using MVCstore.DAL;
using MVCstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCstore.ModelView;

namespace MVCstore.Controllers
{
    public class StockController : Controller
    {
        ProductDal proddal;
        stockRequestDal stockDal;
        RestockViewModel rvm;
        int userID;


        // GET: Stock
        public ActionResult Index()
        {
            if (Session == null)
            {
                RedirectToAction("my404", "Home");
            }
            return View();
        }

        //showStock view

        /// <summary>
        /// show stock view, shows available products and link to request confirmation
        /// </summary>
        /// <returns></returns>
        public ActionResult showStock()
        {
            if (Session == null || Session["type"] == null || !((string)Session["type"]).Equals("Employee"))
            {
                return RedirectToAction("my404", "Home");
            }
            else
            {
                userID = (int)Session["userID"];
            }
            return View();
        }
        
        /// <summary>
        /// function uses products DAL to get all products from server
        /// async request
        /// </summary>
        /// <returns>products table in JSON format</returns>
        public ActionResult getProductsByJSON()
        {
            if (Session == null || Session["type"] == null || !((string)Session["type"]).Equals("Employee"))
            {
                return RedirectToAction("my404", "Home");
            }
            else
            {
                userID = (int)Session["userID"];
            }
            proddal = new ProductDal();
            List<Product> prodList = proddal.products.ToList();
            return Json(prodList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// function uses products DAL to get all products that ran out from server
        /// async request
        /// </summary>
        /// <returns>products with 0 quantity table in JSON format</returns>
        public ActionResult getZeroQuantityByJSON()
        {
            if (Session == null || Session["type"] == null || !((string)Session["type"]).Equals("Employee"))
            {
                return RedirectToAction("my404", "Home");
            }
            else
            {
                userID = (int)Session["userID"];
            }
            proddal = new ProductDal();
            List<Product> prodList = proddal.products.Where(p => p.Quantity == 0).ToList();
            return Json(prodList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// add new request to restock store products
        /// will add quantity to existing requests or add new if product not found
        /// </summary>
        /// <param name="quantityToAdd"> quantity of product to be requested</param>
        /// <param name="model"> model of product to update</param>
        /// <returns></returns>
        public ActionResult restockRequest(int quantityToAdd, string model)
        {
            if (Session == null || Session["type"] == null || !((string)Session["type"]).Equals("Employee"))
            {
                return RedirectToAction("my404", "Home");
            }
            else
            {
                int empID = (int)Session["userID"];
            }
            stockDal = new stockRequestDal();
            StockRequest sr = null;
            
            //add: stock requests by empID
            List<StockRequest> srlist = stockDal.stockRequests.Where(s => s.arrivedDate == null).ToList();
            sr = srlist.FirstOrDefault(s => s.Model == model);
            if (sr != null)
            {
                // request exists, update quantity
                sr.Quantity += quantityToAdd;
            }
            else
            {
                sr = new StockRequest();
                // request does not exist, update quantity
                sr.Quantity = quantityToAdd;
                sr.Model = model;
                stockDal.stockRequests.Add(sr);
            }
            
            
            stockDal.SaveChanges();
            //return JSON with updates list
            return new EmptyResult();
        }


        //showRestockRequests view

        /// <summary>
        /// show all restock requests submitted by active employee
        /// </summary>
        /// <returns></returns>
        public ActionResult showRestockRequests()
        {
            if (Session == null || Session["type"] == null || !((string)Session["type"]).Equals("Employee"))
            {
                return RedirectToAction("my404", "Home");
            }else
            {
                int empID = (int)Session["userID"];
            }

            rvm = new RestockViewModel();
            stockDal = new stockRequestDal();
            rvm.sr = new StockRequest();
            
            //add: stock requests by empID
            rvm.stockRequestList = stockDal.stockRequests.Where(sr => sr.arrivedDate == null).ToList<StockRequest>();
            return View(rvm);
        }

        /// <summary>
        /// update products quantity with active employee's request(s)
        /// </summary>
        /// <returns></returns>
        public ActionResult restockProducts()
        {
            if (Session == null || Session["type"] == null || !((string)Session["type"]).Equals("Employee"))
            {
                return RedirectToAction("my404", "Home");
            }
            else
            {
                int empID = (int)Session["userID"];
            }

            stockDal = new stockRequestDal();
            
            //add: stock requests by empID
            List<StockRequest> srlist = stockDal.stockRequests.Where(sr => sr.arrivedDate == null).ToList();
            
            string query = "UPDATE Products " +
                            "SET " +
                            "Products.Quantity = Products.Quantity + s.Quantity " + 
                            "FROM " +
                            "Products " +
                            "INNER JOIN " +
                            "(SELECT * FROM stockOrders WHERE arrivedDate IS NULL) s " +
                            "ON " +
                            "Products.Model = s.Model; ";
            proddal = new ProductDal();
            proddal.Database.ExecuteSqlCommand(query);

            srlist.ForEach(sr => sr.arrivedDate = DateTime.Now);
            stockDal.SaveChanges();
            return new EmptyResult();
        }

        // show Requests History view

        /// <summary>
        /// show all requests, both pending and approved
        /// </summary>
        /// <returns></returns>
        public ActionResult viewRequestsHistory()
        {
            if (Session == null || Session["type"] == null || !((string)Session["type"]).Equals("Employee"))
            {
                return RedirectToAction("my404", "Home");
            }
            else
            {
                int empID = (int)Session["userID"];
            }

            stockDal = new stockRequestDal();
            rvm = new RestockViewModel();
            rvm.sr = new StockRequest();
            
            //add: stock requests by empID
            rvm.stockRequestList = stockDal.stockRequests.ToList<StockRequest>();
            return View(rvm);
        }
    }
}