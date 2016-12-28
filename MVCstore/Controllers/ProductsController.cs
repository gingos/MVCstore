using MVCstore.DAL;
using MVCstore.Models;
using MVCstore.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCstore.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult showProducts()
        {
            ProductDal dal = new ProductDal();
            List<Product> productList = dal.products.ToList();
            
            ProductViewModel pvm = new ProductViewModel();
            ProductViewModel pvm2 = new ProductViewModel();

            pvm.product = new Product();
            pvm2.product = new Product();

            pvm.productList = productList;
            pvm2.productList = productList;

            return View(pvm);
        }
    }
}