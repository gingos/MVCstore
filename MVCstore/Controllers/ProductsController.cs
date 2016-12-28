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

        public ActionResult getPCbyJSON()
        {
            PCDal pcdal = new PCDal();
            List<PC> pcList = pcdal.PCs.ToList();   
            return Json(pcList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getPrinterByJSON()
        {
            PrinterDal prdal = new PrinterDal();
            List<Printer> printerList = prdal.printers.ToList();
            return Json(printerList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getLaptopByJSON()
        {
            LaptopDal lapdal = new LaptopDal();
            List<Laptop> lapList = lapdal.laptops.ToList();
            return Json(lapList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult showProducts()
        {
            List<Product> productList = new ProductDal().products.ToList();
            List<PC> pcList = new PCDal().PCs.ToList();
            List<Printer> printerList = new PrinterDal().printers.ToList();
            List<Laptop> laptopList = new LaptopDal().laptops.ToList();
            

            ProductViewModel pvm = new ProductViewModel();

            pvm.product = new Product();
            pvm.pc = new PC();
            pvm.printer = new Printer();
            pvm.laptop = new Laptop();
            

            pvm.productList = productList;
            pvm.pcList = pcList;
            pvm.printerList = printerList;
            pvm.laptopList = laptopList;

            return View(pvm);
        }
    }
}