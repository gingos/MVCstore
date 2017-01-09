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

        public StockController()
        {
            proddal = new ProductDal();
            stockDal = new stockRequestDal();
            rvm = new RestockViewModel();
        }

        // GET: Stock
        public ActionResult Index()
        {
            return View();
        }

        //TODO:
        // 
        // showStock with 0 quantity

        //showStock
        public ActionResult showStock()
        {
            return View();
        }
        public ActionResult getProductsByJSON()
        {
            List<Product> prodList = proddal.products.ToList();
            return Json(prodList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getZeroQuantityByJSON()
        {
            List<Product> prodList = proddal.products.Where(p => p.Quantity == 0).ToList();
            return Json(prodList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult restockRequest(int quantityToAdd, string model)
        {
            StockRequest sr = new StockRequest();
            sr.Quantity = quantityToAdd;
            sr.Model = model;
            stockDal.stockRequests.Add(sr);
            stockDal.SaveChanges();

            return new EmptyResult();
        }
        public ActionResult showConfirm()
        {
            //TODO: add "show confirm" view
            // is strongly typed to request VM
            // shows uncofirmed requests, on clock, assigns date and updates product quantity
            rvm.sr = new StockRequest();
            rvm.stockRequestList = stockDal.stockRequests.Where(sr => sr.arrivedDate == null).ToList<StockRequest>();
            return View(rvm);
        }



    }
}