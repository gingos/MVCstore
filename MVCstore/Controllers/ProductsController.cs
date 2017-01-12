﻿using MVCstore.DAL;
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
        OrderDal orddal;
        ProductDal proddal;
        OrderViewModel ovm;
        int custID;
        
        public ProductsController()
        {
            orddal = new OrderDal();
            ovm = new OrderViewModel();
            proddal = new ProductDal();
            
            
        }

        // GET: Products
        public ActionResult Index()
        {
            return View();
        }

        // Products View
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
            #region wrap all products in single model view (deprecated)
            /*
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
            */
            #endregion
            return View();
        }
        /*
        public EmptyResult addToCart(int quantity, string model, int price)
        {
            Order order = new Order();

            order.CustID = "1";
            order.Model = model;
            order.Price = price;
            order.Quantity = quantity;

            orddal.orders.Add(order);
            orddal.SaveChanges();

            return new EmptyResult();
        }
        */
        
        /// <summary>
        /// checkStock: function returns product quantity
        /// </summary>
        /// function checks if products is in stock, subtracts
        ///     needed quantity from stock and updates orders table
        /// <param name="quantityToBuy"></param>
        /// <param name="model"></param>
        /// <param name="price"></param>
        /// <returns>order object: model, price, purchased quantity</returns>
        public ActionResult checkStock(int quantityToBuy, string model, int price)
        {
            Product prod = proddal.products.First(p => p.Model.Equals (model));

            Order order = new Order();
            order.CustID = (int)Session["userID"];
            order.Model = model;
            order.Price = price;

            if (prod.Quantity - quantityToBuy >= 0)
            {
                prod.Quantity -= quantityToBuy;
                order.Quantity = quantityToBuy;
            }
            else
            {
                order.Quantity = prod.Quantity;
                prod.Quantity = 0;
            }

            proddal.SaveChanges();
            if (order.Quantity > 0)
            {
                orddal.orders.Add(order);
                orddal.SaveChanges();
            }
            
            return Json(order, JsonRequestBehavior.AllowGet);
        }
        
        // Cart View
        /// <summary>
        /// show cart, which is all orders that were not yet shipped - clicked "cash out"
        /// </summary>
        /// <returns> order view model, list</returns>
        public ActionResult showCart()
        {
            custID = (int)Session["userID"];
            ovm.order = new Order(); //add o. user = session user
            ovm.orderList = orddal.orders.Where(o=> o.CustID == custID && o.shippedDate == null).ToList<Order>();
            return View(ovm);
        }

        /// <summary>
        /// add today's date to "shipped" culumn
        /// triggered by "cash out" button
        /// </summary>
        /// <param name="custID"> customer the order belongs to</param>
        public EmptyResult updateShipment(int custID)
        {
            //custID = (int)Session["userID"];
            List<Order> olist = orddal.orders.Where(o => o.CustID == (custID)).ToList();
            olist.ForEach(o => o.shippedDate = DateTime.Now);
            orddal.SaveChanges();
            return new EmptyResult();
        }

        // Order History View
        /// <summary>
        /// Show All Orders User has made
        /// </summary>
        /// <returns></returns>
        public ActionResult viewHistory()
        {
            custID = (int)Session["userID"];
            ovm.order = new Order();
            ovm.orderList = orddal.orders.Where(o => o.CustID== custID).ToList<Order>();
            return View(ovm);
        }

        public ActionResult searchOrder()
        {
            string searchValue = Request.Form["search_txt"].ToString();
            List<Order> olist = orddal.orders.Where(o => o.Model.Equals(searchValue)).ToList();
            ovm.orderList = olist;

            return View(ovm);
        }
    }
}