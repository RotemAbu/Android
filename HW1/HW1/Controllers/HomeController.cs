using HW1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HW1.Dal;
using HW1.ViewModel;

//submit by : Rotem Abohtzira
//Id: 203129598
namespace HW1.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        public ActionResult Index()
        {
            return RedirectToAction("HomePage");
        }

        public ActionResult HomePage()
        {
            DataLayer dal = new DataLayer();
            List<Product> objProducts = dal.Products.ToList<Product>();
            List<Order> objOrders = dal.Orders.ToList<Order>();
            ProductViewModel pvm = new ProductViewModel();
            pvm.product = new Product();
            pvm.products = objProducts;
            return View(pvm);
        }

        public ActionResult Terms()
        {
            return View("Terms");
        }

        public ActionResult ShowSearchByName()
        {
            ProductViewModel pvm = new ProductViewModel();
            pvm.product = new Product();
            pvm.products = new List<Product>();
            return View("SearchProductByName", pvm);
        }

        public ActionResult SearchProductByName()
        {
            DataLayer dal = new DataLayer();
            ProductViewModel pvm = new ProductViewModel();

            string searchVal = Request.Form["txtProductName"].ToString(); //get value for search
            List<Product> objProducts = (from x in dal.Products
                                         where x.ProName.Contains(searchVal)
                                         select x).ToList<Product>(); //query that returns the objects that contains the search val

            Product objProd = new Product(); //create new product object
            objProd.ProName = searchVal; // get the correct object we want
            pvm.product = objProd;
            pvm.products = objProducts; //list of the correct objects we searched

            return View(pvm);

        }

        public ActionResult ShowSearchByPrice()
        {
            ProductViewModel pvm = new ProductViewModel();
            pvm.product = new Product();
            pvm.products = new List<Product>();
            return View("SearchProductByPrice", pvm);
        }

        public ActionResult SearchProductByPrice()
        {
            DataLayer dal = new DataLayer();
            ProductViewModel pvm = new ProductViewModel();

            int searchVal1 = int.Parse(Request.Form["txtFromPrice"]); //get values for search
            int searchVal2 = int.Parse(Request.Form["txtToPrice"]);

            var objprod = (from x in dal.Products
                           where x.Price >= searchVal1 && x.Price <= searchVal2 //query that returns the product object
                           select x);

            List<Product> objProducts = (from x in dal.Products
                                         where x.Price >= searchVal1 && x.Price <= searchVal2
                                         select x).ToList<Product>();//query that returns the competable objects

            Product objProd = new Product(); //create new product object
            pvm.product = objProd; 
            pvm.products = objProducts; //list of the correct objects we searched

            return View(pvm);

        }

        [HttpPost]
        public ActionResult BuyProduct()  
        {
            Order obj = new Order();
            DataLayer dal = new DataLayer();

            var num = (from x in dal.Orders
                       select x.OrderNum).Count(); //get the number of orders that i already have in data base

            obj.OrderNum = (num+1).ToString(); //set new order number (the last order +1)
            obj.ProductId = Request.Form["product.ProductId"].ToString(); //set product id from the user choose by text box
            obj.ProName = Request.Form["product.ProName"].ToString();
            obj.BuyerName = Request.Form["order.BuyerName"].ToString();
            obj.Quantity = Request.Form["order.Quantity"].ToString();
            obj.PurchaseDate = DateTime.Now; //set current date of the new order

            try
            {
                if (ModelState.IsValid)
                {
                    List<Product> productToUpdate = (from x in dal.Products
                                                     where x.ProductId == obj.ProductId
                                                     select x).ToList<Product>(); //get the product that the user want to buy

                    List<Order> orderToadd = (from x in dal.Orders
                                              where x.ProductId == obj.ProductId
                                              select x).ToList<Order>();

                    if (productToUpdate.Count != 0 && int.Parse(productToUpdate[0].ProAmount) >= int.Parse(obj.Quantity))
                    {//check if we can provide the order (if we have in stock)

                        dal.Orders.Add(obj); //enter the new order to data base
                        productToUpdate[0].ProAmount = (int.Parse(productToUpdate[0].ProAmount) - int.Parse(obj.Quantity)).ToString();
                        //update the amount of the product in stock table
                        dal.SaveChanges(); //save changes in db
                        TempData["notice"] = "Thank you for your purchase, your order has been succesfully saved";
                    }   
                }
            }
            catch
            {
                return View("");
            }
            return View("");
        }   
	}
}