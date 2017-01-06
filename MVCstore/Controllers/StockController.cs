using MVCstore.DAL;
using MVCstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCstore.Controllers
{
    public class StockController : Controller
    {
        ProductDal proddal;
        stockOrderDal stockDal;

        public StockController()
        {
            proddal = new ProductDal();
            stockDal = new stockOrderDal();
        }

        // GET: Stock
        public ActionResult Index()
        {
            return View();
        }

        //TODO:
        // 3. stock tables similar to show products
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


    }
}